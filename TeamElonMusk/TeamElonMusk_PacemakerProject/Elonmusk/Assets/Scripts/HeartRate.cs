using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartRate : MonoBehaviour
{
    public GameObject spectograph;
    public GameObject barTemplate;
    public Text message;
    public AudioSource audioSource;
    public GameObject prefab;
    float[] data;
    public GameObject[] musicCube;

    private int maxReadings = 256;
   
    private List<double> accelerometerReadings = new List<double>();
    private float spectroScale = 1.0f;
    private float spectroOffset = -3.0f;
    private int spectroStep = 4;
    private float smoothedPeak = 0.0f;
    private float smoothingFactor = 0.98f;
    private float minFreq = 0.50f;
    private float maxFreq = 3.20f;

    void Start()
    {
        musicCube = new GameObject[512];
        data = new float[512];
        for (int i = 0; i < 512; i++)
        {
            musicCube[i] = Instantiate(prefab, Vector3.Slerp(transform.right * 1, -transform.right * 1, i / 512.0f), Quaternion.identity);
        }
    }

    void Update()
    {
        audioSource.GetSpectrumData(data, 0, FFTWindow.Hamming);
        for (int i = 0; i < 512; i++)
        {
            musicCube[i].transform.localScale = new Vector3(0.1f, data[i] * 50 + 1, 0.1f);
        }
    }

    void FixedUpdate()
    {
        Vector3 acc = Input.acceleration;
        accelerometerReadings.Add(acc.magnitude);
        while (accelerometerReadings.Count > maxReadings)
        {
            accelerometerReadings.RemoveAt(0);
        }
        if (accelerometerReadings.Count == maxReadings)
        {
            double[] readings = accelerometerReadings.ToArray();
            double[] readingsComplex = new double[maxReadings];

            for (int i = 0; i < maxReadings; i++)
            {
                readingsComplex[i] = 0.0f;
            }
            int m = (int)(Mathf.Log(maxReadings) / Mathf.Log(2.0f));
            FFTLibrary.Complex.FFT(1, m, readings,readingsComplex);

            float baseFreq = 1.0f / Time.deltaTime;
            float binFreq = baseFreq / maxReadings;

            if (spectograph != null)
            {
                foreach (Transform child in spectograph.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            float maxLevel = 0.0f;
            bool maxFound = false;
            float peakFreq = 0.0f;

            for (int i = 1; i < maxReadings / 2; i += spectroStep)
            {
                float total = 0.0f;
                for (int j = 0; j < spectroStep; j++)
                {
                    int index = i + j;
                    float v = 1000.0f * (float)(readings[index] * readings[index] + readingsComplex[index] * readingsComplex[index]);
                    float freq = index * binFreq;
                    if ((freq >= minFreq) && (freq <= maxFreq))
                    {
                        if ((!maxFound) || (maxLevel < v))
                        {
                            maxLevel = v;
                            maxFound = true;
                            peakFreq = freq;
                        } 
                    }
                    total += v;
                }
                GameObject g = GameObject.Instantiate(barTemplate);
                g.transform.position = new Vector3(spectroScale * (float)i / (float)(maxReadings / 2) + spectroOffset, 0, 0);
                g.transform.localScale = new Vector3(spectroScale / (float)(maxReadings / (2 * spectroStep)), 0.1f + total, 1.0f / (float)(maxReadings / (2 * spectroStep)));
                g.transform.SetParent(spectograph.transform);
            }
            smoothedPeak = smoothingFactor * smoothedPeak +(1.0f - smoothingFactor) * peakFreq;
            message.text = "Base freq: " + baseFreq + "Hz" + "\n" + "Bin freq: " + binFreq + "Hz" + "\n " + "Peak freq: " + smoothedPeak + "Hz" + " = " + (smoothedPeak * 60) + "bpm";
        }
        else
        {
            message.text = "Starting: " + (100.0f * accelerometerReadings.Count / maxReadings) + "%";
        }
    }
}
