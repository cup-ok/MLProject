using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PathfinderAgent : Agent
{
    [SerializeField] private GameObject targetTransform;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private TrailRenderer trail;

    public override void OnEpisodeBegin()
    {
        // Reset and distribute objects randomly for each episode

        // Reset agent position and rotation
        transform.localPosition = new Vector3(Random.Range(5f, -5f), 0.5f, -16);
        transform.eulerAngles = Vector3.zero;

        // Randomise new reward position
        //targetTransform.transform.localPosition = new Vector3(Random.Range(16f, -16f), 0.5f, 16);

        // Randomise obstacle location
        obstacle.transform.localPosition = new Vector3(Random.Range(16f, -16f), 0.5f, 0);

        trail.Clear(); // Clear trail renderer to prevent artifact on reset
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.rotation); // Agent rotation
        sensor.AddObservation(transform.localPosition); // Agent position
        sensor.AddObservation(targetTransform.transform.position); // Goal/Reward position
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Store action variables for clarity
        float rotate = actions.ContinuousActions[0];
        float velocity = actions.ContinuousActions[1];

        float moveSpeed = 0.1f; // move speed multiplier
        transform.Rotate(0, rotate * 3, 0); // Rotate agent
        transform.localPosition += transform.forward * velocity * moveSpeed; // Apply velocity to agent

        //AddReward(-1f / MaxStep); // Negative reward to improve performance in later generations
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Create heuristic controls for player demonstrations
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Create negative reward and reset episode when colliding
        SetReward(-1f);
        EndEpisode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == targetTransform)
        {
            // If reaching the goal, add a positive reward
            SetReward(+1f);
            EndEpisode();
        }
    }
}
