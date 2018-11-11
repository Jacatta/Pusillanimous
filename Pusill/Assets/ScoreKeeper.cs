using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {


   // public int Score;
    public int streakInt;
    public Text streak;

    public int scoreInt;
    public Text score;

    public int gainInt;
    public Text gain;

    public int coinInt;
    public Text coin;

    public int MultiplierInt;
    public Text Multiplier;
    // Use this for initialization
    void Start () {
        scoreInt = 0;
        coinInt = 0;

}
	
	// Update is called once per frame
	void Update () {
       // streak.text = streakInt.ToString();

        score.text = scoreInt.ToString();

      //s  Multiplier.text ='X'+ MultiplierInt.ToString();

       // gain.text = gainInt.ToString();



    }
    //Streak.text = Streakint.ToString();
}
