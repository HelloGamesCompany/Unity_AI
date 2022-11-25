using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("MyConditions/GoBenchState")]
[Help("No help!!!")]
public class OldCopGoBenchCondition : ConditionBase
{
    [InParam("OldCopController")]
    public OldcopController oldCop = null;

    [OutParam("bench")]
    public GameObject bench;

    public override bool Check()
    {
        bench = oldCop.bench.gameObject;
        if (oldCop)
        {
            if (oldCop.CanISit())
            {
                return false;
            }
        }
        return true;
    }
}
