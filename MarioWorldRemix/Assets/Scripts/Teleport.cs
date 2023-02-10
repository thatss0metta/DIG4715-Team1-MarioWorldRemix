using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    public AudioSource Level1;
    public AudioSource Level2;
    public AudioClip teleportClip;
    public Camera cam;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();
        if (controller != null)
        {
            Level1.Stop();
            player.transform.position = teleportTarget.transform.position;
            cam.transform.position = new Vector3(11.8f, 20.9f, -10f);
            controller.PlaySound(teleportClip);
            Level2.Play();
        }
    }
}
