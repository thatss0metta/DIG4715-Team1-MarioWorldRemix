using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCatch : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null)
        {
            controller.Death();
        }
    }
}
