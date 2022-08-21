using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControllerWidgetScript : MonoBehaviour
{
    [SerializeField] TrajectorySimulatorScript _trajectorySimulator;
    [SerializeField] SimulationControllerScript _simulationController;
    [SerializeField] PauseMenuWidgetScript _pauseMenuWidget;

    private void Update()
    {
        if (
            //Input.GetAxis("StartDebugSimulation") == 1f
            Input.GetKeyDown(KeyCode.F1)
            )
            Button_StartSimulation();
        if (
            //Input.GetAxis("StartCheckSimulation") == 1f
            Input.GetKeyDown(KeyCode.F2)
            )
            Button_StartCheckSimulation();
        if (
            //Input.GetAxis("Back") == 1f ||
            Input.GetKeyDown(KeyCode.Escape)
            && !_pauseMenuWidget.isShowing)
            Button_ShowMenu();
    }

    public void Button_StartSimulation()
    {
        _simulationController.PrepareEndSimulation();
        _trajectorySimulator.Simulate();
    }
    public void Button_EndSimulation()
    {
        _simulationController.PrepareEndSimulation();
        _trajectorySimulator.StopSimulating();
    }
    public void Button_SetSimulationSpeed(int val)
    {
        _simulationController.SetSpeed(val);
    }
    public void Button_StartCheckSimulation()
    {
        _simulationController.StartCheckSimulation();
    }

    public void Button_ExitToMenu()
    {
        Utils.LoadScene("DemoMenu0");
    }

    public void Button_ShowMenu()
    {
        _pauseMenuWidget.Show();
    }
}
