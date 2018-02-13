using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventar : MonoBehaviour {

    public Items myItems;
    public Crafting myCrafting;
    public GameObject InventoryPanel;
    public GameObject RezeptPanel;
    private GameObject Rightarm,Leftarm;
    public GameObject SlotPanel;
    private GameObject Handwerk;
    private Transform Childrightarm;
    private Transform Childleftarm;
    private Transform Child;
    public Sprite SlotBackground;
    public Sprite SlotItem;
    public Sprite SlotCursor;
    private GameObject slotnamecursor;
    private GameObject slotnamecursorsave;
    private string Childrightarmname;
    private string ChildthisSlotname;
    private string Childleftarmname;
    private int Rightarmchildcounter;
    private int Leftarmchildcounter;
    private int Counter;
    public int CursorIndex = 1;
    private int Cursorsave;
    private string ChildsofSlotsname;
    private string nameInventarSlots;
    private string childname;
    int SlotCounter = 1;
    int CounterItems = 1;


    // Use this for initialization
    void Start ()
    {
        Rightarm = GameObject.Find("Rightarm");
        Leftarm = GameObject.Find("Leftarm");
        Handwerk = GameObject.Find("Handwerk");
    }

    // Update is called once per frame
    void Update()
    {
        SlotPanel = GameObject.Find("SlotPanel");
        // Von rechten Arm in Inventar hinzufügen

        Rightarmchildcounter = Rightarm.transform.childCount;

        if (Input.GetKey(KeyCode.Y) && Rightarmchildcounter > 0)
        {   
            Childrightarm = Rightarm.gameObject.transform.GetChild(0);
            DeactivateSpriteofChilds(Childrightarm);
            Childrightarmname = Childrightarm.name;
            foreach (Transform InventarSlotsR in SlotPanel.transform)
            {
                Transform ChildR = InventarSlotsR.gameObject.transform.GetChild(0);
                int InventarSlotcounterR = ChildR.transform.childCount;
                if (InventarSlotcounterR == 1)
                {
                    string name = Childrightarm.name;
                    Childrightarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childrightarm.transform.SetParent(ChildR.transform);
                    Childrightarm.transform.localPosition = new Vector3(0, 0, 1);
                }
                Transform ChildthisSlot = ChildR.gameObject.transform.GetChild(1);
                ChildthisSlotname = ChildthisSlot.name;
                if (InventarSlotcounterR > 1 && InventarSlotcounterR < 20 && Childrightarmname == ChildthisSlotname)
                {
                    string name = Childrightarm.name;
                    Childrightarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childrightarm.transform.SetParent(ChildR.transform);
                    Childrightarm.transform.localPosition = new Vector3(0, 0, 1);
                }
            }
        }

        // Von linken Arm in Inventar hinzufügen

        Leftarmchildcounter = Leftarm.transform.childCount;

        if (Input.GetKey(KeyCode.Y) && Leftarmchildcounter > 0)
        {
            Childleftarm = Leftarm.gameObject.transform.GetChild(0);
            DeactivateSpriteofChilds(Childleftarm);
            Childleftarmname = Childleftarm.name;
            foreach (Transform InventarSlotsL in SlotPanel.transform)
            {
                Transform ChildL = InventarSlotsL.gameObject.transform.GetChild(0);
                int InventarSlotcounterL = ChildL.transform.childCount;
                if (InventarSlotcounterL == 1)
                {
                    string name = Childleftarm.name;
                    Childleftarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childleftarm.transform.SetParent(ChildL.transform);
                    Childleftarm.transform.localPosition = new Vector3(0, 0, 1);
                }
                Transform ChildthisSlot = ChildL.gameObject.transform.GetChild(1);
                ChildthisSlotname = ChildthisSlot.name;
                if (InventarSlotcounterL > 1 && InventarSlotcounterL < 20 && Childleftarmname == ChildthisSlotname)
                {
                    string name = Childleftarm.name;
                    Childleftarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childleftarm.transform.SetParent(ChildL.transform);
                    Childleftarm.transform.localPosition = new Vector3(0, 0, 1);
                }
            }
        }

        // Inventar Cursor------------------------------------------------------------------------
        if (InventoryPanel.activeSelf == true)
            Cursorsprites();

        // Rechts
        if (Input.GetKeyDown(KeyCode.RightArrow) && CursorIndex > -5 && CursorIndex < 24)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex++;
        }

        // Links
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CursorIndex > -3 && CursorIndex <= 24)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex--;
        }
        // Oben
        if (Input.GetKeyDown(KeyCode.UpArrow) && CursorIndex > 0)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex -= 4;
        }

        // Unten

        if (Input.GetKeyDown(KeyCode.DownArrow) && CursorIndex <= 20)
        {
            if (CursorIndex <= 16)
            {
                Cursorsave = CursorIndex;
                string Cursorsavecount = Cursorsave.ToString();
                string Cursorslot = "Slot";
                string Cursorsaveslot = Cursorslot + Cursorsavecount;
                slotnamecursorsave = GameObject.Find(Cursorsaveslot);
                slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            }
            CursorIndex += 4;
        }
        // Cursor Inventar nach oben zu Handwerk
        if (Input.GetKeyDown(KeyCode.UpArrow) && CursorIndex >= -3 && CursorIndex <0)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex += 24;
        } 

        // Cursor Inventar nach unten zu Handwerk
        if (Input.GetKeyDown(KeyCode.DownArrow) && CursorIndex > 16)
        {
            Cursorsave = CursorIndex;
            Cursorsave -= 16;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "CraftSlot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
        }

        // Cursor von Handwerk nach unten zu Inventar
        if (Input.GetKeyDown(KeyCode.DownArrow) && CursorIndex >= 21 && CursorIndex <= 24)
        {
            Cursorsave = CursorIndex;
            Cursorsave -= 16;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "CraftSlot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex -= 20;
        } 

         /*   // Item von Cursor in Handwerk verschieben
            if (Input.GetKeyDown(KeyCode.Return) && CursorIndex < 21)    //X
        {
            string count = CursorIndex.ToString();
            string Slotname = "Item";
            string InventorySlotname = Slotname + count;
            GameObject Slot = GameObject.Find(InventorySlotname);
            int counterI = Slot.transform.childCount;
            if (counterI > 0)
            {
                Child = Slot.gameObject.transform.GetChild(0);
                childname = Child.name;
            }
            foreach (Transform Craftingslots in Handwerk.transform)
            {
                int Craftingslotschildcounter = Craftingslots.transform.childCount;
                string Craftingslotname = Craftingslots.name;
                if (Craftingslotschildcounter == 0 && Craftingslotschildcounter < 20 && counterI > 0)
                {
                    Child.transform.SetParent(Craftingslots.transform);
                }

                if (Craftingslotschildcounter > 0 && counterI > 0)
                {
                    Transform ChildsofSlots = Craftingslots.gameObject.transform.GetChild(0);
                    ChildsofSlotsname = ChildsofSlots.name;
                }
                if (Craftingslotschildcounter > 0 && ChildsofSlotsname == childname && Craftingslotschildcounter < 20 && counterI > 0)
                {
                    Child.transform.SetParent(Craftingslots.transform);
                }
            }
        }*/

        // Item von HandwerksCursor in Inventar verschieben

        if (Input.GetKeyDown(KeyCode.Y) && CursorIndex > 20 && CursorIndex < 25 )
        {
            int CursorIndextemp = CursorIndex - 20;
            string CursorIndexstr = CursorIndextemp.ToString();
            string Handwerkslot = "CraftItem";
            string name = Handwerkslot + CursorIndexstr;

            GameObject Handwerksslot = GameObject.Find(name);

            int childcounterH = Handwerksslot.transform.childCount;
            if (childcounterH > 0)
            {
                Child = Handwerksslot.gameObject.transform.GetChild(0);
                name = Child.name;
            }

            foreach (Transform Inventarslots in SlotPanel.transform)
            {
                Transform ChildH = Inventarslots.gameObject.transform.GetChild(0);
                int childcounterI = Inventarslots.transform.childCount;

                if (childcounterI == 1 && childcounterH > 1)
                {
                    Child.transform.SetParent(ChildH.transform);
                }
                if (childcounterI > 1 && childcounterH > 1)
                {
                    Transform ChildInventarslots = ChildH.gameObject.transform.GetChild(1);
                    nameInventarSlots = ChildInventarslots.name;
                }
                if (childcounterI > 1 && nameInventarSlots == name && childcounterH > 1)
                {
                    Child.transform.SetParent(ChildH.transform);
                }
            }
        }

        // Item von Inventar Cursor in Rightarm und Leftarm verschieben

        if (Input.GetKeyDown(KeyCode.X))  // Return
        {
            string count = CursorIndex.ToString();
            string Slotname = "Item";
            string InventorySlotname = Slotname + count+"UI";
            GameObject Slot = GameObject.Find(InventorySlotname);
            if (Slot == null)
                return;
            int counterI = Slot.transform.childCount;
            if (counterI > 1 && Rightarmchildcounter == 0)
            {
                Child = Slot.gameObject.transform.GetChild(1);
                if (!Child.CompareTag("Building"))
                {
                    Child.transform.SetParent(Rightarm.transform);
                    ActivateSpriteofChilds(Child);
                    Child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    var spritetake = Child.GetComponent<SpriteRenderer>();
                    spritetake.sortingOrder = 10;
                    Vector3 pos = Rightarm.transform.position;
                    float x = pos.x;
                    float y = pos.y;
                    Child.transform.position = new Vector3(x, y - 0.1f, 1);
                    Child.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            if (counterI > 1 && Leftarmchildcounter == 0 && Rightarmchildcounter >0)
            {
                Child = Slot.gameObject.transform.GetChild(1);
                if (!Child.CompareTag("Building"))
                {
                    Child.transform.SetParent(Leftarm.transform);
                    ActivateSpriteofChilds(Child);
                    Child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    var spritetake = Child.GetComponent<SpriteRenderer>();
                    spritetake.sortingOrder = 5;
                    Vector3 pos = Leftarm.transform.position;
                    float x = pos.x;
                    float y = pos.y;
                    Child.transform.position = new Vector3(x, y - 0.1f, 1);
                    Child.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }

        }
    }

    // Cursor Sprites ändern
    public void Cursorsprites()
    {
        if (CursorIndex <= 20 && CursorIndex > 0)
        {
            string Cursorcount = CursorIndex.ToString();
            string Cursorslot = "Slot";
            string Cursorslotname = Cursorslot + Cursorcount;
            slotnamecursor = GameObject.Find(Cursorslotname);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotCursor;
        }
        if (CursorIndex > 20 && RezeptPanel.activeSelf == true)
        {
            int CursorsaveCrafting = CursorIndex;
            CursorsaveCrafting -= 20;
            string Cursorcount = CursorsaveCrafting.ToString();
            string Cursorslot = "CraftSlot";
            string Cursorslotname = Cursorslot + Cursorcount;
            slotnamecursor = GameObject.Find(Cursorslotname);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotCursor;
        }
    }

    public void TakeFishtoInventory()
    {
        Leftarmchildcounter = Leftarm.transform.childCount;

        if (Leftarmchildcounter > 0)
        {
            Childleftarm = Leftarm.gameObject.transform.GetChild(0);
            Childleftarmname = Childleftarm.name;
            foreach (Transform InventarSlotsL in SlotPanel.transform)
            {
                Transform ChildF = InventarSlotsL.gameObject.transform.GetChild(0);
                int InventarSlotcounterL = ChildF.transform.childCount;
                if (InventarSlotcounterL == 1)
                {
                    string name = Childleftarm.name;
                    Childleftarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childleftarm.transform.SetParent(ChildF.transform);
                }
                Transform ChildthisSlot = ChildF.gameObject.transform.GetChild(1);
                ChildthisSlotname = ChildthisSlot.name;
                if (InventarSlotcounterL > 1 && InventarSlotcounterL < 20 && Childleftarmname == ChildthisSlotname)
                {
                    string name = Childleftarm.name;
                    Childleftarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Childleftarm.transform.SetParent(ChildF.transform);
                }
            }
        }
    }

    public void DeactivateSpriteofChilds(Transform Child)
    {
        myItems = Child.GetComponent<Items>();
        foreach(GameObject Children in myItems.Childlist)
        {
         if (Children.GetComponent<SpriteRenderer>() == true)
         Children.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void ActivateSpriteofChilds(Transform Child)
    {
        myItems = Child.GetComponent<Items>();
        foreach (GameObject Children in myItems.Childlist)
        {
            if (Children.GetComponent<SpriteRenderer>() == true)
                Children.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}


