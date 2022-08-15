using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformParams
{
    public class DisplayNameAttribute : System.Attribute
    {
        string _name;
        public string name => _name;
        public DisplayNameAttribute(string name)
        {
            _name = name;
        }
    }

    public class FloatRangeAttribute : System.Attribute
    {
        public FloatRange range => _range;
        FloatRange _range;
        public FloatRangeAttribute(float min, float max)
        {
            _range = new FloatRange { max = max, min = min };
        }
    }

    [DisplayName("Position")]
    public Vector2 position;

    [DisplayName("Rotation")]
    //[FloatRange(-360f, 360f)]float
    public Angle rotation;
}

public class LevelPlatformEditorScript : MonoBehaviour
{
    [SerializeField] GameObject _platformRoot;
    [SerializeField] SignalConverterScript _signalConverter;

    public void Move(Vector2 newPos)
    {
        //_platformRoot.transform.position = newPos;
    }

    

    public void ApplyParams()
    {
        //_signalConverter.absentValue = 
    }
}
