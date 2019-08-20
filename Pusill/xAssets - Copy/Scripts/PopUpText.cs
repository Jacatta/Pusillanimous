using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour {

    BackgroundBehaviors BB;
    Text t;
    Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        t = GetComponent<Text>();

        rb = GetComponent<Rigidbody2D>();

        if(t.color==Color.blue)
        {
            rb.velocity = new Vector2(Random.Range(-40f, 40f), 300f);
        }
        else
        rb.velocity= new Vector2(Random.Range(-60f,60f),200f);
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();

        
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.name == "InstructionText") return;
        

            rb.gravityScale += (BB.sceneSpeed/20f);

        if(t.fontSize<70)
        {
            t.fontSize++;
        }

        if(t.transform.position.y<-200)
        {
   
            Destroy(this.gameObject);
        }

    }
}
