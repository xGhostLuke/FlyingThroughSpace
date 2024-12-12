using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTimerUpgrade : ItemController
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")){
            player.upgrade("ShotTimer");
            Destroy(this.gameObject);
        }
    }
}
