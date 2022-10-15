using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFlock : MonoBehaviour
{
    [HideInInspector]
    public BeeManager manager;

    [HideInInspector]
    public float movementSpeed = 5.0f;

    [HideInInspector]
    public float rotationSpeed = 5.0f;

    [HideInInspector]
    public Vector3 direction;

    // Start is called before the first frame update
    private void Move()
    {
        // Rotate to correct direccion
        transform.rotation = Quaternion.Slerp(transform.rotation,
                  Quaternion.LookRotation(direction),
                  rotationSpeed * Time.deltaTime);

        // Move
        transform.Translate(0.0f, 0.0f, Time.deltaTime * movementSpeed);
    }

    public void CalculateDir()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 leaderDir = manager.beeLeader != null ? manager.beeLeader.transform.position - transform.position : Vector3.zero;
        
        int neighbours = 0;

        foreach (GameObject go in manager.beeList)
        {
            // if isn't me
            if (go != gameObject && go.activeSelf)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= manager.neighbourDistance)
                {
                    cohesion += go.transform.position;

                    align += go.GetComponent<BeeFlock>().direction;

                    separation -= (transform.position - go.transform.position) / Mathf.Pow(distance, 2);

                    neighbours++;
                }
            }
        }
        if (neighbours > 0)
        {
            cohesion = (cohesion / neighbours - transform.position).normalized;

            align /= neighbours;
        }

        direction = (cohesion + align + separation + leaderDir).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
