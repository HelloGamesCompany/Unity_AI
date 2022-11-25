using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/StoleWallet")]
[Help("Checks whether Cop is near the Treasure.")]
public class StoleWallet : ConditionBase
{
    ///<value>Input Target Parameter to to check the distance.</value>
    [InParam("stoleWallet")]
    [Help("Did the thief steal the wallet?")]
    public bool stoleWallet;

    public override bool Check()
    {
        if (stoleWallet)
        {
            ThiefTimer.startTime = 0.0f;
        }
        return stoleWallet;
    }
}