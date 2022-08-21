using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWidgetScript : MonoBehaviour
{
    public void Button_Continue()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
