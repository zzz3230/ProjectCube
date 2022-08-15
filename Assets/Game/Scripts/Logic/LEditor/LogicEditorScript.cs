using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEditorScript : MonoBehaviour
{
    public static LogicEditorScript current { get; private set; }

    [SerializeField] UILineScript cableLineOriginal;
    [SerializeField] UILineScript _cableConnectingPreview;
    [SerializeField] RectTransform _cableParent;
    
    void Start()
    {
        current = this;
        //_cableConnectingPreview = Instantiate(cableLineOriginal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _connecting)
        {
            {
                _cableConnectingPreview.SetPositions(_connecting.transform.position, Input.mousePosition);
            }
        }
        else
        {
            _cableConnectingPreview.SetPositions(-Vector3.one, Vector3.one);

            if (!Input.GetMouseButton(0))
                _connecting = null;
        }
    }

    LogicConnectorWidgetScript _connecting;
    

    public void BeginCableConnecting(LogicConnectorWidgetScript connector)
    {
          _connecting = connector;
    }

    public void EndCableConnecting(LogicConnectorWidgetScript connector)
    {
        if (_connecting && connector != _connecting)
        {
            ConnectCable(_connecting, connector);
        }
    }

    void ConnectCable(LogicConnectorWidgetScript a, LogicConnectorWidgetScript b)
    {
        var c = Instantiate(cableLineOriginal);
        c.SetObjects(a.gameObject, b.gameObject);
        c.transform.parent = _cableParent;
        Debug.Log("connected");
    }

    
}
