using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviors : MonoBehaviour {

    // Use this for initialization
    SpeedKeeper SK;
    SquidBehavior SB;

    ParticleSystem Bubblemiddle;
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

    public float finishLine;
    public bool underTheSea;

    public float distance;


    void Start () {
        BubbleBottom= GameObject.Find("Bubbles bottom").GetComponent<ParticleSystem>();
        BubbleTops = GameObject.Find("Bubbles top").GetComponent<ParticleSystem>();
        Bubblemiddle = GameObject.Find("Bubbles middle").GetComponent<ParticleSystem>();
        PlanktonTop = GameObject.Find("plankton top").GetComponent<ParticleSystem>();
        speedEmmit = GameObject.Find("Speed emmit").GetComponent<ParticleSystem>();
        speedEmmit1 = GameObject.Find("Speed emmit (1)").GetComponent<ParticleSystem>();
        SK = GameObject.FindObjectOfType<SpeedKeeper>();
        SB = GameObject.FindObjectOfType<SquidBehavior>();

        camera = Camera.main;
        squid = GameObject.Find("squid");

        levelOne= 0f;
        sceneSpeed = 0;
        distance = 0;

        speedEmmit.gameObject.SetActive(false);
        speedEmmit1.gameObject.SetActive(false);

        finishLine = 1000;
        underTheSea = true;

    }
	
	// Update is called once per frame
	void Update () {

        //
       levelOne = CheckSpeed();
        if(sceneSpeed<=0)
        {
            sceneSpeed = 0;
        }else
        {
          //  distance+=(sceneSpeed/80);
            distance += sceneSpeed * Time.deltaTime;
            Debug.Log("Time:" + Time.deltaTime);
        }
        sceneSpeed -= .01f;
       // Debug.Log("Distance: "+ distance);

        if(BubbleBottom.isPlaying && sceneSpeed>2)
        {
            BubbleBottom.gravityModifier += .4f;

            if (BubbleBottom.gravityModifier > 400f)
            { BubbleBottom.gameObject.SetActive(false); }

            
        }

        if(PlanktonTop.gravityModifier<=2f)
        PlanktonTop.gravityModifier += .01f;


        if (sceneSpeed > 10f)
        {
            Bubblemiddle.emissionRate = 10f;
            Bubblemiddle.gravityModifier = 30f;

            BubbleTops.gravityModifier = 30f;
            if (sceneSpeed>30f)
            {
                Bubblemiddle.emissionRate = 20f;
                Bubblemiddle.gravityModifier = 60f;

                BubbleTops.gravityModifier = 600f;
                if (sceneSpeed>50f)
                {
                    Bubblemiddle.emissionRate = 30f;
                    Bubblemiddle.gravityModifier = 120f;

                    BubbleTops.gravityModifier = 120f;
                }
            }
        }
        else if (sceneSpeed < 10f&& sceneSpeed > -10f)
        {
            BubbleTops.gravityModifier -=.1f;
        }
        if (sceneSpeed>10f&& speedEmmit.gameObject.activeSelf==false)
        {
            speedEmmit.gameObject.SetActive(true);

            if (sceneSpeed > 60f && speedEmmit1.gameObject.activeSelf==false)
            {
                speedEmmit1.gameObject.SetActive(true);
            }
        }


        if(sceneSpeed<10f)
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
