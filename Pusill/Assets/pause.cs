using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public bool paused = false;

    Canvas MenuPop;
    // Use this for initialization
    void Start()
    {
        MenuPop = GameObject.Find("Menu Canvas").GetComponent<Canvas>();
        MenuPop.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        OnPause();
        
    }


    public void OnPause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            MenuPop.enabled = false;
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            MenuPop.enabled = true;
        }

    }
}
