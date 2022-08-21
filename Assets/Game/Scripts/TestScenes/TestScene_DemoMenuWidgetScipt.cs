using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_DemoMenuWidgetScipt : MonoBehaviour
{
    public void Button_LoadLevel(int index)
    {
        if(index >= 0)
        {
            Utils.LoadScene("DemoScene" + index);
        }
        else if(index == -1)
        {
            Utils.LoadScene("TestBuild0");
        }
    }
    public void Button_Exit()
    {
        Application.Quit();
    }
}
