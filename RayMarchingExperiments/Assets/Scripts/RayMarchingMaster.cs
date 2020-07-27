using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RayMarchingMaster : MonoBehaviour
{
    [SerializeField] private ComputeShader shader = null;
    private RenderTexture rt = null;
    private Camera cam = null;

    private void Awake()
    {
        // Get a reference to the camera
        cam = GetComponent<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        SetShaderParams();
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

    private void SetShaderParams()
    {
        shader.SetMatrix("_CameraToWorld", cam.cameraToWorldMatrix);
        shader.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
    }

    private void Render(RenderTexture destination)
    {
        // Make sure we have a current render target
        InitRenderTexture();

        // Set the target and dispatch the compute shader
        shader.SetTexture(0, "Result", rt);
        int threadGroupsX = Mathf.CeilToInt(cam.pixelWidth / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(cam.pixelHeight / 8.0f);
        shader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        // Blit the result texture to the screen
        Graphics.Blit(rt, destination);
    }
}
