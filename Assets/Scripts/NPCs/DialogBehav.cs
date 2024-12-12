using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBehav : MonoBehaviour
{
    [SerializeField] private string[] sent;
   
    private string t;
    [SerializeField] private int sentNumb; 
    [SerializeField] private TMP_Text text;
    [SerializeField] private float typeSpeed;
    private bool loadText;
    private bool loadStatus;

    private GameController ctr;


    private void Awake()
    {
        loadStatus = false;
        sentNumb = 0;
        loadText = false;
        ctr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        if(ctr.shopActive && !loadStatus)
        {
            text = GameObject.FindGameObjectWithTag("NPCText").GetComponent<TMP_Text>();
            
            loadStatus = true;
        }

        if (ctr.shopActive && !loadText && loadStatus)
        {
            loadText = true;   
            StartCoroutine(typing());
            text.SetText(t);
                       
           
        }
        if (loadText)
        {
            nextSent();
        }
    }

    IEnumerator typing()
    {
        foreach (char c in sent[sentNumb].ToCharArray())
        {
            t += c.ToString();
            text.SetText(t);
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    private void nextSent()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sentNumb < sent.Length-1)
        {
            Debug.Log("NEXT");
            t = "";
            sentNumb++;
            StartCoroutine (typing());

        }    
    }
}
