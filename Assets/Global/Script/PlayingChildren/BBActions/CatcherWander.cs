using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Children/CatcherWander")]

    public class CatcherWander : GOAction
    {
        [InParam("ChildrenManager")]
        public ChildrenManager manager;

        [InParam("Area")]
        public GameObject area;

        public override TaskStatus OnUpdate()
        { 
            if (manager.IsCatcherOnPos() && manager.catcher.goingToPos)
            {
                manager.catcher.goingToPos = false;
                return TaskStatus.COMPLETED;
            }

            if (!manager.IsCatcherGoingToPos())
                manager.MoveCatcherToRandomPos(getRandomPosition());
            return TaskStatus.RUNNING;
        }

        private Vector3 getRandomPosition()
        {
            BoxCollider boxCollider = area != null ? area.GetComponent<BoxCollider>() : null;
            if (boxCollider != null)
            {
                return new Vector3(UnityEngine.Random.Range(area.transform.position.x - area.transform.localScale.x * boxCollider.size.x * 0.5f,
                                                            area.transform.position.x + area.transform.localScale.x * boxCollider.size.x * 0.5f),
                                   area.transform.position.y,
                                   UnityEngine.Random.Range(area.transform.position.z - area.transform.localScale.z * boxCollider.size.z * 0.5f,
                                                            area.transform.position.z + area.transform.localScale.z * boxCollider.size.z * 0.5f));
            }
            else
            {
                SphereCollider sphereCollider = area != null ? area.GetComponent<SphereCollider>() : null;
                if (sphereCollider != null)
                {
                    float distance = UnityEngine.Random.Range(-sphereCollider.radius, area.transform.localScale.x * sphereCollider.radius);
                    float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                    return new Vector3(area.transform.position.x + distance * Mathf.Cos(angle),
                                       area.transform.position.y,
                                       area.transform.position.z + distance * Mathf.Sin(angle));
                }
                else
                {
                    return gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
                }
            }
        }
    }
}
