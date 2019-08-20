using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Wardrobing;

public class StoreManager : MonoBehaviour
{

    Backpack BkPak;
    Wardrobe WrdRb;

    public Text PresetNumber;
    public Text presetaidtext;

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

    public Outfit DressingRoom;

    public ApparelItem HotSeat;
    public Text ItemAbility;
    public int ItemSelected;

    public GameObject SelectedItem;
    public Text SelectedCost;
    public GameObject SelectedFX;

    public GameObject NextItem;
    public GameObject PrevItem;

    // public List<GameObject> StoreItems;


    public GameObject Popup_NotEnough_Item;
    public GameObject Popup_DidntPurchase_Set;
    public GameObject CPopup_setPreset;
    public GameObject Pop_NtEnoughCash_yesBtn;
    public Canvas CPopup_Verify;
   // public Text StoreCoins;
    //  public GameObject R2Pane;

    private Animator newitemAnim;
    private Animator newCostAnim;
    private Animator newFXAnim;

    private string ActiveStore;
    public Text owedForSet;

    ParticleSystem PS_confirm;


    void Start()
    {
        BkPak = GameObject.FindObjectOfType<Backpack>();
        WrdRb = BkPak.WrdRb;
      
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

        CPopup_setPreset = GameObject.Find("PopUp_VerifyPresetChange");
       // CPopup_Verify = GameObject.Find("PopUp_UnpurchasedItems").GetComponentInParent<Canvas>();
        PurchaseButton = GameObject.FindGameObjectWithTag("Purchase");

        Popup_NotEnough_Item = GameObject.Find("PopUp_NotEnoughForItem");
        Popup_DidntPurchase_Set = GameObject.Find("PopUp_NotEnoughForSet");
        Pop_NtEnoughCash_yesBtn = GameObject.Find("NtEnough_YesButton");
        PS_confirm=GameObject.Find("Confirm Particles").GetComponent<ParticleSystem>();

        DressingRoom = new Outfit();
        DressingRoom.Assign(WrdRb.CurrentSet);

        newitemAnim = SelectedItem.GetComponent<Animator>();
        newCostAnim = SelectedCost.GetComponent<Animator>();
        newFXAnim = SelectedFX.GetComponent<Animator>();

       // CPopup_setPreset.gameObject.SetActive(false);
        //CPopup_Verify.gameObject.SetActive(false);

        ItemSelected = 0;

        WrdRb.Wardrobe_HeadGear = Wardrobe_Head.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_BodyGear = Wardrobe_Body.GetComponentsInChildren<Transform>();
        WrdRb.Wardrobe_MiscGear = Wardrobe_Misc.GetComponentsInChildren<Transform>();

        WrdRb.UnSet_Gear();//Turn off all atire
        ResetShop(WrdRb.HeadStock);
        HotSeat = WrdRb.HeadStock[ItemSelected];
        WrdRb.PutOn_Outfit(DressingRoom);

        Popup_NotEnough_Item.SetActive(false);
        Popup_DidntPurchase_Set.SetActive(false);
        CPopup_setPreset.SetActive(false);
        PS_confirm.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


      //  StoreCoins.text = BkPak.Currency[0].Qty.ToString();


        try
        {
            ItemAbility.text = HotSeat.Ability.ToString().ToUpper();
        }
        catch { ItemAbility.text = "Ol'Natural"; }

        presetaidtext.text = "Tap ^ to customize "+ PresetNumber.text;// = presetaidtext;
}

    public void Unset_Gear()
    {
        WrdRb.UnSet_Gear();
        DressingRoom.Assign(WrdRb.BirfdaySuit);
    }


    public void PurchaseItem()
    {

        PurchaseButton.GetComponent<Button>().interactable = false;
        PS_confirm.gameObject.SetActive(false);


        if (validatePurchase(HotSeat))
        {
            Debug.Log("Payment Confirmed");
            Debug.Log(HotSeat.Name + " was purchased");//FinalizeTransation(HotSeat);
            Debug.Log("Item is Owned");

        }
        else
        {
           
            //Popup_NotEnough_Item.SetActive(true);
            Debug.Log("Trade UNSUCCESSFUL!!Not Enough Cash.... stranger!");
        }
    }

