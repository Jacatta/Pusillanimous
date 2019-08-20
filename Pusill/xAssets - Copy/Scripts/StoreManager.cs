using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler
{

    Backpack BkPak;
    TradeManager TrdMgr;

    public class ApparelItem
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public Sprite pic;
        public string Ability;
        public string tag;
    }

    public class Outfit
    {
        public ApparelItem HeadG { get; set; }
        public ApparelItem BodyG { get; set; }
        public ApparelItem MiscG { get; set; }

    }

    // Use this for initialization
    public GameObject Tab1;
    public GameObject Tab2;
    public GameObject Tab3;
    public GameObject Tint;

    public GameObject CPane;
    public GameObject RPane;
    public GameObject LPane;

    public GameObject PurchaseButton;
    public GameObject Wardrobe_Head;
    public GameObject Wardrobe_Body;
    public GameObject Wardrobe_Misc;

    // public List<ApparelItem> Apparel;

    public List<ApparelItem> HeadStock;
    public List<ApparelItem> BodyStock;
    public List<ApparelItem> MiscStock;

    public ApparelItem HotSeat;
    
    public int ItemSelected;

    public GameObject SelectedItem;
    public Text SelectedCost;
    public GameObject SelectedFX;

    public GameObject NextItem;
    public GameObject PrevItem;

    // public List<GameObject> StoreItems;
    public Transform[] Wardrobe_HeadGear;
    public Transform[] Wardrobe_BodyGear;
    public Transform[] Wardrobe_MiscGear;


    public Text StoreCoins;
    //  public GameObject R2Pane;

    private Animator newitemAnim;
    private Animator newCostAnim;
    private Animator newFXAnim;

    private string ActiveStore;


    void Start()
    {

        BkPak = GameObject.FindObjectOfType<Backpack>();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        Tab1 = GameObject.FindGameObjectWithTag("Tab1");
        Tab2 = GameObject.FindGameObjectWithTag("Tab2");
        Tab3 = GameObject.FindGameObjectWithTag("Tab3");
        Tint = GameObject.FindGameObjectWithTag("Tint");

        Tab1.GetComponent<Canvas>().sortingOrder = 3;
        Tab2.GetComponent<Canvas>().sortingOrder = 1;
        Tab3.GetComponent<Canvas>().sortingOrder = 0;
        Tint.GetComponent<Canvas>().sortingOrder = 2;
        ActiveStore = "Tab1";

        CPane = GameObject.FindGameObjectWithTag("CPane");
        RPane = GameObject.FindGameObjectWithTag("RPane");
        LPane = GameObject.FindGameObjectWithTag("LPane");
       

        LPane.GetComponent<Animator>().SetTrigger("CtoL");
        RPane.GetComponent<Animator>().SetTrigger("CtoR");

   
        PurchaseButton = GameObject.FindGameObjectWithTag("Purchase");

        //Apparel = new List<ApparelItem>();
        HeadStock = new List<ApparelItem>();
        BodyStock = new List<ApparelItem>();
        MiscStock = new List<ApparelItem>();


        //TODO Jump to helper functions:
        //Generate HeadStock
        //Generate BodtStock
        //Generate MiscStock



        Outfit preset1 = new Outfit();

        for (int i = 0; i < 10; i++)//TO DO . THIS CANT STAY STATIC
        {
           // GameObject storeTemp = new GameObject();
            ApparelItem temp = new ApparelItem();

            switch (i)
            {
                case 0:
                    temp.Name = "None";
                    temp.Cost = 0;
                    temp.Ability = "ol'Natural";
                    temp.pic = GameObject.Find("Head_None").GetComponent<Image>().sprite;
                    temp.tag = "HeadGear";
                    break;
                case 1:
                    temp.Name = "None";
                    temp.Cost = 0;
                    temp.Ability = "ol'Natural";
                    temp.pic = GameObject.Find("Head_None").GetComponent<Image>().sprite;
                    temp.tag = "BodyGear";
                    break;
                case 2:
                    temp.Name = "None";
                    temp.Cost = 0;
                    temp.Ability = "ol'Natural";
                    temp.pic = GameObject.Find("Head_None").GetComponent<Image>().sprite;
                    temp.tag = "MiscGear";
                    break;
                case 3:
                    temp.Name = "TopHat";
                    temp.Cost = 1000;
                    temp.Ability = "Dapper";
                    temp.pic = GameObject.Find("Head_Tophat").GetComponent<Image>().sprite;
                    temp.tag = "HeadGear";
                    break;
                case 4:
                    temp.Name = "Maracca";
                    temp.Cost = 200;
                    temp.Ability = "Graced";
                    temp.pic = GameObject.Find("Misc_Maracca").GetComponent<Image>().sprite;
                    temp.tag = "MiscGear";
                    break;
                case 5:
                    temp.Name = "Monocle";
                    temp.Cost = 450;
                    temp.Ability = "+ %100 Luck";
                    temp.pic = GameObject.Find("Misc_Monocle").GetComponent<Image>().sprite;
                    temp.tag = "BodyGear";
                    break;
                case 6:
                    temp.Name = "BallCap";
                    temp.Cost = 350;
                    temp.Ability = "+ %200 Luck";
                    temp.pic = GameObject.Find("Head_BallCap").GetComponent<Image>().sprite;
                    temp.tag = "HeadGear";
                    break;
                case 7:
                    temp.Name = "Cowboy";
                    temp.Cost = 900;
                    temp.Ability = "+ 5 Grit";
                    temp.pic = GameObject.Find("Head_Cowboy").GetComponent<Image>().sprite;
                    temp.tag = "HeadGear";
                    break;
                case 8:
                    temp.Name = "StarScar";
                    temp.Cost = 200;
                    temp.Ability = "+ 5 fun";
                    temp.pic = GameObject.Find("Body_StarScar").GetComponent<Image>().sprite;
                    temp.tag = "BodyGear";
                    break;
                case 9:
                    temp.Name = "BridgeBadge";
                    temp.Cost = 200;
                    temp.Ability = "+ 5 fun";
                    temp.pic = GameObject.Find("Misc_BridgeBadge").GetComponent<Image>().sprite;
                    temp.tag = "MiscGear";
                    break;

            }

            //SORT ITEMS
            sortStock(temp);

            preset1.HeadG = temp;   

            newitemAnim = SelectedItem.GetComponent<Animator>();
            newCostAnim = SelectedCost.GetComponent<Animator>();
            newFXAnim = SelectedFX.GetComponent<Animator>();
        }

        ItemSelected = 0;




        Wardrobe_HeadGear = Wardrobe_Head.GetComponentsInChildren<Transform>();
        Wardrobe_BodyGear = Wardrobe_Body.GetComponentsInChildren<Transform>();
        Wardrobe_MiscGear = Wardrobe_Misc.GetComponentsInChildren<Transform>();

        UnSet_Gear();//Turn off all atire
        ResetShop(HeadStock);
        // FindArticle(Apparel[ItemSelected]);//TODO,replace with preset
        //Set atire as preSet1
        //Set_PresetGear(preset1);
    }

    // Update is called once per frame
    void Update()
    {
        StoreCoins.text = BkPak.Currency[0].Qty.ToString();
    }

    public void sortStock(ApparelItem item)
    {


        switch (item.tag)
        {
            case "HeadGear":
                HeadStock.Add(item);
                SelectedCost.text = HeadStock[ItemSelected].Cost.ToString();
                SelectedItem.GetComponent<Image>().sprite = HeadStock[ItemSelected].pic;
                break;
            case "BodyGear":
                BodyStock.Add(item);
                SelectedCost.text = BodyStock[ItemSelected].Cost.ToString();
                SelectedItem.GetComponent<Image>().sprite = BodyStock[ItemSelected].pic;
                break;
            case "MiscGear":
                MiscStock.Add(item);
                SelectedCost.text = MiscStock[ItemSelected].Cost.ToString();
                SelectedItem.GetComponent<Image>().sprite = MiscStock[ItemSelected].pic;
                break;
            default:
                break;

        }

    }

    public void Set_PresetGear(Outfit set1)
    {

    }
    
    public void UnSet_Gear()
    {
        //Transform[] allChildren = Wardrobe_Head.GetComponentsInChildren<Transform>();
        foreach (Transform child in Wardrobe_HeadGear)
        {
            if(child!= Wardrobe_HeadGear[0]) { child.GetComponent<Image>().enabled = false; }
        }
        foreach (Transform child in Wardrobe_BodyGear)
        {
            if (child != Wardrobe_BodyGear[0]) { child.GetComponent<Image>().enabled = false; }
        }
        foreach (Transform child in Wardrobe_MiscGear)
        {
            if (child != Wardrobe_MiscGear[0]) { child.GetComponent<Image>().enabled = false; }
        }
    }
    
    public void Set_Gear(ApparelItem Item)
    {
        Transform[] t;
        t = new Transform[0];
        
        if(ActiveStore== "Tab1")
        {
            t = Wardrobe_HeadGear;
        }
        else if (ActiveStore == "Tab2")
        {
            t = Wardrobe_BodyGear;

        }
        else if (ActiveStore == "Tab3")
        {
            t = Wardrobe_MiscGear;
        }


        foreach (Transform child in t)
        {

            //child.SetActive(false);
           // Debug.Log(Wardrobe_HeadGear.Length);
            if (child != t[0])
            {
                try { child.GetComponent<Image>().enabled = false; }
                catch { Debug.Log("GotCaught Trying"); };

                Debug.Log("child.name == Item.Name ||" + child.name + " == " + Item.Name);
                if (child.name == Item.Name)
                { child.GetComponent<Image>().enabled = true; }//TURNS CORRECT ONE ON
            }
        }
    }

    public void FindArticle(ApparelItem Item)
    {
        Set_Gear(Item);
    }

    public void SetAttire(GameObject Outfit)
    {
        
        //Turn on Headgear
        //Turn on Bodygear
        //Turn on Miscgear
    }

  

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(1);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log(eventData.pointerEnter.GetComponent<Collider2D>().name);//collider.name);
    }


    public void OnPointerClick(PointerEventData eventData) 
    {
        Debug.Log(eventData.pointerEnter.GetComponent<Collider>().name);
    }

    public void ShowMeTab1()
    {
        Debug.Log("Hit tab1");
        Tab1.GetComponent<Canvas>().sortingOrder = 3;
        Tab2.GetComponent<Canvas>().sortingOrder = Tab2.GetComponent<Canvas>().sortingOrder - 1;
        Tab3.GetComponent<Canvas>().sortingOrder = Tab3.GetComponent<Canvas>().sortingOrder - 1;
        ActiveStore = "Tab1";

        //Reset Store
        ResetShop(HeadStock);


    }
    public void ShowMeTab2()
    {
        Debug.Log("Hit tab2");
        Tab1.GetComponent<Canvas>().sortingOrder = Tab1.GetComponent<Canvas>().sortingOrder - 1;
        Tab2.GetComponent<Canvas>().sortingOrder = 3;
        Tab3.GetComponent<Canvas>().sortingOrder = Tab3.GetComponent<Canvas>().sortingOrder - 1;
        ActiveStore = "Tab2";

        ResetShop(BodyStock);
    }
    public void ShowMeTab3()
    {
        Debug.Log("Hit tab3");
        Tab1.GetComponent<Canvas>().sortingOrder = Tab1.GetComponent<Canvas>().sortingOrder - 1;
        Tab2.GetComponent<Canvas>().sortingOrder = Tab2.GetComponent<Canvas>().sortingOrder - 1;
        Tab3.GetComponent<Canvas>().sortingOrder = 3;
        ActiveStore = "Tab3";
        ResetShop(MiscStock);
    }

    public void ResetShop(List<ApparelItem> stock)
    {
        ItemSelected = 0;
        SelectedItem.GetComponent<Image>().sprite = stock[ItemSelected].pic;
        SelectedCost.text = stock[ItemSelected].Cost.ToString();

        newitemAnim.SetTrigger("ItemSwitched");
        newCostAnim.SetTrigger("ItemSwitched");
        newFXAnim.SetTrigger("ItemSwitched");
        // SelectedItem.GetComponent<Image>().sprite = Apparel[ItemSelected].pic;
    }

    public void ToggleRight()
    {
        //PurchaseButton.transform.SetParent(LPane.transform);


        List<ApparelItem> AI = new List<ApparelItem>();

        if (ActiveStore == "Tab1")
        {
            AI = HeadStock;
        }
        else if (ActiveStore == "Tab2")
        {
            AI = BodyStock;
        }
        else if (ActiveStore == "Tab3")
        {
            AI = MiscStock;
        }

        if (ItemSelected < AI.Count) ItemSelected++;
        if (ItemSelected == AI.Count) ItemSelected = 0;

        SelectedItem.GetComponent<Image>().sprite = AI[ItemSelected].pic;

        //WARDROBE SEARCH - Activate Attire
        Set_Gear(AI[ItemSelected]);//ENABLES WARDROBE ON AVATAR

        //HeadSwitch(Apparel[ItemSelected].Name);
        SelectedCost.text = AI[ItemSelected].Cost.ToString();

        newitemAnim.SetTrigger("ItemSwitched");
        newCostAnim.SetTrigger("ItemSwitched");
        newFXAnim.SetTrigger("ItemSwitched");

        LPane.GetComponent<Animator>().SetTrigger("CtoR");
        CPane.GetComponent<Animator>().SetTrigger("CtoR");
        RPane.GetComponent<Animator>().SetTrigger("CtoR");

        RPane.name = "Pannel_L";
        CPane.name = "Pannel_R";
        LPane.name = "Pannel_C";
    }
    public void ToggleLeft()
    {
        List<ApparelItem> AI = new List<ApparelItem>();

        if (ActiveStore == "Tab1")
        {
            AI = HeadStock;
        }
        else if (ActiveStore == "Tab2")
        {
            AI = BodyStock;
        }
        else if (ActiveStore == "Tab3")
        {
            AI = MiscStock;
        }

        if (ItemSelected == 0) ItemSelected = AI.Count;
        if (ItemSelected > 0) ItemSelected--;

        SelectedItem.GetComponent<Image>().sprite = AI[ItemSelected].pic;


        SelectedCost.text = AI[ItemSelected].Cost.ToString();//SetCost

        Set_Gear(AI[ItemSelected]);//ENABLES WARDROBE ON AVATAR

        newitemAnim.SetTrigger("ItemSwitched");
        newCostAnim.SetTrigger("ItemSwitched");
        newFXAnim.SetTrigger("ItemSwitched");
        //Toggle Wardrobe

        CPane.GetComponent<Animator>().SetTrigger("CtoL");
        RPane.GetComponent<Animator>().SetTrigger("CtoL");
        LPane.GetComponent<Animator>().SetTrigger("CtoL");
                
        RPane.name = "Pannel_C";
        CPane.name = "Pannel_L";
        LPane.name = "Pannel_R";
     
    }

    public IEnumerator rFade()
    {
     
        yield return new WaitForSeconds(1f);
    }

    public void validateSelected(int Item, List<ApparelItem> stock)
    {
        //Debug.Log("attemp 1: "+ Apparel[Item].Cost);
       // Debug.Log("attemp 1: " + BkPak.Currency[0].Qty);

        if (BkPak.Currency[0].Qty> stock[Item].Cost)
        {
            Debug.Log("Trade SUCCESSFULL");
        }else
        Debug.Log("Trade UNSUCCESSFUL!!K!EK!K!KE !! Not Enough Cash.... stranger!");
    }

    public void TradeOne()
    {
        //validateSelected(0);
        //purchaseSelected(0);// CODE ALREADY EXISTS IN TRADE MANAGER> TO DO < Consolidate. 
    }

    public void purchaseSelected(int item, List<ApparelItem> stock)
    {
        BkPak.Currency[0].Qty -= stock[item].Cost;

    }






}
