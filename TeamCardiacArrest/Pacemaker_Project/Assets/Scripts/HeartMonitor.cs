using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    public SoundManager sound;    
    //List of spectrum values
    private List<double> timeSeries = new List<double>();
    private List<double> savedSeries = new List<double>();
    //Number of sensor readings
    private int numReading = 256;

    private float width = 6.0f; //Width of spectrum chart
    private float timeOffset = 0.0f;  //Offset t o center of the spectrum chart

    private int timeStep = 4; //Number of adjacent bins that are condensed into one
    public float smoothedPeak = 0.0f; //Smoothed value for the peak measured at each frame
    public float smoothingFactor = 0.98f; // Proportion of the smoothed value retained on each frame.

    private float minFreq = 0.50f;
    private float maxFreq = 3.90f;

    public int BPM;

    public GameObject timeSeriesParent;
    public GameObject frequencySeriesParent;
    public GameObject barTemplate;

    public Text debugText;
    public Text toggleText;
    public Button toggleButton;
    public Button recordButton;

    private bool enter;
    private bool playRecording;

    void CreateBeat()
	{
	}

    private void drawChart(List<double> values, GameObject parent)
    {
        //Clear spectrum chart
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < values.Count / 2; i+= timeStep)
        {
            //Spectrum width (How long it is)
            GameObject g = Instantiate(barTemplate);
            g.transform.position = new Vector3(width * (float) i / (float) (numReading / 2) + timeOffset, 0, 0);
            g.transform.localScale = new Vector3(width / (float) (numReading / (2 * timeStep)), 
                                                0.5f * (float) values[i],
                                                width / (float)(numReading / (2 * timeStep)));
            g.transform.SetParent(parent.transform, false);
        }
    }

    private void drawChart()
	{
        drawChart(savedSeries, timeSeriesParent);
	}

    //Saves current view of the frequency for later use.
    public void RecordHeartbeat()
	{
        debugText.text = "Current heartrate view saved";
        toggleButton.interactable = true;


        for (int i = 0; i < numReading; i++)
		{
            savedSeries[i] = timeSeries[i];
		}

        drawChart(savedSeries, frequencySeriesParent);
    }

    //Toggle the playRecording boolean and (change the play button text )
    public void recordingPlay()
	{
        playRecording = !playRecording;

        if (playRecording)
        {
            debugText.text = "Recording playing now";
            enter = true;
            toggleText.text = "Stop";
            recordButton.interactable = false;

            for (int i = 0; i < numReading; i++)
            {
                savedSeries[i] = timeSeries[i];
            }
        }
        else
        { 
            debugText.text = "Recording stopping";
            enter = true;
            toggleText.text = "Play";
            recordButton.interactable = true;
        }
	}

    //Clear saved recording
    public void recordingClear()
	{
        savedSeries.Clear();
        debugText.text = "Saved cleared";
        enter = true;
        toggleButton.interactable = false;
    }

    private void Awake()
    {
        StartCoroutine(Display(2.0f));
    }

    void FixedUpdate()
    {
        //Sample acceleration
        Vector3 acc = Input.acceleration;
        timeSeries.Add(acc.magnitude);

        while (timeSeries.Count > numReading)
        {
            timeSeries.RemoveAt(0);
        }
        if (timeSeries.Count == numReading)
        {
            if (!playRecording)
            {

                double[] seriesReal = new double[numReading];
                double[] seriesComplex = new double[numReading];
                for (int i = 0; i < numReading; i++)
                {
                    seriesReal[i] = timeSeries[i];
                    seriesComplex[i] = 0.0;
                }
                
                int m = (int)(Mathf.Log(numReading) / Mathf.Log(2.0f));
                //Transform sensor data into frequency values.
                FFTLibrary.Complex.FFT(1, m, seriesReal, seriesComplex);

                //Sensor sample rate.
                float baseFreq = 1.0f / Time.deltaTime;
                //Frequency range occupied by each bin.
                float binFreq = baseFreq / numReading;

                //Find peak amplitude
                float maxLevel = 0.0f;
                bool maxFound = false;
                float peakFreq = 0.0f;

                for (int i = 1; i < numReading / 2; i += timeStep)
				{
                    for (int j = 0; j < timeStep; j++)
					{
                        int index = i + j;
                        float v = 1000.0f * (float) (seriesReal[i] * seriesReal[i] + seriesComplex[i] * seriesComplex[i]);
                        float freq = index * binFreq;
                        if ((freq >= minFreq) && (freq <= maxFreq))
						{
                            if((!maxFound) || (maxLevel < v))
							{
                                maxLevel = v;
                                maxFound = true;
                                peakFreq = freq;
							}
						}
                    }
                }
                BPM = (int)Mathf.RoundToInt(smoothedPeak * 60); // Calculate BPM (Beats per second * 60 seconds)
                smoothedPeak = smoothingFactor * smoothedPeak + (1.0f - smoothingFactor) * peakFreq;
             
                drawChart(timeSeries, timeSeriesParent);

                //Heartbeat Sound using Aya's sounds
                if (peakFreq > 3.5f)
				{
                    sound.beep();
				}
            }

            else
			{
                //Play recording that's saved
                InvokeRepeating("drawChart", 1.0f, 0.5f);
                //drawChart(savedSeries, timeSeriesParent);
			}
        }
        else
		{
            debugText.text = "Starting: " + (100.0f * timeSeries.Count / numReading) + "%";
        }
    }

    IEnumerator Display(float time)
    {
        while (true)
        {
            if (!enter) debugText.text = BPM.ToString() + " bpm";
            yield return new WaitForSeconds(time);
            enter = false;
        }
    }

}