using UnityEngine;

public class WanderState : StateMachineBehaviour
{
    [SerializeField]
    private float updateFreq = 1.0f;

    private float counter = 0;

    private Wander wanderScript = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderScript = animator.GetComponent<Wander>();

        counter = updateFreq;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter += Time.deltaTime;

        if (wanderScript && counter >= updateFreq) 
        {
            wanderScript.Go();
            counter = 0;
        }
    }
}