    public void PurchaseOutfit()
    {

        int tempCost = 0;
        bool ownsAll = true;

        if (DressingRoom.HeadG.owned == false )
        {
            tempCost += DressingRoom.HeadG.Cost;
            ownsAll = false;
            WrdRb.ShoppingCart.Add(DressingRoom.HeadG);
        }
        if (DressingRoom.BodyG.owned == false)
        {
            tempCost += DressingRoom.BodyG.Cost;
            ownsAll = false;
            WrdRb.ShoppingCart.Add(DressingRoom.BodyG);
        }
        if (DressingRoom.MiscG.owned == false)
        {
            tempCost += DressingRoom.MiscG.Cost;
            ownsAll = false;
            WrdRb.ShoppingCart.Add(DressingRoom.MiscG);
        }

        Debug.Log(tempCost);
        owedForSet.text = tempCost.ToString();
        if (!ownsAll)
        {
            if (BkPak.Currency[0].Qty > tempCost)
            {
                //CPopup_Verify.gameObject.SetActive(true);
                Popup_DidntPurchase_Set.SetActive(true);
                Pop_NtEnoughCash_yesBtn.GetComponent<Button>().interactable = true;
            }
            else
            {
                Debug.Log("Sry you dont have the cash for those items");//TO DO: MAKE A NOT ENOUGH CASH NOTIFICATION 
                Pop_NtEnoughCash_yesBtn.GetComponent<Button>().interactable = false;//enabled = false;
                Popup_DidntPurchase_Set.SetActive(true);//CREATE POP UP- "Sry you dont have the cash for those items" - BUTTON OK
                                                    // LoadHomeScene();
            }
        }
        else
        {
            WrdRb.PutOn_Outfit(DressingRoom);
            WrdRb.CurrentSet.Assign(DressingRoom);
            LoadHomeScene();
        }

    }

    public bool validatePurchase(ApparelItem AI)
    {
        if (BkPak.Currency[0].Qty > AI.Cost)
        {
            BkPak.Currency[0].Qty -= AI.Cost;
            AI.owned = true;
            // Debug.Log("Purchase SUCCESSFULL"); 
            return true;
        }
        else
            return false;
    }
    
    public void ShowMeTab1()
    {
        if(Tab1.GetComponent<Canvas>().sortingOrder == 3) { return; }
        Debug.Log("Hit tab1");
        Tab1.GetComponent<Canvas>().sortingOrder = 3;
        Tab2.GetComponent<Canvas>().sortingOrder = Tab2.GetComponent<Canvas>().sortingOrder - 1;
        Tab3.GetComponent<Canvas>().sortingOrder = Tab3.GetComponent<Canvas>().sortingOrder - 1;
        ActiveStore = "Tab1";

        //Reset Store
        ResetShop(WrdRb.HeadStock);
        //WrdRb.CurrentSet.HeadG = BirfdaySuit.HeadG;
        //If Unpurchased body gear is on, remove it.
        //If Unpurchased misc gear is on, remove it.
        //ORRRRR keep all on ....

    }
    public void ShowMeTab2()
    {
        if (Tab2.GetComponent<Canvas>().sortingOrder == 3) { return; }
        Debug.Log("Hit tab2");
        Tab1.GetComponent<Canvas>().sortingOrder = Tab1.GetComponent<Canvas>().sortingOrder - 1;
        Tab2.GetComponent<Canvas>().sortingOrder = 3;
        Tab3.GetComponent<Canvas>().sortingOrder = Tab3.GetComponent<Canvas>().sortingOrder - 1;
        ActiveStore = "Tab2";

        ResetShop(WrdRb.BodyStock);
        //WrdRb.CurrentSet.BodyG = WrdRb.BirfdaySuit.BodyG;
    }
    public void ShowMeTab3()
    {
        if (Tab3.GetComponent<Canvas>().sortingOrder == 3) { return; }
        Debug.Log("Hit tab3");
        Tab1.GetComponent<Canvas>().sortingOrder = Tab1.GetComponent<Canvas>().sortingOrder - 1;
        Tab2.GetComponent<Canvas>().sortingOrder = Tab2.GetComponent<Canvas>().sortingOrder - 1;
        Tab3.GetComponent<Canvas>().sortingOrder = 3;
        ActiveStore = "Tab3";
        ResetShop(WrdRb.MiscStock);
       // WrdRb.CurrentSet.MiscG = BirfdaySuit.MiscG;
    }

