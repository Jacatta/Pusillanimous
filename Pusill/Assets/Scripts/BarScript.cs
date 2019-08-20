using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    CircleShrink CS;
    public GameObject knob;
    public GameObject filling;
    float bot = 13.23f;
    float top = 362.243f;

    // Use this for initialization
    void Start() {
       // rb = knob.GetComponent<Rigidbody2D>();
       // rb.freezeRotation = true;
     //   rb.position.x;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //Top:  337
        //Bot: -500
        //Sooo total space to work in is top - bot = 837
        //Your current fill is knob.y - bot = (how far up it has moved).
        //Now it's total percentage is how far it has moved / it's total space
        //(knob.y - bot) / (top - bot)
      
        //filling.GetComponent<Image>().fillAmount = ((knob.transform.position.y + 500) / 4.0f);
        //filling.GetComponent<Image>().fillAmount = ((knob.transform.position.y - bot) / (top - bot));
    }

    /*
    public void KnobBump(float vertVelo)
    {
        Debug.Log(rb.position.x);
         
        if (rb.velocity.y < 10) // If it has a low velocity add a fat velocity to it (So that it can jump back up
        {
            rb.velocity += new Vector2(0f, vertVelo);
        } else // If it's headed up, then add a little so it's hard to reach the top.
        {
            rb.velocity += new Vector2(0f, vertVelo / 10);
        }
        //rb.velocity = new Vector2(0f, vertVelo);
    }
    */
}
