using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceanTop : MonoBehaviour {

    BackgroundBehaviors BB;

	// Use this for initialization
	void Start () {

        BB=GameObject.FindObjectOfType<BackgroundBehaviors>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BB.underTheSea==false)
        {
            transform.position -= new Vector3(0f, 1f, 0);
        }
	}
}
