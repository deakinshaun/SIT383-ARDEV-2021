using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Module 2
    public List<AudioSource> soundList = new List<AudioSource>();
    public float dropoffDistanceConstant = 0.9f;
    public float attenuationFactor = 1.5f;
    public float speedOfSound = 330.0f;


    public GameObject avatarListener;
    public GameObject vrListener;
    public GameObject listener;

    public GameObject beginnerPortal;
    public GameObject intermediatePortal;
    public GameObject advancedPortal;
    public GameObject monitor1;
    public GameObject monitor2;
    public GameObject monitor3;
    public GameObject button1;
    public GameObject button2;


    private AudioSource soundBeginnerPortal;
    private AudioSource soundIntermediatePortal;
    private AudioSource soundAdvancedPortal;
    public AudioSource soundMonitorBeep1;
    public AudioSource soundMonitorBeep2;
    public AudioSource soundMonitorBeep3;
    public AudioSource soundMonitorFlatLine1;
    public AudioSource soundMonitorFlatLine2;
    public AudioSource soundMonitorFlatLine3;
    private AudioSource soundButton1;
    private AudioSource soundButton2;

    public AudioClip FlatLine;

    // Start is called before the first frame update

    void Start()
    {
        soundBeginnerPortal = beginnerPortal.GetComponent<AudioSource>();
        soundList.Add(soundBeginnerPortal);

        soundIntermediatePortal = intermediatePortal.GetComponent<AudioSource>();
        soundList.Add(soundIntermediatePortal);

        soundAdvancedPortal = advancedPortal.GetComponent<AudioSource>();
        soundList.Add(soundAdvancedPortal);

        soundMonitorBeep1 = monitor1.GetComponent<AudioSource>();
        soundList.Add(soundMonitorBeep1);

        soundMonitorBeep2 = monitor2.GetComponent<AudioSource>();
        soundList.Add(soundMonitorBeep2);

        soundMonitorBeep3 = monitor3.GetComponent<AudioSource>();
        soundList.Add(soundMonitorBeep3);

        soundMonitorFlatLine1 = monitor1.AddComponent<AudioSource>();
        soundMonitorFlatLine1.clip = FlatLine;
        soundList.Add(soundMonitorFlatLine1);

        soundMonitorFlatLine2 = monitor2.AddComponent<AudioSource>();
        soundMonitorFlatLine2.clip = FlatLine;
        soundList.Add(soundMonitorFlatLine2);

        soundMonitorFlatLine3 = monitor3.AddComponent<AudioSource>();
        soundMonitorFlatLine3.clip = FlatLine;
        soundList.Add(soundMonitorFlatLine3);

        soundButton1 = button1.GetComponent<AudioSource>();
        soundList.Add(soundButton1);

        soundButton2 = button2.GetComponent<AudioSource>();
        soundList.Add(soundButton2);

        Debug.Log("Sounds have been added!");

        if(avatarListener.gameObject != null)
        {
            listener = avatarListener;
        }
        else if(vrListener.gameObject != null)
        {
            listener = vrListener;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (soundMonitorBeep1 == null || soundMonitorBeep2 == null || soundMonitorBeep3 == null) //issues caused with multiplayer spawning new monitors
        {
            soundList.Clear();
            Start(); //try to re-gain sources.
            return;
        }
        else if (listener != null)
        {
            foreach (AudioSource audio in soundList)
            {
                //Module 2
                GameObject audioObject = audio.gameObject;
                float distance = Vector3.Distance(audioObject.transform.position, listener.transform.position);
                audio.volume = 1.0f / Mathf.Pow(dropoffDistanceConstant * distance, attenuationFactor);
            }
        }
    }

    public void beginnerPortalSoundPlay() //So it can be called by other functions when needed.
    {
        soundBeginnerPortal.Play();
    }

    public void intermediatePortalSoundPlay()
    {
        soundIntermediatePortal.Play();
    }

    public void advancedPortalSoundPlay()
    {
        soundAdvancedPortal.Play();
    }

    public void monitor1SoundPlay()
    {
        soundMonitorBeep1.Play();
    }

    public void monitor2SoundPlay()
    {
        soundMonitorBeep2.Play();
    }

    public void monitor3SoundPlay()
    {
        soundMonitorBeep3.Play();
    }

    public void monitor1FlatLineSoundPlay()
    {
        soundMonitorFlatLine1.Play();
    }

    public void monitor2FlatLineSoundPlay()
    {
        soundMonitorFlatLine2.Play();
    }

    public void monitor3FlatLineSoundPlay()
    {
        soundMonitorFlatLine3.Play();
    }

    public void button1SoundPlay()
    {
        soundButton1.Play();
    }

    public void button2SoundPlay()
    {
        soundButton2.Play();
    }
}
