using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public bool paused;

    BackgroundBehaviors Background;
    GameManager GM;
    ScrollingBG Scroll;
    AudioManager Composer;
    Canvas MenuPop;
    
    Canvas GUI_Canvas;
    public Canvas ResultsBack;
    public Canvas ResultsFront;
    public Canvas ResultsPop;
    
    float LastSceneSpeed;
    // Use this for initialization
    void Start()
    {


        GM = FindObjectOfType<GameManager>();
        //GUI_Canvas.enabled = true;
        MenuPop = GameObject.Find("Canvas_Menu").GetComponent<Canvas>();
        MenuPop.enabled = false;

        Scroll = GameObject.FindObjectOfType<ScrollingBG>();
        Background = GameObject.FindObjectOfType<BackgroundBehaviors>();
        Composer = GameObject.FindObjectOfType<AudioManager>();
        paused = false;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Got Clicked");
        menuPause();


    }



    public void menuPause()
    {
        
        if (paused)
        {
            MenuPop.enabled = false;
            Composer.PlaySong();

        }
        else
        {
            MenuPop.enabled = true;
            Composer.PauseSong();
        }
        OnPause();
    }

    public void OnPause()
    {
        Debug.Log("Paused: "+ paused);
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
           

            GM.sceneSpeed = LastSceneSpeed;
        }
        else
        {
            Time.timeScale = 0f;
            paused = true;

            LastSceneSpeed = GM.sceneSpeed;
            GM.sceneSpeed = 0;
        }
    }

    public void OnTimeTest()
    {
        Debug.Log("TimeWarped: " + paused);
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;


           // Background.sceneSpeed = LastSceneSpeed;
            StartCoroutine(TaperBGSpeed(30f, .05f, (LastSceneSpeed)));
        }
        else
        {
            //Time.timeScale = .2f;
            paused = true;

            LastSceneSpeed = GM.sceneSpeed;
            StartCoroutine(TaperBGSpeed(30f,.05f, (LastSceneSpeed/10f))); //inc,time wait in while, end speed
            //  Background.sceneSpeed = (Background.sceneSpeed / 50);
            

        }
    }

    public void OnSlowPause(float i)
    {
        Debug.Log("SLOWPaused: " + paused);
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;


            GM.sceneSpeed = LastSceneSpeed;
        }
        else
        {
            Time.timeScale = 0f;// Was a slow. @ .2 
            paused = true;

            LastSceneSpeed = GM.sceneSpeed;
           // StartCoroutine(TaperBGSpeed((Background.sceneSpeed / 5)));
            GM.sceneSpeed = (GM.sceneSpeed/5);
        }
    }


    public IEnumerator TaperBGSpeed(float inc, float waitTime, float EndSpeed)
    {
        Time.timeScale = 0;
        Debug.Log("TaperingSpeed: ");
        if (GM.sceneSpeed > (EndSpeed))
        {
            
            while (GM.sceneSpeed > (EndSpeed))
            {
                GM.sceneSpeed += -(inc);
                yield return new WaitForSeconds(waitTime);
            }
        }
        else if (GM.sceneSpeed < (EndSpeed))
        {
            while (GM.sceneSpeed < (EndSpeed))
            {
                GM.sceneSpeed += inc;
                yield return new WaitForSeconds(waitTime);
            }
        }
        Time.timeScale = 1;
    }



    public void ToTitleScene()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadTradeScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(1);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
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

    public void ExitGame()
    {

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif
        
    }

}
