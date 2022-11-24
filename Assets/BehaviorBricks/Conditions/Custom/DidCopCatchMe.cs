using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("My Conditions/DidCopCatchMe")]
    [Help("Checks if the cop catched me")]
    public class DidCopCatchMe : GOCondition
    {
        [InParam("cop")]
        public GameObject cop;

        [InParam("oldman")]
        public GameObject oldman;

        [OutParam("foundTarget")]
        public bool foundTarget;

        [OutParam("stoleWallet")]
        public bool stoleWallet;


        private UnityEngine.AI.NavMeshAgent navAgent;

        /// <summary>
        /// Checks whether a target is in sight depending on a given angle, casting a raycast to the target and then compare the angle of forward vector
        /// with de raycast direction.
        /// </summary>
        /// <returns>True if the angle of forward vector with the  raycast direction is lower than the given angle.</returns>
        public override bool Check()
        {
            if (Vector3.Distance(cop.transform.position, gameObject.transform.position) <= 1.5f)
            {
                oldman.GetComponent<OldmanController>().StopHelp();

                // Reset my variables
                navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                navAgent.speed = 1.5f;
                foundTarget = false;
                stoleWallet = false;

                gameObject.SetActive(false);

                GameObject.Find("ThiefManager").GetComponent<ThiefManager>().RespawnThief();
                return true;
            }
            return false;
        }
    }
}
