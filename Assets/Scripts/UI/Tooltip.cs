using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Tooltip : MonoBehaviour {

    private Items myItems;
    private float currentDurability;
    private float maxDurability;
    public GameObject Tooltippanel;
    private GameObject Mouse;
    private GameObject Rightarm;
    private GameObject Leftarm;
    private Transform Child;
    private int childcounterR;
    private int childcounterL;


    // Use this for initialization
    void Start ()
    {
       // Tooltippanel.SetActive(false);
       Tooltippanel = GameObject.FindGameObjectWithTag("Tooltip");
       Mouse = GameObject.Find("Mouse");
       Rightarm = GameObject.Find("Rightarm");
       Leftarm = GameObject.Find("Leftarm");
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnMouseOver()
    {
        GameObject Slot = GameObject.Find(this.name);
        int childcounterSlot = Slot.transform.childCount;
        if(this.name.Contains("Rightarm"))
        childcounterR = Rightarm.transform.childCount;
        if (this.name.Contains("Leftarm"))
        childcounterL = Leftarm.transform.childCount;
        if (childcounterSlot >1 || childcounterR >0 || childcounterL >0)
        {
            Tooltippanel.SetActive(true);
            if(childcounterSlot > 1)
            Child = Slot.gameObject.transform.GetChild(1);
            if(childcounterR >0 && this.name.Contains("Rightarm"))
            Child = Rightarm.gameObject.transform.GetChild(0);
            if (childcounterL >0 && this.name.Contains("Leftarm"))
            Child = Leftarm.gameObject.transform.GetChild(0);
            myItems = Child.GetComponent<Items>();
            currentDurability = Child.GetComponent<Items>().currentDurability;
            string currentDurabilitystr = currentDurability.ToString();
            maxDurability = Child.GetComponent<Items>().maxDurability;
            string maxDurabilitystr = maxDurability.ToString();
            Tooltippanel.GetComponentsInChildren<Text>()[0].text = Child.name;
            Tooltippanel.GetComponentsInChildren<Text>()[1].text = "Durability:\n" + currentDurabilitystr +" / "+ maxDurabilitystr;
            Tooltippanel.GetComponentsInChildren<Text>()[2].text = GetItemAttribute(Child.name);

            Vector3 mousePos = Input.mousePosition;
            float x = mousePos.x;
            float y = mousePos.y;
            mousePos = new Vector3(x + 110, y + 90, +5);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Tooltippanel.transform.position = mousePos;
        }
    }

    void OnMouseExit()
    {
        Tooltippanel.transform.position = new Vector3(15.2f, 628, 0);
    }

    private string GetItemAttribute(string name)
    {
        string text = "";
        int value = 0;
        float valueA = 0;

        if (myItems.healwerte.TryGetValue(name, out value))
        {
            text = "Heal +"+ value;
        }
        if (myItems.essenswerte.TryGetValue(name, out value))
        {
            text = "Hunger +" + value;
        }
        if (myItems.drinkingwerte.TryGetValue(name, out value))
        {
            text = "Drinking +"+ value;
        }
        if (myItems.fuelwerte.TryGetValue(name, out value))
        {
            text = "Fuel "+ value;
        }
        if (myItems.Weapon.TryGetValue(name, out value))
        {
            text = "Damage "+ value;
        }
        if(Equipment.instance.Armour.TryGetValue(name, out valueA))
        {
            text = "Armour +"+valueA;
        }
        if (myItems.ItemDamage.TryGetValue(name, out value))
        {
            text = "Damage " + value;
        }


        return text;
    }
}
