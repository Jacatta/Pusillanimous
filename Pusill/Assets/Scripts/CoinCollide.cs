using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollide : MonoBehaviour {

    ScoreKeeper SK;

    // Use this for initialization
    void Start () {
        SK = GameObject.FindObjectOfType<ScoreKeeper>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
        SK.coin++;
    }
}
