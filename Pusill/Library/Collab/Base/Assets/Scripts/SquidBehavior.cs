using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquidBehavior : MonoBehaviour {

    int MAXINCREMENT = 20;

    ScoreKeeper SK;
    Backpack BkPak;
    BackgroundBehaviors BB;

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
    // Use this for initialization
    void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        SK = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();

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
        ReadyToFollow = false;

        followSpeed = 5;
        Physics2D.IgnoreLayerCollision(9, 10);
        B_follow=false;
        tethered = false;
        R_Arm = GameObject.Find("R_Arm");
        L_Arm = GameObject.Find("L_Arm");
    }

    // Update is called once per frame
    void Update()
    {
        if (BB.LevelEnd)
        {
            return;
        }
        else
        {
            if (ReadyToFollow)
            {
                Behavior_Follow();
                
            }
        }
 
        

        if (transform.position.y >=2500)
        {
            ReadyToFollow = true;
            
        }

        GainFollowSpeed();


    }

    public void Behavior_Tether()
    {
        Debug.Log("Hit tether");
        tethered = true;
        DistanceToTether = Vector3.Distance(transform.position, currentPosition);
        R_Arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, R_Ankor);

        L_Arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, L_Ankor);

       // R_Arm.transform.localScale = new Vector3(1, DistanceToTether, 1f);
        R_Arm.transform.localScale = new Vector3(1, 4, 1f);
        L_Arm.transform.localScale = new Vector3(1, 4, 1f);

    }

    public void ResetArms()
    {
        R_Arm.transform.localScale = Vector3.Lerp(R_Arm.transform.position, new Vector3 (1, 0.1f, 1f), 1000f);
        L_Arm.transform.localScale = Vector3.Lerp(L_Arm.transform.position, new Vector3(1, 0.1f, 1f), 1000f);
       // L_Arm.transform.localScale = new Vector3(1, 1, 1f);
    }

    public void Behavior_Follow()
    {

        Follow_mouse();
        if (B_follow)
        {
            
        }
   
    }

    public void Behavior_Delay()
    {
        Debug.Log("Hit");

        StartCoroutine(DelayFollowMouse());
        
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

        if (BB.LevelStart == true)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                                    new Vector3(currentPos.x * .5f, currentPos.y * .5f, currentPos.z * .5f), speed * Time.deltaTime);
        }




    }

    private void OnMouseUp()
    {
        Debug.Log("Launched");
        RB.velocity = (InitialMousePosition - Input.mousePosition) * 1000;
        //RB.velocity = (InitialMousePosition - Input.mousePosition)*1000;
        BB.LevelStart = false;
        ReadyToFollow = false;
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
        Vector3 currentPos = new Vector3();
        currentPos = Input.mousePosition;
       
        RB.velocity = (currentPos - transform.position)*followSpeed;
       

        if (RB.velocity.magnitude < 600)
        {
            RB.velocity *= 2;
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, currentPos - transform.position);
    }

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
        if(BB.sceneSpeed<=0)
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
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Trash")
        {
            followSpeed *= .1f;
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
        
   
    }
    public IEnumerator DelayFollowMouse()
    {
        yield return new WaitForSeconds(.4f);
        ReadyToFollow = true;
        followSpeed *= .3f;
       // B_follow = true;
    }


    
}
