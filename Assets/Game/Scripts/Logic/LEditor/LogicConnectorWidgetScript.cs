using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class LogicConnectorWidgetScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool _isInput;
    public bool isInput => _isInput;

    public void OnPointerDown(PointerEventData eventData)
    {
        LogicEditorScript.current.BeginCableConnecting(this); 
        print("DOWN" + eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("UP" + eventData.pointerCurrentRaycast.gameObject);
        if(eventData.pointerCurrentRaycast.gameObject.TryGetComponent<LogicConnectorWidgetScript>(out var conn))
        {
            print(conn);
            LogicEditorScript.current.EndCableConnecting(conn);
        }

        //var conn = eventData.hovered.Where(x => x != gameObject && x.TryGetComponent<LogicConnectorWidgetScript>(out _)).First();
        /*Debug.Log(eventData.hovered.Count);

        
        foreach (var obj in eventData.hovered)
        {
            Debug.Log(obj);
            if (obj != gameObject && obj.TryGetComponent<LogicConnectorWidgetScript>(out var con))
            {
                LogicEditorScript.current.EndCableConnecting(con);
            }
        }
        */
        //LogicEditorScript.current.EndCableConnecting(conn.GetComponent<LogicConnectorWidgetScript>());
    }
}
