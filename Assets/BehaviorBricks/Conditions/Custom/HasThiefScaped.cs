using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("My Conditions/HasThiefEscaped")]
    [Help("Checks if the thief being chased has already escaped")]
    public class HasThiedEscaped : GOCondition
    {
        [InParam("thief")]
        public GameObject thief;

        private UnityEngine.AI.NavMeshAgent navAgent;

        /// <summary>
        /// Checks whether a target is in sight depending on a given angle, casting a raycast to the target and then compare the angle of forward vector
        /// with de raycast direction.
        /// </summary>
        /// <returns>True if the angle of forward vector with the  raycast direction is lower than the given angle.</returns>
        public override bool Check()
        {
            if (!thief.activeSelf)
            {
                navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                navAgent.speed = 1.5f;
                return false;
            }
            return true;
        }
    }
}
