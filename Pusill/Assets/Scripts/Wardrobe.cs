using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Wardrobing {

    public class ApparelItem
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public Sprite pic;
        public string Ability;
        public string tag;
        public bool owned = false;
    }

    public class Outfit
    {
        public ApparelItem HeadG { get; set; }
        public ApparelItem BodyG { get; set; }
        public ApparelItem MiscG { get; set; }

        public void Assign(Outfit O2)
        {
            this.HeadG = O2.HeadG;
            this.BodyG = O2.BodyG;
            this.MiscG = O2.MiscG;
        }

        public ApparelItem[] ReturnItems()
        {
            ApparelItem[] T;

            T = new ApparelItem[3];

            T[0] = HeadG;
            T[1] = BodyG;
            T[2] = MiscG;
            return T;
        }
    }



    public class Wardrobe
    {

        public Outfit BirfdaySuit;
        public Outfit CurrentSet;
        public Outfit Preset1;
        public Outfit Preset2;
        public Outfit Preset3;

        public List<ApparelItem> HeadStock;
        public List<ApparelItem> BodyStock;
        public List<ApparelItem> MiscStock;
        public List<ApparelItem> ShoppingCart;

        public Transform[] Wardrobe_HeadGear;
        public Transform[] Wardrobe_BodyGear;
        public Transform[] Wardrobe_MiscGear;


        //static Wardrobe instance;
        public Wardrobe()
        {
            //Apparel = new List<ApparelItem>();
            HeadStock = new List<ApparelItem>();
            BodyStock = new List<ApparelItem>();
            MiscStock = new List<ApparelItem>();
            ShoppingCart = new List<ApparelItem>();

            //TODO Jump to helper functions:
            //Generate HeadStock
            //Generate BodtStock
            //Generate MiscStock
            BirfdaySuit = new Outfit();
            CurrentSet = new Outfit();
            Preset1 = new Outfit();
            Preset2 = new Outfit();
            Preset3 = new Outfit();


            for (int i = 0; i < 12; i++)//TO DO . THIS CANT STAY STATIC
            {
                // GameObject storeTemp = new GameObject();
                ApparelItem temp = new ApparelItem();
                
                switch (i)
                {
                    case 0:
                        temp.Name = "None";
                        temp.Cost = 0;
                        temp.Ability = "ol'Natural";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/Empty");
                        temp.tag = "HeadGear";
                        temp.owned = true;
                        BirfdaySuit.HeadG = temp;
                        break;
                    case 1:
                        temp.Name = "None";
                        temp.Cost = 0;
                        temp.Ability = "ol'Natural";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/Empty");
                        temp.tag = "BodyGear";
                        temp.owned = true;
                        BirfdaySuit.BodyG = temp;
                        break;
                    case 2:
                        temp.Name = "None";
                        temp.Cost = 0;
                        temp.Ability = "ol'Natural";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/Empty");
                        temp.tag = "MiscGear";
                        temp.owned = true;
                        BirfdaySuit.MiscG = temp;
                        break;
                    case 3:
                        temp.Name = "TopHat";
                        temp.Cost = 1000;
                        temp.Ability = "Dapper";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/HeadGear/clipart-hat-steampunk-19");
                        temp.tag = "HeadGear";
                        break;
                    case 4:
                        temp.Name = "Maracca";
                        temp.Cost = 200;
                        temp.Ability = "Graced";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/MiscGear/maraca");
                        temp.tag = "MiscGear";
                        break;
                    case 5:
                        temp.Name = "Monocle";
                        temp.Cost = 450;
                        temp.Ability = "+ %100 Luck";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/BodyGear/mono");
                        temp.tag = "BodyGear";
                        break;
                    case 6:
                        temp.Name = "BallCap";
                        temp.Cost = 350;
                        temp.Ability = "+ %200 Luck";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/HeadGear/BallCap");
                        temp.tag = "HeadGear";
                        break;
                    case 7:
                        temp.Name = "Cowboy";
                        temp.Cost = 900;
                        temp.Ability = "+ 5 Grit";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/HeadGear/cowboyhat");
                        temp.tag = "HeadGear";
                        break;
                    case 8:
                        temp.Name = "StarScar";
                        temp.Cost = 200;
                        temp.Ability = "+ 5 fun";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/BodyGear/starsCAR");
                        temp.tag = "BodyGear";
                        break;
                    case 9:
                        temp.Name = "BridgeBadge";
                        temp.Cost = 200;
                        temp.Ability = "+ 5 fun";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/BodyGear/TrekBadge");
                        temp.tag = "MiscGear";
                        break;
                    case 10:
                        temp.Name = "Naruto";
                        temp.Cost = 2000;
                        temp.Ability = "Believe it!";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/HeadGear/HeadBand");
                        temp.tag = "HeadGear";
                        break;
                    case 11:
                        temp.Name = "Kunai";
                        temp.Cost = 1000;
                        temp.Ability = "Believe it!";
                        temp.pic = Resources.Load<Sprite>("Wardrobe/MiscGear/kunai");
                        temp.tag = "MiscGear";
                        break;

                }

                sortStock(temp);

            }
            CurrentSet.Assign(BirfdaySuit);
            Preset1.Assign(BirfdaySuit);
            Preset2.Assign(BirfdaySuit);
            Preset3.Assign(BirfdaySuit);
        }

        public void sortStock(ApparelItem item)
        {
            switch (item.tag)
            {
                case "HeadGear":
                    HeadStock.Add(item);
                    //SelectedCost.text = HeadStock[ItemSelected].Cost.ToString();
                    //SelectedItem.GetComponent<Image>().sprite = HeadStock[ItemSelected].pic;
                    break;
                case "BodyGear":
                    BodyStock.Add(item);
                    //SelectedCost.text = BodyStock[ItemSelected].Cost.ToString();
                    //SelectedItem.GetComponent<Image>().sprite = BodyStock[ItemSelected].pic;
                    break;
                case "MiscGear":
                    MiscStock.Add(item);
                    //SelectedCost.text = MiscStock[ItemSelected].Cost.ToString();
                    //SelectedItem.GetComponent<Image>().sprite = MiscStock[ItemSelected].pic;
                    break;
                default:
                    break;

            }

        }
        
        public void UnSet_Gear()
        {

            PutOn_BirthdaySuit();
           // CurrentSet.Assign(BirfdaySuit);
        }

        public void Set_Gear(ApparelItem Item)
        {
            Transform[] t;
            t = new Transform[0];

            if (Item.tag == "HeadGear")
            {
                t = Wardrobe_HeadGear;
            }
            else if (Item.tag == "BodyGear")
            {
                t = Wardrobe_BodyGear;

            }
            else if (Item.tag == "MiscGear")
            {
                t = Wardrobe_MiscGear;
            }


            foreach (Transform child in t)
            {

                if (child != t[0])
                {
                    try { child.GetComponent<Image>().enabled = false; }
                    catch { Debug.Log("GotCaught Trying"); };

                    if (child.name == Item.Name)
                    { child.GetComponent<Image>().enabled = true; }//TURNS CORRECT ONE ON
                }
            }
        }
        
        public void PutOn_Outfit(Outfit lookAtMeNow)
        {
            Set_Gear(lookAtMeNow.HeadG);
            Set_Gear(lookAtMeNow.BodyG);
            Set_Gear(lookAtMeNow.MiscG);
        }

        public void PutOn_BirthdaySuit()
        {
            Set_Gear(HeadStock[0]);
            Set_Gear(BodyStock[0]);
            Set_Gear(MiscStock[0]);
          //  DressingRoom.Assign
          //  CurrentSet.Assign(BirfdaySuit);
           // CurrentSet = BirfdaySuit;
        }

        public List<GameObject> Game_Set_Gear()
        {


            Transform[] t;
            t = new Transform[0];
            List<GameObject> Temp;
            Temp = new List<GameObject>();

            foreach (ApparelItem AI in CurrentSet.ReturnItems())
            {
                if (AI.tag == "HeadGear")
                {
                    t = Wardrobe_HeadGear;
                }
                else if (AI.tag == "BodyGear")
                {
                    t = Wardrobe_BodyGear;

                }
                else if (AI.tag == "MiscGear")
                {
                    t = Wardrobe_MiscGear;
                }

                foreach (Transform child in t)
                {
                       try { child.GetComponent<Image>().enabled = false; }
                        catch { Debug.Log("GotCaught Trying"); };

                        if (child.name == AI.Name)
                        { child.GetComponent<Image>().enabled = true;
                        Temp.Add(child.gameObject);
                        }
                }
            }
            return Temp;
        }

        public void Game_unSet_Gear()
        {

        }
    }
}
