using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCatch : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null)
        {
            controller.Death();
        }
    }
}
