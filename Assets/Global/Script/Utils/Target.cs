using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Target[] nextTargets;

    // Get random next target
    public Target GetNextTarget(Target comingFrom)
    {
        int randNum = Random.Range(0, nextTargets.Length);

        Target next = nextTargets[randNum];

        if (next == comingFrom) next = nextTargets[randNum == nextTargets.Length-1 ? 0 : ++randNum];

        //Debug.Log(next);

        return next;
    }
}
