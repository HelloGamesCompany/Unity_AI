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

        behaviour.agent.speed = behaviour.speed * 2.5f; 

        //Debug.Log("Runner runs in panick!");
    }
    public void LeavePanicRun()
    {
        followedByBees = false;

        behaviour.target = targetManager.GetNearestRoad(transform.position);

        behaviour.agent.speed = behaviour.speed;

        //Debug.Log("Runner runs out panick!");
    }
}
