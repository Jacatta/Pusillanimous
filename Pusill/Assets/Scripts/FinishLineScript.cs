using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour {

    BackgroundBehaviors BB;
    GameManager GM;
    public float speedDevisor = 2;
	// Use this for initialization
	void Start () {
        GM = FindObjectOfType<GameManager>();
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0f, GM.sceneSpeed/ speedDevisor, 0f);
    }
}
