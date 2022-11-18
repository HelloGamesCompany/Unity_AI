using UnityEngine;
using UnityEngine.AI;

public class SitState : StateMachineBehaviour
{
    [SerializeField]
    private float minSitTime = 1.0f;
    [SerializeField]
    private float maxSitTime = 5.0f;

    private float sitTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sitTime = Random.Range(minSitTime, maxSitTime);

        if(animator.TryGetComponent(out NavMeshAgent agent))
        {
            agent.isStopped = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sitTime -= Time.deltaTime;

        if (sitTime <= 0)
        {
            animator.SetTrigger("Wander");
        }
    }
}
