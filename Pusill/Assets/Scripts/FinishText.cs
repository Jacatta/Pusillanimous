using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;

public class FinishText : MonoBehaviour {

    Rigidbody2D rb2d;
    //Text t;
	// Use this for initialization
	void Start () {
        //t = GetComponentInChildren<Text>();
        rb2d = GetComponent<Rigidbody2D>();
        //t.fontSize = 0;
       // rb2d.gravityScale = 0;


    }
	
	// Update is called once per frame
	void Update () {
        //rb2d.transform.position =
      //  t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - .001f);

    }
}
