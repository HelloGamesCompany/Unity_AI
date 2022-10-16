using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeekeeperController : MonoBehaviour
{
    [SerializeField]
    float randomMoveRadius = 1.0f;

    [SerializeField]
    [Range(1,20)]
    float runSpeed = 1.0f;

    [SerializeField]
    [Range(1, 20)]
    float followBeeSpeed = 1.0f;

    [HideInInspector]
    public NavMeshAgent agent = null;

    [HideInInspector]
    public GameObject targetBee = null;

    private Vector3 nextTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("FindTarget", 0, 0.2f);
    }

    void FindTarget()
    {
        if (targetBee)
        {
            agent.speed = followBeeSpeed;
            if (!targetBee.activeSelf) targetBee = null;
            else agent.destination = targetBee.transform.position;
            return;
        }

        agent.speed = runSpeed;

        nextTarget = Random.insideUnitCircle * randomMoveRadius;

        nextTarget = new Vector3(nextTarget.x, transform.position.y, nextTarget.y);

        agent.destination = nextTarget;
    }
}
