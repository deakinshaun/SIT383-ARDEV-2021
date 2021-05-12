using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //private List<AudioSource> soundList = new List<AudioSource>();

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
        //soundList.Add(soundBeginnerPortal);

        soundIntermediatePortal = intermediatePortal.GetComponent<AudioSource>();
        //soundList.Add(soundIntermediatePortal);

        soundAdvancedPortal = advancedPortal.GetComponent<AudioSource>();
        //soundList.Add(soundAdvancedPortal);

        soundMonitor = soundMonitor.GetComponent<AudioSource>();
        //soundList.Add(soundMonitor);

        //soundButton = button.GetComponent<AudioSource>();
        //soundList.Add(soundButton);

        Debug.Log("Sounds have been added!");
    }

    // Update is called once per frame
    void Update()
    {
        if(listener != null)
        {
            float beginnerPortalDistance = Vector3.Distance(beginnerPortal.transform.position, listener.transform.position);
            soundBeginnerPortal.volume = 1.0f / beginnerPortalDistance;

            float intermediatePortalDistance = Vector3.Distance(intermediatePortal.transform.position, listener.transform.position);
            soundIntermediatePortal.volume = 1.0f / intermediatePortalDistance;

            float advancedPortalDistance = Vector3.Distance(beginnerPortal.transform.position, listener.transform.position);
            soundAdvancedPortal.volume = 1.0f / advancedPortalDistance;

            float monitorDistance = Vector3.Distance(monitor.transform.position, listener.transform.position);
            soundMonitor.volume = 1.0f / monitorDistance;

            //float buttonDistance = Vector3.Distance(button.transform.position, listener.transform.position);
            //soundButton.volume = 1.0f / buttonDistance;
        }
    }

    public void beginnerPortalSoundPlay() //So it can be called by other objects.
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
    /*
    public void EndSimulation()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].Stop();
        }
    }
    */
}
