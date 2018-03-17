using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightDayCircel : MonoBehaviour {

    public Spawnmanager mySpawnmanager;
    public GameObject Spawnmanager;
    public GameObject Light;
    public Text Day;
    public Text Hour;
    public Text Minute;
    public int day;
    public int hour;
    public float minute;
    public float speed = 1f;
    public float factor;
    public Light lt;


    // Use this for initialization
    void Start ()
    {
        mySpawnmanager = Spawnmanager.GetComponent<Spawnmanager>();
        factor = 0.013888888888f;
        hour = 8;
        lt = GetComponent<Light>();
        lt.intensity = 6.6666666666666f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        

        // UI Update-------------------------------------------------------------------

        Minute.text = minute.ToString("00");
        Hour.text = hour.ToString("00");
        Day.text = day.ToString("00");

        // Tages Stunden und Minutenzähler
        minute += Time.deltaTime * speed;
        if (minute > 59)
        {
            hour++;
            minute = 0;
        }
        if (hour > 23)
        {
            day++;
            hour = 0;
            mySpawnmanager.SpawnTrigger();
        }

        // Zeitraffer-------------------------------------------------------------------
        if(Input.GetKeyUp(KeyCode.KeypadPlus) && speed <10)
        {
            speed++;
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus) && speed >1f)
        {
            speed --;
        }

        // Licht / Zeit Parameter  -----------------------------------------------------
        if (hour >= 8)
           lt.intensity = 10f;
        if (hour >= 20)
            lt.intensity = 0;
            
      //  lt.intensity = Mathf.Sin((hour + minute / 60) / 24 * Mathf.PI * 2) *speed;
      //  Debug.Log(lt.intensity);

    }
}
