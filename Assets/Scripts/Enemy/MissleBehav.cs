using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleBehav : MonoBehaviour
{
    private GameController ctr;
    private GameObject player;

    private float timer, t;
    [SerializeField] private float moveSpeed;

    private Vector3 vec;

    private void Awake()
    {
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        movement();
    }

    private void movement()
    {
        vec = new Vector3(player.transform.position.x - this.transform.position.x + UnityEngine.Random.Range(0, 5), player.transform.position.y - 10);
        this.transform.position += vec.normalized * moveSpeed / 1000;
        if(transform.position.y <= -6) { Destroy(this.gameObject); }
        transform.LookAt(player.transform);
    }   
}
