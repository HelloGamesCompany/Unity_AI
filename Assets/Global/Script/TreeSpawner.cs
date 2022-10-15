using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [HideInInspector]
    public BeeManager beeManager;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Runner"))
        {
            if (collision.gameObject.GetComponent<RunnerState>().followedByBees) return;

            beeManager.SetTarget(collision.gameObject);
            collision.GetComponent<RunnerState>().PanickRun();
        }
    }
}
