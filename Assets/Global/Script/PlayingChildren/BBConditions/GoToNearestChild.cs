using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Children/GoToNearestChild")]

    public class GoToNearestChild : GOAction
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        [OutParam("Catched Child")]
        public bool catchedChild = false;

        public override void OnStart()
        {
            manager.catcher.goingToPos = false;
            Debug.Log("Start NearestChild");
            Vector3 closestHidingChild = manager.GetNearestChildToCatcher();

            manager.MoveCatcherToRandomPos(closestHidingChild);
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log("update nearest child");
            if (manager.IsCatcherOnPos())
            {
                catchedChild = true;
                Debug.Log("Compelte nearest child");
                manager.catcher.goingToPos = false;
                return TaskStatus.COMPLETED;
            }
            return TaskStatus.RUNNING;
        }
    }
}

