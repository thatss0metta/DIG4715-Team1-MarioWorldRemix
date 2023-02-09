using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public int value;

    void OntriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinCounter.instance.IncreaseCoins(value);
        }
    }

}
