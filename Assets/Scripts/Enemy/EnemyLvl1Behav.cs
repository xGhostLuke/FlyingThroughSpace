using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLvl1Behav : EnemyController
{

    protected override void shoot()
    {
        if (canShoot)
        {
            tempTimer = shootTimer + Random.Range(0, 2);
            canShoot = false;
            Instantiate(shot, firepoint.transform.position, Quaternion.identity).GetComponent<ShotController>().origin = "Enemy";
            a.playSound("shot");
        }
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = tempTimer;
            canShoot = true;
        }
    }
}
