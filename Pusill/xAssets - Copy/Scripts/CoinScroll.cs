using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScroll : MonoBehaviour {
    BackgroundBehaviors BB;
    GameObject go;

    // Use this for initialization
    void Start()
    {
        BB = FindObjectOfType<BackgroundBehaviors>();
    }
	
	// Update is called once per frame
	void Update () {
        if (BB.underTheSea == false)
            transform.position -= new Vector3(0f, BB.sceneSpeed/3, 0f); // Same speed as background
        //transform.position -= new Vector3(0f, BB.sceneSpeed*Time.deltaTime, 0f);
    }
}
