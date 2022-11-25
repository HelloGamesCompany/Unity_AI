using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("My Conditions/IsOldManScreaming")]
    [Help("Checks if a scream by an old man is close enough of the cop")]
    public class IsOldManScreaming : GOCondition
    {
        [InParam("detectionDistance")]
        public float detectionDistance;

        private UnityEngine.AI.NavMeshAgent navAgent;

        /// <summary>
        /// Checks whether a target is in sight depending on a given angle, casting a raycast to the target and then compare the angle of forward vector
        /// with de raycast direction.
        /// </summary>
        /// <returns>True if the angle of forward vector with the  raycast direction is lower than the given angle.</returns>
        public override bool Check()
        {
            GameObject[] oldMans = GameObject.FindGameObjectsWithTag("Oldman");
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            foreach (var oldMan in oldMans)
            {
                if(oldMan.TryGetComponent(out OldmanController controller) && controller.callingForHelp)
                {
                    if (Vector3.Distance(gameObject.transform.position, oldMan.transform.position) <= detectionDistance)
                    {
                        navAgent.speed = 3.2f;
                        return true;
                    }
                }
            }
            navAgent.speed = 1.5f;
            return false;

        }
    }
}
