using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] SimulationControllerScript _simulationController;
    [SerializeField] FinishWidgetScript _finishWidget;

    private void Awake()
    {
        instance = this;
    }

    public static LevelScript instance { get; private set; }

    public bool canEditLevel { get; private set; } = true;
    public bool isCheckSimulation { get; private set; } = false;

    public void PlayerFinished(MainCubeScript cube)
    {
        if (isCheckSimulation)
        {
            Debug.Log("FINISH");
            _finishWidget.Show();
        }
            
    }

    public void RequestStopSimulating(MainCubeScript cube)
    {
        _simulationController.PrepareEndSimulation();
        _simulationController.trajectorySimulator.StopSimulating();
    }

    public void StartCheckSimulation()
    {
        canEditLevel = false;
        isCheckSimulation = true;
    }
    public void EndCheckSimulation()
    {
        canEditLevel = true;
        isCheckSimulation = false;
    }
}
