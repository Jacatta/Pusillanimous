using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour
{
    BackgroundBehaviors BB;
    GameManager GM;
    SquidBehavior SB;
    
    Image OceanBack;
    public GameObject[] BackGrounds;
    public float OG_XPosition;
    public float VertPos;
    public int SpeedDivisor = 1;
    public float BoxHeight;

   // private float GrowingInt;
    private int CurrentIndex;
    private int TopIndex;
    private float PreviousSpeed;

    // Use this for initialization
    void Start()
    {
        SB = FindObjectOfType<SquidBehavior>();
        GM = FindObjectOfType<GameManager>();

        BB = FindObjectOfType<BackgroundBehaviors>();
        GM.underTheSea = true;
      //  GrowingInt = 1f;

        if (BackGrounds.Length == 0)
        {
            Debug.Log("BackGround is not set");
            return;
        }

        OG_XPosition = BackGrounds[0].GetComponent<RectTransform>().anchoredPosition.x;
        BoxHeight = BackGrounds[0].GetComponent<RectTransform>().rect.height;
        CurrentIndex = 0;
        TopIndex = BackGrounds.Length - 1;
        PreviousSpeed = GM.sceneSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (SB.behaviorState.state == "EndOfLevel" || GM.paused == true)
        {
           // ScrollBackground();
            return;
        }

        VertPos = BackGrounds[CurrentIndex].GetComponent<RectTransform>().anchoredPosition.y;
        // Emerge from the Ocean
        if (PreviousSpeed > 0 && GM.sceneSpeed < 0)
            SwitchARoo();
        PreviousSpeed = GM.sceneSpeed;

        ScrollBackground();
      //  if (GM.splash)
         //   GrowingInt += .5f;



        // Moved the image to the top so it can scrolldown. 
        if (VertPos < -1000)
        {
            BackGrounds[CurrentIndex].GetComponent<RectTransform>().anchoredPosition = new Vector3(OG_XPosition, BackGrounds[TopIndex].GetComponent<RectTransform>().anchoredPosition.y + BoxHeight, BackGrounds[CurrentIndex].transform.position.z);
            CurrentIndex++;
            TopIndex++;
            if (CurrentIndex >= BackGrounds.Length)
                CurrentIndex = 0;
            if (TopIndex >= BackGrounds.Length)
                TopIndex = 0;

            BoxHeight = BackGrounds[CurrentIndex].GetComponent<RectTransform>().rect.height;

            if (GM.underTheSea)
            {
                //Do something with image/looks
            }
            else//if not under the sea
            {
                //Do something different with image/looks
            }

        }
        else if (VertPos > 1100 && GM.underTheSea == false)// Astrid is falling
        {
            BackGrounds[CurrentIndex].GetComponent<RectTransform>().anchoredPosition = new Vector3(OG_XPosition, BackGrounds[TopIndex].GetComponent<RectTransform>().anchoredPosition.y - BoxHeight, BackGrounds[CurrentIndex].transform.position.z);
            CurrentIndex--;
            TopIndex--;
            if (CurrentIndex < 0)
                CurrentIndex = BackGrounds.Length - 1;
            if (TopIndex < 0)
                TopIndex = BackGrounds.Length - 1;
            BoxHeight = BackGrounds[CurrentIndex].GetComponent<RectTransform>().rect.height;
        }
    }

    public void SwitchARoo()
    {
        Debug.Log("BG_Switcharoo");
        int temp = CurrentIndex;
        CurrentIndex = TopIndex;
        TopIndex = temp;
    }

    public void ScrollBackground()
    {
        if (GM.underTheSea && this.gameObject.tag == "TopSide_BackGround")
            return;

        for (int i = 0; i < BackGrounds.Length; i++)
        {
            BackGrounds[i].transform.position -= new Vector3(0f, GM.sceneSpeed / SpeedDivisor, 0f);
        }
    }
}
