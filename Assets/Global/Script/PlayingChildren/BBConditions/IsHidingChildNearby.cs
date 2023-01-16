using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Children/IsHidingChildNearby")]

    public class IsHidingChildNearby : GOCondition
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        public override bool Check()
        {
            return !manager.IsCatcherNearbyHidingSpot();
        }

    }
}
