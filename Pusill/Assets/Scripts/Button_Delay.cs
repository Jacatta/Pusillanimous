using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Delay : MonoBehaviour {

    Button B;
    int i;
	// Use this for initialization
	void Start () {
        B = GetComponent<Button>();
        B.enabled = false;
        i = 0;
    }
	
	// Update is called once per frame
	void Update () {
        i++;
        
        if(i==100)
        {
            B.enabled = true;
            i = 0;
        }
	}
}
