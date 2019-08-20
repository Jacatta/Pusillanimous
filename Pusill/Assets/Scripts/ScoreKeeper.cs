using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {


    BackgroundBehaviors BB;
    GameManager GM;
    Backpack BkPak;

    // public int Score;
   // public int speedInt;
    public Text speed;

    public int maxStreak;
    public int streak;
    public Text streakString;

    public int score;
    public Text scoreString;

    public int gain;
    public Text gainString;

    public int coin;
    public Text coinString;

    public int MultiplierInt;
    public Text Multiplier;
    // Use this for initialization
    void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = FindObjectOfType<GameManager>();
        BkPak = GameObject.FindObjectOfType<Backpack>();
        score = 0;
        coin = 0;
        maxStreak = 0;
        streak = 0;
    }
	
	// Update is called once per frame
	void Update () {
       // streak.text = streakInt.ToString();

       // score.text = scoreInt.ToString();
        GM.CoinText.text = coin.ToString();
        if(GM.underTheSea)// While under water: this will show exit speed
        speed.text = Mathf.Round(GM.sceneSpeed).ToString();

        //s  Multiplier.text ='X'+ MultiplierInt.ToString();

        // gain.text = gainInt.ToString();

        streakString.text = streak.ToString();

    }

}
