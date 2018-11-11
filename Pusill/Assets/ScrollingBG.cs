using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour {

    BackgroundBehaviors BB;
    Image img;
    GameObject go;
    public float sceneSpeed = 1f;
    Image OceanBack;

    

	// Use this for initialization
	void Start () {
        OceanBack = GameObject.Find("Background OceanTop").GetComponent<Image>();
        img = GetComponent<Image>();
        go = GameObject.Find("BG1");
        BB =FindObjectOfType<BackgroundBehaviors>();
        BB.underTheSea = true;
    }
	
	// Update is called once per frame
	void Update () {

        sceneSpeed = BB.sceneSpeed;
        // Emerge from the OCean
        if (BB.distance>BB.finishLine)
        {
            BB.underTheSea = false;
        }
        transform.position -= new Vector3(0f, sceneSpeed, 0f);
       // Debug.Log(transform.position+" position ");


        // Moved the image to the top so it can fall. 
        if (transform.position.y < -800 && BB.underTheSea==true )
        {
            Debug.Log("Got to -800");
            transform.position = new Vector3(transform.position.x, 2500f, transform.position.z);
            //Debug.Log("MOVE BG");
        }else if(transform.position.y < -800 && BB.underTheSea == false)
        {
            transform.position = new Vector3(-1000, 2500f, transform.position.z);
            oceanFall();
        }
    
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = 100;
            Debug.Log("Screen Pos: " + screenPosition);

            Debug.Log("Rect Position"+ Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position));
                
                //GameObject.Find("Altitude Bar").gameObject.transform.position.);
        }

	}


    public void oceanFall()
    {
       // OceanBack.transform.position -= new Vector3(0, 4f, 0);
    }

}
