using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField]private GameObject parent;
    private GameController ctr;


    private void Awake()
    {
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            ctr.enemyCount--;
            Destroy(this.parent);
        }
    }
}
