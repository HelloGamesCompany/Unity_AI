using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerBehaviour : MonoBehaviour
{
    private float axisX, axisY;

    public float speed = 1.0f;

    public float radius = 2.0f, offset = 0.2f;

    public Target target;

    Vector3 targetPos;

    private NavMeshAgent agent;
    private RunnerState state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        state = GetComponent<RunnerState>();
    }

    void Seek()
    {
        if (target == null) return;

        agent.destination = target.gameObject.transform.position;

        if (Vector3.Distance(transform.position, target.transform.position) < radius)
        {
            target = target.GetComponent<Target>().GetNextTarget(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state.followedByBees) agent.speed = speed * 2.5f;
        Seek();
    }
}
