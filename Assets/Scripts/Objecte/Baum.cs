using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baum : MonoBehaviour
{
    public Ctrl myCtrl;
    public SoundSettings mySoundsettings;
    public GameObject SoundManager;
    public GameObject Charakter;
    public Dictionary<string, int> BaumDurability;
    public Dictionary<string, int> BaumDamage;
    public int currentDurability;
    Animator animator;
    public int id;


    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        mySoundsettings = SoundManager.GetComponent<SoundSettings>();
        animator = GetComponent<Animator>();
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();

        // Haltbarkeit
        BaumDurability = new Dictionary<string, int>();
        BaumDurability.Add("Beech", 25);
        BaumDurability.Add("Firtree", 50);

        // Schaden
        BaumDamage = new Dictionary<string, int>();
        BaumDamage.Add("Beech", 5);
        BaumDamage.Add("Firtree", 10);

        // Eigene Haltbarkeit festlegen
        currentDurability = BaumDurability[this.name];

    }

    void Update()
    {
        if (currentDurability <= 0)
        {
            animator.SetBool("fall", true);
            if(id == 0)
            id = mySoundsettings.PlaySound("treefall");
        }
        if (myCtrl.workrdy == false)
            animator.SetBool("hit", false);
    }

    void OnTriggerEnter2D(Collider2D Tool)
    {
        if (Tool.name.Contains("Axe") && myCtrl.workrdy == true)
        {
            // Referenz herstellen
            Items myItems = Tool.gameObject.GetComponent<Items>();
            // Von Item Schaden holen
            int dmgvalue = myItems.ItemDamage[Tool.name];
            // Eigenen Damage holen
            int owndmgvalue = BaumDamage[this.name];

            // Haltbarkeit Tool / Ader gegenseitig abziehen
            Tool.SendMessage("TakeDamage", owndmgvalue);
            currentDurability -= dmgvalue;

            SoundManager.SendMessage("PlaySound", "axt");
            animator.SetBool("hit", true);
        }
    }

    private void DropItem()
    {
        for (int i = 0; i < 3; i++)
        {
            float placeX = Random.Range(-0.5f, 0.5f);
            float placeY = Random.Range(-0.5f, 0.5f);          
            // Log droppen
            GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject("Log"), new Vector3(0, 0, 0), Quaternion.identity);
            Spawn.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
            Spawn.name = "Log";
            var spritetake = Spawn.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 1;

            // Ast droppen
            placeX = Random.Range(-0.5f, 0.5f);
            placeY = Random.Range(-0.5f, 0.5f);
            GameObject Spawn2 = Instantiate(Prefabliste.Instance().GetGameObject("Twig"), new Vector3(0, 0, 0), Quaternion.identity);
            Spawn2.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
            Spawn2.name = "Twig";
            var spritetake2 = Spawn2.GetComponent<SpriteRenderer>();
            spritetake2.sortingOrder = 1;

            // Resin droppen
            placeX = Random.Range(-0.5f, 0.5f);
            placeY = Random.Range(-0.5f, 0.5f);
            GameObject Spawn3 = Instantiate(Prefabliste.Instance().GetGameObject("Resin"), new Vector3(0, 0, 0), Quaternion.identity);
            Spawn3.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
            Spawn3.name = "Resin";
            var spritetake3 = Spawn3.GetComponent<SpriteRenderer>();
            spritetake3.sortingOrder = 1;

            // Sapling droppen
            placeX = Random.Range(-0.5f, 0.5f);
            placeY = Random.Range(-0.5f, 0.5f);
            GameObject Spawn4 = Instantiate(Prefabliste.Instance().GetGameObject("Sapling"), new Vector3(0, 0, 0), Quaternion.identity);
            Spawn4.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
            Spawn4.name = "Sapling";
            var spritetake4 = Spawn4.GetComponent<SpriteRenderer>();
            spritetake4.sortingOrder = 1;
        }      
        Destroy(this.gameObject);
        Charakter.SendMessage("EXP", 10);
    }
}
