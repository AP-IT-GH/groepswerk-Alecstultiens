using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class AgentBehaviour : Agent
{
    public GameObject agentProjectile;
    public GameObject gun;

    public float rotationSpeed = 100f;
    public float timer = 10f;
    bool start = false;
    public float shootRate = 3f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float rotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
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
