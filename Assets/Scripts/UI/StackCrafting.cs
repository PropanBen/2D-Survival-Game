using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class StackCrafting : MonoBehaviour
{
    public Crafting myCrafting;
    public Sprite SlotItem;
    public GameObject RezeptPanel;
    public GameObject Handwerk;
    public GameObject CraftedItem;
    public string Name;
    public bool slot;

      private int _childcounter; //_
      public int childcounter
      {
          get
          {
              return _childcounter;//_
          }
          set
          {
            if (value != _childcounter) //_
            {
                myCrafting.ProcessRecipeResources();
            }
              _childcounter = value; //_
          }
      }


    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = SlotItem;
        CraftedItem = GameObject.Find("CraftedItem");
        slot = false;
        Invoke("SetImage", 0.1f);
    }

    // Update is called once per frame
    public void Update()
    {
        Handwerk = GameObject.Find("Handwerk");
        myCrafting = Handwerk.GetComponent<Crafting>();

        // Stack und Icons im Handwerk anzeigen


        childcounter = this.transform.childCount;
        int childcountertxt = childcounter - 1;
        string childcounterstr = childcountertxt.ToString();
        GameObject Slot = GameObject.Find(this.name);
        if (slot)
        {
            if (this.GetComponent<Image>() != null && this.GetComponent<Image>().sprite !=null)
            {
                Name = this.GetComponent<Image>().sprite.name;
            }
        }


        if (childcounter > 1)
        {
            Slot.GetComponentsInChildren<Text>()[0].text = childcounterstr;
            Transform ChildthisSlot = this.gameObject.transform.GetChild(1);
            Sprite Spritereader = ChildthisSlot.gameObject.GetComponent<Items>().Icon;
            Slot.gameObject.GetComponent<Image>().sprite = Spritereader;
        }
        if (childcounter == 1 && !Name.Contains("grey"))
        {
             Slot.gameObject.GetComponent<Image>().sprite = SlotItem;
             Slot.GetComponentsInChildren<Text>()[0].text = "0";
        }

    }

    public void SetImage()
    {
        this.gameObject.GetComponent<Image>().sprite = SlotItem;
        CraftedItem.gameObject.GetComponent<Image>().sprite = SlotItem;
        slot = true;
    }

}



