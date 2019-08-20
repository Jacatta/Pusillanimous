using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceanTop : MonoBehaviour {

    BackgroundBehaviors BB;

    private float offset;
    public float StartPosition;

    private bool LevelEnded;

    // Use this for initialization
    void Start () {

        BB=GameObject.FindObjectOfType<BackgroundBehaviors>();

        StartPosition = transform.position.y;
        offset = StartPosition - 2000f;
        LevelEnded = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (LevelEnded) return;
        if (BB.underTheSea == false && !BB.InstructionsHit && transform.position.y > offset)
        {
            transform.position -= new Vector3(0f, BB.sceneSpeed, 0);
        }
        else if (BB.Apex == true)
        {

            Debug.Log("LevelEnded");
                
        }
        else { transform.position -= new Vector3(0f, BB.sceneSpeed, 0); }
        /*
        else if (BB.splash)
        {
            if (transform.position.y > StartPosition)
            {
                BB.EndLevel();
                LevelEnded = true;
            }
            else
            transform.position -= new Vector3(0f, BB.sceneSpeed, 0);
        }
        */
    }
}
