using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCompScript : BaseCompScript
{
    // Start is called before the first frame update
    public override float GetValueBySocket(int socket)
    {
        return socket == 0 ? Mathf.Sin(input[0].GetValue()) : throw new CompSocketIndexException();
    }
    
    public override bool Verify()
    {
        return input.Count == 1;
    }
}
