using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int direction = -1;
    private Vector3 movement;
    public SpriteRenderer enemy;


    void Update()
    {
        movement = new Vector3(2 * direction, 0f, 0f);
        transform.position = transform.position + movement * Time.deltaTime;
        if (transform.position.y < -14)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        direction = direction * -1;

        if ((collision.gameObject.tag == "Player") && (!player.StarPowerup))
        {
           player.Death();
        }

        if (direction == 1)
            enemy.flipX = false;
        else
            enemy.flipX = true;
    }
}
