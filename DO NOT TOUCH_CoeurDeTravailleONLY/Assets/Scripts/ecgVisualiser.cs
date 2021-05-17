using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcgVisualiser : MonoBehaviour
{
    /*
     * EGC Visualiser Component
     * 
     * Creates a visualisation of the three audio sources - heart, atrial, ventricle
     * 
     * Currently uses heartbeat, breathing simulation, and background noise.
     * Could be expanded to include more realistic sound simulations for Atrial and 
     * Ventricle noise.
     * 
     * Develop by Stephen Caines for SIT383 Augemented Reality Systems
     * 
     * Uses Random Guassian code developed by Oneiros90 (this will cluster randoms around
     * the normal rather than the extremes of the allowed range)
     */

    [Tooltip ("Audio file for heartbeat")]
    public AudioClip heartAudio;
    [Tooltip("Audio file for breathing")]
    public AudioClip breathAudio;
    [Tooltip("Inverted Audio file to negate breathing")]
    public AudioClip breathInvertedAudio;
    [Tooltip("Audio file for background noise")]
    public AudioClip noiseAudio;
    [Tooltip("Inverted Audio file to negate background noise")]
    public AudioClip noiseInvertedAudio;

    public GameObject heart;

    private float minHeight = 10.0f;
    private float maxHeight = 200.0f;
    private float currentHeartRate = 60.0f;
    private float currentRespiratoryRate = 20.0f;

    //Booleans for component synching - task complete when all = true
    private bool heartSync = false;
    private bool atrialSync = false;
    private bool ventricleSync = false;

    //Rectangular ECG display option
    private Color ecgColor = Color.yellow;
    private EcgObjectScript [] ecgBars;

    private AudioSource ecgSource;
    private AudioSource breathingSource;
    private AudioSource noiseSource;

    private AudioSource cancelOutBreathing;
    private AudioSource cancelOutNoise;

    public bool GetHeartSync()
    {
        return heartSync;
    }
    public bool GetAtrialSync()
    {
        return atrialSync;
    }
    public bool GetVentricleSync()
    {
        return ventricleSync;
    }

    public void GameReset()
    {
        heartSync = false;
        atrialSync = false;
        ventricleSync = false;
        breathingSource.volume = 1.0f;
        noiseSource.volume = 1.0f;

        heart.GetComponent<AtrialDetails>().Restart();
        heart.GetComponent<VentricleDetails>().Restart();
        heart.GetComponent<HeartDetails>().Restart();

    }
    public void CheckCancelBreathing()
    {
        float currentValueV = heart.GetComponent<VentricleDetails>().GetTargetVentricleValue();
        float targetValueV = heart.GetComponent<VentricleDetails>().GetCurrentVentricleValue();
        float intitalValueV = heart.GetComponent<VentricleDetails>().GetInitialVentricleValue();

        float startToTargetGap = Mathf.Abs(intitalValueV - targetValueV);
        float currentToTargetGap = Mathf.Abs(currentValueV - targetValueV);

        if (currentToTargetGap > 0.1)
        {
            ventricleSync = false;
            //Debug.Log("Current breathing gap is " + currentToTargetGap);

            float delta = currentToTargetGap / startToTargetGap;
            if (delta > 1.0f) delta = 1.0f;

            //delta = 1 - delta;

            if (delta < 0.05f) delta = 0.0f;

            //If currentToTargetGap = 0, play the cancel noise at 1 to negate base noise
            breathingSource.volume = delta;
            //Debug.Log("Cancel breathing volume is " + delta);

        }
        else
        {
            breathingSource.volume = 0;
            ventricleSync = true;
        }
    }

    public void CheckForHeartSync()
    {
        float currentHeart = heart.GetComponent<HeartDetails>().GetCurrentHeartrate();
        float targetHeart = heart.GetComponent<HeartDetails>().GetTargetHeartrate();

        float heartGap = Mathf.Abs(targetHeart - currentHeart);

        if (heartGap < 0.1)
        {
            heartSync = true;
        }
    }

    public void CheckCancelNoise()
    {
        float currentValueV = heart.GetComponent<AtrialDetails>().GetTargetAtrialValue();
        float targetValueV = heart.GetComponent<AtrialDetails>().GetCurrentAtrialValue();
        float intitalValueV = heart.GetComponent<AtrialDetails>().GetInitialAtrialValue();

        float startToTargetGap = Mathf.Abs(intitalValueV - targetValueV);
        float currentToTargetGap = Mathf.Abs(currentValueV - targetValueV);

        if (currentToTargetGap > 0.1)
        {
            atrialSync = false;
            //Debug.Log("Current noise gap is " + currentToTargetGap);

            float delta = currentToTargetGap / startToTargetGap;
            if (delta > 1.0f) delta = 1.0f;

            //delta = 1 - delta;

            if (delta < 0.05f) delta = 0.0f;

            //If currentToTargetGap = 0, play the cancel noise at 1 to negate base noise
            noiseSource.volume = delta;
            //Debug.Log("Cancel noise volume is " + delta);

        }
        else
        {
            noiseSource.volume = 0;
            atrialSync = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ecgBars = GetComponentsInChildren<EcgObjectScript>();

        if (!heartAudio) return;

        ecgSource = new GameObject("ecgAudioSource").AddComponent<AudioSource>();
        ecgSource.loop = true;
        ecgSource.clip = heartAudio;

        breathingSource = new GameObject("breathAudioSource").AddComponent<AudioSource>();
        breathingSource.loop = true;
        breathingSource.clip = breathAudio;
/*
        cancelOutBreathing = new GameObject("cancelBreathing").AddComponent<AudioSource>();
        cancelOutBreathing.loop = true;
        cancelOutBreathing.clip = breathInvertedAudio;
        cancelOutBreathing.volume = 0.0f;
*/
        noiseSource = new GameObject("noiseAudioSource").AddComponent<AudioSource>();
        noiseSource.loop = true;
        noiseSource.clip = noiseAudio;

        cancelOutNoise = new GameObject("cancelBackgroundNoise").AddComponent<AudioSource>();
        cancelOutNoise.clip = noiseInvertedAudio;

        ecgSource.Play();
        breathingSource.Play();
        //cancelOutBreathing.Play();

        //Noise Plays at a standard pitch
        noiseSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentHeartRate = heart.GetComponent<HeartDetails>().GetCurrentHeartrate();

        ecgSource.pitch = currentHeartRate / 60.0f;

        float[] heartData = new float[256];
        float[] breathData = new float[256];
        float[] cancelBreathData = new float[256];
        float[] combinedData = new float[256];

        ecgSource.GetSpectrumData(heartData, 0, FFTWindow.Blackman);
        breathingSource.GetSpectrumData(breathData, 0, FFTWindow.Blackman);

        ecgSource.GetSpectrumData(combinedData, 0, FFTWindow.Blackman);

        for (int j = 0; j < heartData.Length; j++)
        {
            combinedData[j] = heartData[j] + (breathData[j]  * 5.0f);
        }
        
        for (int i = 0; i < ecgBars.Length; i++)
        {
            Vector2 ecgRectSize = ecgBars[i].GetComponent<RectTransform>().rect.size;

            ecgRectSize.y = minHeight + (combinedData[i] * (maxHeight - minHeight) * 5.0f);

            if (ecgRectSize.y > maxHeight*1.5f) ecgRectSize.y = maxHeight * 1.5f;

            ecgBars[i].GetComponent<RectTransform>().sizeDelta = ecgRectSize;

            ecgBars[i].GetComponent<Image>().color = ecgColor;
        }
    }

    public float RandomGaussian(float minValue, float maxValue)
    {
        // Code by Oneiros90 - This will produce random heart rates aligned to a
        // standard distribution curve - e.g. more in normal range
        float u, v, S;
        float temp;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;

        temp = Mathf.Clamp(std * sigma + mean, minValue, maxValue);

        return temp;
    }
}
