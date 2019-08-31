using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGame_Manager : MonoBehaviour {

    pause PP;
    SquidBehavior SB;
    GameManager GM;

    GameObject Success_GO;
    GameObject Fail_GO;
    GameObject FinishLine_GO;
    GameObject Alerts;

    public Canvas ResultsBack;
    public Canvas ResultsFront;
    public Canvas ResultsPop;
    public Canvas GUI_Canvas;
    public Canvas Coin_Canvas;
    // Use this for initialization
    void Start () {


        Success_GO = GameObject.Find("Successful");
        Fail_GO = GameObject.Find("Un-Successful");
        FinishLine_GO = GameObject.Find("FinishLine");
        Alerts = GameObject.Find("HeadsUp_Incomming");

        ResultsBack = GameObject.Find("Results_Canvas_Background").GetComponent<Canvas>();
        ResultsFront = GameObject.Find("Results_Canvas_Forground").GetComponent<Canvas>();
        ResultsPop = GameObject.Find("Results_Canvas_PopUp").GetComponent<Canvas>();
        GUI_Canvas = GameObject.Find("Canvas_UI").GetComponent<Canvas>();
        Coin_Canvas = GameObject.Find("Canvas_Coins").GetComponent<Canvas>();

        PP = GameObject.FindObjectOfType<pause>();
        SB = GameObject.FindObjectOfType<SquidBehavior>();
        GM = GameObject.FindObjectOfType<GameManager>();

        //////////////////////////////////////////////////////////////////////////////////////
        Success_GO.SetActive(false);
        Fail_GO.SetActive(false);
        FinishLine_GO.SetActive(false);

        ResultsBack.gameObject.SetActive(false);
        ResultsFront.enabled = false;
        ResultsPop.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndG_Success()
    {
        Debug.Log("EndG _ WIN called");
        Success_GO.SetActive(true);
        StartCoroutine(FinishDelay());
        DeactivateAstrid();
      //  StartCoroutine(runWithDelay(Deactivateworld));
        
        //Send to EndGame- Results pop up
    }
    
    public void EndG_UnSuccess()
    {
        Fail_GO.SetActive(true);
        StartCoroutine(FinishDelay());
        DeactivateAstrid();
     //   StartCoroutine(runWithDelay(Deactivateworld));
        //Send to EndGame- Results pop up
    }
    

    public void DeactivateAstrid()
    { SB.RemoveAstrid(); }

    public void Deactivateworld(int i)
    {
        GameObject.Find("CoinPurse (1)").SetActive(false);
        
    }

    public void OnFinish()
    {
        if(GM.Alerts_B)
        {
            Alerts.SetActive(false);
        }

        Debug.Log(GUI_Canvas.name);
        ResultsFront.enabled = true;
        ResultsPop.enabled = true;
        GUI_Canvas.enabled = false; 
        //Coin_Canvas
        GameObject.Find("CoinPurse (1)").SetActive(false);

    }

    public IEnumerator runWithDelay(Action<int> myMethodName)
    {

        Debug.Log("RunWith Delay - blow up coins");
        Transform[] B;
        B = GameObject.Find("CoinPurse (1)").GetComponentsInChildren<Transform>();
        foreach(Transform t in B)
        {
            //EXPLODE
            try
            {
                t.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                t.gameObject.GetComponent<ParticleSystem>().Play();
               // yield return new WaitForSeconds(.05f);

            }
                catch
            {
                //yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(.001f);

        }
        //myMethodName(1);


    }
    
        public IEnumerator DelayDestroy(int i)
    {
        yield return new WaitForSeconds(i);
    }

        public IEnumerator FinishDelay()
    {
        FinishLine_GO.SetActive(true);
        yield return new WaitForSeconds(2f);
        OnFinish();
        // ToDO Turn off Scene speed and astrid Behavior. 

    }
}
