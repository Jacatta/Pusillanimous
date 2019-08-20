using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency_Level_Manager : MonoBehaviour
{

    ScoreKeeper Sk;
    public Backpack BkPak;
    public Text CoinTracker;
    AudioSource CoinCountAudio;
    ParticleSystem PS;
    TitleManager ResultsManager;

    // Use this for initialization
    void Start()
    {
        Sk = GameObject.FindObjectOfType<ScoreKeeper>();
        BkPak = GameObject.FindObjectOfType<Backpack>();
        ResultsManager = GameObject.FindObjectOfType<TitleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResultsTime()
    {
        Debug.Log("Start CoRoutine");
        StartCoroutine(CoinCounter());
        PS = GameObject.Find("coin_Animated").GetComponent<ParticleSystem>();
        var emiss = PS.emission;
        emiss.rateOverTime = Sk.coin / 3;
        PS.Play();
    }

    IEnumerator CoinCounter()
    {
        CoinTracker = GameObject.Find("Coin_Int").GetComponent<Text>();
        CoinCountAudio = GameObject.Find("UI_Piggy").GetComponent<AudioSource>();

        BkPak.Currency[0].Qty += Sk.coin;
        ResultsManager.numToSubtract = Sk.coin;
        ResultsManager.bSubtract = true;

        int i = 0;
        for (i = 0; i < Sk.coin; i++)
        {
            yield return new WaitForSeconds(.01f);
            //Debug.Log("Play Coin Sound here");
            CoinCountAudio.Play();
            CoinTracker.text = "= " + (i + 1).ToString();
        }
        ResultsManager.bSubtract = false;
    }

    public bool PayCost(string n, int qty)
    {

        Backpack.baseCurrency tempCurrency = new Backpack.baseCurrency();
        tempCurrency.Name = n;
        tempCurrency.Qty = qty;
        Debug.Log("tempcurrency name: " + tempCurrency.Name);

        int i=0;
        switch (tempCurrency.Name)
        {
            case "coinPrize":
                BkPak.Currency[0].Qty += tempCurrency.Qty;
                i = 0;
                break;
            case "BrnzKey":
              
                i = 1;
                break;
            case "SlvrKeys":
                BkPak.Currency[2].Qty += tempCurrency.Qty;
                i = 2;
                break;
            case "gemPrize":
                BkPak.Currency[3].Qty += tempCurrency.Qty;
                break;
            case "RainGems":
                BkPak.Currency[4].Qty += tempCurrency.Qty;
                break;
        }

        if (BkPak.Currency[i].Qty > 0) {
            BkPak.Currency[i].Qty--; return true; } else { return false; }
            
    }

    public void AdjustCurrency(string n, int qty)
    {
        Backpack.baseCurrency tempCurrency = new Backpack.baseCurrency();
        tempCurrency.Name = n;
        tempCurrency.Qty = qty;
        Debug.Log("tempcurrency name: " + tempCurrency.Name);
        switch (tempCurrency.Name)
        {
            case "coinPrize":
                BkPak.Currency[0].Qty += tempCurrency.Qty;
                break;
            case "keyPrize":
                BkPak.Currency[1].Qty += tempCurrency.Qty;
                break;
            case "SlvrKeys":
                BkPak.Currency[2].Qty += tempCurrency.Qty;
                break;
            case "gemPrize":
                BkPak.Currency[3].Qty += tempCurrency.Qty;
                break;
            case "RainGems":
                BkPak.Currency[4].Qty += tempCurrency.Qty;
                break;
        }
    }
}