using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawnmanager : MonoBehaviour {

    public MapGenerator myMapGenerator;
    public GameObject MapGenerator;

    public Dictionary<string, int> ToHavelist = new Dictionary<string, int>();
    public Dictionary<string, int> Havelist = new Dictionary<string, int>();
    public List<string> Objectlist = new List<string>();


    

  //  {Hase,Fuchs,Bär,Hase,Bär,Fuchs }

    // Use this for initialization
    void Start ()
    {
        myMapGenerator = MapGenerator.GetComponent<MapGenerator>();

        ToHavelist.Add("Bear", 5);
        ToHavelist.Add("Wolve", 10);
        ToHavelist.Add("Deer", 10);
        ToHavelist.Add("Alpaca", 5);
        ToHavelist.Add("Chicken", 10);
        ToHavelist.Add("Crawler", 10);
        ToHavelist.Add("Fox", 10);
        ToHavelist.Add("Grashopper", 10);
        ToHavelist.Add("Opossum", 5);
        ToHavelist.Add("Rabbit", 10);
        ToHavelist.Add("Squirrel", 5);
        ToHavelist.Add("Apple", 50);
        ToHavelist.Add("Cherry", 50);
        ToHavelist.Add("Firestone", 10);
        ToHavelist.Add("Grape", 10);
        ToHavelist.Add("Gras", 50);
        ToHavelist.Add("Healplant", 20);
        ToHavelist.Add("Leaf", 20);
        ToHavelist.Add("Leek", 10);
        ToHavelist.Add("Paprika", 10);
        ToHavelist.Add("Pear", 10);
        ToHavelist.Add("Radish", 10);
        ToHavelist.Add("Sandpile", 3);
        ToHavelist.Add("Stone", 20);
        ToHavelist.Add("Wheat", 20);


        SpawnTrigger();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.K))
            SpawnTrigger();
    }

    public void SpawnTrigger()
    {
        Objectlist.Clear();
        // Alle GameObjecte in Szene holen und nach Layer Mask filtern und in Liste schreiben
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject Obj in allObjects)
        {
           if(Obj.layer == 12)
            {
                Objectlist.Add(Obj.name);
            }
        }
        // Anzahl der Elemente in Liste ermitteln
        Havelist.Clear();
        foreach (string Obj in Objectlist)
        {
            int amount = Objectlist.Where(a => a == Obj).Count();
            if (!Havelist.ContainsKey(Obj))
            {
                Havelist.Add(Obj, amount);
            }
        }

        // Vergleichen der Dictionaries und Differenzermitteln
        foreach(KeyValuePair<string, int> Tohave in ToHavelist)
        {
            int haveval;
            Havelist.TryGetValue(Tohave.Key, out haveval);
            int dif = Tohave.Value - haveval;

            if (dif !=0)
            {
                InstantObject(Tohave.Key, dif);
            }     
        }
    }

    public void InstantObject(string name, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range((float)-myMapGenerator.mapWidth / 10, (float)myMapGenerator.mapWidth / 10);
            float y = Random.Range((float)-myMapGenerator.mapHeight / 10, (float)myMapGenerator.mapHeight / 10);
            GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject(name), new Vector3(x, y, 0), Quaternion.identity);
            Spawn.name = name;
        }
    }

}
