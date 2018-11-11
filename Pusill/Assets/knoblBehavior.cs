using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knoblBehavior : MonoBehaviour {

    BackgroundBehaviors BB;
    float BarHeight;
    float Bardiff;
    float BarBase;
    // Use this for initialization
    void Start () {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        transform.position = new Vector3(transform.position.x, Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position).y-150, transform.position.z);
        
        RectTransform rt = GetComponent<RectTransform>();
        BarBase = Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position).y;
        BarHeight = BarBase + rt.rect.height;
        Debug.Log("BarHeight: " + BarHeight);
        Debug.Log("BarBase: " + BarBase);
        Debug.Log("Bardiff: " + Bardiff);
        Debug.Log("Time:" + Time.deltaTime);
        Bardiff = 500;//BarBase - BarHeight-100f;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(250f, (Bardiff*(BB.distance/BB.finishLine))-100f, 600f);//(BB.distance/BB.finishLine), 600f);
    }
}
