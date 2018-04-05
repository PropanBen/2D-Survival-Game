using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackEquipment : MonoBehaviour
{
    public Mouse myMouse;
    public Sprite SlotItem;
    public GameObject EquipmentPanel;
    public GameObject Charakter;
    public GameObject Mouse;
    private string NameItem;
    private GameObject PlayerEquip;
    private GameObject SlotUI;
    private GameObject Slot;
    private Sprite EquipmentSprite;
    private Object[][] AtlasArray;


    // Use this for initialization
    void Start()
    {
        Mouse = GameObject.Find("Mouse");
        myMouse = Mouse.GetComponent<Mouse>();
        Charakter = GameObject.Find("Charakter");


        //*****************************************************************//
        //  HIER NEUE RÜSTUNGSATLASE EINFÜGEN                              //
        //*****************************************************************//
        Object[] SpriteAtlas_Leather = Resources.LoadAll<Sprite>("Charakter/Leather");
        Object[] SpriteAtlas_Body = Resources.LoadAll<Sprite>("Charakter/Body_Leather");
        Object[] SpriteAtlas_Iron = Resources.LoadAll<Sprite>("Charakter/Iron");
        AtlasArray = new Object[][] { SpriteAtlas_Leather, SpriteAtlas_Body, SpriteAtlas_Iron };


    }


    // Update is called once per frame
    void Update()
    {

        // Stack und Icons im Inventar anzeigen
      
            int childcounter = this.transform.childCount;
            int childcountertxt = childcounter - 1;
            string childcounterstr = childcountertxt.ToString();
            //GameObject Slot = GameObject.Find(this.name);// +UI

            if (childcounter > 1)
            {
                Slot = GameObject.Find(this.name);// +UI

            // Icon im UI Anzeigen
                Transform ChildthisSlot = this.gameObject.transform.GetChild(1);
                Sprite Spritereader = ChildthisSlot.gameObject.GetComponent<Items>().Icon;
                Slot.gameObject.GetComponent<Image>().sprite = Spritereader;

                // Ausrüstung am Charakter im Equipment Fenster und am Live Charakter anzeigen
                string NameItem = this.name.Substring(0, this.name.Length - 6);
                string Name = NameItem + "UI";
                SlotUI = GameObject.Find(Name);
                PlayerEquip = GameObject.Find(NameItem);

                // Richtiges Item aus richtigem Atlas holen
                foreach (Object[] atlas in AtlasArray)
                {
                    for (int i = 0; i < atlas.Length; i++)
                    {
                        if (atlas[i].name == ChildthisSlot.name)
                        {
                            SlotUI.gameObject.GetComponent<SpriteRenderer>().sprite = atlas[i] as Sprite;
                            PlayerEquip.gameObject.GetComponent<SpriteRenderer>().sprite = atlas[i] as Sprite;
                        }
                    }
                }
            }
            if (childcounter == 1)
            {
                // Leere Slots auf Deflaut Sprite setzen
                Slot = GameObject.Find(this.name);
                NameItem = this.name.Substring(0, this.name.Length - 6);
                string Nameempty = NameItem+"UI"; //+"UI"
                SlotUI = GameObject.Find(Nameempty);
                PlayerEquip = GameObject.Find(NameItem);
                Slot.GetComponentsInChildren<Text>()[0].text = "0";
                Slot.gameObject.GetComponent<Image>().sprite = SlotItem;
                EquipmentSprite = Resources.Load("Charakter/" + NameItem, typeof(Sprite)) as Sprite;
                SlotUI.gameObject.GetComponent<SpriteRenderer>().sprite = EquipmentSprite;
                PlayerEquip.gameObject.GetComponent<SpriteRenderer>().sprite = EquipmentSprite;
            }
        }
    }

