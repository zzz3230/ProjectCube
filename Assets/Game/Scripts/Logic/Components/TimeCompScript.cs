using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCompScript : BaseCompScript
{
    public override float GetValueBySocket(int socket)
    {
        return Time.timeSinceLevelLoad;
    }
    
    public override bool Verify()
    {
        return true;
    }
}
