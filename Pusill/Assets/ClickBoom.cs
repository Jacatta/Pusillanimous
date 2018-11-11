using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClickBoom : MonoBehaviour, IPointerDownHandler
{

    public ParticleSystem Ps;
    public Canvas Canv;

    private Vector3 screenpos;

    // Use this for initialization
    void Start () {
        Canv = GameObject.FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log("Clicked - OnMouseDown");
        screenpos = Input.mousePosition;
        screenpos.z = 100;
        
       //,// Vector3 UseMe = Camer

        ParticleSystem newPop = ParticleSystem.Instantiate(Ps, Camera.main.ScreenToWorldPoint(screenpos), Quaternion.identity,Canv.transform);
        //newPop.transform.position = Camera.main.ScreenToWorldPoint(screenpos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log("Clicked - OnPointerDown");
        //ParticleSystem newPop = ParticleSystem.Instantiate(Ps, Input.mousePosition, Quaternion.identity);
    }
}
