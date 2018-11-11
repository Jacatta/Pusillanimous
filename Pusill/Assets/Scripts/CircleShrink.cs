using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CircleShrink : MonoBehaviour// IPointerDownHandler
{
    KnobBehavior Kb;
    BackgroundBehaviors Bb;
    BarScript Bs;
    RectTransform Rt;
    ScoreKeeper Keeper;
    SpeedKeeper SKeeper;
    pause Paus;
    public float shrinkRate;
    GameObject sisterCircle;
    public GameObject newBoom;
    
    ParticleSystem PS;
    //ParticleSystem.Particle[] particles;
    Canvas canv;

    AudioSource aud;


    public Material OrangeMat;
    public Material GreenMat;
    public Material BlueMat;

    public Text newText;
    public Text newScore;
    public Text newMultplier;
    public Text newGain;

    Image selfImage;
    private float ImageAlpha;
    ParticleSystem.MainModule mainMod;
    ParticleSystemRenderer psr;

    //Circle Shrink Vars
    float coreCircleX;
    float RingCircleX;
    float circlePercent;
    float ogDifference;

    public int streak;

    float tooSoonPercent = 1f;
    float goodPercentCutOff = .55f;
    float greatPercentCutOff = .15f;
    float perfectPercentCutOff = .05f;

    // Use this for initialization
    void Start () {
        //CircleShrinkingMathZones
        Rt = GetComponent<RectTransform>();
        

        coreCircleX = 1;
        RingCircleX = Rt.localScale.x;


        Kb = GameObject.FindObjectOfType<KnobBehavior>();
        Bb = GameObject.FindObjectOfType<BackgroundBehaviors>();
        Bs = GameObject.FindObjectOfType<BarScript>();
        Paus = GameObject.FindObjectOfType<pause>();
        Keeper = GameObject.FindObjectOfType<ScoreKeeper>();
        SKeeper = GameObject.FindObjectOfType<SpeedKeeper>();
        canv = GameObject.Find("Canvas_Pop").GetComponentInParent<Canvas>();
        shrinkRate = .03f;  
        
        sisterCircle = GameObject.Find("circle1");
        PS = GameObject.FindObjectOfType<ParticleSystem>();

        selfImage = GetComponent<Image>();

        ogDifference = (RingCircleX - coreCircleX);

        aud = PS.GetComponent<AudioSource>();

        streak = 0;
        //aud.Play(0);
        // Debug.Log(selfImage);
    }
	
	// Update is called once per frame
	void Update () {
        
        newBoom = GameObject.Find("hereComestheBoom");
        if (PS == null) { PS = GameObject.FindObjectOfType<ParticleSystem>(); }
        
        mainMod = PS.main;
        psr = PS.GetComponent<ParticleSystemRenderer>();

        RingCircleX = Rt.localScale.x;
        circlePercent = ((RingCircleX - coreCircleX)/ogDifference);

        if (!Paus.paused)
        {
            Rt.localScale = new Vector3(Rt.localScale.x - shrinkRate, Rt.localScale.y - shrinkRate, Rt.localScale.z - shrinkRate);
           
        }
        
        //Missed Target - DESTROY
        if (circlePercent <= 0)
        {
            // Keeper.streakInt = 0;
            // Keeper.gainInt = 0;
            // Keeper.MultiplierInt = 0;

            DeleteMe();
        }

        switch (TargetZones())
        {
            case 3://Good
                //GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(255f / 255f, 163f / 255f, 18f / 255f, ImageAlpha), .1f);// GetComponent<Image>().color = new Color(255f/255f,163f/255f,18f/255f, ImageAlpha);
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(243f / 255f, 52f / 255f, 148f / 255f, ImageAlpha), .1f);
                break;
            case 2://Great 
                //GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(18f / 255f, 255f / 255f, 118f / 255f, ImageAlpha), .1f);//GetComponent<Image>().color = (new Color(18f/255f,255f / 255f, 118f / 255f, ImageAlpha));
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(255f / 255f, 253f / 255f, 0f / 255f, ImageAlpha), .1f);
                break;
            case 1://Perfect            
                //GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(18f / 255f, 152f / 255f, 255f / 255f, ImageAlpha), .1f);//GetComponent<Image>().color = new Color(18f / 255f, 152f / 255f, 255f / 255f, ImageAlpha);
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(0f / 255f, 255f / 255f, 255f / 255f, ImageAlpha), .1f);
                
                break;
            case 0://OUT OF BOUNDS
                
                GetComponent<Image>().color = new Color(18f / 255f, 152f / 255f, 255f / 255f, ImageAlpha);
                break;
            default:
                break;
        }

    }
    public void DeleteMe()
    {
        Keeper.streakInt = 0;
        Bb.sceneSpeed -= (Bb.sceneSpeed*.4f);
        if (PS != null)
        {
            Destroy(PS.gameObject);
        }
        Destroy(newBoom);
    }

    public void OnMouseDown()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 100;
        Debug.Log("Screen Pos: " + screenPosition);
        switch (TargetZones())
        {
            case 3: //Good
                Keeper.scoreInt += 100;
                newScore.text = "100";
                Keeper.gainInt = 100;
                Bb.sceneSpeed += .1f;
              //  Kb.hit = 1;
                //Bs.KnobBump(80f);
                // Bs.KnobBump(9f);
                break;
            case 2: //Great
                //GameObject.Instantiate(newText, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);
                Keeper.scoreInt += 200;
                newScore.text = "200";
                Keeper.gainInt = 200;
                Bb.sceneSpeed += 1;
                //   Kb.hit = 2;
                //  Bs.KnobBump(12f);
                break;
            case 1: // Perf
                newScore.text = "300";
                Keeper.gainInt = 300;
                Bb.sceneSpeed += 5f;

                Debug.Log("Got PERF");
              //  Kb.hit = 3;
                //   Bs.KnobBump(20f);
                break;
            case 0:
                Keeper.streakInt = 0;
                //  Bs.KnobBump(-200f);
                DeleteMe();
                return; 
            default:
                Keeper.streakInt = 0;
                DeleteMe();
                return;
        }

        //Bb.onClickBounce(25);
        //GetComponent<AudioSource>();
       // streak++;
        Bb.speedUpScene();
        ScoreCheck();
        aud.Play(0);
        PS.Play();
        GameObject.Instantiate(newText, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        GameObject.Instantiate(newScore, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        GameObject.Instantiate(newMultplier, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        SKeeper.currentSpeed += 10f;
        Destroy(newBoom);

       
        
    }

    private IEnumerator ScoreClear()
    {
        yield return new WaitForSeconds(1f);
        Keeper.gainInt = 0;
        Keeper.MultiplierInt = 0;
    }

    public void ScoreCheck()
    {
        Keeper.streakInt++;
        if(Keeper.MultiplierInt>0)
        {
            Keeper.scoreInt += Keeper.gainInt * Keeper.MultiplierInt;
        }
        else
        {
            Keeper.scoreInt += Keeper.gainInt;
        }
            
        switch (Keeper.streakInt)
        {
            case 5:
                Keeper.MultiplierInt = 2;
                newMultplier.text = "X" + 2;
                break;

            case 15:
                Keeper.MultiplierInt = 3;
                newMultplier.text = "X" + 3;
                break;

            case 30:
                Keeper.MultiplierInt = 5;
                newMultplier.text = "X" + 5;
                break;

        }
        StartCoroutine(ScoreClear());
    }

    public int TargetZones() {
        int zone = 3;

        if (circlePercent > tooSoonPercent)
        {
            ImageAlpha = .0f;
            mainMod.startSpeed = 10f;
            return 0;
        } else if(circlePercent > goodPercentCutOff)//3
        {
            ImageAlpha = .2f;
            mainMod.startSpeed = 50f;
            psr.trailMaterial = OrangeMat;
            newText.text = "Good";
            newText.color = Color.yellow;
            return 3;
        } else if(circlePercent > greatPercentCutOff)//2
        {
            ImageAlpha = .6f;
            mainMod.startSpeed = 110f;
            psr.trailMaterial = GreenMat;
            newText.text = "Great";
            newText.color = Color.green;

            return 2;
        } else if(circlePercent > perfectPercentCutOff)//1
        {
            ImageAlpha = 1f;
            mainMod.startSpeed = 280f;
            psr.trailMaterial = BlueMat;
            newText.text = "Perfect";
            newText.color = Color.blue;

            return 1;
            
        }
        return zone;
    }

}
