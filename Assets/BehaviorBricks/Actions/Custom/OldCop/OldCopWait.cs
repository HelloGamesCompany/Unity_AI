using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;

/// <summary>
/// It is an action to move towards the given goal using a NavMeshAgent.
/// </summary>
[Action("My Actions/OldCopWait")]
[Help("Calls old cop wait method")]
public class OldCopWait : GOAction
{
    ///<value>Input target game object towards this game object will be moved Parameter.</value>
    [InParam("oldCopController")]
    public OldcopController target = null;

    public override void OnStart()
    {
        if (target && !target.isSitting)
            target.Sit();
    }

}


