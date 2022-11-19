using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [SerializeField]
    private float wanderSpeed = 1.0f;

    [SerializeField]
    private float moveRadius = 50.0f;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public bool Go()
    {
        if (!agent) return false;

        agent.speed = wanderSpeed;

        Vector3 nextTarget = Random.insideUnitCircle * moveRadius;

        nextTarget = new Vector3(nextTarget.x, transform.position.y, nextTarget.y);

        agent.destination = nextTarget;

        return true;
    }
}