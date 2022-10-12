using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeekeeperController : MonoBehaviour
{
    [SerializeField]
    float radius = 1.0f;

    [SerializeField]
    float runSpeed = 1.0f;

    private NavMeshAgent agent = null;

    private Vector3 nextTarget;

    public Vector3 test;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = runSpeed;

        FindTarget();     
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(nextTarget, transform.position) <= 0.5f) FindTarget();
    }

    void FindTarget()
    {
        nextTarget = Random.insideUnitCircle * radius;

        nextTarget = new Vector3(nextTarget.x, transform.position.y, nextTarget.y);

        agent.destination = nextTarget;

        Debug.Log(nextTarget);
    }
}
