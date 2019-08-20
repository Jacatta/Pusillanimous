using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency_Manager : MonoBehaviour
{

    Backpack BkPk;

    public Text coinCount;
    public Text gemCount;
    public Text bKeyCount;
    public Text sKeyCount;

    void Start()
    {
        BkPk= FindObjectOfType<Backpack>();
    }

    // Update is called once per frame
    void Update()
    {
        coinCount.text = BkPk.coinPurse.ToString();
        gemCount.text = BkPk.gemPurse.ToString();
        bKeyCount.text = BkPk.bronzeKeyStock.ToString();
        sKeyCount.text = BkPk.silverKeyStock.ToString();
    }
}
