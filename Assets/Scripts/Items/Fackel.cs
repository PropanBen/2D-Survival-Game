using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fackel : MonoBehaviour {


    public SoundSettings mySoundsettings;
    public GameObject Soundmanager;
    public Ctrl myCtrl;
    public GameObject Charakter;
    public Animator animator;
    public Light Light;
    public GameObject LightGO;
    public GameObject SoundManager;
    public bool lighton;
    public float switcher;
    public AudioSource AudioSource;
    public AudioClip fire;
    public GameObject UI;
    public int id;

	// Use this for initialization
	void Start ()
    {
        SoundManager = GameObject.Find("SoundManager");
        mySoundsettings = SoundManager.GetComponent<SoundSettings>();
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
        SoundManager = GameObject.Find("SoundManager");
        switcher = 1;
        animator = GetComponent<Animator>();
        UI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (lighton)
        {
            LightGO.SetActive(true);
            if (id == 0)
                id = mySoundsettings.PlaySound("firesound");
            animator.SetBool("burn", true);
        }
        else
        {
            animator.SetBool("burn", false);
            LightGO.SetActive(false);
            Destroy(MatchId(id));
            id = 0;
        }
        if (switcher <= 1 && switcher >0)
            Light.intensity = 100f;
        if (switcher < 0 && switcher > -1f)
        {
            Light.intensity = 90f;
        }
        if (switcher < -1)
            switcher = 1;

            switcher -= Time.deltaTime ;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tinderdrill") && myCtrl.Wftiming == true && myCtrl.Wftimer < 0.5f)
        {
            lighton = true;
        }
    }

    public void ExtinguishFire()
    {
        lighton = false;
        UI.SetActive(!UI.activeSelf);
    }

    private void OnMouseOver()
    {
            if (Input.GetMouseButtonDown(0))
            {
                UI.SetActive(!UI.activeSelf);
            }

    }

    public GameObject MatchId(int id)
    {
        Transform Obj;
        GameObject GO = null;

        int childcounter = SoundManager.transform.childCount;
        if (childcounter > 0)

            for (int i = 0; i < childcounter; i++)
            {
                Obj = SoundManager.transform.GetChild(i);
                if (id == Obj.transform.GetInstanceID())
                {
                    GO = Obj.gameObject;
                }
            }
        return GO;
    }
}
