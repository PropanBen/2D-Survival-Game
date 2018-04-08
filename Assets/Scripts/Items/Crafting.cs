using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Crafting : MonoBehaviour
{
    public Dropdownmenu myDropdownmenu;
    public GameObject Dropdown;
    public Rezepte myRezepte;
    public GameObject Handwerk;
    public GameObject CrafttimerbarCurrent;
    public GameObject CrafttimerBackground;
    public GameObject Dropdown1;
    private Transform Child;
    private GameObject Crafted;
    public GameObject SlotPanel;
    public Text CrafttimerText;
    public static string TextofButton;
    public GameObject CraftItem1UI, CraftItem2UI, CraftItem3UI, CraftItem4UI, Description, CraftedItem;
    public Button CraftingButton;
    public int amount;
    public int LvL;
    public string Res1 = null;
    public string Res2 = null;
    public string Res3 = null;
    public string Res4 = null;
    public int rescount1 = 0;
    public int rescount2 = 0;
    public int rescount3 = 0;
    public int rescount4 = 0;
    public string description = null;
    public int readamount = 1;
    public Sprite SlotItem;
    public int childcounter1;
    public int childcounter2;
    public int childcounter3;
    public int childcounter4;
    public string name1;
    public string name2;
    public string name3;
    public string name4;
    public bool proof;
    public bool crafttiming = false;
    public float crafttimer = 2;

    void Start()
    {
        myDropdownmenu = Dropdown1.GetComponent<Dropdownmenu>();
        Handwerk = GameObject.Find("Handwerk");
        CrafttimerbarCurrent.SetActive(false);
        CrafttimerBackground.SetActive(false);
    }

    void Update()
    {
        childcounter1 = CraftItem1UI.transform.childCount;
        childcounter2 = CraftItem2UI.transform.childCount;
        childcounter3 = CraftItem3UI.transform.childCount;
        childcounter4 = CraftItem4UI.transform.childCount;

    /*    print("Res1: " + Res1);
        print("name1: " + name1);
        print("Res2: " + Res2);
        print("name2: " + name2);
        print("Res3: " + Res3);
        print("name3: " + name3);
        print("name4: " + Res4);
        print("Res4: " + name4);

        print("rescount1 : " + rescount1);
        print("childcounter1: " + (childcounter1 - 1));
        print("rescount2 : " + rescount2);
        print("childcounter2: " + (childcounter2 - 1));
        print("rescount3 : " + rescount3);
        print("childcounter3: " + (childcounter3 - 1));
        print("rescount4 : " + rescount4);
        print("childcounter4: " + (childcounter4 - 1)); */

        GetResourcesofSlots();
        ProofCrafting();

        // Crafting Timer Anzeige--------------------------------------------------------------------------

        if (proof == true)
        {
            if (crafttiming == true)
            {
                crafttimer -= Time.deltaTime;
                CrafttimerbarCurrent.SetActive(true);
                CrafttimerBackground.SetActive(true);
            }
            else
            {
                CrafttimerbarCurrent.SetActive(false);
                CrafttimerBackground.SetActive(false);
            }
            float ratio = crafttimer / (readamount * 2);
            CrafttimerbarCurrent.transform.localScale = new Vector3(1 - ratio, 1, 1);
            if (crafttimer < 2 * readamount)
                CrafttimerText.text = crafttimer.ToString("0.#") + "sec";
            else CrafttimerText.text = "";
            if (crafttiming == true && crafttimer <= 0 && proof)
            {
                Craft();
                crafttiming = false;
                crafttimer = 2 * readamount;
                CrafttimerBackground.SetActive(false);
                proof = false;
            }
        }
        //---------------------------------------------------------------------------------------------------




    }

    // Schritt 1--------------------------------------------------------------------------------

    // Auszuführen im Dropdownmenü unter DropdownonValueChange
    public void DropdownValueChange()
    {
        int Dropdownvalue = Dropdown1.GetComponent<Dropdown>().value;

        // Rezeptliste anhand des Dropdownmenüs ausgewählt
        switch (Dropdownvalue)
        {
            case 0: Rezepte.ListofLists = Rezepte.Handwerksrezepteliste; break;
            case 1: Rezepte.ListofLists = Rezepte.Werkzeugliste; break;
            case 2: Rezepte.ListofLists = Rezepte.Essensliste; break;
            case 3: Rezepte.ListofLists = Rezepte.Kleidungsliste; break;
            case 4: Rezepte.ListofLists = Rezepte.Buildinglist; break;
            case 5: Rezepte.ListofLists = Rezepte.Weaponlist; break;
            case 6: Rezepte.ListofLists = Rezepte.Otherlist; break;
        }

        // Buttons mit Rezepten versehen
        foreach (Rezepte List in Rezepte.ListofLists)
        {
            string LvL = List.LvL.ToString();
            string buttonstr = List.buttonnumber;
            string name = List.name;
            int buttonint = int.Parse(buttonstr);
            buttonint--;
            Button Buttonsearch = Dropdownmenu.Buttonliste[buttonint];
            Buttonsearch.GetComponentsInChildren<Text>()[0].text = name + " LvL " + LvL;
        }
    }

    // Schritt 2--------------------------------------------------------------------------------

    // Button mit Rezept gedrückt
    public void SelectRecipe()
    {
        Res1 = null;
        Res2 = null;
        Res3 = null;
        Res4 = null;
        rescount1 = 0;
        rescount2 = 0;
        rescount3 = 0;
        rescount4 = 0;
        description = null;

        foreach (Rezepte rez in Rezepte.ListofLists)
        {
            if (rez.name == TextofButton)
            {
                switch (rez.rescount)
                {
                    case 1:
                        LvL = rez.LvL;
                        Res1 = rez.Resource1;
                        rescount1 = rez.Resource1count *readamount;
                        description = rez.description;
                        break;
                    case 2:
                        LvL = rez.LvL;
                        Res1 = rez.Resource1;
                        rescount1 = rez.Resource1count * readamount;
                        Res2 = rez.Resource2;
                        rescount2 = rez.Resource2count * readamount;
                        description = rez.description;
                        break;
                    case 3:
                        LvL = rez.LvL;
                        Res1 = rez.Resource1;
                        rescount1 = rez.Resource1count * readamount;
                        Res2 = rez.Resource2;
                        rescount2 = rez.Resource2count * readamount;
                        Res3 = rez.Resource3;
                        rescount3 = rez.Resource3count * readamount;
                        description = rez.description;
                        break;
                    case 4:
                        LvL = rez.LvL;
                        Res1 = rez.Resource1;
                        rescount1 = rez.Resource1count * readamount;
                        Res2 = rez.Resource2;
                        rescount2 = rez.Resource2count * readamount;
                        Res3 = rez.Resource3;
                        rescount3 = rez.Resource3count * readamount;
                        Res4 = rez.Resource4;
                        rescount4 = rez.Resource4count * readamount;
                        description = rez.description;
                        break;
                }
            }
        }
    }

    // Schritt 3--------------------------------------------------------------------------------

    public void ProcessRecipeResources()
    {
        CraftItem1UI.GetComponent<Image>().sprite = SlotItem;
        CraftItem2UI.GetComponent<Image>().sprite = SlotItem;
        CraftItem3UI.GetComponent<Image>().sprite = SlotItem;
        CraftItem4UI.GetComponent<Image>().sprite = SlotItem;
        CraftItem1UI.GetComponentsInChildren<Text>()[0].text = "0";
        CraftItem2UI.GetComponentsInChildren<Text>()[0].text = "0";
        CraftItem3UI.GetComponentsInChildren<Text>()[0].text = "0";
        CraftItem4UI.GetComponentsInChildren<Text>()[0].text = "0";


        if (Res1 != null)
        {
            Sprite Sprite1 = Resources.Load("Grau/" + Res1 + "_grey", typeof(Sprite)) as Sprite;
            CraftItem1UI.GetComponent<Image>().sprite = Sprite1;
        }
        if (Res2 != null)
        {
            Sprite Sprite2 = Resources.Load("Grau/" + Res2 + "_grey", typeof(Sprite)) as Sprite;
            CraftItem2UI.GetComponent<Image>().sprite = Sprite2;
        }
        if (Res3 != null)
        {
            Sprite Sprite3 = Resources.Load("Grau/" + Res3 + "_grey", typeof(Sprite)) as Sprite;
            CraftItem3UI.GetComponent<Image>().sprite = Sprite3;
        }
        if (Res4 != null)
        {
            Sprite Sprite4 = Resources.Load("Grau/" + Res4 + "_grey", typeof(Sprite)) as Sprite;
            CraftItem4UI.GetComponent<Image>().sprite = Sprite4;
        }
        if (rescount1 != 0)
        {
            CraftItem1UI.GetComponentsInChildren<Text>()[0].text = rescount1.ToString();
        }
        if (rescount2 != 0)
        {
            CraftItem2UI.GetComponentsInChildren<Text>()[0].text = rescount2.ToString();
        }
        if (rescount3 != 0)
        {
            CraftItem3UI.GetComponentsInChildren<Text>()[0].text = rescount3.ToString();
        }
        if (rescount4 != 0)
        {
            CraftItem4UI.GetComponentsInChildren<Text>()[0].text = rescount4.ToString();
        }
        if (description != null)
        {
            Description.GetComponent<Text>().text = description;
        }

        // Rezept dem Crafted Item zuweisen
        Sprite CraftedSprite = Resources.Load("Crafted/" + TextofButton, typeof(Sprite)) as Sprite;
        CraftedItem.gameObject.GetComponent<Image>().sprite = CraftedSprite;
        CraftedItem.GetComponentsInChildren<Text>()[0].text = readamount.ToString();

    }

    public void GetResourcesofSlots()
    {
        if (childcounter1 > 1)
        {
            Transform Child1 = CraftItem1UI.transform.GetChild(1);
            name1 = Child1.name;
        }
        else { name1 = null; }
        if (childcounter2 > 1)
        {
            Transform Child2 = CraftItem2UI.transform.GetChild(1);
            name2 = Child2.name;
        }
        else { name2 = null; }
        if (childcounter3 > 1)
        {
            Transform Child3 = CraftItem3UI.transform.GetChild(1);
            name3 = Child3.name;
        }
        else { name3 = null; }
        if (childcounter4 > 1)
        {
            Transform Child4 = CraftItem4UI.transform.GetChild(1);
            name4 = Child4.name;
        }
        else { name4 = null; }
    }

    public void ProofCrafting()
    {
        if (Res1 == name1 && Res2 == name2 && Res3 == name3 && Res4 == name4 &&
         rescount1 <= childcounter1 - 1 && rescount2 <= childcounter2 - 1 &&
         rescount3 <= childcounter3 - 1 && rescount4 <= childcounter4 - 1)
        {
            proof = true;
            CraftingButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            proof = false;
            CraftingButton.GetComponent<Image>().color = Color.red;
        }
    }

    public void PlusCraft()
    {
        Text readamounttxt = CraftedItem.GetComponentsInChildren<Text>()[0];
        readamount = int.Parse(readamounttxt.text);
        if (readamount < 99)
        {
            readamount++;
            SelectRecipe();
            ProcessRecipeResources();
        }
        string readamountstring = readamount.ToString();
        CraftedItem.GetComponentsInChildren<Text>()[0].text = readamountstring;
    }

    public void MinusCraft()
    {
        Text readamounttxt = CraftedItem.GetComponentsInChildren<Text>()[0];
        readamount = int.Parse(readamounttxt.text);
        if (readamount > 1)
        {
            readamount--;
            SelectRecipe();
            ProcessRecipeResources();
        }
        string readamountstring = readamount.ToString();
        CraftedItem.GetComponentsInChildren<Text>()[0].text = readamountstring;
    }

    public void SetCrafttiming()
    {
        crafttiming = true;
        crafttimer = crafttimer * readamount;
    }

    public void Craft()
    {
        int childcounterI = 0;
        int slotcounter = 0;


        // Prüfen ob Inventar voll
        foreach (Transform InventarSlot in SlotPanel.transform)
        {
            Transform ChildofInventarSlot = InventarSlot.gameObject.transform.GetChild(0);
            childcounterI = ChildofInventarSlot.childCount;
            if (childcounterI > 1)
                slotcounter++;
        }

        // Gegenstand herstellen----------------------------------------------------------

        if (proof == true && slotcounter < 20)
        {
            CrafttimerbarCurrent.transform.localScale = new Vector3(0, 1, 1);
            for (int i = 0; i < readamount; i++)
            {
                Crafted = Instantiate(Prefabliste.Instance().GetGameObject(TextofButton), new Vector3(0, 0, 0), Quaternion.identity);
                Crafted.name = Crafted.name.Substring(0, Crafted.name.Length - 7);

                foreach (Transform InventarSlots in SlotPanel.transform)
                {
                    Transform ChildofInventarSlot = InventarSlots.gameObject.transform.GetChild(0);
                    int InventarSlotcounter = ChildofInventarSlot.transform.childCount;
                    if (InventarSlotcounter == 1)
                    {
                        Crafted.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        Crafted.transform.SetParent(ChildofInventarSlot.transform);
                    }
                    if (InventarSlotcounter > 1)
                    {
                        Child = ChildofInventarSlot.gameObject.transform.GetChild(1);
                    }
                    if (InventarSlotcounter > 1 && InventarSlotcounter < 21 && Crafted.name == Child.name)
                    {
                        Crafted.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        Crafted.transform.SetParent(ChildofInventarSlot.transform);
                    }
                }
                Crafted = null;
                Handwerk.transform.root.SendMessage("EXP", 10);
            }

            // Resourcen löschen

            for (int j = 0; j < rescount1; j++)
            {
                Transform Child = CraftItem1UI.gameObject.transform.GetChild(j + 1);
                Destroy((Child as Transform).gameObject);
            }
            for (int j = 0; j < rescount2; j++)
            {
                Transform Child = CraftItem2UI.gameObject.transform.GetChild(j + 1);
                Destroy((Child as Transform).gameObject);
            }
            for (int j = 0; j < rescount3; j++)
            {
                Transform Child = CraftItem3UI.gameObject.transform.GetChild(j + 1);
                Destroy((Child as Transform).gameObject);
            }
            for (int j = 0; j < rescount4; j++)
            {
                Transform Child = CraftItem4UI.gameObject.transform.GetChild(j + 1);
                Destroy((Child as Transform).gameObject);
            }
            proof = false;
            ProcessRecipeResources();
        }
    }
}