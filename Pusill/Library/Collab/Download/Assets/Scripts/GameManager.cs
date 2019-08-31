using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wardrobing;

public class GameManager : MonoBehaviour
{

    ScoreKeeper Sk;
    Backpack BkPak;
    Wardrobe WrdRb;
    BackgroundBehaviors BB;
    SquidBehavior SB;
    EndGame_Manager End;

    public Text CoinTracker;
    public Text CoinText;
    AudioSource CoinCountAudio;
    ParticleSystem PS;
    //TitleManager ResultsManager;


    public Canvas Begin;
    public GameObject Wardrobe_Head;
    public GameObject Wardrobe_Body;
    public GameObject Wardrobe_Misc;

    public GameObject Astrid;
    public GameObject TrashSystem;
    public GameObject StreamSystem;
    bool bSubtract = false;
    int numToSubtract = 0;

    public GameObject[] Alerts;

    public float levelOne;
    public float sceneSpeed;
    public float WaterLine;
    public float FinishLine;
    public float distance;
    public float Acceleration;
    public float ExitSpeed;

    public bool underTheSea;
    public bool LevelEnd;
    public bool Tutorial;
    public bool LevelStart;
    public bool JustOnceOnStart;
    public bool Apex;
    public bool paused;



    // Use this for initialization
    void Start()
    {
        Sk = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();
        BB = FindObjectOfType<BackgroundBehaviors>();
        SB = FindObjectOfType<SquidBehavior>();
        End = FindObjectOfType<EndGame_Manager>();
        WrdRb = BkPak.WrdRb;
        //ResultsManager = GameObject.FindObjectOfType<TitleManager>();

        Astrid = GameObject.FindGameObjectWithTag("Player");
        StreamSystem = GameObject.Find("Stream");
        TrashSystem = GameObject.Find("Trash_Bottles");


        WrdRb.Wardrobe_HeadGear = Wardrobe_Head.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_BodyGear = Wardrobe_Body.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_MiscGear = Wardrobe_Misc.GetComponentsInChildren<Transform>();

        Alerts = GameObject.FindGameObjectsWithTag("Alert");
        foreach(GameObject GO in Alerts)
        {
            GO.SetActive(false);
        }

        Begin = GameObject.Find("Canvas_OnStart").GetComponent<Canvas>();
        Begin.enabled = false;


        WrdRb.Game_unSet_Gear();


        
        List<GameObject> G;
        G = WrdRb.Game_Set_Gear();

        foreach(GameObject go in G)
        {
            go.transform.parent = Astrid.transform;
        }

        WaterLine = 4000;
        FinishLine = 6000;
        underTheSea = true;
        LevelEnd = false;
        Tutorial = false;
        LevelStart = true;
        JustOnceOnStart = false;
        Apex = false;
        paused = false;

        levelOne = 0f;
        sceneSpeed = 0;
        distance = 0;
        Acceleration = -10f;
    }

    // Update is called once per frame
    void Update()
    {

        //If Level is Ended- Begin EndLevel
        if (LevelEnd)
        {
            if (Astrid.activeSelf) Astrid.SetActive(false);

            return;
        }


        if (JustOnceOnStart == true)
        {
            Debug.Log("Just once at level start");
            StartCoroutine(OnLevelStart());
            JustOnceOnStart = false;
        }
        if (LevelStart == true)
        {

        }

        if (underTheSea && distance > WaterLine)       // Hit the Waterline
        {
            TrashSystem.SetActive(false);
            underTheSea = false;
           // BB.StopOceanParticles();
            ExitSpeed = sceneSpeed;
            //God.Coins();???
            if (Tutorial) { }                           // New Tutorial pop up- Collect Coins.
        }


        if (!underTheSea && sceneSpeed < 1 && !paused)  // Out of Water &&  did not reach FinishLine
        {
            Debug.Log("Did Not get to FINISHLINE");
            SB.behaviorState.state = "EndOfLevel";
            End.EndG_UnSuccess();
            LevelEnd = true;
            TrashSystem.SetActive(false);
            StreamSystem.SetActive(false);
        }
        else if (distance > FinishLine - 40f)           // Reached FinishLine
        {
            Debug.Log("Got TO FINISHLINE");
            if (LevelEnd == false)
            {
                Debug.Log("Got TO FINISHLINE1");
                End.EndG_Success();
                LevelEnd = true;

            }
            SB.behaviorState.state = "EndOfLevel";
            // SB.RemoveAstrid();
        }




        if (!underTheSea   )                             //Once out of water. Slows down scene speed.  Option for Apex.
        {
            sceneSpeed += Acceleration * Time.deltaTime;
            if (!paused && sceneSpeed < 10) { Apex = true; }
        }
        else                                            //if playing the game.
        {
            if(sceneSpeed>700)
            { sceneSpeed -= 4; }
            else if (sceneSpeed < 1)
            {
                sceneSpeed = 0f;
            }
            else
            {
                sceneSpeed -= 1f;
            }
        }

        distance += sceneSpeed * Time.deltaTime;

        // I Want this string to show coins gained in this level, not total count. 

        
        // Subtracts the total coins from the top so it can be added when you add coins at the end of level 
        if (bSubtract)
        {
            CoinText.text = (BkPak.Currency[0].Qty - numToSubtract).ToString();
        }
        else
        {
            CoinText.text = numToSubtract.ToString();//
        }
        
    }

