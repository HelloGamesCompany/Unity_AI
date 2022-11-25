using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/RecentlyStoleWallet")]
[Help("Checks whether a wallet was stolen recently.")]
public class RecentlyStoleWallet : ConditionBase
{ 
    public override bool Check()
    {
        if (ThiefTimer.startTime == 0.0f)
        {
            ThiefTimer.startTime = Time.time;
            return true;
        }
        float timeDifference = Time.time - ThiefTimer.startTime;

        if (timeDifference > 5.0f)
        {
            return false;
        }
        return true;
    }
}