using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFlock : MonoBehaviour
{
    [HideInInspector]
    public BeeManager manager;

    public float speed = 1.0f;

    private float movementSpeed = 5.0f;

    public bool followLeader = false;

    [HideInInspector]
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = manager.leaderSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.beeLeader == this.gameObject) return;
           
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 align = Vector3.zero;

        int neighbour = 0;
        foreach (GameObject go in manager.beeList)
        {
            // if isn't me
            if (go != gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= manager.neighbourDistance)
                {
                    cohesion += go.transform.position;
        
                    align += go.GetComponent<BeeFlock>().direction;

                    separation -= (transform.position - go.transform.position) / Mathf.Pow(distance,2);

                    neighbour++;
                }
            }
        }
        if (neighbour > 0)
        {
            cohesion = speed * (cohesion / neighbour - transform.position).normalized;

            align /= neighbour;
            speed = Mathf.Clamp(align.magnitude, manager.minSpeed, manager.maxSpeed);
        }

        //Debug.Log("cohesion:" + cohesion);
        //Debug.Log("align:" + align);
        //Debug.Log("separation:" + separation);

        Vector3 leaderDir = manager.beeLeader.transform.position - this.transform.position;

        if (Vector3.Distance(manager.beeLeader.transform.position, this.transform.position) > manager.radiusFromLeader)
        {
            direction = leaderDir.normalized * speed;

            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            direction = (cohesion + align + separation + leaderDir).normalized * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(leaderDir),
                                     manager.rotationSpeed * Time.deltaTime);
        }

        transform.Translate(0.0f, 0.0f, Time.deltaTime * movementSpeed);
    }
}
