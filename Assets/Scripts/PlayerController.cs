using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public int health;
    public int startHealth;
    [SerializeField]private int moveSpeed;
    

    [SerializeField]private GameObject shot;
    [SerializeField]private GameObject firepoint;
    private AudioManager a;

    private GameController ctr;

    public float shootCD;
    [SerializeField]private bool canShoot;
    [SerializeField]private float tempCD;

    [SerializeField] private int maxX;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        canShoot = true;
        a = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (!ctr.paused)
        {
            movement();
            shoot();
        }
    }

    private void movement()
    {
        float playerInputX = Input.GetAxisRaw("Horizontal");
        
        
            if(playerInputX < 0 && transform.position.x > -maxX)
            {
                transform.position += new Vector3(playerInputX, 0).normalized*Time.deltaTime*moveSpeed;
            }
            if (playerInputX > 0 && transform.position.x < maxX)
            {
                transform.position += new Vector3(playerInputX, 0).normalized * Time.deltaTime * moveSpeed;
            }  
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            tempCD = shootCD;
            Instantiate(shot, firepoint.transform.position, Quaternion.identity).GetComponent<ShotController>().origin = "Player";
            a.playSound("shot");
            canShoot = false;
        }
        if (!canShoot)
        {  
            tempCD -= Time.deltaTime;
            if(tempCD <= 0)
            {
                canShoot = true;
            }
        }
    }

    public void upgrade(string type)
    {
        switch (type)
        {
            case "ShotTimer":
                shootCD -= 0.1f;
                Debug.Log("ShotTimer reduced");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Shot"))
        {
            health -= ctr.enemyDMG;
        }
    }



    


}
