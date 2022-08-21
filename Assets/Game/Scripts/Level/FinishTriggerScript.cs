using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerScript : MonoBehaviour
{
    /// <summary>
    /// Enter
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<MainCubeScript>(out var cube))
        {
            if (collision.attachedRigidbody && collision.attachedRigidbody.velocity == Vector2.zero)
                LevelScript.instance.PlayerFinished(cube);
        }
    }
}
