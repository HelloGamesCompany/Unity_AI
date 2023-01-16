using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayingChild : MonoBehaviour
{
    public enum ChildState
    {
        GOING_TO_CENTER,
        HIDING,
        WAITING,
        SEARCHING
    }

    public Transform centerPos;

    private NavMeshAgent _navMeshAgent;

    public ChildState state;

    public Vector3 hidingSpot;

    public bool goingToPos = false;

    private void Start()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void GoToSpot(Vector3 spot)
    {
        _navMeshAgent.SetDestination(spot);
        goingToPos = true;
    }

    public void GoToCenter()
    {
        _navMeshAgent.SetDestination(centerPos.position);
    }

    public void GoToHidingSpot()
    {
        _navMeshAgent.SetDestination(hidingSpot);
    }
}
