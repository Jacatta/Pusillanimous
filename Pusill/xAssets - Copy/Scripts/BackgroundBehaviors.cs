using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BackgroundBehaviors : MonoBehaviour {

    // Use this for initialization
    Backpack BkPak;
    Tutorial_Manager Tutor;
    SpeedKeeper SK;
    SquidBehavior SB;
    Creator God;
    pause pp;
    CountCoins CC;
    knoblBehavior sKnobB;
    ParticleSystem Bubblemiddle;
    EndGame_Manager Ender;
   
    ParticleSystem BubbleTops;
    ParticleSystem BubbleBottom;
    ParticleSystem speedEmmit;
    ParticleSystem speedEmmit1;
    ParticleSystem PlanktonTop;

    Vector3 screenPos;
    Camera camera;
    GameObject squid;

    public float levelOne;
    public float sceneSpeed;
    public float SurfaceLine;
    public float FinishLine;
    public float distance;
    public float Acceleration;
    public float ExitSpeed;

    public bool underTheSea;
    public bool LevelEnd;
    public bool InstructionsHit;
    public bool LevelStart;
    public bool Apex;

    //public bool bTutorial;


    void Start () {
        BubbleBottom= GameObject.Find("Bubbles bottom").GetComponent<ParticleSystem>();

       
        BubbleTops = GameObject.Find("Bubbles top").GetComponent<ParticleSystem>();
        Bubblemiddle = GameObject.Find("Bubbles middle").GetComponent<ParticleSystem>();
        PlanktonTop = GameObject.Find("plankton top").GetComponent<ParticleSystem>();
        speedEmmit = GameObject.Find("Speed emmit").GetComponent<ParticleSystem>();
        speedEmmit1 = GameObject.Find("Speed emmit (1)").GetComponent<ParticleSystem>();

        BkPak = GameObject.FindObjectOfType<Backpack>();
        Ender = GameObject.FindObjectOfType<EndGame_Manager>();
        Tutor = GameObject.FindObjectOfType<Tutorial_Manager>();
        SK = GameObject.FindObjectOfType<SpeedKeeper>();
        SB = GameObject.FindObjectOfType<SquidBehavior>();
        pp = GameObject.FindObjectOfType<pause>();
        God = GameObject.FindObjectOfType<Creator>();
        CC = GameObject.FindObjectOfType<CountCoins>();
        sKnobB = GameObject.FindObjectOfType<knoblBehavior>();

        camera = Camera.main;
        squid = GameObject.Find("Astrid");

        levelOne= 0f;
        sceneSpeed = 30;
        distance = 0;
        Acceleration = -10f;

        speedEmmit.gameObject.SetActive(false);
        speedEmmit1.gameObject.SetActive(false);

        SurfaceLine = 1000;
        FinishLine = 2000;
        underTheSea = true;
        LevelEnd = false;
        InstructionsHit = false;
        LevelStart = true;
        Apex = false;
        //bTutorial = true;

        sKnobB.StartUp(this);
    }
	
	// Update is called once per frame
	void Update () {

        if (LevelEnd) { return; }

        if (LevelStart == true)
        {
            LevelStart = false;
            //pp.paused = false;
            StartCoroutine(pp.OnBegin());
            return;
        }


        if (underTheSea && distance > SurfaceLine)
        {
            underTheSea = false;
            StopParticles();
            SB.SetRestPositionHigh();//Sets Astrid position to be top down
            ExitSpeed = sceneSpeed;
            God.Coins();
        }

        if (distance > SurfaceLine)                // Hits Surface line
        {
            if(!InstructionsHit && BkPak.bTutorial)          // Activate Tutorial
            {
                Tutor.SurfaceTutorialToggle();
                //PAUSE TIME
                //God.newInstructions();
                Debug.Log("Got TO SurfaceLine");
                Debug.Log("distance: " + distance);
                Debug.Log("sceen speed: " + sceneSpeed);
                InstructionsHit = true;
            }
        }

        if(!underTheSea && sceneSpeed<1 && !pp.paused)             // Did NOT reach FinishLine
        {
            Debug.Log("Did Not get to FINISHLINE");
                                                                   // OPPPHHHHHH Try again!
            Ender.EndG_UnSuccess();
            LevelEnd = true;
        }

        if (distance > FinishLine-40f )                            // Reached FinishLine
        {
            Debug.Log("Got TO FINISHLINE");
            if(LevelEnd == false)
            {
                Debug.Log("Got TO FINISHLINE1");
                Ender.EndG_Success();
                
            }
            LevelEnd = true;
           // SB.RemoveAstrid();


            //God.EnableFinishLine();
            //ACTIVATE FINISH LINE. 
        }

        /*
        if (!underTheSea && ExitSpeed < (-sceneSpeed-10f))
        {
            splash = true;
        }
        */

        if (!underTheSea)//&& (!splash)
        {
            sceneSpeed += Acceleration * Time.deltaTime;
            if (pp.paused == false && sceneSpeed<10)//Used to activate End of Level
            {
                Apex = true;
            }


        } else
        {
            //levelOne = CheckSpeed();
            if (sceneSpeed < 1)
            {
                sceneSpeed = 0f;
            }
            else
            {
                //distance += sceneSpeed * Time.deltaTime;
                //Debug.Log("Time:" + Time.deltaTime);
                sceneSpeed -= .01f;
            }
            
        }

        distance += sceneSpeed * Time.deltaTime;
        ParticleChecks();
       
       
    }


    public void StopParticles()
    {
        Bubblemiddle.gameObject.SetActive(false);
         BubbleTops.gameObject.SetActive(false);
        BubbleBottom.gameObject.SetActive(false);

        PlanktonTop.gameObject.SetActive(false);
    }

    public void ParticleChecks()
    {
        // Physics.gravity = new Vector3(0, -1f*(sceneSpeed), 0);
        var BBmain = BubbleBottom.main;

        if (BubbleBottom.isPlaying && sceneSpeed > 2)
        {

            BBmain.gravityModifierMultiplier += .4f;

            if (BBmain.gravityModifierMultiplier > 400f)
            { BubbleBottom.gameObject.SetActive(false); }
        }

        if (PlanktonTop.gravityModifier <= 2f)
            PlanktonTop.gravityModifier += .01f;


        if (sceneSpeed > 10f)
        {
            Bubblemiddle.emissionRate = 10f;
            Bubblemiddle.gravityModifier = 30f;

            BubbleTops.gravityModifier = 30f;
            if (sceneSpeed > 30f)
            {
                Bubblemiddle.emissionRate = 20f;
                Bubblemiddle.gravityModifier = 60f;

                BubbleTops.gravityModifier = 600f;
                if (sceneSpeed > 50f)
                {
                    Bubblemiddle.emissionRate = 30f;
                    Bubblemiddle.gravityModifier = 120f;

                    BubbleTops.gravityModifier = 120f;
                }
            }
        }
        else if (sceneSpeed < 10f && sceneSpeed > -10f)
        {
            BubbleTops.gravityModifier -= .1f;
        }
        if (sceneSpeed > 10f && speedEmmit.gameObject.activeSelf == false)
        {
            speedEmmit.gameObject.SetActive(true);

            if (sceneSpeed > 60f && speedEmmit1.gameObject.activeSelf == false)
            {
                speedEmmit1.gameObject.SetActive(true);
            }
        }


        if (sceneSpeed < 10f)
        {
            speedEmmit.gameObject.SetActive(false);
        }
        if (sceneSpeed < 60)
        {
            speedEmmit1.gameObject.SetActive(false);
        }


    }



    public void speedUpScene()
    {
        //Debug.Log("Swim");
        Vector2 mousePos = Input.mousePosition;
        screenPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - camera.transform.position.z));
        //  Debug.Log(screenPos.z);
        if (screenPos.x > squid.transform.position.x)
        {
            HorizontalMovement(true);
        }
        else
            HorizontalMovement(false);
 
       // Debug.Log(Vector3.Distance(SB.currentPosition, SB.restPosition));
        if (Vector3.Distance(SB.currentPosition, SB.restPosition) > 10f) //SB.currentPosition )
        {
            speedEmmit.startColor = new Color(speedEmmit.startColor.r, speedEmmit.startColor.g, speedEmmit.startColor.b, .1f);
            speedEmmit.gameObject.SetActive(true);
        }
        else
        {
            speedEmmit.startColor = new Color(speedEmmit.startColor.r, speedEmmit.startColor.g, speedEmmit.startColor.b, 0f);
            speedEmmit.gameObject.SetActive(false);
        }
    }

    public float CheckSpeed()
    {
        return (SK.currentSpeed);
    }


    public void HorizontalMovement(bool Right)
    {
        var speed0 = speedEmmit.velocityOverLifetime;
        var speed1 = speedEmmit1.velocityOverLifetime;
        if (!Right)
        {
           // Debug.Log("Right");
            speed0.x = 300;
            speed0.y = -1*sceneSpeed;
            speed1.x = 1000;
            speed1.y = -1 * sceneSpeed;
          //  velLev2.x = 20;
         //   velLev2.y = -levelOne;
        }
        else
        {
           // Debug.Log("Left");
            speed0.x = -300;
            speed0.y = -1 * sceneSpeed;
            speed1.x = -300;
            speed1.y = -1 * sceneSpeed;
            //  velLev1.x = -100;
            //  velLev1.y = -400;
            //  velLev2.x = -20;
            //  velLev2.y = -levelOne;
        }
        StartCoroutine(CurrentFlow(1));
    }

    private IEnumerator CurrentFlow(int ti)
    {
        var vel0 = speedEmmit.velocityOverLifetime;
        var vel1 = speedEmmit1.velocityOverLifetime;
        var f = speedEmmit.trails.lifetime;
        sceneSpeed += 2;
        f = 0f;
        yield return new WaitForSeconds(ti);
        vel0.x = 0;
        vel1.x = 0;
        f = .2f;
        sceneSpeed += 1;

        // vel.y = 0;
    }
}
