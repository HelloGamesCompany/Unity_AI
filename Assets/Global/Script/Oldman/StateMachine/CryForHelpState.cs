using UnityEngine;
using UnityEngine.AI;

public class CryForHelpState : StateMachineBehaviour
{
    private SphereCollider voice = null;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!voice)
        {
            voice = animator.GetComponent<SphereCollider>();
        }

        if (voice)
        {
            voice.enabled = true;
            voice.radius = 10.0f;
        }

        if(animator.TryGetComponent(out NavMeshAgent agent))
        {
            agent.isStopped = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (voice)
        {
            voice.enabled = false;
            voice.radius = 0.5f;
        }
    }
}