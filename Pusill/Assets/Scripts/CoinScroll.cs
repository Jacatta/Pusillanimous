using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScroll : MonoBehaviour {
    GameManager GM;//  BackgroundBehaviors BB;
    GameObject go;

    // Use this for initialization
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GM.underTheSea == false)
            transform.position -= new Vector3(0f, GM.sceneSpeed/3, 0f); // Same speed as background
        //transform.position -= new Vector3(0f, BB.sceneSpeed*Time.deltaTime, 0f);
    }
}
