using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour {
    pause Paus;
    public GameObject newBubble;
    RectTransform Rt;
    ParticleSystem PSB;
    // Use this for initialization
    void Start () {
        Rt = GetComponent<RectTransform>();
        Paus = GameObject.FindObjectOfType<pause>();

        PSB = GameObject.Find("OtherParticle").GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (PSB == null) { PSB = GameObject.FindObjectOfType<ParticleSystem>(); }
        if (!Paus.paused)
        {
          
            Rt.localScale = new Vector3(Rt.localScale.x +1.5f, Rt.localScale.y + 1.5f, Rt.localScale.z + 1.5f);
        }

        if(Rt.localScale.x >160f)
        {
            DeleteMe();
        }

    }

    public void OnMouseDown()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 100;

        PSB.Play();
    }

    public void DeleteMe()
    {
        if (PSB != null)
        {
            Destroy(PSB.gameObject);
        }
        Destroy(newBubble.gameObject);
    }
}
