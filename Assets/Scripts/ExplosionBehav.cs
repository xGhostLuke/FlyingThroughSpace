using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehav : MonoBehaviour
{
    [SerializeField] private Animator anim;
  
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("End")){
            Destroy(this.gameObject);
            Debug.Log("Destory!");
        }
    }
}
