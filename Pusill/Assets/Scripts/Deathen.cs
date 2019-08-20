using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathen : MonoBehaviour {


    bool wasActive=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem PS = GetComponent<ParticleSystem>();
        if(PS.isPlaying)
        {
           // Debug.Log("Got Activated");
            wasActive = true;
        }

        if(wasActive && (!PS.isPlaying))
        {
            //Debug.Log("Got destrooyed");
            Destroy(PS.gameObject);

        }
        
	}
}
