using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerManager : MonoBehaviour
{
    private BeeManager[] beeManagers;

    private void Awake()
    {
        GameObject beeManager = gameObject.transform.Find("Bees").gameObject;
        beeManagers = beeManager.GetComponentsInChildren<BeeManager>();

        GameObject trees = gameObject.transform.Find("Trees").gameObject;
        TreeSpawner[] treeSpawners = trees.GetComponentsInChildren<TreeSpawner>();
        for (int i = 0; i < treeSpawners.Length; i++)
        {
            treeSpawners[i].beeManager = beeManagers[i];
            beeManagers[i].transform.position = treeSpawners[i].transform.position;
        }
    }
}
