using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCompScript : BaseCompScript
{
    public override float GetValueBySocket(int socket)
    {
        return socket == 0 ? input[0].GetValue() + input[1].GetValue() : throw new CompSocketIndexException();
    }

    public override bool Verify()
    {
        return input.Count == 2;
    }
}