    public void presetToggleR()
    {
        Outfit temp = new Outfit();
        if (PresetNumber.text == "Preset 1")
        {
            Debug.Log("WrdRb.Preset1=>WrdRb.Preset2");
            PresetNumber.text = "Preset 2";
            temp.Assign(WrdRb.Preset2);
        }
        //temp = WrdRb.Preset2; }
        else if (PresetNumber.text == "Preset 2")
        {
            Debug.Log("WrdRb.Preset2=>WrdRb.Preset3");
            PresetNumber.text = "Preset 3";
            temp.Assign(WrdRb.Preset3);
        }

        else if (PresetNumber.text == "Preset 3")
        {
            Debug.Log("WrdRb.Preset3=>WrdRb.Preset1");
            PresetNumber.text = "Preset 1";
            temp.Assign(WrdRb.Preset1);
        }

        try
        {
            WrdRb.PutOn_Outfit(temp);

            Debug.Log("Try worked");
            Debug.Log(WrdRb.CurrentSet.HeadG.Name);
            Debug.Log(WrdRb.CurrentSet.BodyG.Name);
            Debug.Log(WrdRb.CurrentSet.MiscG.Name);
        }
        catch { Debug.Log("catch worked");
            //WrdRb.CurrentSet = BirfdaySuit;
            //Set_Gear(BirfdaySuit);
            Debug.Log(WrdRb.BirfdaySuit.HeadG.Name);
            Debug.Log(WrdRb.BirfdaySuit.BodyG.Name);
            Debug.Log(WrdRb.BirfdaySuit.MiscG.Name);
        };

    }
    public void presetToggleL()
    {
        Outfit temp = new Outfit();
        if (PresetNumber.text == "Preset 1")
        {
            PresetNumber.text = "Preset 3";
            temp.Assign(WrdRb.Preset3);
        }
        else if (PresetNumber.text == "Preset 2")
        {
            PresetNumber.text = "Preset 1";
            temp.Assign(WrdRb.Preset1);
        }
        //temp = WrdRb.Preset1; }
        else if (PresetNumber.text == "Preset 3")
        {
            PresetNumber.text = "Preset 2";
            temp.Assign(WrdRb.Preset2);
        }
        try
        {
            WrdRb.PutOn_Outfit(temp);
        }
        catch { Debug.Log("catch worked");
            //Set_Gear(BirfdaySuit);
        };
    }

    public void StoreToggleRight()
    {

        List<ApparelItem> AI = new List<ApparelItem>();
        PurchaseButton.GetComponentInChildren<Text>().text = "Purchase Item";
        if (ActiveStore == "Tab1")
        {
            AI = WrdRb.HeadStock;
            if (ItemSelected < AI.Count) ItemSelected++;
            if (ItemSelected == AI.Count) ItemSelected = 0;
            DressingRoom.HeadG = AI[ItemSelected];
       //     Debug.Log("WrdRb.CurrentSet just added: " + AI[ItemSelected].Name);
        }
        else if (ActiveStore == "Tab2")
        {
            AI = WrdRb.BodyStock;
            if (ItemSelected < AI.Count) ItemSelected++;
            if (ItemSelected == AI.Count) ItemSelected = 0;
            DressingRoom.BodyG = AI[ItemSelected];
        }
        else if (ActiveStore == "Tab3")
        {
            AI = WrdRb.MiscStock;
            if (ItemSelected < AI.Count) ItemSelected++;
            if (ItemSelected == AI.Count) ItemSelected = 0;
            DressingRoom.MiscG = AI[ItemSelected];
        }

        PurchaseButton.GetComponent<Button>().interactable = (!AI[ItemSelected].owned);
        PS_confirm.gameObject.SetActive(true);


        if (AI[ItemSelected].Cost > BkPak.Currency[0].Qty)
        {
            PurchaseButton.GetComponentInChildren<Text>().text = "Not Enough Coins";
            PurchaseButton.GetComponent<Button>().interactable = false;
        }
        if (AI[ItemSelected].owned)
        {
            PurchaseButton.GetComponentInChildren<Text>().text = "Already Owned";
            PurchaseButton.GetComponent<Button>().interactable = false;
        }

        SelectedItem.GetComponent<Image>().sprite = AI[ItemSelected].pic;

        //WARDROBE SEARCH - Activate Attire
        WrdRb.Set_Gear(AI[ItemSelected]);//ENABLES WARDROBE ON AVATAR

        //HeadSwitch(Apparel[ItemSelected].Name);
        SelectedCost.text = AI[ItemSelected].Cost.ToString();

        newitemAnim.SetTrigger("ItemSwitched");
        //newCostAnim.SetTrigger("ItemSwitched");
        //newFXAnim.SetTrigger("ItemSwitched");

        LPane.GetComponent<Animator>().SetTrigger("CtoR");
        CPane.GetComponent<Animator>().SetTrigger("CtoR");
        RPane.GetComponent<Animator>().SetTrigger("CtoR");

        RPane.name = "Pannel_L";
        CPane.name = "Pannel_R";
        LPane.name = "Pannel_C";

        HotSeat = AI[ItemSelected];
      //  PurchaseButton.GetComponent<Button>().interactable = (!HotSeat.owned);//If owned (true), button is !interactable
        if (HotSeat.owned)
        {
            SelectedCost.text = "Owned";
            SelectedCost.color = Color.gray;// new Color(SelectedCost.color.r, SelectedCost.color.g, SelectedCost.color.b, 50);
        }else SelectedCost.color = Color.white;
    }

