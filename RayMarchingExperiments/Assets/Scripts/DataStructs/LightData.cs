using UnityEngine;

public struct LightData
{
    public Vector3 position;
    public Vector3 direction;
    public Vector4 color;
    public float intensity;
    public float spotAngle;
    public int type;

    public static int GetSize()
    {
        return sizeof(float) * 12 + sizeof(int) * 1;
    }
};

