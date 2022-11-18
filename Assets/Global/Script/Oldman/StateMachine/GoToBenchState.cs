using UnityEngine;
using UnityEngine.AI;

public class GoToBenchState : StateMachineBehaviour
{
    private Vector3 benchPos = new Vector3(0, 0, 0);

    NavMeshAgent agent = null;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OldmanController controller = animator.GetComponent<OldmanController>();

        if (controller)
        {
            agent = animator.GetComponent<NavMeshAgent>();

            if (agent)
            {
                agent.destination = benchPos = controller.benchPos;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("DISTANCE: "+Vector3.Distance(agent.transform.position, benchPos));

        if (Vector3.Distance(agent.transform.position, benchPos) <= 1.5f)
        {
            animator.SetTrigger("SitDown");
        }
    }
}
