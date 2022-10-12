using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    private TreeSpawnerManager treeManager;

    private void Start()
    {
        treeManager = FindObjectOfType<TreeSpawnerManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Runner"))
        {
            if (collision.gameObject.GetComponent<RunnerState>().followedByBees) return;

            if(treeManager.CanSpawnBees())
            {
                treeManager.SpawnBee(this.transform.position, collision.gameObject);
            }
        }
    }


}
