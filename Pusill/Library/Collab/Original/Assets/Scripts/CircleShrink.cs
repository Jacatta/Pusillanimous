using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CircleShrink : MonoBehaviour// IPointerDownHandler
{
    AudioManager Audio;
    KnobBehavior Kb;
    BackgroundBehaviors Bb;
    GameManager GM;
    BarScript Bs;
    RectTransform Rt;
    ScoreKeeper Keeper;
    SpeedKeeper SKeeper;
    pause Paus;
    public float shrinkRate;
    GameObject sisterCircle;
    public GameObject newBoom;
    public Camera mainCam;

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
   // float height;
   // float width;

    float tooSoonPercent = 1f;
    float perfectPercentCutOff = .55f;
    float greatPercentCutOff = .15f;
    float goodPercentCutOff = .05f;

    // Use this for initialization
    void Start () {
        //CircleShrinkingMathZones
        Rt = GetComponent<RectTransform>();
        

        coreCircleX = 1;
        RingCircleX = Rt.localScale.x;

        Audio = GameObject.FindObjectOfType<AudioManager>();
        Kb = GameObject.FindObjectOfType<KnobBehavior>();
        Bb = GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = GameObject.FindObjectOfType<GameManager>();
        Bs = GameObject.FindObjectOfType<BarScript>();
        Paus = GameObject.FindObjectOfType<pause>();
        Keeper = GameObject.FindObjectOfType<ScoreKeeper>();
        SKeeper = GameObject.FindObjectOfType<SpeedKeeper>();
        canv = GameObject.Find("Canvas_Pop").GetComponentInParent<Canvas>();
        shrinkRate = .06f;  
        
        sisterCircle = GameObject.Find("circle1");
        PS = GameObject.FindObjectOfType<ParticleSystem>();

        selfImage = GetComponent<Image>();

        ogDifference = (RingCircleX - coreCircleX);


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

            DeleteMe(); // Resets Sreak, Slows speed & more
        }

        switch (TargetZones())
        {
            case 3://Good
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(243f / 255f, 52f / 255f, 148f / 255f, ImageAlpha), .1f);
                break;
            case 2://Great 
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(255f / 255f, 253f / 255f, 0f / 255f, ImageAlpha), .1f);
                break;
            case 1://Perfect            
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
        //Keeper.streak = 0;
        //MAJOR SLOW DOWN IF MISSED A TARGET

        GM.sceneSpeed -= (GM.sceneSpeed *.15f);
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
       // Debug.Log("Screen Pos: " + screenPosition);
        switch (TargetZones())
        {
            case 3: //Good
                Keeper.score += 100;
                newScore.text = "100";
                Keeper.gain = 100;
                GM.sceneSpeed += .25f;
              //  Kb.hit = 1;
                //Bs.KnobBump(80f);
                // Bs.KnobBump(9f);
                break;
            case 2: //Great
                //GameObject.Instantiate(newText, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);
                Keeper.score += 200;
                newScore.text = "200";
                Keeper.gain = 200;
                GM.sceneSpeed += 3f;
                //   Kb.hit = 2;
                //  Bs.KnobBump(12f);
                break;
            case 1: // Perf
                newScore.text = "300";
                Keeper.gain = 300;
                GM.sceneSpeed += 7f;

                Debug.Log("Got PERF");
              //  Kb.hit = 3;
                //   Bs.KnobBump(20f);
                break;
            case 0:
                Keeper.streak = 0;
                //  Bs.KnobBump(-200f);
                DeleteMe();
                return; 
            default:
                Keeper.streak = 0;
                DeleteMe();
                return;
        }

        //Bb.onClickBounce(25);
        //GetComponent<AudioSource>();
       // streak++;
        Bb.boost_SceneSpeed();
        ScoreCheck();//Score & Streak info. 
        
        //Audio.AudioClipSwitch();
        //aud.Play(0);
        PS.Play();//ParticleSystem
        GameObject.Instantiate(newText, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        GameObject.Instantiate(newScore, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        GameObject.Instantiate(newMultplier, Camera.main.ScreenToWorldPoint(screenPosition), Quaternion.identity, canv.transform);//CREATE TEXT 
        SKeeper.currentSpeed += 10f;
        
        Destroy(newBoom);

       
        
    }



    private IEnumerator ScoreClear()
    {
        yield return new WaitForSeconds(1f);
        Keeper.gain = 0;
        Keeper.MultiplierInt = 0;
    }

    public void ScoreCheck()
    {
        Keeper.streak++;
        if (Keeper.maxStreak < Keeper.streak)
            Keeper.maxStreak = Keeper.streak;

        if(Keeper.MultiplierInt>0)
        {
            Keeper.score += Keeper.gain * Keeper.MultiplierInt;
        }
        else
        {
            Keeper.score += Keeper.gain;
        }
            
        switch (Keeper.streak)
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
        }
        else if (circlePercent > perfectPercentCutOff)//1
        {
            ImageAlpha = 1f;
            mainMod.startSpeed = 300f;
            psr.trailMaterial = BlueMat;
            newText.text = "Perfect";
            newText.color = Color.white;

            return 1;
        } else if(circlePercent > greatPercentCutOff)//2
        {
            ImageAlpha = .6f;
            mainMod.startSpeed = 140f;
            psr.trailMaterial = GreenMat;
            newText.text = "Great";
            newText.color = Color.green;

            return 2;
        } else if (circlePercent > goodPercentCutOff)//3
        {
            ImageAlpha = .2f;
            mainMod.startSpeed = 80f;
            psr.trailMaterial = OrangeMat;
            newText.text = "Good";
            newText.color = Color.yellow;
            return 3;
        }
        return zone;
    }

}
