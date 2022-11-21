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

        // animation
        if (animator.transform.GetChild(0).TryGetComponent(out Animator anim))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("OldMan_Sit"))
            {
                anim.SetTrigger("Sit");
            }
        }

        // Emotions
        EmotionController emController = animator.GetComponentInChildren<EmotionController>();

        if (emController)
        {
            emController.ChangeSprite(0);
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

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EmotionController emController = animator.GetComponentInChildren<EmotionController>();

        if (emController)
        {
            emController.ChangeSprite(-1);
        }
    }
}
