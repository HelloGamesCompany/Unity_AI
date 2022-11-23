using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("My Conditions/IsTargetWithTagInSight")]
    [Help("Checks if the closest game object with a given tag is in sight depending on a given angle")]
    public class IsTargetWithTagInSight : GOCondition
    {
        ///<value>Input Target Parameter to check the angle.</value>
        [InParam("tag")]
        [Help("tag to check the angle")]
        public string tag;

        ///<value>Input view angle parameter to consider that the target is in sight.</value>
        [InParam("angle")]
        [Help("The view angle to consider that the target is in sight")]
        public float angle;

        [OutParam("target")]
        [Help("Detected target to be used later on")]
        public GameObject tagTarget;

        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;

        /// <summary>
        /// Checks whether a target is in sight depending on a given angle, casting a raycast to the target and then compare the angle of forward vector
        /// with de raycast direction.
        /// </summary>
        /// <returns>True if the angle of forward vector with the  raycast direction is lower than the given angle.</returns>
        public override bool Check()
        {
            Debug.Log("Check target in sight");
            // Get every game object with teh given tag
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            if (targets.Length == 0)
                return false; // If there are no game objects with the given target, return false.

            // Find closest target to the agent.
            GameObject closestTarget = targets[0];
            float closestDistance = float.MaxValue;
            foreach (var target in targets)
            {
                float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            // Check if closest target is close enough
            Vector3 dir = (closestTarget.transform.position - gameObject.transform.position);
            if (dir.sqrMagnitude > closeDistance * closeDistance)
                return false;

            // Check if the closest target is within sight
            RaycastHit hit;
            Ray ray = new Ray();
            ray.direction = dir;
            ray.origin = gameObject.transform.position;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 7, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.gameObject == closestTarget && Vector3.Angle(dir, gameObject.transform.forward) < angle * 0.5f)
                {
                    tagTarget = closestTarget;
                    return true;
                }
            }

            return false;
        }
    }
}