using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Wardrobing;

public class BackgroundBehaviors : MonoBehaviour {

    //Scripts
    Backpack BkPak;
    Tutorial_Manager Tutor;
    SpeedKeeper SK;
    SquidBehavior SB;
    Creator God;
    pause pp;
    knoblBehavior sKnobB;
    ParticleSystem Bubblemiddle;
    EndGame_Manager Ender;
    GameManager GM;

    //Particles
    ParticleSystem BubbleTops;
    ParticleSystem BubbleBottom;
    ParticleSystem speedEmmit;
    ParticleSystem speedEmmit1;
    ParticleSystem PlanktonTop;
    ParticleSystem TrashObsticles;

    Vector3 screenPos;
    Camera camera;
    GameObject squid;

    Animator FGR;


    //public bool bTutorial;


    void Start () {
        //BubbleBottom= GameObject.Find("Bubbles bottom").GetComponent<ParticleSystem>();
       // BubbleTops = GameObject.Find("Bubbles top").GetComponent<ParticleSystem>();
      //  Bubblemiddle = GameObject.Find("Bubbles middle").GetComponent<ParticleSystem>();
      //  PlanktonTop = GameObject.Find("plankton top").GetComponent<ParticleSystem>();
        speedEmmit = GameObject.Find("Speed emmit").GetComponent<ParticleSystem>();
        speedEmmit1 = GameObject.Find("Speed emmit (1)").GetComponent<ParticleSystem>();


        BkPak = GameObject.FindObjectOfType<Backpack>();
      //  MC = GameObject.FindObjectOfType<CameraShake>();
        Ender = GameObject.FindObjectOfType<EndGame_Manager>();
        Tutor = GameObject.FindObjectOfType<Tutorial_Manager>();
        SK = GameObject.FindObjectOfType<SpeedKeeper>();
        SB = GameObject.FindObjectOfType<SquidBehavior>();
        pp = GameObject.FindObjectOfType<pause>();
        God = GameObject.FindObjectOfType<Creator>();
      //  CC = GameObject.FindObjectOfType<CountCoins>();
        sKnobB = GameObject.FindObjectOfType<knoblBehavior>();
        GM = FindObjectOfType<GameManager>();

        camera = Camera.main;
        squid = GameObject.Find("Astrid");
        FGR = GameObject.Find("ForgroundRocks").GetComponent<Animator>();



        speedEmmit.gameObject.SetActive(false);
        speedEmmit1.gameObject.SetActive(false);

        sKnobB.StartUp(this);

       // WrdRb.UnSet_Gear();
    }
	
	// Update is called once per frame
	void Update () {

        if(GM.sceneSpeed>=430)
        {
            FGR.SetBool("GoingPlaid", true);
            Debug.Log("speed dropped below 230"); 
            // MC.ShakeIt();  //Shaking the camera does nothing when its a camera overlay
        }
        else { FGR.SetBool("GoingPlaid", false);
            Debug.Log("speed below 230");
        }
        
       // ParticleChecks();
       
       
    }


    public void StopOceanParticles()
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

        if (BubbleBottom.isPlaying && GM.sceneSpeed > 2)
        {

            BBmain.gravityModifierMultiplier += .4f;

            if (BBmain.gravityModifierMultiplier > 400f)
            { BubbleBottom.gameObject.SetActive(false); }
        }

        if (PlanktonTop.gravityModifier <= 2f)
            PlanktonTop.gravityModifier += .01f;


        if (GM.sceneSpeed > 10f)
        {
            Bubblemiddle.emissionRate = 10f;
            Bubblemiddle.gravityModifier = 30f;

            BubbleTops.gravityModifier = 30f;
            if (GM.sceneSpeed > 30f)
            {
                Bubblemiddle.emissionRate = 20f;
                Bubblemiddle.gravityModifier = 60f;

                BubbleTops.gravityModifier = 600f;
                if (GM.sceneSpeed > 50f)
                {
                    Bubblemiddle.emissionRate = 30f;
                    Bubblemiddle.gravityModifier = 120f;

                    BubbleTops.gravityModifier = 120f;
                }
            }
        }
        else if (GM.sceneSpeed < 10f && GM.sceneSpeed > -10f)
        {
            BubbleTops.gravityModifier -= .1f;
        }
        if (GM.sceneSpeed > 10f && speedEmmit.gameObject.activeSelf == false)
        {
            speedEmmit.gameObject.SetActive(true);

            if (GM.sceneSpeed > 60f && speedEmmit1.gameObject.activeSelf == false)
            {
                speedEmmit1.gameObject.SetActive(true);
            }
        }


        if (GM.sceneSpeed < 10f)
        {
            speedEmmit.gameObject.SetActive(false);
        }
        if (GM.sceneSpeed < 60)
        {
            speedEmmit1.gameObject.SetActive(false);
        }


    }



    public void boost_SceneSpeed()
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
            speed0.y = -1* GM.sceneSpeed;
            speed1.x = 1000;
            speed1.y = -1 * GM.sceneSpeed;
  
        }
        else
        {
           // Debug.Log("Left");
            speed0.x = -300;
            speed0.y = -1 * GM.sceneSpeed;
            speed1.x = -300;
            speed1.y = -1 * GM.sceneSpeed;

        }
        StartCoroutine(CurrentFlow(1));
    }

    private IEnumerator CurrentFlow(int ti)
    {
        var vel0 = speedEmmit.velocityOverLifetime;
        var vel1 = speedEmmit1.velocityOverLifetime;
        var f = speedEmmit.trails.lifetime;
        //GM.sceneSpeed += 2;
        f = 0f;
        yield return new WaitForSeconds(ti);
        vel0.x = 0;
        vel1.x = 0;
        f = .2f;
        //GM.sceneSpeed += 1;

        // vel.y = 0;
    }
}
