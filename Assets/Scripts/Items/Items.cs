using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{   
    public Ctrl myCtrl;
    public ItemColor myItemColor;
    public  static bool holdingR = false;
    public  static bool holdingL = false;
    public  static bool holstertR = false;
    public static bool holstertL = false;
    public bool isgrounded;
    public GameObject Charakter;
    public GameObject PlayerCursor;
    public GameObject SoundManager;
    public Sprite Icon;
    public float currentDurability = 100;
    public float maxDurability = 100;
    public List<string> Heilungsliste;
    public List<string> Essensliste;
    public List<string> Drinkinglist;
    public List<string> Waterfilllist;
    public List<GameObject> Childlist;
    public Dictionary<string, int> essenswerte;
    public Dictionary<string, int> healwerte;
    public Dictionary<string, int> ItemDurability;
    public Dictionary<string, int> ItemDamage;
    public Dictionary<string, int> drinkingwerte;
    public Dictionary<string, int> fuelwerte;
    public Dictionary<string, int> Weapon;
    public Dictionary<string, bool> eatable;
    public Dictionary<string, int> animalfood;
    public Sprite spritedrop;




    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        Charakter = GameObject.Find("Charakter");
        PlayerCursor = GameObject.Find("PlayerCursor");
        myCtrl = Charakter.GetComponent<Ctrl>();
        myItemColor = PlayerCursor.GetComponent<ItemColor>();
        Childlist = new List<GameObject>();
        TakeChildstoList(transform);

        //*****************************************************************//
        //  HIER ITEM ATTRIBUTE EINFÜGEN                                   //
        //*****************************************************************//

        // Haltbarkeit
        ItemDurability = new Dictionary<string, int>();

        ItemDurability[this.name] = 100;
        ItemDurability["Pickaxe_wood"] = 25;
        ItemDurability["Pickaxe_stone"] = 50;
        ItemDurability["Pickaxe_copper"] = 100;
        ItemDurability["Pickaxe_bronze"] = 150;
        ItemDurability["Pickaxe_iron"] = 200;
        ItemDurability["Pickaxe_gold"] = 225;
        ItemDurability["Pickaxe_diamond"] = 250;
        ItemDurability["Axe_wood"] = 25;
        ItemDurability["Axe_stone"] = 50;
        ItemDurability["Axe_copper"] = 100;
        ItemDurability["Axe_bronze"] = 150;
        ItemDurability["Axe_iron"] = 200;
        ItemDurability["Axe_gold"] = 225;
        ItemDurability["Axe_diamond"] = 250;
        ItemDurability["Sword_stone"] = 50;


        // Schaden
        ItemDamage = new Dictionary<string, int>();  
       
        ItemDamage[this.name] = 0;
        ItemDamage["Pickaxe_wood"] = 5;
        ItemDamage["Pickaxe_stone"] = 10;
        ItemDamage["Pickaxe_copper"] = 15;
        ItemDamage["Pickaxe_bronze"] = 25;
        ItemDamage["Pickaxe_iron"] = 30;
        ItemDamage["Pickaxe_gold"] = 35;
        ItemDamage["Pickaxe_diamond"] = 40;
        ItemDamage["Axe_wood"] = 5;
        ItemDamage["Axe_stone"] = 10;
        ItemDamage["Axe_copper"] = 15;
        ItemDamage["Axe_bronze"] = 25;
        ItemDamage["Axe_iron"] = 30;
        ItemDamage["Axe_gold"] = 35;
        ItemDamage["Axe_diamond"] = 40;


        //*****************************************************************//
        //  HIER KONSUMLISTEN EINFÜGEN                                     //
        //*****************************************************************//

        // Listen
        Essensliste = new List<string>
        { "Apple","Pear","Apple_roasted","Meat_roasted","Egg_fried" };
        Waterfilllist = new List<string> { "Leaf","Tinderdrill", "Flint" };
        Drinkinglist = new List<string> { "Leaf_water" };
        Heilungsliste = new List<string> { "Healpotion" };

        // Werte für Animalfood
        animalfood = new Dictionary<string, int>();
        animalfood.Add("Gras", 0);
        animalfood.Add("Egg", 1);
        animalfood.Add("Apple", 1);
        animalfood.Add("Meat", 1);


        // Werte für Heilung
        healwerte = new Dictionary<string, int>();
        healwerte.Add("Healpotion", 30);

        // Werte für Essen
        essenswerte = new Dictionary<string, int>();
        essenswerte.Add("Apple", 5);
        essenswerte.Add("Apple_roasted", 10);
        essenswerte.Add("Pear", 5);
        essenswerte.Add("Meat_roasted", 20);
        essenswerte.Add("Egg_fried", 15);

        eatable = new Dictionary<string, bool>();
        eatable.Add("Apple", true);


        // Werte für Trinken
        drinkingwerte = new Dictionary<string, int>();
        drinkingwerte.Add("Leaf_water", 5);

        // Werte für Brennstoff
        fuelwerte = new Dictionary<string, int>();
        fuelwerte.Add("Twig", 60);
        fuelwerte.Add("Log", 120);
        fuelwerte.Add("Coal", 240);

        // Werte für Waffen
        Weapon = new Dictionary<string, int>();
        Weapon.Add("Spear", 10);
        Weapon.Add("Arrow_stone", 10);
        Weapon.Add("Sword_wood", 10);
        Weapon.Add("Sword_stone", 20);
        Weapon.Add("Sword_copper", 30);
        Weapon.Add("Sword_bronze", 40);
        Weapon.Add("Sword_iron", 50);
        Weapon.Add("Sword_gold", 60);
        Weapon.Add("Sword_diamond", 70);

        // Eigene Haltbarkeit festlegen
        currentDurability = ItemDurability[this.name];
        maxDurability = ItemDurability[this.name];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDurability <= 0)
            Destroy(this.gameObject);

        Flip();

        // Item drop---------------------------------------------------------------------
        Transform parent = transform.parent;

        var spritedrop = this.GetComponent<SpriteRenderer>();
        GameObject rightarm = GameObject.Find("Rightarm");
        GameObject leftarm = GameObject.Find("Leftarm");
        Vector3 pos = transform.position;
        float x = pos.x;
        float y = pos.y;

        int countholdingR = rightarm.transform.childCount;
        int countholdingL = leftarm.transform.childCount;
             
            if (myCtrl.itemdropperR == true && countholdingR > 0 
                && myCtrl.itemtakerR == false && myCtrl.QisPressed == true
                && myCtrl.EisPressed == false)
            { // Rechte Hand
                Transform Rightarmchild = rightarm.gameObject.transform.GetChild(0);
                Rightarmchild.transform.eulerAngles = new Vector3(0, 0, 0);
                Rightarmchild.GetComponent<Collider2D>().enabled = true;
                Rightarmchild.transform.SetParent(null);
                holdingR = false;
                isgrounded = true;
                spritedrop = Rightarmchild.GetComponent<SpriteRenderer>();
                spritedrop.sortingOrder = 1;
            }
        
            if (myCtrl.itemdropperL == true && countholdingL > 0
                && myCtrl.itemtakerL == false && myCtrl.QisPressed == true 
                && myCtrl.EisPressed == false)
            { // Linke Hand
                Transform Leftarmchild = leftarm.gameObject.transform.GetChild(0);
                Leftarmchild.transform.eulerAngles = new Vector3(0, 0, 0);
                Leftarmchild.GetComponent<Collider2D>().enabled = true;
                Leftarmchild.transform.SetParent(null);
                holdingL = false;
                isgrounded = true;
                spritedrop = Leftarmchild.GetComponent<SpriteRenderer>();
                spritedrop.sortingOrder = 1;
            }       
    }

    // Damage

    private void TakeDamage(float damage)
    {
        currentDurability -= damage;
        if (currentDurability < 0)
        {
            currentDurability = 0;
            SoundManager.SendMessage("PlaySound", "brokentool");
        }
    }

    // Repair

    private void RepairDamage(float repair)
    {
        currentDurability += repair;
        if (currentDurability > 100)
        {
            currentDurability = 100;
        }
    }

    // Item Flip----------------------------------------------------------------------
    public void Flip()
    {
        GameObject leftarm = GameObject.Find("Leftarm");
        GameObject rightarm = GameObject.Find("Rightarm");

        GameObject whoflipsra = null;
        GameObject whoflipsla = null;
        GameObject whoflipsLA = null;
        GameObject whoflipsRA = null;

        int childcounterRA = rightarm.transform.childCount;

        int childcounterLA = leftarm.transform.childCount;

        if (childcounterLA > 0)
        {
            whoflipsla = leftarm;
            whoflipsLA = leftarm;
        }
        if (childcounterRA > 0)
        {
            whoflipsra = rightarm;
            whoflipsRA = rightarm; 
        }

        if (holdingR == true && myCtrl.setleft == false && whoflipsra != null)
        {
            whoflipsra.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        }
        if (holdingL == true &&myCtrl.setleft == false && whoflipsla != null)
        {
            whoflipsla.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Item take----------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D col)
    {
        // Rechter Arm

        GameObject Rightarm = GameObject.Find("Rightarm"); 
        int childcounterR = Rightarm.transform.childCount;

        if (col.CompareTag("Rightarm") && myCtrl.itemtakerR == true && childcounterR == 0
            && !myCtrl.itemdropperR == true && myCtrl.QisPressed ==false &&
            myCtrl.EisPressed == true && myItemColor.ItemTarget == this.gameObject
            && !this.CompareTag("Building"))
       
        {
            Transform parent = transform.parent;
            GameObject armr = GameObject.Find("Rightarm");
            Vector3 pos = armr.transform.position;                
            float x = pos.x;                                      
            float y = pos.y;                                     
            this.transform.SetParent(armr.transform);
            SoundManager.SendMessage("PlaySound", "flop");
            transform.position = new Vector3(x, y-0.1f , 0);     
            transform.eulerAngles = new Vector3(0, 0, 0);        

            var spritetake = GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 10;
            isgrounded = false;
            holdingR = true;
        }

        // Linker Arm

        GameObject Leftarm = GameObject.Find("Leftarm"); 
        int childcounterL = Leftarm.transform.childCount;

        if (col.CompareTag("Leftarm") && myCtrl.itemtakerL == true && childcounterL == 0
            && myCtrl.QisPressed == false && myCtrl.EisPressed == true
            && myItemColor.ItemTarget == this.gameObject && !this.CompareTag("Building"))
        {
            Transform parent = transform.parent;
            GameObject arml = GameObject.Find("Leftarm");
            Vector3 pos = arml.transform.position;                
            float x = pos.x;                                    
            float y = pos.y;                                      
            this.transform.SetParent(arml.transform);
            SoundManager.SendMessage("PlaySound", "flop");
            transform.position = new Vector3(x-0.02f, y - 0.11f, 0);     
            transform.eulerAngles = new Vector3(0, 0, 0);       

            var spritetake = GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 4;
            isgrounded = false;
            holdingL = true;
        }


        //*****************************************************************//
        //  Not yet working                                                //
        //*****************************************************************//
        // Item zerstören wenn auf Wasser spawnt

        if (col.GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer Sprite = col.GetComponent<SpriteRenderer>();
            string name = Sprite.name;
            print(name);
            if(name == "water")
            {
                print("Wasser");
                Destroy(this.gameObject);
            }
        }

    }
    // Childs des Items in Liste hinzufügen   
    private void TakeChildstoList(Transform obj)
    {
        foreach (Transform child in obj)
        {
            Childlist.Add(child.gameObject);
            TakeChildstoList(child);
        }
    }
}

