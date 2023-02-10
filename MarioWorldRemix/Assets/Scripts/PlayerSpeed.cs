using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public AudioClip powerUpClip;
    // Start is called before the first frame update
     void powerUp()
    {
        Player controller = GetComponent<Player>();

        if (controller != null)
        {
            controller.PlaySound(powerUpClip);

            controller.maxSpeed = 20;
            controller.moveSpeed = 20;
            StartCoroutine(changeSpeed(controller));
        }
    }

    IEnumerator changeSpeed(Player controller)
    {
        yield return new WaitForSeconds(2f);
        controller.PlaySound(powerUpClip);
    }
}
