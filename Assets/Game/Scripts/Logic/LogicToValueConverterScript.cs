using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicToValueConverterScript : ValueGetterScript
{
    public LogicCableScript input;

    private void Start()
    {
        //InvokeRepeating();
        StartCoroutine(Upd());
    }

    private void Update()
    {
        
        //Debug.Log(input.GetValue());
    }

    IEnumerator Upd()
    {
        while (true)
        {
            CallValueChanged(input.GetValue());
            yield return new WaitForSeconds(0.1f);
        }
    }
}
