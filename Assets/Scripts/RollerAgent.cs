using System.Collections.Generic;
using UnityEngine;
using MLAgents;
//using MLAgents.Sensors;

public class RollerAgent : Agent
{
    //As you Wished
    Rigidbody rBody;
    public float speed = 10;
    public Transform Wall;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        // Move the target to a new spot
        // Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }
    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);
        AddVectorObs(Wall.position);

        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public override void AgentAction(float[] vectorAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        // Debug.Log("X:" + vectorAction[0]);
        // Debug.Log("Z:" + vectorAction[1]);
        rBody.AddForce(controlSignal * speed);
        rBody.transform.LookAt(Target);
        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            Debug.Log("Touch");
            animator.SetTrigger("Attack");
            SetReward(1.0f);
            Done();
        }
        else
        {
            animator.SetBool("Walk", true);
        }

        // Fell off platform
        if (this.transform.position.y < 0)
        {
            Done();
        }

    }
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}