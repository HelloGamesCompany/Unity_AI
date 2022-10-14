using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    private float axisX, axisY;

    public float speed = 1.0f;

    public float radius = 2.0f, offset = 0.2f;

    public Target target;

    Vector3 targetPos;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Seek()
    {
        agent.destination = target.gameObject.transform.position;

        if (Vector3.Distance(transform.position, target.transform.position) < radius)
        {
            target = target.GetComponent<Target>().GetNextTarget(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Seek();
    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 targetPos = transform.TransformPoint(localTarget);
        targetPos.y = 0f;

        agent.destination = targetPos;

        target.transform.position = targetPos;
    }
}
