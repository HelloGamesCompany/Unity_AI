using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Children/GoToCenter")]

    public class GoToCenter : GOAction
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        public override void OnStart()
        {
            manager.MoveAllChildrenToCenter();
        }

        public override TaskStatus OnUpdate()
        {
            if (manager.AreChildrenInCenter())
                return TaskStatus.COMPLETED;
            return TaskStatus.RUNNING;
        }
    }
}

