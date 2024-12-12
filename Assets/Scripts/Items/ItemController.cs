using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    [SerializeField] private int moveSpeed;
    protected PlayerController player;
    protected GameController ctr;
    [SerializeField] private float timer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int force;
    private int temp, forceX, forceY;
    private bool needsBoost;

    protected AudioManager a;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        a = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        temp = Random.Range(1, 3);
        forceX = Random.Range(1, force);
        forceY = Random.Range(1, force);
        boost();
    }

    private void Update()
    {
        if (ctr.paused)
        {
            rb.Sleep();
            needsBoost = true;
           
        }
        if (!ctr.paused)
        {
            rb.WakeUp();
            if (needsBoost)
            {
                boost();
            }
            needsBoost = false;
            
            if (this.transform.position.y < -6)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void boost()
    {
        if (temp == 1) { rb.AddForce(new Vector2(forceX, 0)); }
        else { rb.AddForce(new Vector2(forceX, 0)); }
    }

}
