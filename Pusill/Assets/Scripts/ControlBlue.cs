using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBlue : MonoBehaviour {


    Rigidbody2D rb;
    private bool direction;
    public float speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        direction = false;
        speed = 4f;
	}

    // Update is called once per frame
    void Update()
    {
        /*
        if (direction == false)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        */

       if(Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.x > 135)// Right side of screen
            {
                rb.velocity = new Vector2(3, 5);
            }
            else//Left side of screen
            {
                rb.velocity = new Vector2(-3, 5);
            }
           // Debug.Log(Input.mousePosition);
            
        }
        rb.rotation = 0f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.layer == 5)
        {
            Debug.Log("other.name:"+ other.gameObject.name);
            flipDirection();
        }
    
    }

    public void flipDirection()
    {
        direction = !direction;
    }
}
