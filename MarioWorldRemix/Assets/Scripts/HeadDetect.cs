using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetect : MonoBehaviour
{
    GameObject Enemy;

    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        GetComponent<Collider2D>().enabled = false;
        Enemy.GetComponent<SpriteRenderer>().flipY = true;
        Enemy.GetComponent<Collider2D>().enabled = false;
        Vector3 movement = new Vector3(Random.Range(40, 70), Random.Range(-40, 40), 0f);
        Enemy.transform.position += movement * Time.deltaTime;
        }
    }
}
