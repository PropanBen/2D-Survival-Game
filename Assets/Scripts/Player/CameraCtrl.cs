using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public Ctrl myCtrl;
    private GameObject Charakter;
    private float ortho;
    private float speed = 1;
    private Vector3 pos;
    private float x;
    private float y;

	// Use this for initialization
	void Start ()
    {
        ortho = this.GetComponent<Camera>().orthographicSize = 1.5f;
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void LateUpdate()
    {
        GameObject charakter = GameObject.Find("Charakter");
        if (charakter.activeSelf == true)
            {
            pos = charakter.transform.position;
             x = pos.x;
             y = pos.y;

            Vector3 posc = this.transform.position;

            if (myCtrl.death)
            {
                pos = charakter.transform.position;
                 x = pos.x;
                 y = pos.y;

                if (ortho > 0.5f)
                    ortho -= Time.deltaTime * 0.5f;
                if (ortho <= 0.5f)
                    ortho = 0.5f;
                this.GetComponent<Camera>().orthographicSize = ortho;
                transform.position = new Vector3(x, y, -5);
            }
            else
            {
                ortho = this.GetComponent<Camera>().orthographicSize = 1.5f;
                transform.position = new Vector3(x, y, -5);
            }
        }
    }
}
