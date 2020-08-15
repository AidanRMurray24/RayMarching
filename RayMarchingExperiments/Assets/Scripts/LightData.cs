using UnityEngine;

public struct LightData
{
    public Vector3 position;
    public Vector3 direction;
    public Vector4 color;
    public int type;

    public static int GetSize()
    {
        return sizeof(float) * 10 + sizeof(int) * 1;
    }
};

