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

    public void CancelBreathing()
    {
        // To cancel breathing we will apply an amplifier amount between 0 and 1 on each 
        // of ten frequency bands each corresponding to one of our 10 sensitivity levels
        float [] breathingFilter = new float[10];

        noiseSource.volume = 0.0f;

        float currentValueV = heart.GetComponent<VentricleDetails>().GetTargetVentricleValue();
        float targetValueV = heart.GetComponent<VentricleDetails>().GetCurrentVentricalValue();

        noiseSource.volume = 1.0f - Mathf.Abs(currentValueV / targetValueV);

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

        cancelOutBreathing = new GameObject("cancelBreathing").AddComponent<AudioSource>();
        cancelOutBreathing.clip = breathInvertedAudio;

        noiseSource = new GameObject("noiseAudioSource").AddComponent<AudioSource>();
        noiseSource.loop = true;
        noiseSource.clip = noiseAudio;

        cancelOutNoise = new GameObject("cancelBackgroundNoise").AddComponent<AudioSource>();
        cancelOutNoise.clip = noiseInvertedAudio;

        ecgSource.Play();
        breathingSource.Play();

        //Noise Plays at a standard pitch
        noiseSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentHeartRate = heart.GetComponent<HeartDetails>().GetCurrentHeartrate();
        currentRespiratoryRate = currentHeartRate * RandomGaussian(20.0f, 40.0f) / 100.0f;

        ecgSource.pitch = currentHeartRate / 60.0f;
        breathingSource.pitch = currentRespiratoryRate/20.0f;

        float[] heartData = new float[256];
        float[] breathData = new float[256];
        float[] combinedData = new float[256];

        ecgSource.GetSpectrumData(heartData, 0, FFTWindow.Blackman);
        breathingSource.GetSpectrumData(breathData, 0, FFTWindow.Blackman);

        ecgSource.GetSpectrumData(combinedData, 0, FFTWindow.Blackman);

        for (int j = 0; j < heartData.Length; j++)
        {
            combinedData[j] = heartData[j] + (breathData[j] * 5.0f);
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
