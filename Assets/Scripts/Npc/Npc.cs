using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {


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
    public float speed = 1f;
    public float waittimer;
    private float placeholderX;
    private float placeholderY;
    private int childcounterR;
    private float weaponspawntimer = 3f;
    public float distancetoenemy;



    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        Mapgenerator = GameObject.Find("MapGenerator");
        myMapgenerator = Mapgenerator.GetComponent<MapGenerator>();
        Direction();
        moverandom = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Funktionen ausführen
        MoveRandom();
        SpawnWeapon();
        AttackEnemy();

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



        //---------------------------------------------------------------------------------

        // Nach erreichter Position neue Position Koordinaten auswürfeln

        if (this.transform.position == Targetposition)
        {
            Direction();
        }
        //---------------------------------------------------------------------------------

        // Animationen

        //---------------------------------------------------------------------------------

        // Npc flip

        if (setleft == true)
        { this.transform.localScale = new Vector3(-1, 1, 1); }

        else { this.transform.localScale = new Vector3(1, 1, 1); }

        //---------------------------------------------------------------------------------

        // Gegenstandsanzahl im Rechten Arm prüfen

        childcounterR = Rightarm.transform.childCount;

        // Gegnerreichweite
        Collider2D[] arrayofEnemys = Physics2D.OverlapCircleAll(transform.position, 5.0f, layermask);
        foreach (Collider2D Player in arrayofEnemys)
        {
            distancetoenemy = Vector2.Distance(this.transform.position, Player.transform.position);
            Enemy = Player.gameObject;
        }
        if(distancetoenemy < 2)
        {
            moverandom = false;
            attacking = true;
        }


    }

    void MoveRandom()
    {
        if (moverandom == true)
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

    void AttackEnemy()
    {
        if(attacking)
        {
            Targetposition = Enemy.transform.position;
            if (this.transform.position.x > Enemy.transform.position.x)
            {
                Targetposition += new Vector3(2, 0, 0);
                setleft = true;
                animator.SetBool("run", true);
            }
            if (this.transform.position.x < Enemy.transform.position.x)
            {
                Targetposition += new Vector3(-2, 0, 0);
                setleft = false;
                animator.SetBool("run", true);
            }
            if(distancetoenemy == 2)
            {
                animator.SetBool("run", false);
            }


            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Targetposition, step);
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

        if(childcounterR == 0 && weaponspawntimer <=0)
        {
            Weapon = Instantiate(Prefabliste.Instance().GetGameObject("Spear"), new Vector3(0, 0, 0), Quaternion.identity);
            Weapon.name = "Spear";
            Weapon.transform.SetParent(Rightarm.transform);
            weaponspawntimer = 3f;
        }
    }
}
