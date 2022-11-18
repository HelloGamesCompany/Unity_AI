using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;

[Action("MyActions/Flee")]
[Help("Get the GameObject to flee from.")]
public class HideBB : GOAction
{
    [InParam("fleeSource")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject fleeSource;

    [InParam("area")]
    [Help("game object that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted")]
    public GameObject area;

    private UnityEngine.AI.NavMeshAgent navAgent;

    private Transform fleeTransfrom;

    /// <summary>Initialization Method of MoveToGameObject.</summary>
    /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
    public override void OnStart()
    {
        Debug.Log("Start fleeing!");
        if (fleeSource == null)
        {
            Debug.LogError("The movement target of this game object is null", gameObject);
            return;
        }
        fleeTransfrom = fleeSource.transform;

        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
            navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }

        navAgent.speed = 2.5f;
        navAgent.SetDestination(getRandomPosition());

#if UNITY_5_6_OR_NEWER
        navAgent.isStopped = false;
#else
                navAgent.Resume();
#endif
    }

    public override TaskStatus OnUpdate()
    {
        if (fleeSource == null)
            return TaskStatus.FAILED;
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            return TaskStatus.COMPLETED;
        return TaskStatus.RUNNING;
    }

    private Vector3 getRandomPosition()
    {
        BoxCollider boxCollider = area != null ? area.GetComponent<BoxCollider>() : null;
        if (boxCollider != null)
        {
            return new Vector3(UnityEngine.Random.Range(area.transform.position.x - area.transform.localScale.x * boxCollider.size.x * 0.5f,
                                                        area.transform.position.x + area.transform.localScale.x * boxCollider.size.x * 0.5f),
                               area.transform.position.y,
                               UnityEngine.Random.Range(area.transform.position.z - area.transform.localScale.z * boxCollider.size.z * 0.5f,
                                                        area.transform.position.z + area.transform.localScale.z * boxCollider.size.z * 0.5f));
        }
        else
        {
            SphereCollider sphereCollider = area != null ? area.GetComponent<SphereCollider>() : null;
            if (sphereCollider != null)
            {
                float distance = UnityEngine.Random.Range(-sphereCollider.radius, area.transform.localScale.x * sphereCollider.radius);
                float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                return new Vector3(area.transform.position.x + distance * Mathf.Cos(angle),
                                   area.transform.position.y,
                                   area.transform.position.z + distance * Mathf.Sin(angle));
            }
            else
            {
                return gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
            }
        }
    }
}
