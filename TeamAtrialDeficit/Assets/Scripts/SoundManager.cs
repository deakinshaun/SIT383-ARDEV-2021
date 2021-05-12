using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Module 2
    private List<AudioSource> soundList = new List<AudioSource>();
    public float dropoffDistanceConstant = 0.9f;
    public float attenuationFactor = 1.5f;
    public float speedOfSound = 330.0f;


    public GameObject avatarListener;
    public GameObject vrListener;
    public GameObject listener;

    public GameObject beginnerPortal;
    public GameObject intermediatePortal;
    public GameObject advancedPortal;
    public GameObject monitor;
    public GameObject button;


    private AudioSource soundBeginnerPortal;
    private AudioSource soundIntermediatePortal;
    private AudioSource soundAdvancedPortal;
    private AudioSource soundMonitor;
    private AudioSource soundButton;


    // Start is called before the first frame update

    void Start()
    {
        soundBeginnerPortal = beginnerPortal.GetComponent<AudioSource>();
        soundList.Add(soundBeginnerPortal);

        soundIntermediatePortal = intermediatePortal.GetComponent<AudioSource>();
        soundList.Add(soundIntermediatePortal);

        soundAdvancedPortal = advancedPortal.GetComponent<AudioSource>();
        soundList.Add(soundAdvancedPortal);

        soundMonitor = soundMonitor.GetComponent<AudioSource>();
        soundList.Add(soundMonitor);

        //soundButton = button.GetComponent<AudioSource>();
        //soundList.Add(soundButton);

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
        if(listener != null)
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

    public void monitorSoundPlay()
    {
        soundBeginnerPortal.Play();
    }

    public void buttonSoundPlay()
    {
        soundButton.Play();
    }
}
