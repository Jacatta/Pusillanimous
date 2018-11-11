using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

    public Canvas Canv;
    public Camera mainCam;

    public GameObject newBoom;
    public ParticleSystem ParticleBoom;

    public GameObject newBubble;

    //GameObject newPart;

    // Use this for initialization
    void Start () {
        Canv = GameObject.Find("Canvas_Pop").GetComponent<Canvas>();

    }
	
	// If no Target Exists, make ONE! 
	void Update () {

        if (GameObject.Find("hereComestheBoom") == null)
        {
            float width = mainCam.scaledPixelWidth;
            float height = mainCam.scaledPixelHeight;
            //Vector3 spawnPosition = new Vector3(Random.Range(50, 400), Random.Range(0,620), 500f); // CREATES A RANDOME SPAWN POSITION
            Vector3 spawnPosition = new Vector3(Random.Range(50, (width-50)), Random.Range(200, (height-100)), mainCam.nearClipPlane+1); // CREATES A RANDOME SPAWN POSITION

            ParticleSystem newP = ParticleSystem.Instantiate(ParticleBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
            newP.name = ParticleBoom.name;
            //INSTANCIATE TARGET
            GameObject GoTempTarget = GameObject.Instantiate(newBoom, mainCam.ScreenToWorldPoint(spawnPosition), Quaternion.identity, Canv.transform);
            GoTempTarget.name = newBoom.name;

        }
    }
}
