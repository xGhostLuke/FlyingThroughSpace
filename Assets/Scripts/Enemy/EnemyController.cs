using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected GameController ctr;
    [SerializeField] private int health;
    [SerializeField] protected float shootTimer, tempTimer;
    protected bool canShoot;
    [SerializeField] protected GameObject firepoint, shot;
    [SerializeField] private int ySpawn;

    [SerializeField] private float spawnTimer;
    [SerializeField] private bool spawnedNew;
    [SerializeField] private BoxCollider2D CheckBox;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject shotSpeedItem;

    private bool needMove;
    [SerializeField] private float moveTime;

    [SerializeField] private GameObject expl;

    protected AudioManager a;
    

   
    
    void Awake()
    {
        spawnTimer = 0.1f;
        canShoot = false;
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        a = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        tempTimer = shootTimer;
        spawnedNew = true;
        ctr.checkForSpawn = true;
        needMove = true;
        checkForNewSpawn();
    }

    void Update()
    {
        if (!ctr.paused)
        {
            checkHP();
            shoot();
            move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && spawnedNew)
        {
           
            ctr.enemyCount--;
            ctr.checkForSpawn = false;
            Destroy(this.gameObject);
            Debug.Log("Failed to Spawn");
        }
        if (collision.gameObject.tag.Equals("Shot") && collision.gameObject.GetComponent<ShotController>().getOrigin().Equals("Player"))
        {
            health -= ctr.playerDMG;
        }
        
    }

    private void move()
    {
        moveTime -= Time.deltaTime;
        if (needMove) { transform.position += new Vector3(0, -moveSpeed) * Time.deltaTime; }
        if(moveTime <= 0) { needMove = false; }
    }

    protected virtual void shoot()
    {
        if (canShoot)
        {
            tempTimer = shootTimer + Random.Range(0, 2);
            canShoot = false;
            Instantiate(shot, firepoint.transform.position, Quaternion.identity).GetComponent<ShotController>().origin = "Enemy";
            a.playSound("shot");
        }
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            shootTimer = tempTimer;
            canShoot = true;
        }
    }

    private void checkForNewSpawn()
    {
        while (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
            if(spawnTimer <= 0)
            {
                spawnedNew = false;
                ctr.checkForSpawn = false;
            }
    }

    private void checkHP()
    {
        if (health <= 0)
        {
            ctr.score++;
            ctr.enemyCount--;
            Instantiate(coin, new Vector3(this.transform.position.x, transform.position.y), Quaternion.identity).GetComponent<CoinController>().value = Random.Range(1, 4);
            Instantiate(expl, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            int rand = Random.Range(1, 11);
            if(rand <= 1)
            {
                Instantiate(shotSpeedItem, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        if (ctr.playerDead)
        {
            Destroy(this.gameObject);
        }
    }
   




}
