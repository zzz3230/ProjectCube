using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class TestScene_WorldScript : MonoBehaviour
{
    [SerializeField] private SignalTransmitterScript triggerA;
    [SerializeField] private SignalReceiverScript platformA;
    
    [SerializeField] private SignalTransmitterScript triggerB;
    [SerializeField] private SignalReceiverScript platformB;
    void Start()
    {
        platformA.Connect(triggerA, null);
        platformB.Connect(triggerB, null);
    }

    [Button]
    public void SpeedX3()
    {
        Time.timeScale = 3;
    }

    [Button]
    public void SpeedX1()
    {
        Time.timeScale = 1;
    }

    [Button]
    public static void ResetAll()
    {
        foreach (var r in GameObject.FindObjectsOfType<ResettableMonoBehaviour>())
        {
            r.Reset();   
        }
    }
}