    public IEnumerator OnLevelStart()
    {

        paused = true;
        Begin.enabled = true;
        //Begin.gameObject.SetActive(true);

        Time.timeScale = 0;
        float pauseEndTime = Time.realtimeSinceStartup + 2f;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            if (Time.realtimeSinceStartup >= pauseEndTime - .4f)
            {
                Begin.GetComponentInChildren<Text>().text = "GO!!!";
                Begin.GetComponentInChildren<Text>().fontSize = 160;
            }
            yield return 0;
        }

        Time.timeScale = 1;
        paused = false;
        Begin.gameObject.SetActive(false);
    }

    public void ResultsTime()
    {
        Debug.Log("Start CoRoutine");
        StartCoroutine(CoinCounter());
        PS = GameObject.Find("coin_Animated").GetComponent<ParticleSystem>();
        var emiss = PS.emission;
        emiss.rateOverTime = Sk.coin / 3;
        PS.Play();
    }

    IEnumerator CoinCounter()
    {
        CoinTracker = GameObject.Find("Coin_Int").GetComponent<Text>();
        CoinCountAudio = GameObject.Find("UI_Piggy").GetComponent<AudioSource>();

        BkPak.Currency[0].Qty += Sk.coin;
        numToSubtract = Sk.coin;
        bSubtract = true;

        int i = 0;
        for (i = 0; i < Sk.coin; i++)
        {
            yield return new WaitForSeconds(.01f);
            //Debug.Log("Play Coin Sound here");
            CoinCountAudio.Play();
            CoinTracker.text = "= " + (i + 1).ToString();
            numToSubtract -= 1;
        }
        bSubtract = false;
    }

    public bool PayCost(string n, int qty)
    {

        Backpack.baseCurrency tempCurrency = new Backpack.baseCurrency();
        tempCurrency.Name = n;
        tempCurrency.Qty = qty;
        Debug.Log("tempcurrency name: " + tempCurrency.Name);

        int i=0;
        switch (tempCurrency.Name)
        {
            case "coinPrize":
               
                i = 0;
                break;
            case "BrnzKey":
              
                i = 1;
                break;
            case "SlvrKeys":
                
                i = 2;
                break;
            case "gemPrize":
               
                break;
            case "RainGems":
               
                break;
        }

        if (BkPak.Currency[i].Qty > 0) {
           // BkPak.Currency[i].Qty--;
            BkPak.Currency[i].Qty -= tempCurrency.Qty;
            return true;
        } else {
            return false;
        }
            
    }

    public void AdjustCurrency(string n, int qty)
    {
        Backpack.baseCurrency tempCurrency = new Backpack.baseCurrency();
        tempCurrency.Name = n;
        tempCurrency.Qty = qty;
        Debug.Log("tempcurrency name: " + tempCurrency.Name);
        switch (tempCurrency.Name)
        {
            case "coinPrize":
                BkPak.Currency[0].Qty += tempCurrency.Qty;
                break;
            case "keyPrize":
                BkPak.Currency[1].Qty += tempCurrency.Qty;
                break;
            case "SlvrKeys":
                BkPak.Currency[2].Qty += tempCurrency.Qty;
                break;
            case "gemPrize":
                BkPak.Currency[3].Qty += tempCurrency.Qty;
                break;
            case "RainGems":
                BkPak.Currency[4].Qty += tempCurrency.Qty;
                break;
        }
    }
}