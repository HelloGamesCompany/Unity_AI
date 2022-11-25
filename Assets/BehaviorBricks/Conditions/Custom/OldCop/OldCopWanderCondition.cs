using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/WanderState")]
[Help("No help!!!")]
public class OldCopWanderCondition : ConditionBase
{
    [InParam("OldCopController")]
    public OldcopController oldCop = null;
    public override bool Check()
    {
        if (oldCop)
        {
            return !(oldCop.seeBench && oldCop.canSit);
        }
        return false;
    }
}