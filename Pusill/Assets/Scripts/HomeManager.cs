using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wardrobing;

public class HomeManager : MonoBehaviour {

    Backpack BkPak;

    Wardrobe WrdRb;
    public Text coinText;
    public Text gemText;
    public Text rainbowGemText;
    public Text LevelText;
    public Text bronzeKeyText;
    public Text silverKeyText;

    public GameObject Wardrobe_Head;
    public GameObject Wardrobe_Body;
    public GameObject Wardrobe_Misc;

    public AudioSource homeAudio;

    public Canvas OptionsCanvas;


    public int numToSubtract = 0;
    public bool bSubtract = false;
    public bool bTutorial;
    // Use this for initialization
    void Start() {
        BkPak = GameObject.FindObjectOfType<Backpack>();
        //it is  WrdRb = new Wardrobe();
        OptionsCanvas = GameObject.Find("Canvas_Options").GetComponent<Canvas>();

        WrdRb = BkPak.WrdRb;

        WrdRb.Wardrobe_HeadGear = Wardrobe_Head.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_BodyGear = Wardrobe_Body.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_MiscGear = Wardrobe_Misc.GetComponentsInChildren<Transform>();

        WrdRb.UnSet_Gear();//Turn off all atire

        WrdRb.PutOn_Outfit(WrdRb.CurrentSet);
        homeAudio = this.GetComponent<AudioSource>();
        OptionsCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frameo
    void Update() {
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
        if (LevelText.ToString() == "Tutorial")
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

    //OPTIONS////////////////////////////////////////////////////////////

    public void EnableOptions()
    {
        Debug.Log("Enable options");
        
        if(OptionsCanvas.isActiveAndEnabled)
        {
            OptionsCanvas.gameObject.SetActive(false);
        }
        else
        {
            OptionsCanvas.gameObject.SetActive(true);
        }
      
    }

    //AUDIO//////////////////////////////////////////////////////////////

    public void MuteHomeAudio()
    {
        if (homeAudio.mute == false)
        {
            homeAudio.mute = true;
        }
        else
            homeAudio.mute = false;
    }
}
