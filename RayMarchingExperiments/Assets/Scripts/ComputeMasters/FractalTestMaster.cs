using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
public class FractalTestMaster : MonoBehaviour
{
    [SerializeField] private ComputeShader shader = null;
    [SerializeField] private Light mainLight = null;
    [SerializeField] private float gloss = .5f;
    [SerializeField] private uint iterations = 10;
    [SerializeField] private float scale = 2f;
    [SerializeField] private float blendStrength = 1f;
    private List<ComputeBuffer> buffersToDispose = null;
    private RenderTexture rt = null;
    private Camera cam = null;
    private int kernel = 0;

    private void Awake() => cam = GetComponent<Camera>();

    private void Start() => kernel = shader.FindKernel("CSMain");

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
        shader.SetFloat("_Gloss", gloss);
        shader.SetFloat("_Scale", scale);
        shader.SetFloat("_BlendStrength", blendStrength);
        shader.SetInt("_Iterations", (int)iterations);
        shader.SetTexture(kernel, "Source", source);
        SetLightData();
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
