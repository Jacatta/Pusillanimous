using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour {


    Backpack BkPak;
	// Use this for initialization
	void Start () {
        BkPak = GameObject.FindObjectOfType<Backpack>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KeyCheck()
    {
        if (BkPak.bronzeKeyStock>0)
        {
            BkPak.bronzeKeyStock--;

        }
        else
        {
            Debug.Log("You'd Need a Key to Open That");
         //   Debug.LogError("You'd Need a Key to Open That");
        }
    }
}
