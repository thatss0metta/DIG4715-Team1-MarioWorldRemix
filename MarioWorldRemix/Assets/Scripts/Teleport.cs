using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    // public Transform teleportTargetCam;
    public GameObject player;
    public AudioSource Level1;
    public AudioSource Level2;
    public AudioClip teleportClip;
    public Camera cam;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            Level1.Stop();
            player.transform.position = teleportTarget.transform.position;
            controller.PlaySound(teleportClip);
            Level2.Play();
        }
    }
}
