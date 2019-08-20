using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    Transform playerPOS;
    GameObject player;
    // Use this for initialization
    void Start () {
        
        player=GameObject.Find("Mr.Blue");
        playerPOS = player.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        Camera.main.transform.Translate(new Vector3(playerPOS.position.x,playerPOS.position.y, playerPOS.position.z-10f));
    }
}
