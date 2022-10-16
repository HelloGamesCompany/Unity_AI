using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [HideInInspector]
    public BeeManager beeManager;

    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Runner"))
        {
            if (collision.gameObject.GetComponent<RunnerState>().followedByBees) return;
            if (!beeManager.gameObject.activeSelf || beeManager.beeLeader != null) return;

            beeManager.SetTarget(collision.gameObject);
            collision.GetComponent<RunnerState>().PanickRun();
        }
    }
}
