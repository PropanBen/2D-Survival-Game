using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishable : MonoBehaviour {


   // public GameObject Haken;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /*   void OnTriggerEnter2D(Collider2D col)
       {
           if (col.CompareTag("Haken"))
           {
               col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
           }
       }

       void OnTriggerExit2D(Collider2D col)
       {
           if (col.CompareTag("Haken"))
           {
               col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
           }
       }*/

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Items>() != null)
        {
                Destroy(col.gameObject);
           
        }
    }
}
