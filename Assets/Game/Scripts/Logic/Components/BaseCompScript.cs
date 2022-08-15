using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSocketIndexException : System.Exception {}

public class BaseCompScript 
{
    public List<LogicCableScript> input = new List<LogicCableScript>();
    public virtual float GetValueBySocket(int socket)
    {
        return -666;
    }

    public virtual bool Verify()
    {
        return false;
    }
}
