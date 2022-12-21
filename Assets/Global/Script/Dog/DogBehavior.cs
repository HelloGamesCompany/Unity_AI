using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehavior : MonoBehaviour
{
    [SerializeField]
    private Target _currentTarget;

    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    [SerializeField]
    private GameObject child;

    private bool waitingForChild = false;
    private bool onTarget = false;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(child.transform.position, transform.position) <= 2.5f)
        {
            if (onTarget)
            {
                Debug.Log("Child Collision");
                waitingForChild = false;
            }
        }

        if (waitingForChild)
            return;

        onTarget = false;

        Vector3 dir = (_currentTarget.transform.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= stoppingDistance)
        {
            _currentTarget = _currentTarget.GetNextTarget(_currentTarget);
            waitingForChild = true;
            onTarget = true;
        }
    }
}
