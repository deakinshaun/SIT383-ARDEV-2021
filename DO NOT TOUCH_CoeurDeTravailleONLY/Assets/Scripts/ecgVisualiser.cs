using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ecgVisualiser : MonoBehaviour
{
    private float minHeight = 10.0f;
    private float maxHeight = 200.0f;
    private float currentHeartRate = 60.0f;
    private float currentRespiratoryRate = 20.0f;

    //Rectangular ECG display option
    private Color ecgColor = Color.yellow;
    private ecgObjectScript [] ecgBars;

    private AudioSource ecgSource;
    private AudioSource breathingSource;
    private AudioSource noiseSource;

    public AudioClip heartAudio;
    public AudioClip breatheAudio;
    public AudioClip noiseAudio;

    public float heartrate;
    public float respirationRate;
    public GameObject heart;

    public int ecgSample = 64;


    // Start is called before the first frame update
    void Start()
    {
        ecgBars = GetComponentsInChildren<ecgObjectScript>();

        if (!heartAudio) return;

        ecgSource = new GameObject("ecgAudioSource").AddComponent<AudioSource>();
        ecgSource.loop = true;
        ecgSource.clip = heartAudio;

        breathingSource = new GameObject("breathAudioSource").AddComponent<AudioSource>();
        breathingSource.loop = true;
        breathingSource.clip = breatheAudio;

        noiseSource = new GameObject("noiseAudioSource").AddComponent<AudioSource>();
        noiseSource.loop = true;
        noiseSource.clip = noiseAudio;

        ecgSource.Play();
        breathingSource.Play();
        noiseSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentHeartRate = heart.GetComponent<heartDetails>().getCurrent();
        currentRespiratoryRate = currentHeartRate * RandomGaussian(20.0f, 40.0f) / 100.0f;

        ecgSource.pitch = currentHeartRate / 60.0f;
        breathingSource.pitch = currentRespiratoryRate/20.0f;

        float[] heartData = new float[256];
        float[] breathData = new float[256];
        float[] combinedData = new float[256];

        ecgSource.GetSpectrumData(heartData, 0, FFTWindow.Rectangular);
        breathingSource.GetSpectrumData(breathData, 0, FFTWindow.Rectangular);

        ecgSource.GetSpectrumData(combinedData, 0, FFTWindow.Rectangular);

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
