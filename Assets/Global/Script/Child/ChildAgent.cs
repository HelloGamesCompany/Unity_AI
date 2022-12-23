using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class ChildAgent : Agent
{
    private List<Transform> obstacles = new List<Transform>();

    bool dead = false;

    float distance = 0;

    //Rigidbody rBody;
    void Start()
    {
        //rBody = GetComponent<Rigidbody>();

        foreach (var g in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obstacles.Add(g.GetComponent<Transform>());
        }

        Debug.Log("obstacles parameters: " + obstacles.Count * 3);

        InvokeRepeating(nameof(CheckDistance), 0, 1.0f);
    }

    void CheckDistance()
    {
        float newDistance = Vector3.Distance(transform.position, Target.transform.position);

        if (newDistance < distance)
        {
            Debug.Log("Reward for distance" + distance);
            AddReward(0.02f * (1 / distance));
        }

        distance = newDistance;
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        if (dead)
        {
            transform.localPosition = new Vector3(0, 1.5f, 0);
            dead = false;
        }

        // If the Agent fell, zero its momentum
        //if (transform.position.y < 1.0f)
        //{
        //    //rBody.angularVelocity = Vector3.zero;
        //    //rBody.velocity = Vector3.zero;
        //    transform.localPosition = new Vector3(0, 1.5f, 0);
        //}

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value * 20 - 10,
                                           1.5f,
                                           Random.value * 20 - 10);
    }

    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        //rBody.AddForce(controlSignal * forceMultiplier);

        //transform.Translate(controlSignal * forceMultiplier);

        transform.position += controlSignal * Time.deltaTime * forceMultiplier;

        //Debug.Log("velocity: " + controlSignal);

        // Rewards
        float distanceToTarget = Vector3.Distance(transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.4f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform
        else if (transform.position.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(transform.localPosition);

        // Agent velocity
        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            Debug.Log("COL with border");
            dead = true;
            SetReward(-1.0f);
            EndEpisode();
        }
        else if (other.CompareTag("Obstacle"))
        {
            //Debug.Log("Chock with obstacle");
            AddReward(-0.1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            //Debug.Log("Stay chock with obstacle");
            AddReward(-0.0005f);
        }
    }
}
