using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    private float axisX, axisY;

    public float speed = 1.0f;

    public float radius = 2.0f, offset = 0.2f;

    public GameObject target;

    Vector3 targetPos;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        //InvokeRepeating("Wander", 1, 5);
    }

    void Seek()
    {
        agent.destination = target.transform.position;

        if (Vector3.Distance(transform.position, target.transform.position) < radius)
        {
            //target.GetComponent<MMController>().ChangeState();

            Target t;

            if(target.TryGetComponent<Target>(out t))
            {
                target = target.GetComponent<Target>().nextTarget;
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Seek();
        //Wander();

        //UnityEngine.Random.insideUnitCircle
        //axisX = Input.GetAxis("Horizontal");
        //axisY = Input.GetAxis("Vertical");

        //transform.transform.position += (new Vector3(axisX, 0, axisY) * speed * Time.deltaTime);
    }

    public void DetectExTarget(GameObject target)
    {
        this.target = target;
    }

    void Wander()
    {
        Debug.Log("a");
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 targetPos = transform.TransformPoint(localTarget);
        targetPos.y = 0f;

        agent.destination = targetPos;

        target.transform.position = targetPos;
    }
}
