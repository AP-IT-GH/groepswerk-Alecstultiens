using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("HIIIT");
            
            if(gameObject.tag == "Target")
            {
                Debug.Log("HIT TARGET");
            }
            if (gameObject.tag == "DontHitTarget")
            {
                Debug.Log("HIT WRONG TARGET");
            }


        }
    }
}
