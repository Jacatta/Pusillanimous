using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBounce : MonoBehaviour {

    Rigidbody2D rb;
    Renderer rend;
    public Sprite flying;
    Image image;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        rb.freezeRotation = true;
        image = GetComponent<Image>();

        //GetComponent<Image>().sprite = (flying);

        //  sp = GetComponent<Sprite>();
    }
	
	// Update is called once per frame
	void Update () {

        if (rb.velocity.y > 0f)
        {

        }
        else
        {

        }
	}

    public void onClickBounce(float vertVelo)
    {
        //rb.velocity = new Vector2(0f, vertVelo);
      //  Debug.Log(rb.velocity.y + "Debugtime: " + Time.time);
    }
}
