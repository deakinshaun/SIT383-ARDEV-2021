using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class addHeartbeat : MonoBehaviour
{
    private float currentHeartrate;

    private float scalePeriod;

    private float maxScaleX, maxScaleY, maxScaleZ;
    private float initialScaleX, initialScaleY, initialScaleZ;
    private float scaleIncreaseX, scaleIncreaseY, scaleIncreaseZ;

    //Audio Settings
    public AudioSource audioSource;
    public AudioClip heartBeatClip;
    private float beatPitch;

    // Start is called before the first frame update
    void Start()
    {
        maxScaleX = 0.1f;
        maxScaleY = 0.0f;
        maxScaleZ = 0.1f;

        //Get Heart objects initial scale values
        initialScaleX = this.transform.localScale.x;
        initialScaleY = this.transform.localScale.y;
        initialScaleZ = this.transform.localScale.z;

        currentHeartrate = GetComponent<heartDetails>().getCurrent();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHeartrate = GetComponent<heartDetails>().getCurrent();

        // Scale of 1 = 60 beats per second
        scalePeriod = currentHeartrate / 60.0f;
        // Convert Scale to Pitch (standard clip is 1 beat per minute)
        beatPitch = scalePeriod;
        audioSource.pitch = beatPitch;

        scaleIncreaseX = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleX;
        scaleIncreaseY = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleY;
        scaleIncreaseZ = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI * scalePeriod)) * maxScaleZ;

        /*
        if(scaleIncreaseX == 0)
        {
            audioSource.PlayOneShot(heartBeatClip, 1.0f);
        }
        */

        //Calculate Scale
        this.transform.localScale = new Vector3(initialScaleX + scaleIncreaseX,
                                                initialScaleY + scaleIncreaseY,
                                                initialScaleZ + scaleIncreaseZ);
    }
}
