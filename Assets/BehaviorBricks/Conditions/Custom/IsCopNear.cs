using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{
    ///<value>Input Target Parameter to to check the distance.</value>
    [InParam("cop")]
    [Help("Target to check the distance")]
    public GameObject cop;

    [InParam("old man")]
    [Help("Target to check the distance")]
    public GameObject oldMan;

    ///<value>Input maximun distance Parameter to consider that the target is close.</value>
    [InParam("closeDistance")]
    [Help("The maximun distance to consider that the target is close")]
    public float distance;

    public override bool Check()
    {
        return Vector3.Distance(cop.transform.position, oldMan.transform.position) < distance;
    }
}