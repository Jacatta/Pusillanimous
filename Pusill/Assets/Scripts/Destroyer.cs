using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    ParticleSystem ps;
    bool active;
    GameObject pa;
    GameObject part;

    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        active = false;
        pa = GameObject.Find("hereComestheBoom");
        part = ps.transform.parent.gameObject;
        Debug.Log("DESTROYER");
    }
	
	// Update is called once per frame
	void Update () {
        //ps = GetComponent<ParticleSystem>();
        if (GetComponent<ParticleSystem>().isPlaying)
        {
            active = true;
        }
        
		if(!ps.isPlaying&&active==true)
        {
            Destroy(pa);
            Destroy(ps.gameObject);// transform.parent.gameObject);
        }
	
    }
}
