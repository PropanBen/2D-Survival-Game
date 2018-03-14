using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speer : MonoBehaviour {

    public Ctrl myCtrl;
    public GameObject Charakter;
    public GameObject Mouse;
    public GameObject SoundManager;
    public Sprite TargetSprite;
    private Vector3 Targetposition;
    private Transform Spear;
    private Transform Child;
    private bool throwingR;
    private bool throwingL;
    public bool flying;

    // Use this for initialization
    void Start ()
    {
        SoundManager = GameObject.Find("SoundManager");
        Mouse = GameObject.Find("Mouse");
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (myCtrl.throwthing == true)
        {
            Mouse.GetComponent<SpriteRenderer>().sprite = TargetSprite;
            var spritetake = Mouse.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 1;
        }

        if (myCtrl.childcounterR > 0 && Input.GetMouseButtonUp(0) && myCtrl.throwthing == true)
        {
            ThrowSpear(myCtrl.ChildR);
            throwingR = true;
            SoundManager.SendMessage("PlaySound", "throw");
        }

        if (myCtrl.childcounterL > 0 && Input.GetMouseButtonUp(1) && myCtrl.throwthing == true)
        {
            ThrowSpear(myCtrl.ChildL);
            throwingL = true;
            SoundManager.SendMessage("PlaySound", "throw");
        }

        if (throwingR)
        {
            myCtrl.animator.SetBool("speerR", false);
            myCtrl.attack = true;
            Transform Speer = myCtrl.ChildR;
            myCtrl.ChildR.transform.SetParent(null);
            myCtrl.throwthing = false;
            if (Speer.position != Targetposition)
            {
                float speedobj = 5f;
                float step = speedobj * Time.deltaTime;
                Speer.transform.position = Vector2.MoveTowards(myCtrl.ChildR.transform.position, Targetposition, step);
            }
            if (Speer.position == Targetposition)
            {
                throwingR = false;
                myCtrl.attack = false;
                Speer = null;
            }
        }
        if (throwingL)
        {
            myCtrl.animator.SetBool("speerL", false);
            myCtrl.attack = true;
            Transform Speer = myCtrl.ChildL;
            myCtrl.ChildL.transform.SetParent(null);
            myCtrl.throwthing = false;
            if (Speer.position != Targetposition)
            {
                float speedobj = 5f;
                float step = speedobj * Time.deltaTime;
                Speer.transform.position = Vector2.MoveTowards(myCtrl.ChildL.transform.position, Targetposition, step);
            }
            if (Speer.position == Targetposition)
            {
                throwingL = false;
                myCtrl.attack = false;
                Speer = null;
            }
        }
    }

    public void ThrowSpear(Transform Child)
    {
        Vector3 mousePos = Input.mousePosition;
        float x = mousePos.x;
        float y = mousePos.y;
        mousePos = new Vector3(x, y, +5);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Targetposition = mousePos;
        Spear = Child;
        Vector3 difference = Targetposition - Spear.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Spear.transform.eulerAngles = new Vector3(0, 0, rotation_z);
        if (Targetposition.x > this.transform.position.x)
        {
            Spear.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 180);
        }
        else { Spear.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z); }
        Mouse.GetComponent<SpriteRenderer>().sprite = null;
    }
}
