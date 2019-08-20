using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnobBehavior : MonoBehaviour {

    BackgroundBehaviors BB;
    GameManager GM;
    public int hit = 0;
    private float incriment;
    Canvas final;
    double height;
    double center;

    // Use this for initialization
    void Start () {
        //transform.position.y = Random.Range(0,400);
        //incriment = -.1f;
        //  final = GameObject.Find("FinalScreen").GetComponentInParent<Canvas>();
        //  final.enabled = false;
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = FindObjectOfType<GameManager>();
        GameObject altbar = GameObject.Find("Altitude Bar");
        height = altbar.GetComponent<RectTransform>().rect.height / 3;
        center = altbar.transform.position.y;
        transform.position = new Vector3(transform.position.x, Camera.main.WorldToScreenPoint(GameObject.Find("Altitude Bar").gameObject.transform.position).y,transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {


        KnobMode();
  


    }
    
    public void KnobMode()
    {
        if (transform.position.y < -250)
        {
            transform.position = new Vector3(-157f, -250f, 0f);
        }
        else if (transform.position.y > 200)
        {
            transform.position = new Vector3(-157f, 200f, 0f);
        }
        else
        {
            transform.position = new Vector3(-157f, transform.position.y + GM.distance, 0f);
        }
            /*
              }

              transform.position = new Vector3(0,(transform.position.y+BB.distance),0f);
            /*

              switch (hit)
              {
                  case 0:
                      incriment -= .03f;
                      break;
                  case 1:
                      incriment = 1.0f;
                      hit = 0;
                      break;
                  case 2:
                      incriment = 1.5f;
                      hit = 0;
                      break;
                  case 3:
                      incriment = 2.0f;
                      hit = 0;
                      break;
                  default:
                      hit = 0;
                      break;
              }
              */
        }
    /*
        void EndGame()
    {
        if (!final.enabled)
        {
            ScoreKeeper sk = GameObject.FindObjectOfType<ScoreKeeper>();
            //GameObject.Find("Score").GetComponent<Text>().text += (sk.scoreInt).ToString();
            Debug.Log(GameObject.Find("Score").GetComponent<Text>().text);
            GameObject.Find("Score").GetComponent<Text>().text = "Score: " + sk.scoreInt.ToString();
            Debug.Log(GameObject.Find("Score").GetComponent<Text>().text);
            GameObject.Find("Coins").GetComponent<Text>().text += sk.coinInt.ToString();
            final.enabled = true;
        }
        
    }
    

    private IEnumerator TargetHit(float waitTime)//IncreasesAltitude
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);//(0f, 1f,0f);
        incriment = 1f;                                                                                                 // Debug.Log("Time: "+Time.time);
        yield return new WaitForSeconds(waitTime);
        

    }
    

    private IEnumerator WaitFor(float waitTime)//Holds Altitude
    {
        Debug.Log("Got to wait");
        incriment = 0f;
        yield return new WaitForSeconds(waitTime);
        
    }
    */
 
}
