using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public string origin;
    [SerializeField] private float moveSpeed;
    private bool hit;
    [SerializeField] private float timer;

    private GameController ctr;

    private void Awake()
    {
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void Update()
    {
        if (!ctr.paused) { movement(); }
        if (ctr.playerDead) { Destroy(this.gameObject); }
    }

    private void movement()
    {
        if (origin.Equals("Player"))
        {
            transform.position += new Vector3(0, moveSpeed, 0)*Time.deltaTime;
            if (this.transform.position.y  > 6)
            {
                Destroy(this.gameObject);
            }
        }
        if (origin.Equals("Enemy"))
        {
            transform.position -= new Vector3(0, moveSpeed, 0)*Time.deltaTime;
            if(this.transform.position.y < -6)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public string getOrigin()
    {
        return origin;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shot" && collision.gameObject.GetComponent<ShotController>().getOrigin().Equals("Player") || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
