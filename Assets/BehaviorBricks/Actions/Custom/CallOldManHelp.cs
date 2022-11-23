using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Conditions;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Condition("My Conditions/Call old man Help")]
    [Help("Calls old man controller Help method")]
    public class CallOldManHelp : GOCondition
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target = null;

        public override bool Check()
        {
            int randomNum = Random.Range(0, 2);

            if (randomNum == 0)
            {
                if (target)
                {
                    target.GetComponent<OldmanController>().Help(); // The Oldman noticed 
                    return true;
                }
            }
            return false;
        }
    }
}

