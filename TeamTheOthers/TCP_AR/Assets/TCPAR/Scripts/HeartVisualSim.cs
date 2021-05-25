using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartVisualSim : MonoBehaviour
{
    Vector3 startingScale = new Vector3(1, 1, 1);
    //float beatScaleFactor = 0.75f;
    private float beatXScale = 0.75f;
    private Vector3 beatScaler = new Vector3(1, 1, 1);

    DateTime timeToScale = DateTime.MaxValue;

    int beatScaleTimeMilli = 120;

    AudioSource audioHeartBeat;

    // Start is called before the first frame update
    void Start()
    {
        startingScale = this.gameObject.transform.localScale;
        beatScaler = startingScale - (new Vector3(startingScale.x * beatXScale, startingScale.y * 1, startingScale.z * 1));
        audioHeartBeat = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DateTime.Compare(DateTime.Now, timeToScale) > 0)
        {
            Debug.unityLogger.Log("TCPAR", "Returning scale.");
            timeToScale = DateTime.MaxValue;

            //this.gameObject.transform.localScale.Set(startingScale.x, startingScale.y, startingScale.z);
            this.gameObject.transform.localScale = startingScale;
        }
        
    }

    void Beat()
    {
        Debug.unityLogger.Log("TCPAR", "Heart Vis Beat signal");
        try
        {
            // Show visual beat effect
            this.gameObject.transform.localScale -= beatScaler;

            // Play the heartbeat sound
            audioHeartBeat.Play();
        } catch (Exception e)
        {
            Debug.unityLogger.Log("TCPAR", "Unable to transform... " + e);
        }


         timeToScale = DateTime.Now.AddMilliseconds(beatScaleTimeMilli);
    }
}
