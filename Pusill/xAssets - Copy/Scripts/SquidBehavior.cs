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
    private int timeInc;
    public Vector3 restPosition;
    public Vector3 currentPosition;

    private Vector3 dragLast;
    private Vector3 dragCurrent;
    private float dragSpeed = 10000;
    private float yVelocity;

    float temp;

    private float distanceFromObject;

    AudioSource CoinAudio;
    // Use this for initialization
    void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        SK = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();

       Debug.Log("Is there a BkPak: " +BkPak.name);

        ImageMe = this.GetComponent<Image>();
        RB = this.GetComponent<Rigidbody2D>();
        
        camera = Camera.main;
        CoinAudio = GameObject.Find("Coin_OG").GetComponent<AudioSource>();
        speed = 0;
        restPosition = GetComponent<Transform>().position;
        timeInc = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if(BB.LevelEnd)
        {
            return;
        }
        else if (BB.underTheSea)
        {
            BehaviorUnderTheSea();
        }
        else
        {
            BehaviorOverTheSea();
        }
    }

    private void LateUpdate()
    {
        //dragLast = Input.mousePosition;

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
        /*
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            float t = Time.time;
                return;
        }
        */


        Vector3 currentPos = Input.mousePosition;
        /*
        if (Mathf.Abs((currentPos - dragLast).x) < 50f)
        {
         //   return;
        }
        Vector3 pos = Camera.main.ScreenToViewportPoint(currentPos - dragLast);
        dragLast = currentPos;

        Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

        Vector2 NewV = new Vector2(currentPos.x, yVelocity);

        //transform.Translate(pos, Space.World);
       // RB.velocity = NewV;
       */
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);
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

        Vector2 movement2D = new Vector2(movementDirection.x,0 );//movementDirection.y

        // Debug.Log("distance from OBJ: " + distanceFromObject);
        //distanceFromObject = (Input.mousePosition - camera.WorldToScreenPoint(transform.position)).magnitude;
       
        //Move towards the mouse
        //Debug.Log("Direction: "+direction);
        //Debug.Log("Distance: " + distanceFromObject);
        //Debug.Log("Time: " + Time.deltaTime);
        //Debug.Log("Direction: " + direction);
        
        if (transform.position.y >= (restPosition.y+80))
        { RB.velocity = movement2D*100; }
        else
        { RB.velocity = movement2D * 200; }
        
        timeInc = MAXINCREMENT;
        //RB.AddForce(movementDirection * 1000); //* Time.deltaTime);

    }

    void OnMouseDown()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
       // other.GetComponent<AudioSource>().Play();
        CoinAudio.Play();
        //Debug.Log("played");
        //BkPak.coinPurse++;
        SK.coin++;
    }
}
