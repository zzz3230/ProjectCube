using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstCompScript : BaseCompScript
{
    public override float GetValueBySocket(int socket)
    {
        return socket == 0 ? 1 : throw new CompSocketIndexException();
    }

    public override bool Verify()
    {
        return true;
    }
}
