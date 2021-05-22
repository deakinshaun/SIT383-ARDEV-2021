using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] HeartAudios;
    public AudioSource Audio;
    public static SoundManager instance;
     void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayNormal()
    {
        Audio.clip = HeartAudios[0];
        Audio.Play();
    }
    public void PlayEmergency()
    {
        Audio.clip = HeartAudios[1];
        Audio.Play();
    }
    public void PlayQuick()
    {
        Audio.clip = HeartAudios[2];
        Audio.Play();
    }
    public void Stop()
    {
        Audio.Stop();
    }
}
