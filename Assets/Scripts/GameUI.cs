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

    [SerializeField] private TextMeshProUGUI coinInfo;
    [SerializeField] private TextMeshProUGUI healthInfo;

   public  int coins;
   public int health;

    void Start()
    {
        health = 10;
        coins = 0;

        coinInfo = CoinsText.GetComponent<TextMeshProUGUI>();
        healthInfo = HealthText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       coinInfo.text = "Coins: " + coins;
        healthInfo.text = "Health: " + health;

        if(health < 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}
