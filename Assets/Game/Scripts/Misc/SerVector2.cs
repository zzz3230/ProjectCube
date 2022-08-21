using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SerVector2
{
    public float x;
    public float y;

    public SerVector2(float x, float y)
    {
        this.x = x; this.y = y;
    }

    public static implicit operator Vector2(SerVector2 sv)
    {
        return new Vector2(sv.x, sv.y);
    }
    public static implicit operator SerVector2(Vector2 sv)
    {
        return new SerVector2(sv.x, sv.y);
    }
    public static implicit operator SerVector2(Vector3 sv)
    {
        return new SerVector2(sv.x, sv.y);
    }
    public static implicit operator Vector3(SerVector2 sv)
    {
        return new Vector3(sv.x, sv.y);
    }
}
