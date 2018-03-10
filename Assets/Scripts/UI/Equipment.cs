using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment
{
    private GameObject Head;
    private GameObject Body;
    private GameObject Rightshoulder;
    private GameObject Leftshoulder;
    private GameObject Rightarm;
    private GameObject Leftarm;
    private GameObject Rightlegupper;
    private GameObject Leftlegupper;
    private GameObject Rightleg;
    private GameObject Leftleg;
    public Dictionary<string, float> Armour;


    public static Equipment Instance
    {
        get
        {
            if (instance == null) instance = new Equipment(); return instance;
        }
    }

    private static Equipment instance;

    private Equipment()
    {
        // Armour--------------------------------------------------
   
        //Leather
        Armour = new Dictionary<string, float>();
        Armour.Add("Head_Leather", 10);
        Armour.Add("Body_Leather", 20);
        Armour.Add("Rightshoulder_Leather", 10);
        Armour.Add("Leftshoulder_Leather", 10);
        Armour.Add("Rightarm_Leather", 5);
        Armour.Add("Leftarm_Leather", 5);
        Armour.Add("Rightlegupper_Leather", 10);
        Armour.Add("Leftlegupper_Leather", 10);
        Armour.Add("Rightleg_Leather", 10);
        Armour.Add("Leftleg_Leather", 10);

        //Iron
        Armour.Add("Head_Iron", 20);
        Armour.Add("Body_Iron", 40);
        Armour.Add("Rightshoulder_Iron", 20);
        Armour.Add("Leftshoulder_Iron", 20);
        Armour.Add("Rightarm_Iron", 10);
        Armour.Add("Leftarm_Iron", 10);
        Armour.Add("Rightlegupper_Iron", 20);
        Armour.Add("Leftlegupper_Iron", 20);
        Armour.Add("Rightleg_Iron", 20);
        Armour.Add("Leftleg_Iron", 20);

    }

    public GameObject GetHead
    {
        set
        {
            Head = value;
            if (value == null)
            { GameObject.Find("HeadItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Head.GetComponent<Items>();
                GameObject.Find("HeadItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Head);  
            }
        }
        get
        {
            Debug.Log("geht"); return Head;
        }
    }
    public GameObject GetBody
    {
        set
        {
            Body = value;
            if (value == null)
            { GameObject.Find("BodyItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Body.GetComponent<Items>();
                GameObject.Find("BodyItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Body);
            }
        }
    }
    public GameObject GetRightshoulder
    {
        set
        {
            Rightshoulder = value;
            if (value == null)
            { GameObject.Find("RightshoulderItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Rightshoulder.GetComponent<Items>();
                GameObject.Find("RightshoulderItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Rightshoulder);
            }
        }
    }
    public GameObject GetLeftshoulder
    {
        set
        {
            Leftshoulder = value;
            if (value == null)
            { GameObject.Find("LeftshoulderItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Leftshoulder.GetComponent<Items>();
                GameObject.Find("LeftshoulderItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Leftshoulder);
            }
        }
    }
    public GameObject GetRightarm
    {
        set
        {
            Rightarm = value;
            if (value == null)
            { GameObject.Find("RightarmItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Rightarm.GetComponent<Items>();
                GameObject.Find("RightarmItemUI").GetComponentsInChildren<Text>()[0].text = "" +ConvertAmour(Rightarm);
            }
        }
    }
    public GameObject GetLeftarm
    {
        set
        {
            Leftarm = value;
            if (value == null)
            { GameObject.Find("LeftarmItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Leftarm.GetComponent<Items>();
                GameObject.Find("LeftarmItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Leftarm);
            }
        }
    }
    public GameObject GetRightlegupper
    {
        set
        {
            Rightlegupper = value;
            if (value == null)
            { GameObject.Find("RightlegupperItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Rightlegupper.GetComponent<Items>();
                GameObject.Find("RightlegupperItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Rightlegupper);
            }
        }
    }
    public GameObject GetLeftlegupper
    {
        set
        {
            Leftlegupper = value;
            if (value == null)
            { GameObject.Find("LeftlegupperItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Leftlegupper.GetComponent<Items>();
                GameObject.Find("LeftlegupperItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Leftlegupper);
            }
        }
    }
    public GameObject GetRightleg
    {
        set
        {
            Rightleg = value;
            if (value == null)
            { GameObject.Find("RightlegItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Rightleg.GetComponent<Items>();
                GameObject.Find("RightlegItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Rightleg);
            }
        }
    }
    public GameObject GetLeftleg
    {
        set
        {
            Leftleg = value;
            if (value == null)
            { GameObject.Find("LeftlegItemUI").GetComponentsInChildren<Text>()[0].text = "0"; }
            else
            {
                Items myItems = Leftleg.GetComponent<Items>();
                GameObject.Find("LeftlegItemUI").GetComponentsInChildren<Text>()[0].text = "" + ConvertAmour(Leftleg);
            }
        }
    }

    public void EquipItem(GameObject item)
    {
        string Name = item.name.Split('_')[0];
        switch (Name)
        {
            case "Head": GetHead = item; break;
            case "Body": GetBody = item; break;
            case "Rightshoulder": GetRightshoulder = item; break;
            case "Leftshoulder": GetLeftshoulder = item; break;
            case "Rightarm": GetRightarm = item; break;
            case "Leftarm": GetLeftarm = item; break;
            case "Rightlegupper": GetRightlegupper = item; break;
            case "Leftlegupper": GetLeftlegupper = item; break;
            case "Rightleg": GetRightleg = item; break;
            case "Leftleg": GetLeftleg = item; break;
            default: Debug.Log("error"); break;
        }
        CalcArmourValues();
    }
    public void UnEquipItem(GameObject item)
    {
        string Name = item.name.Split('_')[0];
        switch (Name)
        {
            case "Head": GetHead = null; break;
            case "Body": GetBody = null; break;
            case "Rightshoulder": GetRightshoulder = null; break;
            case "Leftshoulder": GetLeftshoulder = null; break;
            case "Rightarm": GetRightarm = null; break;
            case "Leftarm": GetLeftarm = null; break;
            case "Rightlegupper": GetRightlegupper = null; break;
            case "Leftlegupper": GetLeftlegupper = null; break;
            case "Rightleg": GetRightleg = null; break;
            case "Leftleg": GetLeftleg = null; break;
            default: Debug.Log("error"); break;
        }
        CalcArmourValues();
    }

    public void CalcArmourValues()
    {
        float armourSum = 0;
        armourSum += ReadValue(GameObject.Find("HeadItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("BodyItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("RightshoulderItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("LeftshoulderItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("RightarmItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("LeftarmItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("RightlegupperItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("LeftlegupperItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("RightlegItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);
        armourSum += ReadValue(GameObject.Find("LeftlegItemUI").transform.GetChild(0).GetComponentsInChildren<Text>()[0].text);

        armourValueTotal = armourSum;
    }

    private float ReadValue(string text)
    {
        if (text == "") return 0;       
            float reValue = float.Parse(text);
            return reValue;       
    }

    private float armourValueTotal;
    public float ArmourValueTotal
    {
        get
        {
            return armourValueTotal;
        }
    }

    public float ReduceDurability(float damage)
    {
        float armourDamage = armourValueTotal - damage;

        ChooseArmour(damage * 1f);

        if(armourDamage>0)
        {
            CalcArmourValues();
           // armourValueTotal -= damage;
            damage = 0;
        }
        else
        {
            armourValueTotal = 0;
            damage -= armourDamage;
        }

        return damage;
    }

    public void ChooseArmour(float durabilityDamage)
    {
        int value = Random.Range(0, 10);
        int loopcounter = 0;
        Items Child = GameObject.Find("CharakterSlots").transform.GetChild(value).GetComponentInChildren<Items>();
        while (Child == null)
        {
                loopcounter++;
                value++;
                if (value >= 10) { value = 0; }
                Child = GameObject.Find("CharakterSlots").transform.GetChild(value).GetComponentInChildren<Items>();
                if (loopcounter >= 10) { return; }           
        }
        Child.GetComponentInChildren<Items>().currentDurability -= durabilityDamage;
        EquipItem(Child.gameObject);

    }

    public float ConvertAmour(GameObject Obj)
    {
        Items myItems = Obj.GetComponent<Items>();
        float value = (Armour[Obj.name]);
        float calc = (value / 100) * myItems.currentDurability;
        return calc;
    }
}
