using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    
    Backpack BkPak;
   

    public Text coinText;
    public Text gemText;
    public Text rainbowGemText;public Text LevelText;
    public Text bronzeKeyText;
    public Text silverKeyText;

    

    public int numToSubtract = 0;
    public bool bSubtract = false;
    public bool bTutorial;
    // Use this for initialization
    void Start () {
        BkPak = GameObject.FindObjectOfType<Backpack>();
    }
	
	// Update is called once per frameo
	void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        



        //COIN DISPLAY
        //coinText.text = BkPak.coinPurse.ToString();
        if (bSubtract)
        {
            coinText.text = (BkPak.Currency[0].Qty - numToSubtract).ToString();
        } else
        {
            coinText.text = BkPak.Currency[0].Qty.ToString();
        }
        
        gemText.text = BkPak.gemPurse.ToString();
        rainbowGemText.text = BkPak.rainbowGemPurse.ToString();
        bronzeKeyText.text = BkPak.bronzeKeyStock.ToString();
        silverKeyText.text = BkPak.silverKeyStock.ToString();
    }

    public void TutorialToggle()
    {
        if(LevelText.ToString() == "Tutorial")
        {
            BkPak.bTutorial = true;
        }

    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void ExitGame()
    {
        Debug.Log("QUITTING");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(2);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(1);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    public void LoadTradeScene()
    {
        SceneManager.LoadScene(4);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void LoadStoreScene()
    {
        SceneManager.LoadScene(5);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }



    public void LoadTreasureScene()
    {
        SceneManager.LoadScene(3);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }
}
