using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperPowerup : MonoBehaviour
{
    public AudioClip powerUpClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null)
        {
            Destroy(gameObject);
            controller.powerUp();
        }
    }
}
