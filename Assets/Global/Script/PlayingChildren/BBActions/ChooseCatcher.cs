using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Children/ChooseCatcher")]

    public class ChooseCather : GOAction
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        public override void OnStart()
        {
            manager.ChooseCatcher();
            manager.SetChildrenHidingSpots();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}