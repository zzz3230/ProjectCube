using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class VectorUtils
{
    public static Vector3 WithZ(this Vector3 original, float v)
    {
        return new Vector3(original.x, original.y, v);
    }
    public static Vector3 ToVec3(this Vector2 original, float z)
    {
        return new Vector3(original.x, original.y, z);
    }
    public static Vector3 Round(this Vector3 original, int d)
    {
        return new Vector3(
            (float)Math.Round(original.x, d),
            (float)Math.Round(original.y, d),
            (float)Math.Round(original.z, d)
            );
    }
    public static Vector3 Round(this Vector3 original, float d)
    {
        return new Vector3(
            original.x - original.x % d,
            original.y - original.y % d,
            original.z - original.z % d
            );
    }
}

public static class Utils
{
    /*
    public static T GetComponentInParent<T>(this GameObject go)
    {
        return go.GetComponent<T>() ?? (go.transform ? go.transform.parent.GetComponent<T>() : );
    }
    */

    public static bool TryParseFloat(string str, out float value)
    {
        if (str.EndsWith(","))
        {
            str = str[..^1];
        }

        if(str == string.Empty)
        {
            value = 0f;
            return true;
        }

        if(str == "-")
        {
            value = 0f;
            return true;
        }

        var res = float.TryParse(str, out var v);
        value = v;
        return res;
    }
}
