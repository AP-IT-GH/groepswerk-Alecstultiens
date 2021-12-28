using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;


    float timer = 10f;
    bool start = false;
    public float shootRate = 3f;

    // Update is called once per frame
    void Update()
    {



        float shoot = Input.GetAxis("Fire1");


        if (shoot == 1 && timer >= shootRate)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation);
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
