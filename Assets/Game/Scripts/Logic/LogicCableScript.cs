using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicCableScript
{
    public BaseCompScript input;
    public int inputSocket;
    public BaseCompScript output;
    public int outputSocket;

    public void SetupInput(BaseCompScript comp, int socket)
    {
        input = comp;
        inputSocket = socket;
    }
    
    public void SetupOutput(BaseCompScript comp, int socket)
    {
        output = comp;
        outputSocket = socket;
    }
    
    public float GetValue()
    {
        return input.GetValueBySocket(inputSocket);
    }
}
