using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorStateSpace;


namespace BehaviorStateSpace
{
    //0 = Sling Shot
    //1 = Follow
    //2 = Slip Stream
    //3 = Wipe Out
    //4 = End of Level
    
   public class BehaviorState
    {
        //BehaviorState bs;
        public string state = "None";
        public BehaviorState(string inState) { state = inState; }

        public bool Equals(BehaviorState inState)
        {
            return state == inState.state;
        }

        public bool Equals(string stateStringName)
        {
            return state == stateStringName;
        }

        public bool IsFollowState()
        {
            return state.Equals("FollowState");
        }

        public bool IsSlingShot()
        {
            return state.Equals("SlingShot");
        }

        public bool isSlipStream()
        {
            return state.Equals("SlipStream");
        }

        public bool IsWipeout()
        {
            return state.Equals("Wipeout");
        }

        public bool IsEndOfLevel()
        {
            return state.Equals("EndOfLevel");
        }


    }
}

public class SquidBehavior : MonoBehaviour {

    int MAXINCREMENT = 20;
    float delay = 0;

    // public int behaviorState;
    SlipStream_RePosition SS;
    ScoreKeeper SK;
    Backpack BkPak;
    BackgroundBehaviors BB;
    GameManager GM;
    
   // BehaviorState BS;

    private Image ImageMe;
    private Vector2 mousePos;
    private Vector3 screenPos;
    private Camera camera;
    public Rigidbody2D RB;
    public float speed;
    //  public float lastSpeed;
    private int timeInc;
    public Vector3 restPosition;
    public Vector3 currentPosition;
    public Vector3 target;
    public Vector3 InitialMousePosition;

    public GameObject R_Arm;
    public GameObject L_Arm;
    public Vector3 R_Ankor;
    public Vector3 L_Ankor;


    private Vector3 dragLast;
    private Vector3 dragCurrent;
    private float dragSpeed;
    private float yVelocity;
    private bool B_follow;
    private bool tethered;

    public float followSpeed;
    private float DistanceToTether;


    public bool ReadyToFollow;
    float temp;

    private float distanceFromObject;

    AudioSource CoinAudio;
    public BehaviorState behaviorState;
    // Use this for initialization
    void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        SK = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();
        GM = FindObjectOfType<GameManager>();
        SS = FindObjectOfType<SlipStream_RePosition>();

        //  BS = GameObject.FindObjectOfType<>();


        ImageMe = this.GetComponent<Image>();
        RB = this.GetComponent<Rigidbody2D>();
        
        camera = Camera.main;
        CoinAudio = GameObject.Find("Coin_OG").GetComponent<AudioSource>();
        speed = 1100;
        dragSpeed = 10000;
        restPosition = GetComponent<Transform>().position;
        timeInc = 0;
        DistanceToTether = 0f;

        R_Ankor = GameObject.Find("R_Mid_Ankor").transform.position;
        L_Ankor = GameObject.Find("L_Mid_Ankor").transform.position;

        target = transform.position;
        //ReadyToFollow = false;

        followSpeed = 5;
        Physics2D.IgnoreLayerCollision(9, 10);
        B_follow=false;
        tethered = false;
        R_Arm = GameObject.Find("R_Arm");
        L_Arm = GameObject.Find("L_Arm");

        
        
        behaviorState = new BehaviorState("FollowState");

   


        //BehaviorStateSpace.state = "SlingShot";
       // BehaviorState("SlingShot"); 
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log("Current State : " + behaviorState.state);



        switch (behaviorState.state)
        {
            case "FollowState":
                Behavior_Follow();
                //Debug.Log("FollowState");

                break;

            case "SlingShot":
                Behavior_Tether(mousePos);
                Debug.Log("EndOfLevel");

                break;
            case "SlipStream":
                Behavior_SlipStream();
                break;

            case "WipeOut":
                //Debug.Log("Delay = " + delay + 1f);
               // Debug.Log("Time = " + Time.time);
                if (delay != 0)
                {
                    if (delay + 1f < Time.time)
                    {
                        behaviorState.state = "FollowState";
                        delay = 0;
                    }
                        
                } else
                {
                    Behavior_WipeOut();
                }
                break;
            case "EndOfLevel":

                break;
        }
        

