using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CoinsText;
    public GameObject HealthText;

    public GameObject TimerText;

    [SerializeField] private TextMeshProUGUI coinInfo;
    [SerializeField] private TextMeshProUGUI healthInfo;

    public GameObject playerPrefab;
    private Vector3 startingPosition;
   public  int coins;
   public int health;

    public float GameTime;

    public GameObject player;

    void Start()
    {
        health = 3;
        coins = 0;
        GameTime = 0;
        coinInfo = CoinsText.GetComponent<TextMeshProUGUI>();
        healthInfo = HealthText.GetComponent<TextMeshProUGUI>();

        startingPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       coinInfo.text = "Coins: " + coins;
        healthInfo.text = "Lives: " + health;

        //if(health <= 0)
        //{
        //    Time.timeScale = 0;
        //    Debug.Log("Game Over");
        //}

        GameTime = GameTime + Time.deltaTime;
        //TimerText.GetComponent<Text>().text = "Time:" + GameTime;
    }


    public void ReSpawn()
    {
        Instantiate(playerPrefab, startingPosition, Quaternion.identity);
    }

    public void Jump()
    {
        player.GetComponent<Player>().Jump();
    }

    public void Shoot()
    {
        player.GetComponent<Player>().FiringBullets();
    }
}
