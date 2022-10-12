using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerState : MonoBehaviour
{
    public bool followedByBees = false;

    private RunnerBehaviour behaviour;
    private TargetManager targetManager;
    private void Start()
    {
        targetManager = FindObjectOfType<TargetManager>();
        behaviour = GetComponent<RunnerBehaviour>();
    }

    public void PanickRun()
    {
        followedByBees = true;
        behaviour.target = targetManager.GetNearestKeeper(transform.position);
        Debug.Log("Runner runs in panick!");
    }


}
