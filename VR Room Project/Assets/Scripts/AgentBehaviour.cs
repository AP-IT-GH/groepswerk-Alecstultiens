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

    public void Awake()
    {
        agentInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public override void OnEpisodeBegin()
    {
        agentInstance = this;
        hitTarget = false;
        Debug.Log(hitTarget.ToString());
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
            AddReward(0.001f);
        }
        if (vectorAction[0] == 2)
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
            AddReward(0.001f);
        }
        if (vectorAction[1] == 1)
        {
            GameObject newProjectile = Instantiate(agentProjectile, gun.transform.position + transform.forward, transform.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.VelocityChange);
            start = true;
            timer = 0f;
            AddReward(-0.001f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateAgentReward(GetCumulativeReward());
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        if (hitTarget || GetCumulativeReward() < -3)
        {
            EndEpisode();
            Debug.Log("New episode");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float rotation = rotationSpeed * Time.deltaTime * -1;
            transform.Rotate(0, rotation, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject newProjectile = Instantiate(agentProjectile, gun.transform.position  + transform.forward , transform.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.VelocityChange);
            start = true;
            timer = 0f;
        }

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
