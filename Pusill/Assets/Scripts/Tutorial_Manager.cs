using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Manager : MonoBehaviour {

    CircleShrink CS;
    pause pPower;
    public Text NewInstructionsText;
    // Use this for initialization
    void Start () {
        
        pPower = GameObject.FindObjectOfType<pause>();

        NewInstructionsText.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("I Got Clicked");
       // SurfaceTutorialOff();
    }

    public void SurfaceTutorialToggle()
    {
        if(NewInstructionsText.isActiveAndEnabled)
        {
            NewInstructionsText.gameObject.SetActive(false);
        }
        else {
            NewInstructionsText.gameObject.SetActive(true);
            CS = GameObject.FindObjectOfType<CircleShrink>();
            CS.DeleteMe();
            pPower.OnPause();
        }
    }

    public void SurfaceTutorialOff()
    {
        pPower.OnPause();
        NewInstructionsText.gameObject.SetActive(false);
    }
}
