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

    //Rectangular ECG display option
    private Color ecgColor = Color.yellow;
    private EcgObjectScript [] ecgBars;

    private AudioSource ecgSource;
    private AudioSource breathingSource;
    private AudioSource noiseSource;

    private AudioSource cancelOutBreathing;
    private AudioSource cancelOutNoise;

    public void CheckCancelBreathing()
    {
        float currentValueV = heart.GetComponent<VentricleDetails>().GetTargetVentricleValue();
        float targetValueV = heart.GetComponent<VentricleDetails>().GetCurrentVentricleValue();
        float intitalValueV = heart.GetComponent<VentricleDetails>().GetInitialVentricleValue();

        float startToTargetGap = Mathf.Abs(intitalValueV - targetValueV);
        float currentToTargetGap = Mathf.Abs(currentValueV - targetValueV);

        if (currentToTargetGap > 0.05)
        {
            Debug.Log("Current breathing gap is " + currentToTargetGap);

            float delta = currentToTargetGap / startToTargetGap;
            if (delta > 1.0f) delta = 1.0f;

            //delta = 1 - delta;

            if (delta < 0.05f) delta = 0.0f;

            //If currentToTargetGap = 0, play the cancel noise at 1 to negate base noise
            breathingSource.volume = delta;
            Debug.Log("Cancel breathing volume is " + delta);

        }
        else breathingSource.volume = 0;
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
        //currentRespiratoryRate = currentHeartRate * RandomGaussian(20.0f, 40.0f) / 100.0f;

        ecgSource.pitch = currentHeartRate / 60.0f;
        //breathingSource.pitch = currentRespiratoryRate/20.0f;
        //cancelOutBreathing.pitch = currentRespiratoryRate / 20.0f;

        float[] heartData = new float[256];
        float[] breathData = new float[256];
        float[] cancelBreathData = new float[256];
        float[] combinedData = new float[256];

        ecgSource.GetSpectrumData(heartData, 0, FFTWindow.Blackman);
        breathingSource.GetSpectrumData(breathData, 0, FFTWindow.Blackman);
        //cancelOutBreathing.GetSpectrumData(cancelBreathData, 0, FFTWindow.Blackman);

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
