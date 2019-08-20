using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public Text levelTxt;
    public int CurrentLvl;
	// Use this for initialization
	void Start () {
        CurrentLvl = 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextLevel()
    {
        CurrentLvl++;
        levelTxt.text = ("LEVEL" + CurrentLvl.ToString());
    }


    public void previousLevel()
    {
        if (CurrentLvl == 0) return;
        CurrentLvl--;
        if(CurrentLvl == 0)
        {
            levelTxt.text = ("Tutorial");
        }
        else
        {
            levelTxt.text = ("LEVEL" + CurrentLvl.ToString());
        }
        
    }
}
