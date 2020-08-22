using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
public class RayMarchingMaster : MonoBehaviour
{
    [SerializeField] private ComputeShader shader = null;
    [SerializeField] private Light mainLight = null;
    private List<ComputeBuffer> buffersToDispose = null;
    private RenderTexture rt = null;
    private Camera cam = null;
    private int kernel = 0;

    public static List<Shape> allShapes = new List<Shape>();

    private void Awake()
    {
        // Get a reference to the camera
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        kernel = shader.FindKernel("CSMain");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        buffersToDispose = new List<ComputeBuffer>();
        SetShaderParams(source);
        Render(destination);
    }

    private void InitRenderTexture()
    {
        if (rt == null || rt.width != cam.pixelWidth || rt.height != cam.pixelHeight)
        {
            // Release render texture if we already have one
            if (rt != null)
                rt.Release();

            // Get a render target for Ray Tracing
            rt = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            rt.enableRandomWrite = true;
            rt.Create();
        }
    }

    private void SetShaderParams(RenderTexture source)
    {
        shader.SetMatrix("_CameraToWorld", cam.cameraToWorldMatrix);
        shader.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
        shader.SetVector("_WorldSpaceCameraPos", cam.transform.position);
        shader.SetTexture(kernel, "Source", source);
        SetLightData();
        GetSceneData();
    }

    private void SetLightData()
    {
        LightData[] lightData = new LightData[1];
        lightData[0].position = mainLight.transform.position;
        lightData[0].direction = mainLight.transform.forward;
        lightData[0].color = mainLight.color;
        lightData[0].intensity = mainLight.intensity;
        lightData[0].type = (int)mainLight.type;
        lightData[0].spotAngle = mainLight.spotAngle;

        ComputeBuffer mainLightBuffer = new ComputeBuffer(lightData.Length, LightData.GetSize());
        mainLightBuffer.SetData(lightData);
        shader.SetBuffer(kernel, "lightsBuffer", mainLightBuffer);
        buffersToDispose.Add(mainLightBuffer);
    }

    private void GetSceneData()
    {
        // Sort all the shapes in the order of their operations
        allShapes.Sort((a, b) => a.operation.CompareTo(b.operation));

        // Create a new list that will store the sorted shapes
        List<Shape> orderedShapes = new List<Shape>();

        // Iterate through all the shapes and add them in order
        for (int i = 0; i < allShapes.Count; i++)
        {
            // If this is a child shape skip over it
            if (allShapes[i].transform.parent != null)
                continue;

            // Get the this parent shape's transform and add it to the ordered list
            Transform parentShape = allShapes[i].transform;
            orderedShapes.Add(allShapes[i]);

            // Get the number of children that the parent object has
            allShapes[i].numChildren = parentShape.childCount;
            
            // Add the children with shape components to the ordered list
            for (int j = 0; j < parentShape.childCount; j++)
            {
                // First check if the child object has a shape component, if not, continue
                Shape childShape = parentShape.GetChild(j).GetComponent<Shape>();
                if (childShape == null)
                    continue;

                // Add this child shape to the list
                orderedShapes.Add(childShape);

                // No support for nested children yet, so set the number of children for this child to 0
                orderedShapes[orderedShapes.Count - 1].numChildren = 0;
            }

        }

        // Set up an array of shape data to be passed into the compute shader
        ShapeData[] shapeData = new ShapeData[orderedShapes.Count];
        for (int i = 0; i < orderedShapes.Count; i++)
        {
            // Get the shape object
            Shape s = orderedShapes[i];

            // Fill in the data for this shape
            shapeData[i] = new ShapeData()
            {
                position = s.Position,
                scale = s.Scale,
                colour = new Vector3(s.colour.r, s.colour.g, s.colour.b),
                shapeType = (int)s.shapeType,
                operation = (int)s.operation,
                blendStrength = s.blendStrength * 3,
                numChildren = s.numChildren
            };
        }

        // Set up the buffer for passing in all the shape data to the shader
        ComputeBuffer shapeBuffer = new ComputeBuffer(shapeData.Length, ShapeData.GetSize());
        shapeBuffer.SetData(shapeData);
        shader.SetBuffer(kernel, "shapesBuffer", shapeBuffer);

        // Also pass the number of shapes being passed in
        shader.SetInt("numShapes", shapeData.Length);

        // Add the buffer to the list of buffers to be disposed when done with
        buffersToDispose.Add(shapeBuffer);
    }

    private void Render(RenderTexture destination)
    {
        // Make sure we have a current render target
        InitRenderTexture();

        // Set the target and dispatch the compute shader
        shader.SetTexture(kernel, "Result", rt);
        int threadGroupsX = Mathf.CeilToInt(cam.pixelWidth / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(cam.pixelHeight / 8.0f);
        shader.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);

        // Blit the result texture to the screen
        Graphics.Blit(rt, destination);

        foreach (var buffer in buffersToDispose)
        {
            buffer.Dispose();
        }
    }
}
