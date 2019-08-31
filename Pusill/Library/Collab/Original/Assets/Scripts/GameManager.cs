using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wardrobing;
using UnityEngine.SceneManagement;

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
    public int coinInt;
    AudioSource CoinCountAudio;
    ParticleSystem PS;
    ParticleSystem ps_Boom;
    //TitleManager ResultsManager;


    public Canvas Begin;
    public Canvas SlipStream;
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

    private bool SlipStream_B = false;
    public bool Alerts_B = false;
    private bool TrashObstacles_B = true;

    private Vector3[] touchPoints; 
    

    // Use this for initialization
    void Start()
    {

        Sk = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();
        BB = FindObjectOfType<BackgroundBehaviors>();
        SB = FindObjectOfType<SquidBehavior>();
        End = FindObjectOfType<EndGame_Manager>();
        WrdRb = BkPak.WrdRb;

        touchPoints = new List<Vector3>().ToArray();

        Astrid = GameObject.FindGameObjectWithTag("Player");
        
        WrdRb.Wardrobe_HeadGear = Wardrobe_Head.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_BodyGear = Wardrobe_Body.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_MiscGear = Wardrobe_Misc.GetComponentsInChildren<Transform>();

        Begin = GameObject.Find("Canvas_OnStart").GetComponent<Canvas>();
        //  SlipStream = GameObject.Find("Canvas_SlipStream").GetComponent<Canvas>();


       
        Alerts = GameObject.FindGameObjectsWithTag("Alert");
        if (Alerts != null)
        {
            foreach (GameObject GO in Alerts)
            {
                GO.SetActive(false);
            }
        }

        StreamSystem = GameObject.Find("Stream");
        if (SlipStream_B == false)
        {
            if (StreamSystem != null) { StreamSystem.SetActive(false); }
        }

        TrashSystem = GameObject.Find("Trash_Bottles");
        if (TrashObstacles_B == false)
        {            
            if (TrashSystem != null) { TrashSystem.SetActive(false); }
        }
                
        //Begin.enabled = false;
        WrdRb.Game_unSet_Gear();
        
        //Notsure if this us currently in use. 
        ps_Boom = GameObject.Find("PS_Destruction").GetComponent<ParticleSystem>();


        List<GameObject> Clothes;
        Clothes = WrdRb.Game_Set_Gear();
        foreach(GameObject go in Clothes)
        {
            go.transform.parent = Astrid.transform;
        }

        WaterLine = 1800;
        FinishLine = 2000;
        underTheSea = true;
        LevelEnd = false;
        Tutorial = false;
        LevelStart = true;
        JustOnceOnStart = true;
        Apex = false;
        paused = false;

        levelOne = 0f;
        sceneSpeed = 2;
        distance = 0;
        coinInt = -999;

        Acceleration = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = Sk.coin.ToString();
        //If Level is Ended- Begin EndLevel
        if (LevelEnd)
        {
            if (Astrid.activeSelf) Astrid.SetActive(false);

            return;
        }
        numToSubtract = Sk.coin;


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
            if(TrashObstacles_B)
            { TrashSystem.SetActive(false); }
            if (SlipStream_B)
            { StreamSystem.SetActive(false); }
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
            if(sceneSpeed>100)
            { sceneSpeed -= .2f; }
            else if (sceneSpeed <= 1)
            {
                sceneSpeed = 0f;
            }

            sceneSpeed += .15f;

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
           // Debug.Log("bSubtract-Coinissue");
           CoinText.text = numToSubtract.ToString();//
        }

        SB.behaviorState.state = ("FollowState");
        /*
        if (Input.touchCount > 0)//&& Input.touchCount <= 2)
        {
            Debug.Log("TouchCouont: " + Input.touchCount);
            if (Input.touchCount == 2)
            {
                DualInputDrag();
            }
            else 
            {
                SB.behaviorState.state = ("FollowState");
            }
            // if (Input.GetTou0ch(0).phase == TouchPhase.Began)
            // {
            //      checkTouch(Input.GetTouch(0).position);
            // }
        } else if (Input.touchCount == 0 && SB.behaviorState.IsSlingShot())
        {
           // SB.behaviorState.state="Launch";
        }
        */
    }

    public void DualInputDrag()
    {

        Vector3 currentPos = new Vector3();
        currentPos = Vector3.Lerp(Input.touches[0].position, Input.touches[1].position, 0.5f);
        SB.behaviorState.state = "SlingShot";
       // tethered = false;
       //
        transform.position = Vector3.MoveTowards
          (new Vector3(transform.position.x, transform.position.y, transform.position.z),
          new Vector3(currentPos.x, currentPos.y, currentPos.z), 1000 * Time.deltaTime);

        //  Debug.Log("GM.LevelStart" + GM.LevelStart);



      SB.GainFollowSpeed();
    }

    public IEnumerator OnLevelStart()
    {

        paused = true;
        Begin.enabled = true;
        //Begin.gameObject.SetActive(true);

        Time.timeScale = 0;
        float pauseEndTime = Time.realtimeSinceStartup + 2.2f;
        Debug.Log("OnLevelStart");
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            if (Time.realtimeSinceStartup >= pauseEndTime - .2f)
            {
                Begin.GetComponentInChildren<Text>().text = "GO!!!";
                Begin.GetComponentInChildren<Text>().fontSize = 160;
            }
            yield return 0;
        }

        Time.timeScale = 1;
        paused = false;
        //Begin-Getcomponent<Animation>().SetBool(fade);
        Begin.enabled = false;
        
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

    public IEnumerator Boost_SceneSpeed()
    {
        float pauseEndTime = Time.realtimeSinceStartup + .01f;
        Debug.Log("Boosted");
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            sceneSpeed += 2f;
            yield return 0;
        }
    }
    public IEnumerator Boost_SceneSpeed(int i)
    {
        float pauseEndTime = Time.realtimeSinceStartup + .01f;
        Debug.Log("Super Boosted");
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            sceneSpeed += i;
            yield return 0;
        }
    }

    IEnumerator CoinCounter()
    {
        CoinTracker = GameObject.Find("Coin_Int").GetComponent<Text>();
        CoinCountAudio = GameObject.Find("UI_Piggy").GetComponent<AudioSource>();

        BkPak.Currency[0].Qty += Sk.coin;
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
    public void goboom(Vector3 pos)
    {
        ps_Boom.transform.position = pos;
        ps_Boom.Play();
    }
}