using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedKeeper : MonoBehaviour {


    public float currentSpeed;
	// Use this for initialization
	void Start () {
        currentSpeed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        //currentSpeed -= .001f;
	}

    public void decreaseSpeed()
    {
        
    }

    public void increaseSpeed()
    {
        currentSpeed += 5f;
    }
}
