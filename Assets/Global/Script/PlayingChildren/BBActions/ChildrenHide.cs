using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Children/ChildrenHide")]

    public class ChildrenHide : GOAction
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        public override void OnStart()
        {
            manager.Hide();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
