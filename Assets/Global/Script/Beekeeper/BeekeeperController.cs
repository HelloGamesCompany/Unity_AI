using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeekeeperController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 20)]
    float followBeeSpeed = 1.0f;

    [HideInInspector]
    public NavMeshAgent agent = null;

    [HideInInspector]
    public GameObject targetBee = null;

    private Wander wander;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        wander = GetComponent<Wander>();

        InvokeRepeating(nameof(FindTarget), 0, 0.2f);
    }

    private void FindTarget()
    {
        if (targetBee)
        {
            agent.speed = followBeeSpeed;
            if (!targetBee.activeSelf) targetBee = null;
            else agent.destination = targetBee.transform.position;
            return;
        }

        if(wander) wander.Go();
    }
}