using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControllerWidgetScript : MonoBehaviour
{
    [SerializeField] TrajectorySimulatorScript _trajectorySimulator;

    public void Button_StartSimulation()
    {
        _trajectorySimulator.Simulate();
    }
    public void Button_EndSimulation()
    {
        _trajectorySimulator.StopSimulating();
    }
}
