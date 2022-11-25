using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;

[Action("MyActions/Flee")]
[Help("Get the GameObject to flee from.")]
public class Flee : GOAction
{
    [InParam("fleeObjective")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject fleeObjective;

    [InParam("oldman")]
    [Help("game object that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted")]
    public GameObject oldman;

    [OutParam("foundTarget")]
    [Help("game object that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted")]
    public bool foundTarget;

    [OutParam("stoleWallet")]
    [Help("game object that must have a BoxCollider or SphereColider, which will determine the area from which the position is extracted")]
    public bool stoleWallet;

    private UnityEngine.AI.NavMeshAgent navAgent;

    private Transform fleeTransfrom;
    private bool catched = false;

    /// <summary>Initialization Method of MoveToGameObject.</summary>
    /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
    public override void OnStart()
    {
        catched = false;

        if (oldman.TryGetComponent(out OldmanController controller))
        {
            controller.Help();
            gameObject.GetComponentInChildren<EmotionController>().ChangeSprite(0, 5.0f);
        }
        else
        {
            oldman.GetComponent<OldcopController>().CatchThief();
            foundTarget = false;
            stoleWallet = false;

            gameObject.SetActive(false);
            catched = true;
            oldman = null;
            GameObject.Find("ThiefManager").GetComponent<ThiefManager>().RespawnThief();
            return;
        }

        if (fleeObjective == null)
        {
            Debug.LogError("The movement target of this game object is null", gameObject);
            return;
        }
        fleeTransfrom = fleeObjective.transform;

        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
            navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }

        navAgent.speed = 2.5f;
        navAgent.SetDestination(fleeObjective.transform.position);

#if UNITY_5_6_OR_NEWER
        navAgent.isStopped = false;
#else
                navAgent.Resume();
#endif
    }

    public override TaskStatus OnUpdate()
    {
        if (catched) return TaskStatus.COMPLETED;
        if (fleeObjective == null)
            return TaskStatus.FAILED;
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            fleeObjective.GetComponent<ThiefCarBehaviour>().Run();
            oldman.GetComponent<OldmanController>().StopHelp();

            // Reset my variables
            navAgent.speed = 1.5f;
            foundTarget = false;
            stoleWallet = false;

            gameObject.SetActive(false);
            gameObject.GetComponentInChildren<EmotionController>().ChangeSprite(-1);
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.RUNNING;
    }
}
