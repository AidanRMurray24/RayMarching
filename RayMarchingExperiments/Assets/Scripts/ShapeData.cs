using UnityEngine;

public struct ShapeData
{
    public Vector3 position;
    public Vector3 scale;
    public Vector3 colour;
    public int shapeType;
    public int operation;
    public float blendStrength;
    public int numChildren;

    public static int GetSize()
    {
        return sizeof(float) * 10 + sizeof(int) * 3;
    }
}