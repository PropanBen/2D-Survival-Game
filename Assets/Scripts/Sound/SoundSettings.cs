using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundSettings : MonoBehaviour {

    public AudioMixerGroup SFX;
    public GameObject Obj;
    public AudioSource Music;
    public AudioClip axt, treefall, bite, fishstruggle, Throw, waterfill, brokentool,
                     closechest, drinking, eating, fireout, firesound, flop, heal, openchest,
                     startfire, stonebreak, pickaxe;
    public AudioClip Track1, Track2, Track3, Track4;
    public List<AudioClip> Player = new List<AudioClip>();
    public int childcounter;


	// Use this for initialization
	void Start ()
    {
        Player.Add(Track1);
        Player.Add(Track2);
        Player.Add(Track3);
        Player.Add(Track4);
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayMusic();


        // Destroy all Sounds who are finish playing
        childcounter = this.transform.childCount;
        if(childcounter >1)
        {
            for (int i = 1; i < childcounter; i++)
            {
                Transform Child = this.transform.GetChild(i);
                if(Child.GetComponent<AudioSource>())
                {
                    AudioSource audiosource = Child.GetComponent<AudioSource>();
                    if(!audiosource.isPlaying && Child.name != "Music")
                    {
                        Destroy(Child.gameObject);
                    }
                }
            }
        }
    }

    public int PlaySound(string sound)
    {
        Obj = new GameObject(sound);
        Obj.AddComponent<AudioSource>();
        AudioSource audiosource = Obj.GetComponent<AudioSource>();
        Obj.transform.SetParent(this.transform);
        audiosource.outputAudioMixerGroup = SFX;
        audiosource.volume = 0.2f;
 
        switch (sound)
        {
            case "axt" : audiosource.clip = axt; break;
            case "pickaxe": audiosource.clip = pickaxe; break;
            case "stonebreak": audiosource.clip = stonebreak; break;
            case "treefall": audiosource.clip = treefall; break;
            case "bite": audiosource.clip = bite; break;
            case "fishstruggle": audiosource.clip = fishstruggle; break;
            case "throw": audiosource.clip = Throw; break;
            case "waterfill": audiosource.clip = waterfill; break;
            case "brokentool": audiosource.clip = brokentool; break;
            case "closechest": audiosource.clip = closechest; break;
            case "drinking": audiosource.clip = drinking; break;
            case "eating": audiosource.clip = eating; break;
            case "fireout": audiosource.clip = fireout; break;
            case "firesound": audiosource.clip = firesound; break;
            case "flop": audiosource.clip = flop; break;
            case "heal": audiosource.clip = heal; break;
            case "openchest": audiosource.clip = openchest; break;
            case "startfire": audiosource.clip = startfire; break;
            case "stop": audiosource.Stop(); break;

            default: Debug.Log("error"); break;
        }
        if (!audiosource.isPlaying)
            audiosource.Play();

        return Obj.transform.GetInstanceID();
    }

    public void PlayMusic()
    {
        if (!Music.isPlaying)
        {
            int random = Random.Range(1, 4);

            AudioClip Clip = Player[random];
            Music.clip = Clip;
            Music.Play();
        }
    }

}
