using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    private float distance = 3.0f;
    private float frequency = 0.3f;
    private float decayFactor = 1.0f;
    private float dropoff = 0.1f;
    private List<AudioSource> sounds = new List<AudioSource>();

    private AudioSource ss_trolley;
    private AudioSource ss_ivMachine;
    private AudioSource ss_manInPain;
    private AudioSource ss_hospitalLoud;
    private AudioSource ss_hospitalQuiet;
    private AudioSource ss_beep;
    private AudioSource ss_flatline;
    
    public GameObject listener;

    public GameObject ss_trolleyObject;
    public GameObject ss_ivMachineObject;
    public GameObject ss_manInPainObject;
    public GameObject ss_hospitalLoudObject;
    public GameObject ss_hospitalQuietObject;
    public GameObject ss_beepObject;
    public GameObject ss_flatlineObject;
 
    
    // Start is called before the first frame update

    void Start()
    {    
        sounds.Add(ss_ivMachine = ss_ivMachineObject.GetComponent<AudioSource>());
        sounds.Add(ss_trolley = ss_trolleyObject.GetComponent<AudioSource>());
        sounds.Add(ss_manInPain = ss_manInPainObject.GetComponent<AudioSource>());
        sounds.Add(ss_hospitalLoud = ss_hospitalLoudObject.GetComponent<AudioSource>());
        sounds.Add(ss_hospitalQuiet = ss_hospitalQuietObject.GetComponent<AudioSource>());
        sounds.Add(ss_beep = ss_beepObject.GetComponent<AudioSource>());
        sounds.Add(ss_flatline = ss_flatlineObject.GetComponent<AudioSource>());
        Debug.Log("Sound loaded:" + ss_ivMachine);

        
    }

    // Update is called once per frame
    void Update()
    {
        float ivDistance = (ss_ivMachine.transform.position - listener.transform.position).x;
        ss_ivMachine.volume = 1.0f / Mathf.Pow (ivDistance * dropoff, decayFactor);
        float trolleyDistance = (ss_trolley.transform.position - listener.transform.position).x;
        ss_trolley.volume = 1.0f / Mathf.Pow (trolleyDistance * dropoff, decayFactor);
        float manInPainDistance = (ss_manInPain.transform.position - listener.transform.position).x;
        ss_manInPain.volume = 1.0f / Mathf.Pow (manInPainDistance * dropoff, decayFactor);
        float hospitalLoudDistance = (ss_hospitalLoud.transform.position - listener.transform.position).x;
        ss_hospitalLoud.volume = 1.0f / Mathf.Pow (hospitalLoudDistance * dropoff, decayFactor);
        float hospitalQuietDistance = (ss_hospitalQuiet.transform.position - listener.transform.position).x;
        ss_hospitalQuiet.volume = 1.0f / Mathf.Pow (hospitalQuietDistance * dropoff, decayFactor);
        float beepDistance = (ss_beep.transform.position - listener.transform.position).x;
        ss_beep.volume = 1.0f / Mathf.Pow (beepDistance * dropoff, decayFactor);
    }

    public void ivMachine()
    {
        ss_ivMachine.Play();
    }
    public void hospitalTrolley()
    {
        ss_trolley.Play();        
    }
    public void manInPain()
    {
        ss_manInPain.Play();    
    }
    public void HospitalLoud()
    {
        ss_hospitalLoud.Play();        
    }
    public void HospitalQuiet()
    {
        ss_hospitalQuiet.Play();
    }
    public void beep()
    {
        ss_beep.Play();
    }
    public void flatLine()
    {
        ss_flatline.Play();
    }
    public void EndSimulation()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].Stop();
        }
    }
}
