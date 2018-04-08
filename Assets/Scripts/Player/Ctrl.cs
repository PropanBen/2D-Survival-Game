using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    public SaveGameSettings mySaveGameSettings;
    public float speed = 1.0f;
    public bool setleft;
    public bool rechts;
    public bool links;
    public bool oben;
    public bool unten;
    public bool workrdy;
    public float timer = 1f;
    private float Etimer;
    private float Dtimer;
    public bool Dtiming;
    private bool Etiming;
    public bool Wftiming;
    public float Wftimer;
    public bool timing = false;
    public bool itemtakerR = false;
    public bool itemtakerL = false;
    public bool itemdropperR = false;
    public bool itemdropperL = false;
    public bool QisPressed = false;
    public bool EisPressed = false;
    public bool idle = false;
    public bool pfeil = false;
    public Inventar myInventar;
    private Transform Inventory;
    public GameObject InventoryPanel;
    public GameObject RezeptPanel;
    public GameObject EquipmentPanel;
    public GameObject MouseSlot;
    public GameObject Mouse;
    public GameObject Charakter;
    public GameObject Rightarm;
    public GameObject Leftarm;
    public GameObject Head;
    public GameObject Body;
    public GameObject Slotpanel;
    public GameObject PauseMenu;
    public GameObject SaveGameManager;
    public GameObject SoundManager;
    public Transform ChildR;
    public Transform ChildL;
    public AudioSource AudioSource;
    public AudioClip Heal;
    public AudioClip Eat;
    public AudioClip Drink;
    public AudioClip Throw;
    public AudioClip flop;
    private int childcounterM;
    public int childcounterR;
    public int childcounterL;
    public int childcounterH;
    public Animator animator;
    private bool Inventarbool = false;
    private bool Rezeptbool = false;
    private bool Equipmentbool = false;
    public bool attack = false;
    public float offset = 0.0f;
    public bool throwthing = false;
    public bool death;
    public static bool pause;
    public bool takefish;


    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        SaveGameManager = GameObject.Find("SaveGameManager");
        mySaveGameSettings = SaveGameManager.GetComponent<SaveGameSettings>();
        InventoryPanel = GameObject.Find("InventarPanel");
        RezeptPanel = GameObject.Find("RezeptPanel");
        EquipmentPanel = GameObject.Find("EquimentPanel");
        Mouse = GameObject.Find("Mouse");
        MouseSlot = GameObject.Find("MouseSlot");
        Slotpanel = GameObject.Find("SlotPanel");
        PauseMenu = GameObject.Find("PauseMenu");
        MouseSlot.SetActive(false);
        PauseMenu.SetActive(false);
        Charakter = GameObject.Find("Charakter");
        Rightarm = GameObject.Find("Rightarm");
        Leftarm = GameObject.Find("Leftarm");
        Head = GameObject.Find("Head");
        Body = GameObject.Find("Body");
        AudioSource = GetComponent<AudioSource>();
        Etimer = 1.5f;

        Dtimer = 1.5f;

        // Mit Animator verbinden --------------------------------------------------------

        animator = GetComponent<Animator>();
  
    }
    void Update()
    {

        if(death)
        {
            animator.SetBool("pickaxeR", false);
            animator.SetBool("pickaxeL", false);
        }

        childcounterM = Mouse.transform.childCount;
        childcounterR = Rightarm.transform.childCount;
        childcounterL = Leftarm.transform.childCount;
        if (childcounterR > 0)
            ChildR = Rightarm.gameObject.transform.GetChild(0);
        if (childcounterL > 0)
            ChildL = Leftarm.gameObject.transform.GetChild(0);

        if(Input.GetKey(KeyCode.E))
            Healing();

        Eating();
        Waterfill();
        Drinking();
        TakeItemR();
        TakeItemL();

        //*****************************************************************//
        //  HIER WERKZEUGANIMATIONEN EINFÜGEN                              //
        //*****************************************************************//

        // Werkzeuganimationen-----------------------------------------------------------
        // Axt
        if (childcounterR > 0 && ChildR.name.Contains("Axe"))
        { Workanimation("pickaxeR", null); }
        if (childcounterL > 0 && ChildL.name.Contains("Axe"))
        { Workanimation(null, "pickaxeL"); }
        //Spitzhacke
        if (childcounterR > 0 && ChildR.name.Contains("Pickaxe"))
        { Workanimation("pickaxeR", null); }
        if (childcounterL > 0 && ChildL.name.Contains("Pickaxe"))
        { Workanimation(null, "pickaxeL"); }
        // Angel
        if (childcounterR > 0 && ChildR.name.Contains("Rod"))
        { Workanimation("fishing", null); }

        // Waffenanimation-----------------------------------------------------------
        // Holzschwert
        if (childcounterR > 0 && ChildR.name.Contains("Sword"))
        { Workanimation("pickaxeR", null); }
        if (childcounterL > 0 && ChildL.name.Contains("Sword"))
        { Workanimation(null, "pickaxeL"); }
        // Bogen 
        if (childcounterL > 0 && ChildL.name.Contains("Bow") && pfeil)
        { Workanimation(null, "bogen"); }

        //Speer
        if (childcounterR > 0 && ChildR.name.Contains("Spear"))
        { Workanimation("speerR", null); }
        if (childcounterL > 0 && ChildL.name.Contains("Spear"))
        { Workanimation(null, "speerL"); }

        // Menu Öffnen

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }
        if (PauseMenu.activeSelf == true)
        { pause = true; }
        else { pause = false; }
               
      //  if(pause && !mySaveGameSettings.loading) {Time.timeScale = 0f;}
      //  else{Time.timeScale = 1f;}


        // Inventar öffnen----------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.I) && Inventarbool == false)
        {
            InventoryPanel.transform.localPosition = new Vector3(-500, 200);
            Inventarbool = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && Inventarbool == true)
        {
            InventoryPanel.transform.localPosition = new Vector3(-500, 2000);
            Inventarbool = false;
        }

            // Handwerk öffnen

        if (Input.GetKeyDown(KeyCode.C) && Rezeptbool == false)
        {
            RezeptPanel.transform.localPosition = new Vector3(-200, 60);
            Rezeptbool = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && Rezeptbool == true)
        {
            RezeptPanel.transform.localPosition = new Vector3(-500, 3000);
            Rezeptbool = false;
        }

        // Equipment öffnen
        if (Input.GetKeyDown(KeyCode.J) && Equipmentbool == false)
        {
            EquipmentPanel.transform.localPosition = new Vector3(-500, -100);
            Equipmentbool = true;
        }
        else if (Input.GetKeyDown(KeyCode.J) && Equipmentbool == true)
        {
            EquipmentPanel.transform.localPosition = new Vector3(-500, 4000);
            Equipmentbool = false;
        }


        // MouseSlot anzeigen

        if (childcounterM > 0)
        {
            MouseSlot.SetActive(true);
        }
        else
        {
            MouseSlot.SetActive(false);
        }

        // Charaktersteuerung
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("eating") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("drinking") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("fishing")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("TakeFish")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("TakeItemR")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("TakeItemL")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("waterfill") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {

        }
        else
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            Charakter.transform.position += move * speed * Time.deltaTime;
        }


        // Charakter drehen  (flip) -------------------------------------

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Space) && !death)
            setleft = true;
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space) && !death)
            setleft = false;

        if (setleft == true)
        { Charakter.transform.localScale = new Vector3(-1, 1, 1); }

        else { Charakter.transform.localScale = new Vector3(1, 1, 1); }


        // Walk Animation

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W)
         || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        { animator.SetBool("run", true); }
        else
        { animator.SetBool("run", false); }

    }


    // Arbeitsanimation

    public void Workanimation(string parameterR, string parameterL)
    {
        if (!death)
        {
            // Rechte Animation
            if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift))
            { animator.SetBool(parameterR, true); }
            else
            { animator.SetBool(parameterR, false); }

            // Linke Animation
            if (Input.GetMouseButton(1))
            { animator.SetBool(parameterL, true); }
            else
            { animator.SetBool(parameterL, false); }
        }

        
    }
    // Item mit rechter Hand aufnehmen oder droppen Animation

    public void TakeItemR()
    {
        if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift) && childcounterR == 0)
        {
          animator.SetBool("takeitemr", true);
          EisPressed = true;
        }
        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && childcounterR == 1)
        {
          animator.SetBool("takeitemr", true);
          QisPressed = true;
        }
        else
        {
            animator.SetBool("takeitemr", false);
        }
    }
    public void TakeItemRBooltrue()
    {
            itemtakerR = true;
    }
    public void TakeItemRBoolfalse()
    {  
            itemtakerR = false;
            EisPressed = false;
    }
    public void DropItemRBooltrue()
    {
        itemdropperR = true;
    }
    public void DropItemRBoolfalse()
    {
        itemdropperR = false;
        QisPressed = false;
    }

    // Item mit linker Hand aufnehmen oder droppen Animation
    public void TakeItemL()
    {
        if (Input.GetMouseButtonUp(1) && childcounterL == 0)
        {
            animator.SetBool("takeiteml", true);
            EisPressed = true;
        }
        else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift) && childcounterL == 1)
        {
            animator.SetBool("takeiteml", true);
            QisPressed = true;
        }
        else
        { animator.SetBool("takeiteml", false); }
    }
    public void TakeItemLBooltrue()
    {
        itemtakerL = true;
    }
    public void TakeItemLBoolfalse()
    {
            itemtakerL = false;
            EisPressed = false;
    }
    public void DropItemLBooltrue()
    {
        itemdropperL = true;
    }
    public void DropItemLBoolfalse()
    {
        itemdropperL = false;
        QisPressed = false;
    }

    // Essen Animation

    public void Eating()
    {
        if (childcounterR > 0)
        {
            ChildR = Rightarm.gameObject.transform.GetChild(0);
            Items myItems = ChildR.GetComponent<Items>();

            if (myItems.Essensliste.Contains(ChildR.name) && Input.GetKey(KeyCode.E))
            {
                Etiming = true;
                animator.SetBool("eating", true);
            }
            if (Etiming == true)
            {
                Etimer -= Time.deltaTime;

                if (Etimer < 0)
                {
                    Etiming = false;
                    animator.SetBool("eating", false);
                    int value = myItems.essenswerte[ChildR.name];
                    Charakter.SendMessage("Hungertimer", value);
                    Destroy((ChildR as Transform).gameObject);
                }
            }
            else
            {
                Etimer = 1.5f;
            }
        }
    }

    public void PlayEatSound()
    {
        SoundManager.SendMessage("PlaySound", "eating");
    }

    // Healing Animation

    public void Healing()
    {
        if (childcounterR > 0)
        {
            ChildR = Rightarm.gameObject.transform.GetChild(0);
            Items myItems = ChildR.GetComponent<Items>();
            if (myItems.Heilungsliste.Contains(ChildR.name))
            {
                animator.SetBool("eating", true);
            }
            Invoke("ForceHealing", 1f);
        }
    }
    public void ForceHealing()
    {
        if (childcounterR > 0)
        {
            ChildR = Rightarm.gameObject.transform.GetChild(0);
            Items myItems = ChildR.GetComponent<Items>();
            if (myItems.Heilungsliste.Contains(ChildR.name))
            {
                int value = myItems.healwerte[ChildR.name];
                Charakter.SendMessage("HealDamage", value);
                SoundManager.SendMessage("PlaySound", "heal");
                Destroy((ChildR as Transform).gameObject);
                animator.SetBool("eating", false);
            }
        }
    }
        


    public void Drinking()
    {
        if (childcounterR > 0)
        {

            ChildR = Rightarm.gameObject.transform.GetChild(0);
            Items myItems = ChildR.GetComponent<Items>();
            if (Input.GetKey(KeyCode.E) && myItems.Drinkinglist.Contains(ChildR.name))
            {
                Dtiming = true;
                animator.SetBool("drinking", true);

            }

            if (Dtiming == true)
            {
                Dtimer -= Time.deltaTime;

                if (Dtimer < 0.5)
                {
                    SoundManager.SendMessage("PlaySound", "drinking");
                }
                if (Dtimer < 0)
                {
                    string ChildRnamenew = ChildR.name.Replace("_water", "");
                    Destroy((ChildR as Transform).gameObject);
                    GameObject NewObj = Instantiate(Prefabliste.Instance().GetGameObject(ChildRnamenew), new Vector3(0, 0, 0), Quaternion.identity);
                    NewObj.name = NewObj.name.Replace("(Clone)","");
                    NewObj.transform.SetParent(Rightarm.transform);
                    NewObj.transform.localPosition = new Vector3(0, -0.1f, 1);
                    NewObj.transform.localEulerAngles = new Vector3(0, 0, 0);
                    var spritetake = NewObj.GetComponent<SpriteRenderer>();
                    spritetake.sortingOrder = 10;
                    Dtiming = false;
                    animator.SetBool("drinking", false);
                    int value = myItems.drinkingwerte[ChildR.name];
                    Charakter.SendMessage("Thirsttimer", value);
                }
            }
            else
            {
                Dtimer = 1.5f;
            }
        }
    }

    // Fisch von Angel in linke Hand nehmen
    public void GetFishfromRod()
    {
        animator.SetBool("takefish", true);
    }
    public void BreakGetFishfromRod()
    {
        animator.SetBool("takefish", false);
    }


    // Wasser auffüllen

    public void Waterfill()
    {
        if (childcounterR > 0)
        {
            ChildR = Rightarm.gameObject.transform.GetChild(0);
            Items myItems = ChildR.GetComponent<Items>();
            if (Input.GetKey(KeyCode.E) && myItems.Waterfilllist.Contains(ChildR.name))
            {
                Wftiming = true;
                animator.SetBool("waterfill", true);
            }
            if (Wftiming == true)
            {
                Wftimer -= Time.deltaTime;
                if (Wftimer < 0)
                {
                    Wftiming = false;
                    animator.SetBool("waterfill", false);
                }
            }
            else { Wftimer = 1.5f; }
        }
    }


    public void ExitRezeptPanel()
    {
       // RezeptPanel.SetActive(false);
        RezeptPanel.transform.localPosition = new Vector3(-500, 2000);
    }

    public void ExitInventarPanel()
    {
       // myInventar.InventoryPanel.SetActive(false);
        InventoryPanel.transform.localPosition = new Vector3(-500, 2000);
    }

    public void ExitEquipmentPanel()
    {
        //EquipmentPanel.SetActive(false);
        EquipmentPanel.transform.localPosition = new Vector3(-500, 2000);
    }

    public void Attack()
    {
        attack = true;
    }
    public void EndAttack()
    {
        attack = false;
    }
    public void StartWork()
    {
        workrdy = true;
    }
    public void EndWork()
    {
        workrdy = false;
    }

    public void OnTriggerStay2D(Collider2D Attacker)
    {
        // Tierschaden

        if (Attacker.GetComponent<AnimalCtrl>() != null)
        {
            AnimalCtrl myAnimalCtrl = Attacker.GetComponent<AnimalCtrl>();

            if (myAnimalCtrl.attacking == true && myAnimalCtrl.distance <= 0.251f)
               
            {
                int value = myAnimalCtrl.animaldamage[Attacker.name];
                Charakter.SendMessage("TakeDamage", value);
            }
            if(death)
            {
                myAnimalCtrl.attackrange = false;
                myAnimalCtrl.attacking = false;
                myAnimalCtrl.movetoplayer = false;
                myAnimalCtrl.animator.SetBool("attack", false);
                Attacker = null;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D Spear)
    {
        // Speer Waffe
        if(Spear.name == "Spear" && Spear.GetComponent<Items>() != null)
        {
            Items mySpear = Spear.GetComponent<Items>();
            Speer MySpear = Spear.GetComponent<Speer>();
            int value = 0;
            if (mySpear.Weapon.TryGetValue(Spear.name, out value) && MySpear.flying)
            {
                Charakter.SendMessage("TakeDamage", value);
            }
        }

        // Npc Schaden
        if(Spear.CompareTag("weapon") && Spear.GetComponent<Items>() != null)
        {
           Transform Root = Spear.transform.root;
            if (Root.GetComponent<Npc_sword>() != null)
            {
                Npc_sword mySpear = Root.GetComponent<Npc_sword>();
                Items myItems= Spear.GetComponent<Items>();
                if (mySpear.attack == true)
                {
                    int value = 0;
                    if (myItems.Weapon.TryGetValue(Spear.name, out value))
                    {
                        Charakter.SendMessage("TakeDamage", value);
                    }
                }
            }
        }

    }

        public void StartSpearthrow()
    {
        throwthing = true;
    }
    public void EndSpearthrow()
    {
        throwthing = false;
    }
    public void QuitPause()
    {
        pause = false;
        Time.timeScale = 1f;
    }


    public void Itemdrop()
    {
        foreach(Transform Slot in Slotpanel.transform)
        {
            int childcounterS = Slot.transform.childCount;
            if (childcounterS > 0)
            {
                Transform ItemUI = Slot.gameObject.transform.GetChild(0);
                int childcounterUI = ItemUI.transform.childCount;
                if (childcounterUI > 1)
                {
                    for (int i = 1; i < childcounterUI; i++)
                    {
                        float placeX = Random.Range(-0.2f, 0.2f);
                        float placeY = Random.Range(-0.2f, 0.2f);
                        Transform Item = ItemUI.gameObject.transform.GetChild(1);
                        Item.transform.position = this.transform.position;
                        Item.transform.position += new Vector3(placeX, placeY, 0);
                        Item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                        Item.transform.SetParent(null);
                        var spritetake = Item.GetComponent<SpriteRenderer>();
                        spritetake.sortingOrder = 0;
                        Item.transform.localScale = new Vector3(1,1,1);
                    }
                }
            }
        }
    }

    public void Takefishtrue()
    {
        takefish = true;
    }
    public void Takefishfalse()
    {
        takefish = false;
    }
}