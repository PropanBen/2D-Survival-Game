using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    public Ctrl myCtrl;
    public GameObject Charakter;
    public MapGenerator myMapgenerator;
    public GameObject Mapgenerator;
    public Vector3 Targetposition;
    public Vector3 Target;
    public Animator animator;
    public GameObject Rightarm;
    public GameObject Weapon;
    public LayerMask layermask;
    public GameObject Enemy;
    private float currentHealth = 100;
    public bool moverandom;
    public bool death;
    public bool attacking;
    public bool setleft;
    private float speed = 0.8f;
    public float waittimer;
    private float placeholderX;
    private float placeholderY;
    private int childcounterR;
    private float weaponspawntimer = 5f;
    public float distancetoenemy;
    public bool throwing = false;
    public float TargetposX;
    public float TargetposY;
    private float disappeartimer = 10f;
    public Dictionary<int, string> Item;
    public List<SpriteRenderer> Childlist;



    // Use this for initialization
    void Start()
    {
        Childlist = new List<SpriteRenderer>();
        TakeChildstoList(transform);
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
        animator = GetComponent<Animator>();
        Mapgenerator = GameObject.Find("MapGenerator");
        myMapgenerator = Mapgenerator.GetComponent<MapGenerator>();
        Direction();
        moverandom = true;
        Item = new Dictionary<int, string>();
        Item[1] = "Diamond";
    }

    // Update is called once per frame
    void Update()
    {

        // Funktionen ausführen
        MoveRandom();
        SpawnWeapon();
        AttackEnemy();
        Death();

        // Weaponspawntimer Ablauf
        if (childcounterR == 0)
            weaponspawntimer -= Time.deltaTime;

        //---------------------------------------------------------------------------------

        // Tod Bedingung
        if (currentHealth <= 0)
            death = true;

        if (death)
        {
            attacking = false;
            moverandom = false;
            animator.SetBool("run", false);
            animator.SetBool("run", false);
            animator.SetBool("speernpcR", false);
        }
        //---------------------------------------------------------------------------------
        // Timer zum Ausharren

        waittimer -= Time.deltaTime;

        // Randiom Bedingung
        if (!attacking && !death && waittimer <= 0)
        {
            moverandom = true;
        }
        else { moverandom = false; }
        if(waittimer>0 && !attacking && !death)
        {
            animator.SetBool("run", false);
        }
        if(myCtrl.death)
        {
            attacking = false;
        }



        //---------------------------------------------------------------------------------

        // Nach erreichter Position neue Position Koordinaten auswürfeln

        if (this.transform.position == Targetposition)
        {
            Direction();
        }
        //---------------------------------------------------------------------------------

        // Gegenstandsanzahl im Rechten Arm prüfen
        childcounterR = Rightarm.transform.childCount;

        // Animationen

        //---------------------------------------------------------------------------------

        // Npc flip

        if (setleft == true)
        { this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        //---------------------------------------------------------------------------------

        // Gegnerreichweite
        Collider2D[] arrayofEnemys = Physics2D.OverlapCircleAll(transform.position, 5.0f, layermask);
        foreach (Collider2D Player in arrayofEnemys)
        {
            distancetoenemy = Vector2.Distance(this.transform.position, Player.transform.position);
            Enemy = Player.gameObject;
        }
        if(distancetoenemy < 2 && Enemy !=null)
        {
            if (myCtrl.death == false)
            {
                moverandom = false;
                attacking = true;
            }
        }
        // Speer werfen
        if(throwing && !death && !myCtrl.death)
        {
            if (Weapon != null)
            {
                Weapon.transform.SetParent(null);
                Speer mySpear = Weapon.GetComponent<Speer>();
                mySpear.flying = true;

                Target.x = TargetposX;
                Target.y = TargetposY;
                float step = 5 * Time.deltaTime;
                Weapon.transform.position = Vector2.MoveTowards(Weapon.transform.position, Target, step);
                Vector3 difference = Target - Weapon.transform.position;
                difference.Normalize();
                float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Weapon.transform.eulerAngles = new Vector3(0, 0, rotation_z);
                if (Target.x > this.transform.position.x)
                {
                    Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z+180);
                }
                else { Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z+180); }
                if (Weapon.transform.position == Target)
                {
                    throwing = false;
                    Destroy(Weapon);
                    Weapon = null;
                }
            }
        }


    }

    void MoveRandom()
    {
        if (moverandom == true && !death)
        {
            Targetposition = new Vector3(placeholderX, placeholderY, 0);
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Targetposition, step);

            if(this.transform.position != Targetposition)
                animator.SetBool("run", true);
            else { animator.SetBool("run", false); }

            if (this.transform.position.x > Targetposition.x)
            {
                setleft = true;
            }
            else { setleft = false; }
        }
    }

    // TolanHD's Ansatz
    void AttackEnemy()
    {
        if (attacking && !death && Enemy != null)
        { 
            Vector3 targetPos = Enemy.transform.position;
            
            if (targetPos.x > this.transform.position.x)
            { setleft = false; }
            else
            { setleft = true; }

            if (distancetoenemy > 2)
            {
                animator.SetBool("speernpcR", false);
                animator.SetBool("run", true);

                this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            }
            else if (distancetoenemy <= 2)
            {
                animator.SetBool("run", false);

                if (childcounterR > 0)
                    animator.SetBool("speernpcR", true);
                else
                    animator.SetBool("speernpcR", false);
            }
        }
    }
    // Zielposition auswürfeln ---------------------------------------------
    void Direction()
    {
        waittimer = Random.Range(0, 10);
        placeholderX = Random.Range(-myMapgenerator.mapWidth / 10, myMapgenerator.mapWidth / 10);
        placeholderY = Random.Range(-myMapgenerator.mapHeight / 10, myMapgenerator.mapHeight / 10);
    }

    void SpawnWeapon()
    {

        if(childcounterR == 0 && weaponspawntimer <=0 && !death)
        {
            Weapon = Instantiate(Prefabliste.Instance().GetGameObject("Spear"), new Vector3(0, 0, 0), Quaternion.identity);
            Weapon.name = "Spear";
            Weapon.transform.SetParent(Rightarm.transform);
            Weapon.transform.localPosition = new Vector3(0, -0.1f, 0);
            if(setleft)
            Weapon.transform.eulerAngles = new Vector3(0, 0, -180);
            else
            Weapon.transform.eulerAngles = new Vector3(0, 0, 0);
            weaponspawntimer = 3f;
            var spritetake = Weapon.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 10;
        }
    }

    public void SpearReady()
    {
        throwing = true;
        TargetposX = Enemy.transform.position.x;
        TargetposY = Enemy.transform.position.y;
    }

    // Damage
    private void TakeDamage(float damage)
    {
            currentHealth -= damage;
            if (currentHealth < 0 && !death)
            {
                currentHealth = 0;
            }
        foreach (SpriteRenderer Children in Childlist)
        {
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            Children.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        }
            Invoke("resetDamageColor", 0.2f);
    }

    void resetDamageColor()
    {
        foreach (SpriteRenderer Children in Childlist)
        {
            Children.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }

    // Alle Körperteile des NPCs in Liste setzen

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


    // Damage from Weapon
    void OnTriggerEnter2D(Collider2D Engager)
    {
        // Von Spieler attakiert
        if (myCtrl.attack == true && Engager.CompareTag("weapon") && Engager is PolygonCollider2D && !death)
        {
            Items myEngager = Engager.GetComponent<Items>();
            int value = 0;
            if (myEngager.Weapon.TryGetValue(Engager.name, out value))
            {
                TakeDamage(value);
                myEngager.currentDurability -= value / 2;
            }
        }
    }

    void Death()
    {
        if (death)
        {
            animator.SetBool("death", true);
      
            disappeartimer -= Time.deltaTime;
            if (disappeartimer <= 0)
            {
                if (Enemy.name == "Charakter")
                {
                    Charakter.SendMessage("EXP", 20);
                }
                Destroy(transform.gameObject);
            }
        }
    }

    void Itemdrop()
    {
        Vector3 Pos;
        Pos = this.transform.position;
        int value = Random.Range(1,Item.Count);
        string name = Item[value];
        GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject(name), new Vector3 (0,0,0), Quaternion.identity);
        Spawn.transform.position = Pos;
    }

    void ActivateDeathmenu()
    {

    }
}
