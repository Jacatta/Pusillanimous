using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knoblBehavior : MonoBehaviour {

    BackgroundBehaviors BB;
    GameManager GM;
    float BarHeight;
    float BarMax;
    float BarDiff;
    float BarBase;
    public GameObject SurfLine;
    Vector3 StartScreenP;
    RectTransform rt;
    // SurfaceLine/(Min-FinishLine)  ==== * (min-Finish)// Gives you the Ratio

    void Start () {
        //float Ypos = Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position).y - 150;
        rt = GameObject.Find("Altitude Bar").GetComponent<RectTransform>();

        BarBase = Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position).y;
        BarMax = rt.rect.yMax-10f;
        BarDiff = rt.rect.yMax - rt.rect.yMin;
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = FindObjectOfType<GameManager>();
        //transform.position = new Vector3(transform.position.x, Ypos, transform.position.z);
        SurfLine = GameObject.Find("SurfaceLine");

        StartScreenP = new Vector3(rt.rect.x + GetComponent<RectTransform>().rect.width + 5.5f, BarBase, Camera.main.transform.position.z);
    }

	void Update () {

        if (GM.distance < GM.FinishLine)
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(StartScreenP.x, StartScreenP.y + (BarMax * 4*  (GM.distance / GM.FinishLine)), StartScreenP.z));
            //SurfLine.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(StartScreenP.x, StartScreenP.y + (Bardiff * 4 * ( BB.FinishLine)), StartScreenP.z));
        }

    }

    public void StartUp(BackgroundBehaviors BB)
    {

        Start();
        // SurfLine = GameObject.Find("SurfaceLine");

     //   var Bhal = Camera.main.WorldToScreenPoint(rt.rect.max);
        //StartScreenP = new Vector3(rt.rect.x + GetComponent<RectTransform>().rect.width, BarBase, Camera.main.transform.position.z);
        //SurfLine.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(StartScreenP.x, StartScreenP.y + (BarDiff * 4 * (GM.WaterLine / GM.FinishLine)), StartScreenP.z));

        //SurfLine.transform.position = new Vector3(transform.position.x, (BB.SurfaceLine / BB.FinishLine) * (BarDiff*4), transform.position.z);



        //BarHeight = BarBase + rt.rect.height;

        
    }
}
