using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [HideInInspector]
    public Target[] roadTargets;
    private Target[] beeKeepers;

    private void Awake()
    {
        GameObject bkManager = gameObject.transform.Find("BeeKeepers").gameObject;
        beeKeepers = bkManager.GetComponentsInChildren<Target>();

        GameObject rdManager = gameObject.transform.Find("RoadTargets").gameObject;
        roadTargets = rdManager.GetComponentsInChildren<Target>();
    }

    public Target GetNearestKeeper(Vector3 myPos)
    {
        Target closestKeeper = null;
        float closestDistance = -1.0f;

        foreach (Target keeper in beeKeepers)
        {
            if (closestDistance == -1.0f)
            {
                closestDistance = Vector3.Distance(keeper.transform.position, myPos);
                closestKeeper = keeper;
                continue;
            }

            float distance = Vector3.Distance(keeper.transform.position, myPos);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestKeeper = keeper;
            }
        }
        return closestKeeper;
    }

    public Target GetNearestRoad(Vector3 myPos)
    {
        Target closestRoad = null;
        float closestDistance = -1.0f;

        foreach (Target road in roadTargets)
        {
            if (closestDistance == -1.0f)
            {
                closestDistance = Vector3.Distance(road.transform.position, myPos);
                closestRoad = road;
                continue;
            }

            float distance = Vector3.Distance(road.transform.position, myPos);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoad = road;
            }
        }
        return closestRoad;
    }
}
