using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : ItemController
{
    public int value;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            a.playSound("coin");
            ctr.coins += value;
            Destroy(this.gameObject);
        }
    }
}
