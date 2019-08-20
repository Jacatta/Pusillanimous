using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creator : MonoBehaviour {

    pause pp;
    GameManager GM;
    public Canvas Canv;
    public Camera mainCam;
   // public Canvas FinishLineCanv;
    //public Canvas FinishDisplay;

    public GameObject newBoom;
    public ParticleSystem ParticleBoom;

    public GameObject newBubble;
    public GameObject CoinPurse;
    public GameObject EvenCoinLayer;
    public GameObject OddCoinLayer;
    public GameObject FinishLineGO;
    
   // public Button unPause;
    public bool NewInstructions;

    BackgroundBehaviors BB;
    private int PlayerHeight;

    private int CoinOffset;
    Vector2 bot;
    Vector2 top;
    float width;
    float height;

    



    //GameObject newPart;

    // Use this for initialization
    void Start () {
        pp = GameObject.FindObjectOfType<pause>();
        GM = FindObjectOfType<GameManager>();
        Canv = GameObject.Find("Canvas_Pop").GetComponent<Canvas>();
        CoinPurse = GameObject.Find("CoinPurse");
        FinishLineGO = GameObject.Find("FinishLine");
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        //FinishDisplay = GameObject.Find("Canvas_FinishDisplay").GetComponent<Canvas>();
        PlayerHeight = Mathf.CeilToInt(Camera.main.WorldToScreenPoint(GameObject.Find("Astrid").transform.position).y);
        bot = Camera.main.ViewportToScreenPoint(new Vector2(0, 0));
        top = Camera.main.ViewportToScreenPoint(new Vector2(1, 1));
        CoinOffset = 75;
        NewInstructions = false;
        //NewInstructionsText.gameObject.SetActive(false);
        //NewInstructionsText.enabled = false;
       // unPause.gameObject.SetActive(false);
        //FinishDisplay.enabled = false;
        
        //  Debug.Log("Finishline : "+ FinishLineCanv.name);

    }
	
	// If no Target Exists, make ONE! 
	void Update () {

        if(GM.LevelStart)
        {
           // CreateTarget(1);
        }

        if (GM.underTheSea)
        {
           // CreateTarget(0);
        } else if (!NewInstructions && !GM.underTheSea)
            {
           // Debug.Log("NO LONGER CALLED");

              //  newInstructions();
            //
                //NewInstructions = true;
            }

    }

    void CreateTarget(int i)
    {
        if (GameObject.Find("hereComestheBoom") != null)
            return;


        width = mainCam.scaledPixelWidth;
        height = mainCam.scaledPixelHeight;

        Vector3 centerSpawnPosition = new Vector3(width / 2, height / 2, mainCam.nearClipPlane + 1);

        //Vector3 spawnPosition = new Vector3(Random.Range(50, 400), Random.Range(0,620), 500f); // CREATES A RANDOME SPAWN POSITION
        Vector3 spawnPosition;

        if (i==1)
        {
            spawnPosition = new Vector3(width / 2, height / 2, mainCam.nearClipPlane + 1); //Center Spawn

        }
        else
            spawnPosition = new Vector3(Random.Range(200, (width - 200)), Random.Range(PlayerHeight+50, (height - 400)), mainCam.nearClipPlane + 1); // CREATES A RANDOME SPAWN POSITION

        ParticleSystem newP = ParticleSystem.Instantiate(ParticleBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
        newP.name = ParticleBoom.name;
        //INSTANCIATE TARGET
        GameObject GoTempTarget = GameObject.Instantiate(newBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
        GoTempTarget.name = newBoom.name;
    }

    public void newInstructions()
    {
       // float width = mainCam.scaledPixelWidth;
       // NewInstructionsText.gameObject.SetActive(true);
       // NewInstructionsText.enabled = true;
        //unPause.enabled = true;
        //GameObject.Instantiate(NewInstructionsText, Camera.main.ScreenToWorldPoint(new Vector3(width / 2, height / 2, mainCam.nearClipPlane + 1)), Quaternion.identity, Canv.transform);//CREATE TEXT 
        // GameObject.Instantiate(unPause, Camera.main.ScreenToWorldPoint(new Vector3(width / 2, height / 2, mainCam.nearClipPlane + 1)), Quaternion.identity, Canv.transform);//CREATE TEXT
      //  StartCoroutine(SecondTimeAnommaly());
    }

    private IEnumerator SecondTimeAnommaly()
    {
        pp.OnTimeTest();
        //pp.OnSlowPause(.3f);
        yield return new WaitForSeconds(2);//i / 3);
        //pp.OnSlowPause(.3f);
        pp.OnTimeTest();
    }


    /*

    public void CreateFirstTarget()
    {

        float width = mainCam.scaledPixelWidth;
        float height = mainCam.scaledPixelHeight;
        ParticleSystem newP = ParticleSystem.Instantiate(ParticleBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
        newP.name = ParticleBoom.name;
        
        GameObject GoTempTarget = GameObject.Instantiate(newBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
        GoTempTarget.name = newBoom.name;
    }
    */





}
