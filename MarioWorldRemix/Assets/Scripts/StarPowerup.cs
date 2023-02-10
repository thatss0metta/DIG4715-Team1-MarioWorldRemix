using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerup : MonoBehaviour
{

    public AudioClip powerUpClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null) 
        {
            controller.InvinPowerup();
            Destroy(gameObject);
            controller.PlaySound(powerUpClip);
        }
    }
}
