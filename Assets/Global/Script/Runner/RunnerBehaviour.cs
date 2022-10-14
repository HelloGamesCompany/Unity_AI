using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerBehaviour : MonoBehaviour
{
    public float speed = 1.0f;

    public float radius = 2.0f;

    [HideInInspector]
    public Target target;

    [HideInInspector]
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        TargetManager targetManager = FindObjectOfType<TargetManager>();

        target = targetManager.GetNearestRoad(transform.position);

        InvokeRepeating("Seek", 0, 0.5f);
    }

    void Seek()
    {
        if (target == null) return;

        agent.destination = target.gameObject.transform.position;

        if (target.CompareTag("BeeKeeper")) return;

        if (Vector3.Distance(transform.position, target.transform.position) < radius)
        {
            target = target.GetComponent<Target>().GetNextTarget(target);
        }
    }
}
