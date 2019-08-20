using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountCoins : MonoBehaviour
{

    ScoreKeeper Sk;
    Backpack BkPak;
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
        for(i = 0; i < Sk.coin; i++)
        {
            yield return new WaitForSeconds(.01f);
            Debug.Log("Play Coin Sound here");
            CoinCountAudio.Play();
            CoinTracker.text = "= " + (i+1).ToString();
        }
        ResultsManager.bSubtract = false;
    }
}
