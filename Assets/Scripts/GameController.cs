using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class GameController : MonoBehaviour
{
    [Header("DamageStats")]
    public int playerDMG, enemyDMG;
    [Header("EnemySpawning")]
    public float spawnTimer;
    private float tempTimer;
    private bool canSpawnEnemy;
    public int maxEnemy, enemyCount;
    public bool spawnFailed;
    public bool checkForSpawn;
    [Header("EnemyTypes")]
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject EnemyLv2;
    private PlayerController player;
    public bool playerDead;
    [Header("Statistics")]
    public int score;
    public int coins;
    [SerializeField] private int level;
    public bool increaseLevel;
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText, healthText, coinText;
    [SerializeField] private GameObject restartButton, startButton, continueButton, quitButton;
    private AudioManager a;
    public bool paused;
    private bool showMenu;
    [Header("ShopSystem")]
    public bool needToUnloadShop;
    [SerializeField] private GameObject shopKeeper;
    public GameObject nextLevelButton, healthItem;
    public bool shopActive;
    [Header("Dialoge")]
    [SerializeField] public GameObject dialogeObjects;
    
     
    
    void Start()
    {
        shopActive = false;
        // level = 1;
        canSpawnEnemy = true;
        playerDead = false;
        paused = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        a = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            paused = true;
            showMenu = true;
            continueButton.SetActive(true);
            quitButton.SetActive(true);
        }
        updateUI();
        if (!paused)
        {
            levelManager();
            enemySpawning();
             checkHealth();
        }
    }

    private void enemySpawning()
    {
        if (maxEnemy > enemyCount && canSpawnEnemy && !checkForSpawn)
        {
            if (level == 1)
            {
                Instantiate(Enemy, new Vector2(Random.Range(-7.5f, 7.5f), 6), Quaternion.identity);
                canSpawnEnemy = false;
                tempTimer = spawnTimer;
                enemyCount++;
            }
            if(level == 2)
            {
                Instantiate(EnemyLv2, new Vector2(Random.Range(-7.5f, 7.5f), 6), Quaternion.identity);
                canSpawnEnemy = false;
                tempTimer = spawnTimer;
                enemyCount++;
            }
        }
        if(canSpawnEnemy == false && maxEnemy > enemyCount)
        {
            tempTimer -= Time.deltaTime;
            if(tempTimer <= 0)
            {
                canSpawnEnemy = true;
            }
        }
    }

    private void checkHealth()
    {
        if(player.health <= 0)
        {
            playerDead = true;
            paused = true;
            restartButton.SetActive(true);
        }
    }

    private void updateUI()
    {
        healthText.SetText("health: " + player.health+"");
        scoreText.SetText("score: "+ score + "");
        coinText.SetText("coins: " + coins + "");
        if (!showMenu)
        {
            continueButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }

    private void levelManager()
    {
        switch (score)
        {
            case  20:
                level = 2;
                loadShop();
                paused = true;
                score++;
                break;
        }
    }

    private void loadShop()
    {
        
        destroyEnemy();
        destroyShot();
        Instantiate(shopKeeper, new Vector3(-9, 0), Quaternion.identity);
    }

    private void destroyEnemy()
    {
        GameObject[] e = new GameObject[enemyCount];
        e = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < e.Length; i++)
        {
            Destroy(e[i]);
        }
    }
    
    private void destroyShot()
    {
        GameObject[] e = new GameObject[GameObject.FindGameObjectsWithTag("Shot").Length];
        e = GameObject.FindGameObjectsWithTag("Shot");
        for (int i = 0; i < e.Length; i++)
        {
            Destroy(e[i]);
        }
    }

    public void restart()
    {
        
        destroyEnemy();
        destroyShot();
        player.health = player.startHealth;
        playerDead = false;
        paused = false;
        score = 0;
        enemyCount = 0;
        level = 1;
        
        
        restartButton.SetActive(false);
    }

    public void addHealth()
    {
        if(coins >= 5 && player.health < 100)
        {
            player.health += 10;
            coins -= 15;
            a.playSound("buyHealth");
        }
    }

    public void startGame()
    {
        paused = false;
        startButton.SetActive(false);
    }

    public void continuePressed()
    {
        paused = false;
        showMenu = false;
    }

    public void quitGame()
    {
        Application.Quit(0);
    }

    public void nextLevel()
    {
        shopActive = false;
        needToUnloadShop = true;
        healthItem.SetActive(false);
        nextLevelButton.SetActive(false);
        
    }
    
       
    
}
