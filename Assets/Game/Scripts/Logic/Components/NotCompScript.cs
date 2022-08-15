using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotConpScript : BaseCompScript
{
    public override float GetValueBySocket(int socket)
    {
        return socket == 0 ? input[0].GetValue() : throw new CompSocketIndexException();
    }
    
    public override bool Verify()
    {
        return input.Count == 1;
    }
}