        GainFollowSpeed();


    }

    public void Behavior_Tether(Vector3 mousePosition)
    {
        Debug.Log("Hit tether");
        tethered = true;
        
        float DistanceToLeftTether = Vector3.Distance(L_Ankor, transform.position)*.45f;
        float DistanceToRightTether = Vector3.Distance(R_Ankor, transform.position)*.45f;

        R_Arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, R_Ankor - transform.position);
        L_Arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, L_Ankor - transform.position);

        R_Arm.GetComponent<RectTransform>().sizeDelta = new Vector2(15, DistanceToRightTether);
        L_Arm.GetComponent<RectTransform>().sizeDelta = new Vector2(15, DistanceToLeftTether);

    }

    public void ResetArms()
    {
        R_Arm.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 0);
        L_Arm.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 0);
    }

    public void Behavior_Follow()
    {
        //Debug.Log("Follow Behavior");
        Vector3 currentPos = new Vector3();
        currentPos = Input.mousePosition;

        RB.velocity = (currentPos - transform.position) * followSpeed;


        if (RB.velocity.magnitude < 600)
        {
            RB.velocity *= 2;
        }
        if (tethered)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        else
            transform.rotation = Quaternion.LookRotation(Vector3.forward, currentPos - transform.position);
    }

    public void Behavior_SlipStream()
    {
        //followSpeed *=.95f;
        GM.sceneSpeed += 2f;
        Behavior_Follow();
        //behaviorState.state = "FollowState"; 
        SS.psPlayerEntered.gameObject.SetActive(true);




    }

    public void Behavior_WipeOut()
    {
        Debug.Log("Initial velocity: " + RB.velocity);
        Debug.Log("Initial angular velocity: " + RB.angularVelocity);
        Debug.Log("Initial angular drag: " + RB.angularDrag);
        var opposite = -RB.velocity;
        RB.velocity = opposite;
        RB.angularVelocity = 360f;
        Debug.Log("After velocity: " + RB.velocity);
        Debug.Log("After angular velocity: " + RB.angularVelocity);
        Debug.Log("After angular drag: " + RB.angularDrag);

        // Behavior_Delay();
        //StartCoroutine(Delay_WipeOut());
        delay = Time.time;
    }

    public void Behavior_Delay()
    {
        Debug.Log("Behavior Delay");

        StartCoroutine(Delay_FollowMouse());
        
    }


        public void GainFollowSpeed()
    {
        if (followSpeed < 5)
        { followSpeed += .02f; }
    }

    private void OnMouseDown()
    {
        InitialMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {

        Vector3 currentPos = new Vector3();
        currentPos = Input.mousePosition;

        if (GM.LevelStart == true)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(currentPos.x, currentPos.y, currentPos.z), speed * Time.deltaTime);
            //new Vector3(currentPos.x * .5f, currentPos.y * .5f, currentPos.z * .5f), speed * Time.deltaTime);
        }




    }

    private void OnMouseUp()
    {
        Debug.Log("Launched");
        RB.velocity = (InitialMousePosition - Input.mousePosition) * 1000;
        //RB.velocity = (InitialMousePosition - Input.mousePosition)*1000;
        GM.LevelStart = false;
        
        Behavior_Delay();
        // BB.sceneSpeed = 100;
        ResetArms();

    }

    private void LateUpdate()
    {
        //dragLast = Input.mousePosition;

    }

    public void Follow_mouse()
    {

    }
    
    /*
    public void BehaviorUnderTheSea()
    {
        //Do you want to keep this? It will change the direction even if you miss the bubble...
        if (Input.GetMouseButtonDown(0))
        {
            FaceMousePosition();
        }
        currentPosition = transform.position;


        if (timeInc == 0)
        {
            //Debug.Log(transform.eulerAngles.z);
            if (transform.eulerAngles.z > 180)
                transform.eulerAngles += new Vector3(0, 0, 0.5f);
            else if (transform.eulerAngles.z < 180)
                transform.eulerAngles += new Vector3(0, 0, -0.5f);
            if (transform.eulerAngles.z < 1 && transform.eulerAngles.z > -1)
                transform.eulerAngles = Vector3.zero;

            Vector3 movementDirection = (restPosition - currentPosition);
            if (movementDirection.magnitude > 1)
            {
                movementDirection.Normalize();
                movementDirection *= 10;
            }

            Vector2 movement2D = new Vector2(movementDirection.x, 0); //movementDirection.y
                                                                      // Debug.Log("movement2D: " + movement2D);
            RB.velocity = movement2D * 20;
        }
        else
            timeInc--;
    }

    public void BehaviorOverTheSea()
    {
        // Make Rest position @ the top of the scene. //Acheived in BackGround Behavior - SetRestPositionHigh
        // Make Astrid face down
        if(GM.sceneSpeed<=0)
        {
            FaceTheOcean();
        }
        
        // Vector3 direction = new Vector3(0, 0, -1 * Mathf.Atan2((screenPos.x - transform.position.x), (screenPos.y - transform.position.y)) * Mathf.Rad2Deg);
        // Make controling astrid by drag
        // Make it so Astrid slows down near the top. 
        if(transform.position.y>400)
        {
            yVelocity = 0;
        }

        if (!Input.GetMouseButton(0)) return;
        Vector3 currentPos = Input.mousePosition;
    }
    */
    public void RemoveAstrid()
    {
        GameObject A = GameObject.Find("Astrid");
        A.GetComponent<BoxCollider2D>().enabled = false; // So astrid doesnt collide with objects 
    }

    public void SetRestPositionHigh()
    {
        restPosition.y = 300f;
        currentPosition = transform.position;
        Vector3 movementDirection = (restPosition - currentPosition);
        yVelocity = 100f;

        Vector2 movement2D = new Vector2(0, yVelocity);// movementDirection.y);
        RB.velocity = movement2D;
    }

    public void FaceTheOcean()
    {

        // Vector3 rotationVector = new Vector3(0, 30, 0);
        //Quaternion desination = Quaternion.Euler(direction);
 
        temp = 180f;
  
        
        Vector3 direction = new Vector3(0, 0, temp);
        Quaternion desination = Quaternion.Euler(direction);
        Quaternion og = Quaternion.Euler(transform.position);


       // desination = (og - this.transform.rotation);
       

        if(transform.rotation.z >=100f)
        {
            transform.rotation = Quaternion.Lerp(og, desination, .9f);
        }
        transform.rotation = Quaternion.Lerp(og, desination, .9f);
       // transform.eulerAngles = Quaternion.Lerp(og, desination,2f);//direction);
    }


    public void FaceMousePosition()
    {
        mousePos = Input.mousePosition;
        screenPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - camera.transform.position.z));

        //Vector2 //
        Vector3 direction = new Vector3(0, 0, -1 * Mathf.Atan2((screenPos.x - transform.position.x), (screenPos.y - transform.position.y)) * Mathf.Rad2Deg);
        transform.eulerAngles = direction;

        //Judge the distance from the object and the mouse
        Vector3 movementDirection = (screenPos- this.transform.position);
        movementDirection.Normalize();

        Vector2 movement2D = new Vector2(movementDirection.x,0 );

        if (transform.position.y >= (restPosition.y+80))
        { RB.velocity = movement2D*100; }
        else
        { RB.velocity = movement2D * 200; }
        
        timeInc = MAXINCREMENT;
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Trash")
        {
            Debug.Log("Hit some trash");
            collision.gameObject.tag = "Done";
            
            followSpeed *= .1f;
            GM.sceneSpeed *= .4f;
            if (behaviorState.state.Equals("WipeOut"))
            {
                delay += 1f;
            }
            else
            {
                behaviorState.state = "WipeOut";
            }

            
        }
        else
        {
            if (collision.gameObject.tag == "Bounds") return;

           // Debug.LogError("Hit something thats not a stream or bottle tag: " + collision.gameObject.tag);
            //Debug.LogError("Hit something thats not a stream or bottle tag");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Collectable")
        {
            other.gameObject.SetActive(false);

            CoinAudio.Play();
            SK.coin++;
        }
        else if (other.tag == "Stream")
        {
            Debug.Log("SceneSpeed : " + GM.sceneSpeed);
            //ENTER BEHAVIOR - SLEIP STREAM
            behaviorState.state = "SlipStream";
            Debug.Log("Entered WarmWaterCurrent");
            SS.psSceneSpeedUp.gameObject.SetActive(true);
            //.SlipStreamHighlight.gameObject.SetActive(true);

            // GM.sceneSpeed += 5f;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stream")
        {
            
            behaviorState.state = "FollowState";
            Debug.Log("Exited WarmWaterCurrent");
            GM.sceneSpeed -= 30f;
            SS.psPlayerEntered.gameObject.SetActive(false);
            SS.psSceneSpeedUp.gameObject.SetActive(false);
           // SS.SlipStreamHighlight.gameObject.SetActive(false);
        }
        if (other.tag == "Done")
        {
            other.tag = "Trash";
        }
    }

    public IEnumerator Delay_WipeOut()
    {
        yield return new WaitForSeconds(1f);
        behaviorState.state = "FollowState";
        //  behaviorState.state = "WipeOut";

    }

    public IEnumerator Delay_FollowMouse()
    {
        yield return new WaitForSeconds(.2f);
        behaviorState.state = "FollowState";
        followSpeed *= .3f;
       // B_follow = true;
    }


    
}
