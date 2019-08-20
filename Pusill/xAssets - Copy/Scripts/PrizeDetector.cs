using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeDetector : MonoBehaviour {

    Currency_Level_Manager CurrencyMan;
    BackgroundBehaviors BB;

    public Animation Treasure_Open;
    public Animator Treasure;
    public Animator Reward;

    public GameObject NoCanHaz;



    private IEnumerator coroutine;
    // Use this for initialization
    void Start () {
        // PrizeCan.enabled = false;
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        CurrencyMan = GameObject.FindObjectOfType<Currency_Level_Manager>();

        NoCanHaz = GameObject.Find("NoCanHaz");
        NoCanHaz.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if(BB.LevelEnd==false || other.tag != "Collectable") { return; }

        switch (other.name)
        {
            case "coinPrize":
                break;
            case "gemPrize":
                break;
            case "keyPrize":
                break;
            default:
                break;
        }
        Debug.Log("Landed on: " + other.name);
        try { other.GetComponent<Animator>().SetTrigger("GetReward"); } catch { }
        
        //other.GetComponent<RectTransform>().sizeDelta = new Vector2(1f,2f);
            // SEND TO PRIZE SCREEN
        
        CurrencyMan.AdjustCurrency(other.name, 1);
       
        //CURRENCY_LEVEL_MANAGER- ADD PRIZE TO STUFF

    }
    */

    public void NeedCurrencyForPrize()
    {
        if(NoCanHaz.activeSelf==false)
        {
            NoCanHaz.SetActive(true);
        }else
            NoCanHaz.SetActive(false);
    }


    public void Prize()
    {
        Debug.Log("Prize Opened");
        Debug.Log(CurrencyMan);
        if (CurrencyMan.PayCost("BrnzKey", 1))
        {
            Debug.Log("Had a key, has Payed");
            StartCoroutine(OpenChest());
        }
        else
        {
            NeedCurrencyForPrize();
            
            Debug.Log("No Keys, Cant Pay");
        }
       // Pay();
        
        //Enlarge chest
        //Enable poof
        //Change Prize info
        //Enable Prize animation
    }

    public void Pay()
    {

    }


    IEnumerator OpenChest()
    {
        Treasure.SetTrigger("Open");   //Chest Enlarging Animation
        yield return new WaitForSeconds(.1f);
        Reward.SetTrigger("GetReward");
        CurrencyMan.AdjustCurrency("gemPrize",1);
        yield return new WaitForSeconds(2f);



    }
}
