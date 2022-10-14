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

    [HideInInspector]
    public NavMeshAgent agent = null;

    [HideInInspector]
    public GameObject targetBee = null;

    private Vector3 nextTarget;

    public Vector3 test;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = runSpeed;

        // FindTarget();     

        InvokeRepeating("FindTarget", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(nextTarget, transform.position) <= 0.5f) FindTarget();
    }

    void FindTarget()
    {
        if (targetBee)
        {
            if (!targetBee.activeSelf) targetBee = null;
            else agent.destination = targetBee.transform.position;
            return;
        }

        nextTarget = Random.insideUnitCircle * radius;

        nextTarget = new Vector3(nextTarget.x, transform.position.y, nextTarget.y);

        agent.destination = nextTarget;
    }
}
