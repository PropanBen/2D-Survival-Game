using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerbars : MonoBehaviour {

    public Ctrl myCtrl;
    public Equipment myEquipment;
    private float maxHealth = 100;
    public float currentHealth = 100;
    private float maxArmour = 100;
    private float currentArmour = 0;
    private float maxThirst = 100;
    public float currentThirst = 100;
    public float currentHunger = 100;
    private float maxHunger = 100;
    public float currentEXP = 0;
    private float maxEXP = 100;
    public int LvLcounter = 0;
    private float timer = 1.0f;
    private GameObject Charakter;
    public GameObject EXPbarCurrent;
    public GameObject HealthbarCurrent;
    public GameObject ThirstbarCurrent;
    public GameObject HungerbarCurrent;
    public GameObject AmourbarCurrent;
    public GameObject Damageeffect;
    public GameObject Deathmenu;
    public GameObject Respawnpoint;
    public Text ratioTextHealth;
    public Text ratioTextThirst;
    public Text ratioTextHunger;
    public Text ratioTextEXP;
    public Text ratioTextArmour;
    public Text LvL;
    public Image CurrentHealthbar;
    public Image CurrentThirstbar;
    public Image CurrentHungerbar;
    public Image CurrentEXPbar;
    public Image CurrentArmourbar;
    public List<SpriteRenderer> Childlist;



    // Use this for initialization
    void Start ()
    {
        Childlist = new List<SpriteRenderer>();
        UpdateHealthbar();
        UpdateArmourbar();
        TakeChildstoList(transform);
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
        Damageeffect.SetActive(false);
        Deathmenu.SetActive(false);
        Respawnpoint.transform.position = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        Thirsttimer(0);
        Hungertimer(0);
        UpdateThirstbar();
        UpdateHungerbar();
        UpdateEXPbar();
        UpdateArmourbar();
        SetArmour();

        if(currentHealth <= 0)
        {
            Damageeffect.SetActive(true);
            myCtrl.death = true;
            myCtrl.animator.SetBool("death", true);
        }

        if (!myCtrl.death && (currentHunger <= 0 || currentThirst <= 0))
        {
            TakeDamage(1);
        }
    }

    private void UpdateHealthbar()
    {
        float ratio = currentHealth / maxHealth;
        HealthbarCurrent.transform.localScale = new Vector3(ratio, 1, 1);
        ratioTextHealth.text = (ratio * 100).ToString("0") + "%";
    }

    private void UpdateThirstbar()
    {
        float ratio = currentThirst / maxThirst;
        ThirstbarCurrent.transform.localScale = new Vector3(ratio, 1, 1);
        ratioTextThirst.text = (ratio * 100).ToString("0") + "%";
    }

    private void UpdateHungerbar()
    {
        float ratio = currentHunger / maxHunger;
        HungerbarCurrent.transform.localScale = new Vector3(ratio, 1, 1);
        ratioTextHunger.text = (ratio * 100).ToString("0") + "%";
    }

    private void UpdateEXPbar()
    {
        float ratio = currentEXP / maxEXP;
        EXPbarCurrent.transform.localScale = new Vector3(ratio, 1, 1);
        ratioTextEXP.text = currentEXP.ToString("0") + "%";
        LvL.text = LvLcounter.ToString();
    }
    private void UpdateArmourbar()
    {
        float ratio = currentArmour / maxArmour;
        AmourbarCurrent.transform.localScale = new Vector3(ratio, 1, 1);
        ratioTextArmour.text = currentArmour.ToString("0") + "%";
    }



    // Damage
    private void TakeDamage(float damage)
    {
        if (timer <= 0)
        {
           // Equipment.Instance.CalcArmourValues();
            currentHealth -= Equipment.Instance.ReduceDurability(damage);
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            // Alle Charakterteile rot anzeigen bei Schaden

            foreach (SpriteRenderer Children in Childlist)
            {
                Color tmp = this.GetComponent<SpriteRenderer>().color;
                Children.color = new Color(255, 0, 0, 255);
            }
            Invoke("resetDamageColor", 0.2f);
            timer = 1.0f;
        }
        // Selbstredend
           UpdateArmourbar();
           UpdateHealthbar();
    }

    private void SetArmour()
    {
        currentArmour = Equipment.Instance.ArmourValueTotal;
    }

    // Heal

    private void HealDamage(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthbar();
    } 

    // Thirsttimer
    private void Thirsttimer(float thirst)
    {
        currentThirst += thirst;
        currentThirst -= Time.deltaTime *0.1f;
        if (currentThirst > 100)
            currentThirst = 100;
        if (currentThirst < 0)
            currentThirst = 0;
    }

    // Hungertimer
    private void Hungertimer(float hunger)
    {
        currentHunger += hunger;
        currentHunger -= Time.deltaTime * 0.2f;
        if (currentHunger > 100)
            currentHunger = 100;
        if (currentHunger < 0)
            currentHunger = 0;
    }

    // EXP
    public void EXP(float exp)
    {
        currentEXP += exp;
        if (currentEXP < 0)
            currentEXP = 0;
        if (currentEXP > 100)
        {
            float temp = currentEXP - 100;
            currentEXP = 0;
            currentEXP += temp;
            LvLcounter++;
        }
        if(LvLcounter > 100)
        {
            LvLcounter = 100;
        }
    }

    // Rote Damage Anzeige zurücksetzen
    void resetDamageColor()
    {
        foreach (SpriteRenderer Children in Childlist)
        {
            Children.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }

    private void TakeChildstoList(Transform obj)
    {
        foreach (Transform child in obj)
        {
            SpriteRenderer TempRenderer = child.GetComponent<SpriteRenderer>();
            if (TempRenderer != null)
            {
                Childlist.Add(TempRenderer);
                TakeChildstoList(child);
            }
        }
    }

    private void ActivateDeathmenu()
    {
        Deathmenu.SetActive(true);
    }

    public void Respawn()
    {
        if (currentHealth < 20f)
            currentHealth = 20f;
        myCtrl.death = false;
        Deathmenu.SetActive(false);
        Damageeffect.SetActive(false);
        myCtrl.animator.SetBool("death", false);
        this.transform.position = Respawnpoint.transform.position;
        if (currentHunger< 20f)
            currentHunger = 20f;
        if (currentThirst < 20f)
            currentThirst = 20f;
        UpdateThirstbar();
        UpdateHungerbar();
        UpdateHealthbar();
    }


}
