using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<MainCubeScript>(out var cube))
        {
            LevelScript.instance.RequestStopSimulating(cube);
        }
    }
}
