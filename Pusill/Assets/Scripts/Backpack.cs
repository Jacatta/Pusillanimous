using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wardrobing;

public class Backpack : MonoBehaviour {

    public class baseCurrency
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public Image pic;
    }

    public bool bTutorial;

    public int coinPurse;
    public int gemPurse;
    public int rainbowGemPurse;
    public int bronzeKeyStock;
    public int silverKeyStock;
    public Wardrobe WrdRb;


    public List<baseCurrency> Currency;
    

    static Backpack instance;
    // Use this for initialization
    void Awake()
    {
        Debug.Log("BackPack Called");
        if (instance != null)
        {
            Debug.Log("I am a CLONE!?!>! destroyed. ");
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);

        // Currency[5];
        Currency = new List<baseCurrency>();
        
        for (int i =0;i<5;i++)
        {

          //  Debug.Log("i:" + i);
            
            baseCurrency tempCurrency = new baseCurrency();
            Currency.Add(tempCurrency);
            switch (i)
            {
                case 0:
                    Currency[i].Name = "Coins";
                    break;
                case 1:
                    Currency[i].Name = "BnzKeys";
                    break;
                case 2:
                    Currency[i].Name = "SlvrKeys";
                    break;
                case 3:
                    Currency[i].Name = "Gems";
                    break;
                case 4:
                    Currency[i].Name = "RainGems";
                    break;
            }
            Currency[i].Qty = 0;

        }



        Currency[0].Qty = 1001;

        WrdRb = new Wardrobe();
        bTutorial = false;
         

    }
	
	// Update is called once per frame
	void Update () {

        coinPurse = Currency[0].Qty;
        bronzeKeyStock = Currency[1].Qty;
        silverKeyStock = Currency[2].Qty;
        gemPurse = Currency[3].Qty;
        rainbowGemPurse = Currency[4].Qty;

    }


}
