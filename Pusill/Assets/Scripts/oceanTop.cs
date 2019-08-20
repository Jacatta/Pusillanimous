using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceanTop : MonoBehaviour {

    BackgroundBehaviors BB;
    GameManager GM;

    private float offset;
    public float StartPosition;

    private bool LevelEnded;

    // Use this for initialization
    void Start () {

        BB=GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = FindObjectOfType<GameManager>();
        StartPosition = transform.position.y;
        offset = StartPosition - 2000f;
        LevelEnded = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (LevelEnded) return;
        if (GM.underTheSea == false && !GM.Tutorial && transform.position.y > offset)
        {
            transform.position -= new Vector3(0f, GM.sceneSpeed, 0);
        }
        else if (GM.Apex == true)
        {

            Debug.Log("LevelEnded");
                
        }
        else { transform.position -= new Vector3(0f, GM.sceneSpeed, 0); }

    }
}