    public void StoreToggleLeft()
    {

        PurchaseButton.GetComponentInChildren<Text>().text = "Purchase Item";
        List<ApparelItem> AI = new List<ApparelItem>();

        if (ActiveStore == "Tab1")
        {
            AI = WrdRb.HeadStock;
            if (ItemSelected == 0) ItemSelected = AI.Count;
            if (ItemSelected > 0) ItemSelected--;
            DressingRoom.HeadG = AI[ItemSelected];
        }
        else if (ActiveStore == "Tab2")
        {
            AI = WrdRb.BodyStock;
            if (ItemSelected == 0) ItemSelected = AI.Count;
            if (ItemSelected > 0) ItemSelected--;
            DressingRoom.BodyG = AI[ItemSelected];
        }
        else if (ActiveStore == "Tab3")
        {

            AI = WrdRb.MiscStock;
            if (ItemSelected == 0) ItemSelected = AI.Count;
            if (ItemSelected > 0) ItemSelected--;
            DressingRoom.MiscG = AI[ItemSelected];
        }

        PurchaseButton.GetComponent<Button>().interactable = (!AI[ItemSelected].owned);

        if (AI[ItemSelected].Cost > BkPak.Currency[0].Qty )
        {
            PurchaseButton.GetComponentInChildren<Text>().text = "Not Enough Coins";
            PurchaseButton.GetComponent<Button>().interactable = false;
        }
        if (AI[ItemSelected].owned)
        {
            PurchaseButton.GetComponentInChildren<Text>().text = "Already Owned";
            PurchaseButton.GetComponent<Button>().interactable = false;
        }

        SelectedItem.GetComponent<Image>().sprite = AI[ItemSelected].pic;

        Debug.Log("DressingRoom Change: " + AI[ItemSelected].Name);
        SelectedCost.text = AI[ItemSelected].Cost.ToString();//SetCost

        WrdRb.Set_Gear(AI[ItemSelected]);//ENABLES WARDROBE ON AVATAR

        newitemAnim.SetTrigger("ItemSwitched");
       // newCostAnim.SetTrigger("ItemSwitched");
        //newFXAnim.SetTrigger("ItemSwitched");
        //Toggle Wardrobe

        CPane.GetComponent<Animator>().SetTrigger("CtoL");
        RPane.GetComponent<Animator>().SetTrigger("CtoL");
        LPane.GetComponent<Animator>().SetTrigger("CtoL");
                
        RPane.name = "Pannel_C";
        CPane.name = "Pannel_L";
        LPane.name = "Pannel_R";

        HotSeat = AI[ItemSelected];
       // PurchaseButton.GetComponent<Button>().interactable = (!AI[ItemSelected].owned);
        if (HotSeat.owned)
        {
            SelectedCost.text = "Owned";
            SelectedCost.color = Color.gray;// new Color(SelectedCost.color.r, SelectedCost.color.g, SelectedCost.color.b, 50);
        }
        else SelectedCost.color = Color.white;

    }

    public void ResetShop(List<ApparelItem> stock)
    {
        ItemSelected = 0;
        SelectedItem.GetComponent<Image>().sprite = stock[ItemSelected].pic;
        SelectedCost.text = stock[ItemSelected].Cost.ToString();

        newitemAnim.SetTrigger("ItemSwitched");
        // newFXAnim.SetTrigger("ItemSwitched");

        PurchaseButton.GetComponent<Button>().interactable = false;
    }

    public void SetPresetNo()
    {
       // CPopup_setPreset.gameObject.SetActive(false);
    }

