using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTest : MonoBehaviour
{
    private LogicCableScript outputCable;
    public LogicToValueConverterScript conv;
    void Start()
    {
        var timeComp = new TimeCompScript();
        var sinComp = new SinCompScript();
        var timeToSinCable = new LogicCableScript();
        outputCable = new LogicCableScript();
        
        timeToSinCable.SetupInput(timeComp, 0);
        timeToSinCable.SetupOutput(sinComp, 0);
        
        sinComp.input.Add(timeToSinCable);
        
        outputCable.SetupInput(sinComp, 0);

        conv.input = outputCable;
    }

    private void Update()
    {
        //Debug.Log(outputCable.GetValue());
    }
}
