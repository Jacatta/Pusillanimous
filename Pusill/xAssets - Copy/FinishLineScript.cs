using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour {

    BackgroundBehaviors BB;
    public float speedDevisor = 2;
	// Use this for initialization
	void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0f, BB.sceneSpeed/ speedDevisor, 0f);
    }
}
