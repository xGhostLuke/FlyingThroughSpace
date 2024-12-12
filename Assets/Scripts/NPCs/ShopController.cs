using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private GameController ctr;
    [SerializeField] private float moveSpeed;
    private bool finishedMovement;

    private void Awake()
    {
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        finishedMovement = false;
    }

    private void Update()
    {
        movement();
    }

    private void movement()
    {
        if(transform.position.x < 0)
        {
            transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;
        }
        if(transform.position.x  > -0.2)
        {
            finishedMovement = true;
            ctr.shopActive = true;
            ctr.dialogeObjects.SetActive(true);
        }
        if(finishedMovement)
        {
            ctr.healthItem.SetActive(true);
            ctr.nextLevelButton.SetActive(true);
        }
        if (ctr.needToUnloadShop)
        {
            ctr.dialogeObjects.SetActive(false);
            finishedMovement = false;
            ctr.healthItem.SetActive(false);
            ctr.nextLevelButton.SetActive(false);
            transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;
            if (transform.position.x > 10)
            {
                ctr.paused = false;
                ctr.needToUnloadShop = false;
                Destroy(this.gameObject); 
               
                
            }
        }
    }

    //private void AnimPlay...
}
