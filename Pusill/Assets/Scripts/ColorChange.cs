using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour {


    BackgroundBehaviors BB;
	// Use this for initialization
	void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
	}
	
	// Update is called once per frame
	void Update () {

            ColorSwitch(GetComponent<Text>().text);

    }

    public void ColorSwitch(string num)
    {
        int h = int.Parse(num);
   
        if(h>100)
        {
            GetComponent<Text>().color = Color.green;
            if (h > 400)
            {
                GetComponent<Text>().color = Color.yellow;
                if (h > 100)
                {
                    GetComponent<Text>().color = Color.red;
                }
            }
        }
        else
        {
            GetComponent<Text>().color = Color.white;
            
        }
    }

}
