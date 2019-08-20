using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TradeManager : MonoBehaviour
{

    Backpack BkPak;
    // Use this for initialization



    public class BaseItem
    {

        public static bool operator ==(BaseItem one, BaseItem two)
        {
            if (one.Name == two.Name)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(BaseItem one, BaseItem two)
        {
            if (one.Name == two.Name)
            {
                return false;
            }
            return true;
        }
        public void setImage(GameObject GO, Sprite img,bool isCost)
        {
            //pic = b;
            if(isCost==true)
            {
                GameObject j = GO.transform.Find("CostImg").gameObject;
                j.GetComponent<Image>().sprite = img;
            }
            else
            {
                GameObject j = GO.transform.Find("ProductImg").gameObject;
                j.GetComponent<Image>().sprite = img;
            }
 
        }
        
        public void setPrice(GameObject GO)
        {
            GameObject j = GO.transform.Find("CostQty").gameObject;
            j.GetComponent<Text>().text = Qty.ToString();
        }
        
        public string Name { get; set; }
        public int Qty { get; set; }
        //public Sprite pic;
    }
    
    public class TradeOption
    {
        public BaseItem Cost;
        public BaseItem Product;
        public GameObject ProductTradeGO;
        public GameObject CostTradeGO;
    }

    public GameObject Pannel;
    public GameObject tempTradeGO;

    public List<TradeOption> Trades;
    public List<BaseItem> Costs;
    public List<BaseItem> Products;
    public List<string> Currencies;
    public List<int> Quantities;

    public List<GameObject> CostGO;
    public List<Sprite> CurrencyImages;

    void Start()
    {
        BkPak = GameObject.FindObjectOfType<Backpack>();

        Trades = new List <TradeOption>();
        Costs = new List<BaseItem>();
        Products = new List<BaseItem>();
        Currencies = new List<string>();
        Quantities = new List<int>();


        Currencies.Add("Coins");
        Currencies.Add("Gems");
        Currencies.Add("BnzKeys"); 
        Currencies.Add("SlvrKeys");



        Quantities.Add(100);
        Quantities.Add(200);
        Quantities.Add(500);
        Quantities.Add(1000);

      //  GenCosts();
      //  GenProducts();
      //  GenerateTrades();
        TradeOverride();
        CreateTradeOptions();
        //tempTradeGO = GameObject.Find("Trade 1");

        GameObject Temp = GameObject.Find("Trade1/CostImg");
        Debug.Log(Temp.name);
    }

    public void RegenCost()
    {
        int i = Random.Range(0, Trades.Count-1);

        BaseItem CostT = new BaseItem();
        Debug.Log("Changing Trade: " + i);
        CostT.Name = RandomItem(2);
        CostT.Qty = RandomQty();

        Trades[i].Cost = CostT;
        CreateTradeOptions();

    }

    public void RegenerateDeals()
    {
        Costs.Clear();
        Products.Clear();
        Trades.Clear();


        GenCosts();
        GenProducts();
        GenerateTrades();
        CreateTradeOptions();
    }


    // Update is called once per frame
    void Update()
    {

    }
   
    public void GenCosts()
    {

        for(int i =0; i <4;i++)
        {
            BaseItem CostT = new BaseItem();
            CostT.Name = RandomItem(2);//Passing in 2 keeps the Costs to 2 items - Gems & Coins
            CostT.Qty = RandomQty();
            Costs.Add(CostT);
            Debug.Log(Costs[i].Name);
            Debug.Log(Costs[i].Qty);
        }
    }

    public void GenProducts()
    {

        for (int i = 0; i < 4; i++)
        {
            BaseItem ProductT = new BaseItem();
            ProductT.Name = RandomItem(4);//Passing in 4 expands the products to all 4 items - Gems & Coins & BrnzKey & SlvrKey
            ProductT.Qty = RandomQty();
            Products.Add(ProductT);
            Debug.Log(Products[i].Name);
            Debug.Log(Products[i].Qty);
        }
    }

    public void GenerateTrades()
    {
        
        for (int i = 0; i < 4; i++)
        {
            int x = Random.Range(0, 3);

            TradeOption TradeT = new TradeOption();
            TradeT.Cost = Costs[x];
            TradeT.Product = Products[x];


            //Verify Trade is logical
           // CheckTrade(TradeT);


            Trades.Add(TradeT);

        }
        /*
        BaseCurrency Coin = new BaseCurrency();
        Coin.Name = "Coins";
        Coin.Qty = 1000;

        BaseCurrency Gem = new BaseCurrency();
        Gem.Name = "Gems";
        Gem.Qty = 2;

        TradeOption Trade = new TradeOption();
        Trade.Cost = Coin;
        Trade.Product = Gem;


        Trades.Add(Trade);
        tempTradeGO = GameObject.Find("Trade 1");
        */
    }

    public void CheckTrade(TradeOption tradeT)
    {
        if (tradeT.Cost.Name == "Gem")
        { }
    }

    public void CreateTradeOptions()
    {
        int i = 0;
        foreach (TradeOption T in Trades)
        {
            /*
            Debug.Log("#oftrades: " + i);
            Debug.Log("TCostName: "+ T.Cost.Name);
            Debug.Log("TProduct" +
                "Name: " + T.Product.Name);
                */
            int x = FindItem(T.Cost);
            Debug.Log("Cost is: " +T.Cost.Name);
            Debug.Log("Cost" + i + "CostImg: "+ x + " = "+CurrencyImages[x].name);
           // Debug.Log("Trade GO: " + i + " = " + CostGO[i].name);
            T.Cost.setImage(CostGO[i],CurrencyImages[x],true);//How can i make this a class function? public void BaseCurrency::setImage()
            T.Cost.setPrice(CostGO[i]);

            x = FindItem(T.Product);
            Debug.Log("Product is: " + T.Product.Name);
            Debug.Log("Product "+ i +"ProductImg: " + x + " = " + CurrencyImages[x].name); 
            T.Product.setImage(CostGO[i], CurrencyImages[x],false);
            //T.Product.setPrice(CostGO[i]);

            i++;
        }
    }

    public string RandomItem(int i)
    {
        int select = Random.Range(0, i-1);
        Debug.Log("Random image chosen: " + select);
        return Currencies[select];
    }

    public int RandomQty()
    {
        int select = Random.Range(0, 3);
        return Quantities[select];
    }
    
    public void TradeOverride()
    {

        // Debug.Log("Trade Count: "+ Trades.Count);
        //TRADE1
        BaseItem BC1 = new BaseItem();
        BC1.Name = "Coins";
        BC1.Qty = 100;

        BaseItem BP1 = new BaseItem();
        BP1.Name = "BnzKeys";
        BP1.Qty = 1;

        TradeOption TradeA = new TradeOption();
        TradeA.Cost = BC1;
        TradeA.Product = BP1;
        Trades.Add(TradeA);
        //Trades[0] = TradeA;

        //TRADE2
        BaseItem BC2 = new BaseItem();
        BC2.Name = "Coins";
        BC2.Qty = 1000;

        BaseItem BP2 = new BaseItem();
        BP2.Name = "Gems";
        BP2.Qty = 1;

        TradeOption TradeB = new TradeOption();
        TradeB.Cost = BC2;
        TradeB.Product = BP2;
        Trades.Add(TradeB);
        //Trades[1] = TradeB;

        //TRADE3
        BaseItem BC3 = new BaseItem();
        BC3.Name = "Coins";
        BC3.Qty = 1000;

        BaseItem BP3 = new BaseItem();
        BP3.Name = "SlvrKeys";
        BP3.Qty = 1;

        TradeOption TradeC = new TradeOption();
        TradeC.Cost = BC3;
        TradeC.Product = BP3;
        Trades.Add(TradeC);
        //Trades[2] = TradeC;

        //Debug.Log("Trades[2].Cost.Name: " + Trades[2].Cost.Name);

        //TRADE4
        BaseItem BC4 = new BaseItem();
        BC4.Name = "Gems";
        BC4.Qty = 1;

        BaseItem BP4 = new BaseItem();
        BP4.Name = "SlvrKeys";
        BP4.Qty = 1;

        TradeOption TradeD = new TradeOption();
        TradeD.Cost = BC4;
        TradeD.Product = BP4;
        Trades.Add(TradeD);
        //Trades[3] = TradeD;

        //Debug.Log("Trades[3].Cost.Name: " + Trades[3].Cost.Name);
        //Trades[3] = TradeD;
       // Debug.Log("Trade Count: " + Trades.Count);

    }

    public void CostOverride()
    {
        BaseItem Temp = new BaseItem();
        Temp.Qty = 50;

        //Send to time delay?

        Trades[0].Cost.Qty = 50;
    }

    IEnumerator CostDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    public void ValidifyTrade(TradeOption Trade)
    {
        if (BkPak.Currency[FindItem(Trade.Cost)].Qty < Trade.Cost.Qty)
        {
            Debug.Log("Not enough Cash,   Stranger");
        }
        else
        {
            MakeTrade(Trade);
        }
    }

    public void MakeTrade(TradeOption Trade)
    {
        BkPak.Currency[FindItem(Trade.Cost)].Qty -= Trade.Cost.Qty;

        BkPak.Currency[FindItem(Trade.Product)].Qty += Trade.Product.Qty;
    }

    public int FindItem(BaseItem Currency)
    {
        //Debug.Log("currency name: " + Currency.Name);
        switch (Currency.Name)
        {
            case "Coins":
                return 0;
                break;
            case "BnzKeys":
                return 1;
                break;
            case "SlvrKeys":
                return 2;
                break;
            case "Gems":
                return 3;
                break;
            case "RainGems":
                return 4;
                break;

            default: Debug.LogError("Danger Will Robinson"); return 5;
        }

    }

    public void TradeOne()
    {
        ValidifyTrade(Trades[0]);
    }

    public void TradeTwo()
    {
        ValidifyTrade(Trades[1]);
    }

    public void TradeThree()
    {
        ValidifyTrade(Trades[2]);
    }

    public void TradeFour()
    {
        ValidifyTrade(Trades[3]);
    }


    //PLACE HOLDERS
    public void CoinsToBznKey()
    {
        if (BkPak.coinPurse < 100)
        {
           // Debug.Log(BkPak.coinPurse);
            //Debug.LogError("Not Enough Coins");
            return;
        }
        BkPak.Currency[0].Qty -= 100;
        BkPak.Currency[1].Qty += 1;

    }
    public void CoinsToSlvrKey()
    {
        if (BkPak.coinPurse < 500)
        {
           // Debug.Log(BkPak.coinPurse);
           // Debug.LogError("Not Enough Coins");
            return;
        }
        BkPak.Currency[0].Qty -= 500;
        BkPak.Currency[2].Qty += 1;
    }

}
