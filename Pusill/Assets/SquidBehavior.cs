using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquidBehavior : MonoBehaviour {

    int MAXINCREMENT = 20;

    private Image ImageMe;
    private Vector2 mousePos;
    private Vector3 screenPos;
    private Camera camera;
    public Rigidbody2D RB;
    public float speed;
    private int timeInc;
    public Vector3 restPosition;
    public Vector3 currentPosition;

    private float distanceFromObject;
    // Use this for initialization
    void Start () {

        ImageMe = this.GetComponent<Image>();
        RB = this.GetComponent<Rigidbody2D>();
        

        camera = Camera.main;
        speed = 0;
        restPosition = GetComponent<Transform>().position;
        timeInc = 0;
	}
	
	// Update is called once per frame
	void Update () {
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
                
            Vector2 movement2D = new Vector2(movementDirection.x, 0 ); //movementDirection.y
           // Debug.Log("movement2D: " + movement2D);
            RB.velocity = movement2D * 10;
        }
        else
            timeInc--;
            
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
        distanceFromObject = (Input.mousePosition - camera.WorldToScreenPoint(transform.position)).magnitude;

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

    public IEnumerator swim(Vector2 vect)
    {

       // RB.AddForce(direction * 100 * distanceFromObject); //* Time.deltaTime);
        yield return new WaitForSeconds(1f);
    }

    void OnMouseDown()
    {
       
    }
}
