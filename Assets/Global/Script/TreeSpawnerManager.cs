using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerManager : MonoBehaviour
{
    public GameObject[] trees;

    public BeeManager[] beeManagers;

    public int beeNum;

    private void Awake()
    {
        GameObject beeManager = gameObject.transform.Find("Bees").gameObject;
        beeManagers = beeManager.GetComponentsInChildren<BeeManager>(true);
    }

    public bool CanSpawnBees()
    {
        uint activeBees = 0;
        foreach (BeeManager bee in beeManagers)
        {
            if (bee.gameObject.activeSelf)
            {
                activeBees++;
            }
        }
        return activeBees < beeNum;
    }

    public void SpawnBee(Vector3 spawnPos, GameObject target)
    {
        foreach(BeeManager bee in beeManagers)
        {
            if (!bee.gameObject.activeSelf)
            {
                bee.gameObject.SetActive(true); // Activate bee
                bee.gameObject.transform.position = spawnPos; // Set to current position
                bee.leaderTarget = target;
                target.GetComponent<RunnerState>().PanickRun(); // Activate runner panick
                break;
            }
        }
    }
}
