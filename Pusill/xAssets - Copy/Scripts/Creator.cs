using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creator : MonoBehaviour {

    pause pp;
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

        if(BB.LevelStart)
        {
            CreateTarget(1);
        }

        if (BB.underTheSea)
        {
            CreateTarget(0);
        } else if (!NewInstructions && !BB.underTheSea)
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

    public void Coins()
    {
        return;
        //SARA CODED- CREATES COINS
           
        /*
        speed
        dt
        0 = v + a*t
        t = -v/a
        v[t] = v + a*dt;
        X[t] = v*t + 1/2*a*t;
        x[t] = v*(-v/a) + 1/2*a*(-v/a)
        */

        float MaxDistance = -BB.sceneSpeed * BB.sceneSpeed / BB.Acceleration - 1 / 2 * BB.sceneSpeed;
        float MaxLayers = MaxDistance / CoinOffset;
        
        for (int layer = 0; layer < MaxLayers; layer++)
        {
            Vector3 SpawnPosition = new Vector3(top.x / 2f, top.y + CoinOffset * (1 + layer), mainCam.nearClipPlane + 1); // CREATES A RANDOME SPAWN POSITION
            if (layer%2 == 0)
            {
                GameObject NewLayer = GameObject.Instantiate(EvenCoinLayer, mainCam.ScreenToWorldPoint(SpawnPosition), Quaternion.identity, CoinPurse.transform);
                NewLayer.name = "Coin Layer " + layer;
            }
            else
            {
                SpawnPosition.x += CoinOffset / 2f;
                GameObject NewLayer = GameObject.Instantiate(OddCoinLayer, mainCam.ScreenToWorldPoint(SpawnPosition), Quaternion.identity, CoinPurse.transform);
                NewLayer.name = "Coin Layer " + layer;
            }
        }
    }



}
