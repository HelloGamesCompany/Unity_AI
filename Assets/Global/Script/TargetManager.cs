using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Target[] roadTargets;
    public Target[] beeKeepers;

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
}