    public void SetPresetYes()
    {

        switch (PresetNumber.text)
        {
            case "Preset 1":

                //  Ai
                WrdRb.Preset1.Assign(WrdRb.CurrentSet);
                // WrdRb.Preset1 = WrdRb.CurrentSet;
                Debug.Log("WrdRb.Preset1 got set");
                Debug.Log("head: " + WrdRb.CurrentSet.HeadG.Name + WrdRb.Preset1.HeadG.Name);
                Debug.Log("body: " + WrdRb.CurrentSet.BodyG.Name + WrdRb.Preset1.BodyG.Name);
                Debug.Log("misc: " + WrdRb.CurrentSet.MiscG.Name + WrdRb.Preset1.MiscG.Name);
                Debug.Log("WrdRb.Preset2 is: " + WrdRb.Preset2.HeadG.Name + WrdRb.Preset2.BodyG.Name + WrdRb.Preset2.MiscG.Name);
                break;
            case "Preset 2":
                WrdRb.Preset2.Assign(WrdRb.CurrentSet);
                //WrdRb.Preset2 = WrdRb.CurrentSet;
                Debug.Log("WrdRb.Preset2 got set");
                Debug.Log("head: " + WrdRb.CurrentSet.HeadG.Name + WrdRb.Preset2.HeadG.Name);
                Debug.Log("body: " + WrdRb.CurrentSet.BodyG.Name + WrdRb.Preset2.BodyG.Name);
                Debug.Log("misc: " + WrdRb.CurrentSet.MiscG.Name + WrdRb.Preset2.MiscG.Name);
                Debug.Log("WrdRb.Preset1 is: " + WrdRb.Preset1.HeadG.Name + WrdRb.Preset1.BodyG.Name + WrdRb.Preset1.MiscG.Name);
                Debug.Log("WrdRb.Preset3 is: " + WrdRb.Preset3.HeadG.Name + WrdRb.Preset3.BodyG.Name + WrdRb.Preset3.MiscG.Name);
                break;
            case "Preset 3":
                WrdRb.Preset3.Assign(WrdRb.CurrentSet);
                //WrdRb.Preset3 = WrdRb.CurrentSet;
                Debug.Log("WrdRb.Preset3 got set");
                Debug.Log("head: " + WrdRb.CurrentSet.HeadG.Name + WrdRb.Preset3.HeadG.Name);
                Debug.Log("body: " + WrdRb.CurrentSet.BodyG.Name + WrdRb.Preset3.BodyG.Name);
                Debug.Log("misc: " + WrdRb.CurrentSet.MiscG.Name + WrdRb.Preset3.MiscG.Name);
                Debug.Log("WrdRb.Preset1 is: " + WrdRb.Preset1.HeadG.Name + WrdRb.Preset1.BodyG.Name + WrdRb.Preset1.MiscG.Name);
                Debug.Log("WrdRb.Preset2 is: " + WrdRb.Preset2.HeadG.Name + WrdRb.Preset2.BodyG.Name + WrdRb.Preset2.MiscG.Name);
                break;
            default:
                break;
        }


     //   CPopup_setPreset.gameObject.SetActive(false);
    }

    public void BuyAllYes()
    {
        //Assign(WrdRb.CurrentSet, WrdRb.CurrentSet);//Why
        foreach (ApparelItem AI in WrdRb.ShoppingCart)
        {
            if (validatePurchase(AI))
            {
                Debug.Log("Successful purchase");       // FinalizeTransation(AI);
            }
            else Debug.Log("Not Enough Cash... Stranger");
        }
        WrdRb.CurrentSet.Assign(DressingRoom);// I only want to call this if all items get purchased. 
        // PutOn_Outfit(DressingRoom);
        Popup_DidntPurchase_Set.SetActive(false);
       // CPopup_Verify.gameObject.SetActive(false);
        LoadHomeScene();
    }

    public void BuyAllNo()
    {
        Popup_DidntPurchase_Set.SetActive(true);
        //CPopup_Verify.gameObject.SetActive(false);
        LoadHomeScene();
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(1);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    public void LoadPurchaseScene()
    {
        SceneManager.LoadScene(4);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    public void DeactivateItemPopUp()
    {

        if(Popup_NotEnough_Item.gameObject.activeSelf)
        {
            Popup_NotEnough_Item.SetActive(false);
        }
        else
        {
            Popup_NotEnough_Item.SetActive(true);
        }
    }
}
