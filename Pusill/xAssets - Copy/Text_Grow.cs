using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Grow : MonoBehaviour {

    Text t;
    int i;

    ParticleSystem Ps;
	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
        i = 0;

        Ps = GetComponentInChildren<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
        i++;
        if(t.fontSize<100)
        {
            if(i == 3)
            {
                t.fontSize++;
                i = 0;
            }

        }
        else
        {
            //Ps.Play();
        }
        
	}
}
