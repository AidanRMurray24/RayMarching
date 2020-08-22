using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
public class MandelbrotExplorer : MonoBehaviour
{
    [SerializeField] private ComputeShader shader = null;
    [SerializeField] private Texture colorGradientTexture = null;
    [SerializeField][Range(0,1)] private float color = 0f;
    [SerializeField] private float repeat = 1f;
    [SerializeField] private float speed = 0f;
    [SerializeField] [Range(0, 1)]private float symmetry = 0f;
    private Vector2 position = Vector2.zero;
    private float scale = 4f; 
    private float angle = 0f;
    private InputMaster controls = null;
    private RenderTexture rt = null;
    private Camera cam = null;

    private bool isZooming = false, isMoving = false, isRotating = false;
    private float rotateVal = 0f, zoomVal = 0f;
    private Vector2 moveVal = Vector2.zero;
    private Vector2 smoothPos = Vector2.zero;
    private float smoothScale = 4f, smoothAngle = 0f;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        if (!Application.isPlaying) return;
        controls = new InputMaster();
    }

    private void OnEnable()
    {
        if (!Application.isPlaying) return;

        controls.MandelbrotExplorer.Movement.performed += ctx => { moveVal = ctx.ReadValue<Vector2>(); isMoving = true; };
        controls.MandelbrotExplorer.Rotation.performed += ctx => { rotateVal = ctx.ReadValue<float>(); isRotating = true; };
        controls.MandelbrotExplorer.Zoom.performed += ctx => { zoomVal = ctx.ReadValue<float>(); isZooming = true; };
        controls.MandelbrotExplorer.Movement.canceled += ctx =>  isMoving = false;
        controls.MandelbrotExplorer.Rotation.canceled += ctx => isRotating = false;
        controls.MandelbrotExplorer.Zoom.canceled += ctx =>  isZooming = false;
        controls.MandelbrotExplorer.Enable();
    }

    private void OnDisable()
    {
        if (!Application.isPlaying) return;

        controls.MandelbrotExplorer.Movement.performed -= ctx => { moveVal = ctx.ReadValue<Vector2>(); isMoving = true; };
        controls.MandelbrotExplorer.Rotation.performed -= ctx => { rotateVal = ctx.ReadValue<float>(); isRotating = true; };
        controls.MandelbrotExplorer.Zoom.performed -= ctx => { zoomVal = ctx.ReadValue<float>(); isZooming = true; };
        controls.MandelbrotExplorer.Movement.canceled -= ctx => isMoving = false;
        controls.MandelbrotExplorer.Rotation.canceled -= ctx => isRotating = false;
        controls.MandelbrotExplorer.Zoom.canceled -= ctx => isZooming = false;
        controls.MandelbrotExplorer.Disable();
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
        shader.SetVector("texResolution", new Vector2(cam.pixelWidth, cam.pixelHeight));

        // Calculate the correct scale to pass into the shader
        float scaleX = smoothScale, scaleY = smoothScale;
        float aspectRatio = (float)cam.pixelWidth / (float)cam.pixelHeight;
        if (aspectRatio > 1f)
            scaleY /= aspectRatio;
        else
            scaleX *= aspectRatio;
        shader.SetVector("area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        shader.SetFloat("angle", smoothAngle);
        int kernel = shader.FindKernel("CSMain");
        shader.SetTexture(kernel, "ColorGradients", colorGradientTexture);
        shader.SetFloat("color", color);
        shader.SetFloat("repeat", repeat);
        shader.SetFloat("time", Time.time);
        shader.SetFloat("speed", speed);
        shader.SetFloat("symmetry", symmetry);
    }

    private void Render(RenderTexture destination)
    {
        // Make sure we have a current render target
        InitRenderTexture();

        // Set the target and dispatch the compute shader
        int kernel = shader.FindKernel("CSMain");
        shader.SetTexture(kernel, "Result", rt);
        int threadGroupsX = Mathf.CeilToInt(cam.pixelWidth / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(cam.pixelHeight / 8.0f);
        shader.Dispatch(kernel, threadGroupsX, threadGroupsY, 1);

        // Blit the result texture to the screen
        Graphics.Blit(rt, destination);
    }

    private void FixedUpdate()
    {
        if (!Application.isPlaying) return;

        // Calculate smoothness
        smoothPos = Vector2.Lerp(smoothPos, position, .03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, .03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, .03f);

        if (isZooming)
            Zoom(zoomVal);

        if (isRotating)
            Rotate(rotateVal);

        if (isMoving)
            Move(moveVal);
    }

    private void Zoom(float zoomValue)
    {
        if (zoomValue > 0)
            scale *= .99f;
        else
            scale *= 1.01f;   
    }

    private void Rotate(float direction)
    {
        if (direction > 0)
            angle -= .01f;
        else
            angle += .01f;
    }

    private void Move(Vector2 moveValue)
    {
        Vector2 dir = new Vector2(.01f * scale, 0);

        // X axis
        dir = RotateVectorByAngle(dir, angle);
        if (moveVal.x < 0)
            position -= dir;
        else if (moveVal.x > 0)
            position += dir;


        // Y axis
        dir = new Vector2(-dir.y, dir.x);
        if (moveVal.y < 0)
            position -= dir;
        else if (moveVal.y > 0)
            position += dir;
    }

    private Vector2 RotateVectorByAngle(Vector2 dir, float angle)
    {
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        return new Vector2(dir.x*c - dir.y*s, dir.x*s + dir.y*c);
    }
}
