using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    public Text debugText;

    public GameObject timeSeriesParent;
    public GameObject frequencySeriesParent;
    public GameObject barTemplate;

    private List<double> timeSeries = new List<double>();
    private int numReadings = 256;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void drawChart(double [] values, GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < values.Length; i++)
        {
            float width = 8.0f;
            GameObject g = Instantiate(barTemplate);
            g.transform.position = new Vector3(width * i / (float) numReadings, 0, 0);
            g.transform.localScale = new Vector3(width * 1.0f / numReadings, (float)values[i], width * 1.0f / numReadings);
            g.transform.SetParent(parent.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acc = Input.acceleration;
        // debugText.text = "Acc: " + acc;
        timeSeries.Add(acc.magnitude);
        while (timeSeries.Count > numReadings)
        {
            timeSeries.RemoveAt(0);
        }
        if (timeSeries.Count == numReadings)
        {
            debugText.text = "These are my charts";
            double[] seriesReal = new double[numReadings];
            double[] seriesComplex = new double[numReadings];
            for (int i = 0; i < numReadings; i++)
            {
                seriesReal[i] = timeSeries[i];
                seriesComplex[i] = 0.0;
            }
            drawChart(seriesReal, timeSeriesParent);
            int m = (int) (Mathf.Log (numReadings) / Mathf.Log(2.0f));
            FFTLibrary.Complex.FFT(1, m, seriesReal, seriesComplex);
            double[] seriesMagnitude = new double[numReadings];
            for (int i = 0; i < numReadings; i++)
            {
                seriesMagnitude[i] = 1000.0 * (seriesReal[i] * seriesReal[i] + seriesComplex[i] * seriesComplex[i]);
                if (seriesMagnitude[i] > 1000)
                {
                    debugText.text = "The frequency is too high!";
                }
                else
                {
                    debugText.text = "Everything is normal now.";
                }
            }
            drawChart(seriesMagnitude, frequencySeriesParent);

        }
    }
}
