using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioClip coinClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null)
        {
            controller.CoinCount();
            Destroy(gameObject);
            controller.PlaySound(coinClip);
        }
    }
}
