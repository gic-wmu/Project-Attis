using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDriver : MonoBehaviour {
    public int health = 10;
    public int score = 0;
    public int money = 0;
   
    Text healthText;
    Text moneyText;
    Text scoreText;
    // Use this for initialization
    void Start () {
	    healthText = GameObject.Find("healthText").GetComponent<Text>();
        moneyText = GameObject.Find("moneyText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!healthText.text.Equals(health))
        {
            healthText.text = health.ToString();
        }
        if (!moneyText.text.Equals(money))
        {
            moneyText.text = money.ToString();
        }
        if (!scoreText.text.Equals(score))
        {
            scoreText.text = score.ToString();
        }

    }
}
