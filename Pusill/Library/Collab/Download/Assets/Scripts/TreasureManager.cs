using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManager : MonoBehaviour {


    Backpack BkPak;


    public GameObject Medalion1;
    public GameObject Medalion1_Thumb;

    // Use this for initialization
    void Start () {
        BkPak = GameObject.FindObjectOfType<Backpack>();

        Medalion1.GetComponent<Image>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KeyCheck()
    {
        if (BkPak.bronzeKeyStock>0)
        {
            BkPak.bronzeKeyStock--;

        }
        else
        {
            Debug.Log("You'd Need a Key to Open That");
         //   Debug.LogError("You'd Need a Key to Open That");
        }
    }


    public void ActivateMedalion_1()
    {
        //Find a way To CANCEL ACTION

       // Medalion1_Thumb.GetComponent<Button>().enabled = false;

        if (Medalion1.GetComponent<Image>().enabled==true)
        {
            Medalion1.GetComponent<Image>().enabled = false;
        }
        else
        {
            Medalion1.GetComponent<Image>().enabled = true;
        }

           
    }

}
