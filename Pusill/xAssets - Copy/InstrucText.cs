using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructText : MonoBehaviour
{

    Rigidbody2D rb2d;
    Text t;
    // Use this for initialization
    void Start()
    {
        t = GetComponent<Text>();
       // rb2d = GetComponent<Rigidbody2D>();
        t.fontSize = 0;
        // rb2d.gravityScale = 0;


    }

    // Update is called once per frame
    void Update()
    {

        if (t.fontSize < 80)
        {
            t.fontSize += 1;
           // rb2d.velocity = new Vector2(0, rb2d.velocity.y + 5f);
        }
        else
        {
            if (t.color.a > 0f)
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - .001f);
              //  rb2d.velocity = new Vector2(0, rb2d.velocity.y - 6f);
            }

        }

    }
}
