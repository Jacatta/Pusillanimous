using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour {

    GameManager GameMngr;
    pause PP;
    PrizeDetector PrizeDetect;
    EndGame_Manager Ender;

    public Rigidbody2D rb;
    float drag;
    float RotationSpeed;
    float TickDrag;
    public bool FreeSpin_b;
    public bool RewardPlayer;
    public Animation anim;
    public Animator ticker;

    public Collider2D[] colliders;
    public List<Collider2D> prizes;

    public GameObject TickTick;
    // Use this for initialization
    void Start () {
        GameMngr = GameObject.FindObjectOfType<GameManager>();
        PP = GameObject.FindObjectOfType<pause>();
        PrizeDetect = GameObject.FindObjectOfType<PrizeDetector>();
        Ender = GameObject.FindObjectOfType<EndGame_Manager>();
        rb = GetComponent<Rigidbody2D>();

        FreeSpin_b = true;
        RewardPlayer = false;
        drag = .1f;
        TickDrag = 0;
        Random.InitState(3);
        TickTick = GameObject.Find("TickTick");

        colliders = GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D poly in colliders)
        {
            prizes.Add(poly);
        }
        prizes.Add(TickTick.GetComponentInChildren<BoxCollider2D>());
       
        anim = TickTick.GetComponent<Animation>();

        PrizeColliderToggle(); //deactivate box colliders
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpintheWheel()
    {
        FreeSpin_b = false;
        RotationSpeed = Random.Range(-1500, -1000);
        rb.angularVelocity = RotationSpeed;
       // Debug.Log("AV: " + rb.angularVelocity);
        rb.angularDrag = 0;
        TickDrag = 0;
        drag = .1f;
        

        ticker.speed = 4;
        ticker.SetBool("ActivateTicks", true);
        rb.angularDrag = drag;
        StartCoroutine(increaseDrag());
    }

    IEnumerator increaseDrag()
    {
        float rando = Random.Range(0, 1000);
       // Debug.Log("Randome= " + rando);



       // Debug.Log("AV2: " + rb.angularVelocity);
        if (rb.angularVelocity > -30f) // Wheel is slowing
        {
          //  Debug.Log("TURNING OFF TICKS");
            ticker.SetBool("ActivateTicks", false);
            rb.angularDrag += 10;
            anim.wrapMode = WrapMode.Once;
            ticker.Play("Tick");
            yield return new WaitForSeconds(.17f);
            PrizeColliderToggle();

            RewardPlayer = true;
            //Resolved in PrizeDetector
            StartCoroutine(SecondsDelay(1));
            
        }
        else
        {
            drag += (Mathf.Sqrt(Mathf.Abs(RotationSpeed))*.01f);
            rb.angularDrag = drag;
            if (!anim.isPlaying)
            {
                TickDrag -= drag * .02f;
                // anim["Tick"].speed += TickDrag;
                if (ticker.speed <= 0)
                { ticker.speed = 0; }
                else
                {
                    //Debug.Log("TickerSpeed: " + ticker.speed);
                    ticker.speed += TickDrag;

                }
            }
            
            if((rando % 3==0)  )
            {
                drag-=drag/3;
             //   Debug.Log("Drag: " + drag);
            }
    
            yield return new WaitForSeconds(.1f);
            StartCoroutine(increaseDrag());
        }
    }


    public void PrizeColliderToggle()
    {
        

        foreach(Collider2D poly in prizes) 
        {
            //Debug.Log(poly.enabled);
            if(poly.enabled == true)
            {
                poly.enabled = false;
            }
            else { poly.enabled = true; }
           
        }
    }

    public void DeactivatePopUp()
    {
        Debug.Log("Deactivate pop up");
        Ender.ResultsPop.enabled = false;
        Ender.ResultsBack.gameObject.SetActive(true);
        Ender.ResultsBack.enabled = true;
        Ender.ResultsFront.enabled = true;
        GameMngr.ResultsTime();
    }

    IEnumerator SecondsDelay(int secs)
    {
        yield return new WaitForSeconds(secs);
        DeactivatePopUp();
    }
}
