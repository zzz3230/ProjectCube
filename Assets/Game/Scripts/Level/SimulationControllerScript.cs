using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControllerScript : MonoBehaviour
{
    [SerializeField] TrajectorySimulatorScript _trajectorySimulator;
    public TrajectorySimulatorScript trajectorySimulator => _trajectorySimulator;

    public void SetSpeed(int val)
    {
        if(!LevelScript.instance.isCheckSimulation)
            Time.timeScale = val;
    }

    public void PrepareEndSimulation()
    {
        LevelScript.instance.EndCheckSimulation();
    }

    public void StartCheckSimulation()
    {
        _trajectorySimulator.StopSimulating();
        SetSpeed(1);
        LevelScript.instance.StartCheckSimulation();
        _trajectorySimulator.Simulate();
    }
}
