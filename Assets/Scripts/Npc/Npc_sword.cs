using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_sword : MonoBehaviour {

    public Ctrl myCtrl;
    public GameObject Charakter;
    public MapGenerator myMapgenerator;
    public GameObject Mapgenerator;
    public Vector3 Targetposition;
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
    public float distancetoenemy;
    public float TargetposX;
    public float TargetposY;
    private float disappeartimer = 10f;
    public Dictionary<int, string> Item;
    public List<SpriteRenderer> Childlist;
    public bool attack;



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
        AttackEnemy();
        Death();

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
        if (waittimer > 0 && !attacking && !death)
        {
            animator.SetBool("run", false);
        }
        if (myCtrl.death)
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
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
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
        if (distancetoenemy < 2)
        {
            if (myCtrl.death == false && Enemy !=null)
            {
                moverandom = false;
                attacking = true;
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

            if (this.transform.position != Targetposition)
                animator.SetBool("run", true);
            else { animator.SetBool("run", false); }

            if (this.transform.position.x > Targetposition.x)
            {
                setleft = true;
            }
            else { setleft = false; }
        }
    }


    void AttackEnemy()
    {
        if (attacking && !death)
        {
            Vector3 TargetPos = Enemy.transform.position;

            if (distancetoenemy > 0.3)
            {
          
               if (this.transform.position.x > TargetPos.x)
               {
                    TargetPos += new Vector3(+0.3f, 0, 0);
                    setleft = true;
               }
                if (this.transform.position.x < TargetPos.x)
                {
                    TargetPos += new Vector3(-0.3f, 0, 0);
                    setleft = false;
                }
                animator.SetBool("run", true);

                this.transform.position = Vector3.MoveTowards(this.transform.position, TargetPos, speed * Time.deltaTime);
            }
            else { animator.SetBool("run", false); }

            if(distancetoenemy <= 0.35f)
            {
                animator.SetBool("run", false);
                animator.SetBool("sword", true);
            }
            else
            {
                animator.SetBool("sword", false);
                attack = false;
            }


        }
    }

    public void Attacktrue()
    {
        attack = true;
    }

    public void Attackfalse()
    {
        attack = false;
    }

    // Zielposition auswürfeln ---------------------------------------------
    void Direction()
    {
        waittimer = Random.Range(0, 10);
        placeholderX = Random.Range(-myMapgenerator.mapWidth / 10, myMapgenerator.mapWidth / 10);
        placeholderY = Random.Range(-myMapgenerator.mapHeight / 10, myMapgenerator.mapHeight / 10);
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
        int value = Random.Range(1, Item.Count);
        string name = Item[value];
        GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject(name), new Vector3(0, 0, 0), Quaternion.identity);
        Spawn.transform.position = Pos;
    }

    void ActivateDeathmenu()
    {

    }
}

