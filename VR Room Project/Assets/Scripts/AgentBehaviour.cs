using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentBehaviour : Agent
{
    public static AgentBehaviour agentInstance;
    public GameObject agentProjectile;
    public GameObject gun;

    public float rotationSpeed = 100f;
    public float timer = 10f;
    bool start = false;
    public float shootRate = 3f;
    public bool hitTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public override void OnEpisodeBegin()
    {
        agentInstance = this;
        hitTarget = false; 
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(hitTarget);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var vectorAction = actions.DiscreteActions;

        if (vectorAction[0] == 0)
        {
            AddReward(-0.001f);
        }
        if (vectorAction[0] == 1)
        {
            float rotation = rotationSpeed * Time.deltaTime * -1;
            transform.Rotate(0, rotation, 0);
        }
        if (vectorAction[0] == 2)
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }
        if (vectorAction[1] == 1)
        {

            Transform targetTransform = this.transform.parent.transform;

            if (timer >= shootRate)
            {
                GameObject newProjectile = Instantiate(agentProjectile, gun.transform.position + transform.forward, transform.rotation);
                newProjectile.transform.parent = targetTransform;
                newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.VelocityChange);

                start = true;
                timer = 0f;
            }



            var layerMask = 1 << LayerMask.NameToLayer("Targets");
            if (Physics.Raycast(gun.transform.position, transform.forward, out var hit, 20f, layerMask))
            {
                if (hit.transform.tag == "Target")
                {
                    hitTarget = true;
                    AddReward(1f);
                    EndEpisode();
                }
                if (hit.transform.tag == "Dht")
                {
                    hitTarget = false;
                    AddReward(-0.003f);
                }
            }
            else
            {
                hitTarget = false;
                AddReward(-0.005f);
            }

        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.DiscreteActions;
        continuousActionsOut[0] = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        continuousActionsOut[0] = Input.GetKey(KeyCode.RightArrow) ? 2 : 0;
        continuousActionsOut[1] = Input.GetKey(KeyCode.LeftShift) ? 1 : 0; 
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (timer < shootRate)
                timer += Time.deltaTime;
            else
            {
                timer = shootRate;
                start = false;
            }
        }
    }
}
