using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLv2 : EnemyController
{
   
    
    [SerializeField] private int shotCount = 1;
    [SerializeField] private float div;
    [SerializeField] private float iniTimer;



    protected override void shoot()
    {
        if (canShoot)
        {

            Instantiate(shot, new Vector3(firepoint.transform.position.x - 0.2f, firepoint.transform.position.y), Quaternion.identity).GetComponent<ShotController>().origin = "Enemy";
            Instantiate(shot, new Vector3(firepoint.transform.position.x + 0.2f, firepoint.transform.position.y), Quaternion.identity).GetComponent<ShotController>().origin = "Enemy";
            a.playSound("shot");
           
            canShoot = false;
            
        }
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            if (shotCount == 1)
            {
                shotCount++;
                shootTimer = iniTimer / div;
            }
            else
            {
                shootTimer = iniTimer;
                shotCount = 1;
            }
            canShoot = true;
        }
    }
}
