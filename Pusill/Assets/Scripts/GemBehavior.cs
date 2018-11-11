using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour {


    GameObject meee;
    // Use this for initialization
    void Start () {
		 meee = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter()
    {
        Debug.Log("This: " + this);
        Destroy(this);
        

    }
}
