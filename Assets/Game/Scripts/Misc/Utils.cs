using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static Vector3 ToVec3(this SerVector2 original, float z)
    {
        return ToVec3((Vector2)original, z);
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

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static T GetComponentOrInChildren<T>(this GameObject go) where T : Component
    {
        if(go.TryGetComponent<T>(out var comp))
        {
            return comp;
        }
        return go.GetComponentInChildren<T>();
    }

    public static string GetDataFolderPath(string name)
    {
        //, "/../"
        return new DirectoryInfo(Application.dataPath).Parent.FullName + $"/{name}/";
    }
}
