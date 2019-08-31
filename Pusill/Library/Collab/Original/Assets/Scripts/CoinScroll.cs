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
    void Update() {
        if (GM.underTheSea == false && GM.paused == false)
            transform.position -= new Vector3(0f, GM.sceneSpeed / 3, 0f); // Same speed as background
        else if (this.name == "GameCoins" && GM.paused == false)
        {
            transform.position -= new Vector3(0f, GM.sceneSpeed / 3, 0f);
        }
        else {
            transform.position -= new Vector3(0f, 0, 0f);


          }
    }

}
