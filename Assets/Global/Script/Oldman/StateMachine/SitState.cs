using UnityEngine;
using UnityEngine.AI;

public class SitState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NavMeshAgent agent = animator.gameObject.GetComponent<NavMeshAgent>();

        if (agent)
        {
            agent.isStopped = true;
        }
    }
}
