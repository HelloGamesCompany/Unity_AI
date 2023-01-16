using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaichiHead : MonoBehaviour
{
    public float speed = 1.0f;

    public float radius = 2.0f;

    [HideInInspector]
    public Target target;

    [HideInInspector]
    public NavMeshAgent agent;

    //private List<TaichiFollower> _followers = new();

    [SerializeField]
    private int waitingCounter = 30;

    private TargetManager targetManager;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        targetManager = FindObjectOfType<TargetManager>();

        target = targetManager.GetNearestRoad(transform.position);

        InvokeRepeating("Seek", 0, 0.5f);
    }

    void Seek()
    {
        if (target == null) return;

        agent.destination = target.gameObject.transform.position;

        if (target.CompareTag("BeeKeeper")) return;

        if(agent.speed == 0)
        {
            if(waitingCounter-- <= 0)
            {
                agent.speed = speed;
            }
        }

        else if (Vector3.Distance(transform.position, target.transform.position) < radius)
        {
            target = targetManager.GetRandomRoad();
            waitingCounter = 30;
            agent.speed = 0;
        }
    }
}
