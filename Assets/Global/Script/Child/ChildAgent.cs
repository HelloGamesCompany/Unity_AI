using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class ChildAgent : Agent
{
    private List<Transform> obstacles = new List<Transform>();

    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

        foreach (var g in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obstacles.Add(g.GetComponent<Transform>());
        }

        Debug.Log("obstacles parameters: " + obstacles.Count * 3);
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (transform.position.y < 0)
        {
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 1.5f, 0);
        }

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
        rBody.AddForce(controlSignal * forceMultiplier);

        Debug.Log("velocity: " + controlSignal);

        // Rewards
        float distanceToTarget = Vector3.Distance(transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            // Debug.Log("Get reward");
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
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Debug.Log("faild");
            AddReward(-0.005f);
            // EndEpisode();
        }
    }
}
