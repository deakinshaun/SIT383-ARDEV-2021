using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartMonitor : MonoBehaviour
{
    private SoundManager sound;    
    
    //List of spectrum values
    private List<double> timeSeries = new List<double>();

    //Number of sensor readings
    private int numReading = 128;

    private float width = 2.0f; //Width of spectrum chart
    private float timeOffset = 0.0f;  //Offset to center of the spectrum chart

    private int timeStep = 4; //Number of adjacent bins that are condensed into one
    public float smoothedPeak = 0.0f; //Smoothed value for the peak measured at each frame
    public float smoothingFactor = 0.98f; // Proportion of the smoothed value retained on each frame.

    private float minFreq = 0.50f;
    private float maxFreq = 3.90f;

    //editable
    //Linked to rate dial - Chris
    public float BPM = 60f;
    //Linked to output dial - Chris
    public float Apower = 3.8f;
    //Linked to sensitivity dial - Chris
    public float Vpower = 2.2f;
    public string difficulty;

    //Creating public 3DText objects to be assigned in the editor - Chris
    public TextMeshPro debugBPM;

    //saved (Not Implemented)
    /*
    public float savedBPM;
    public float savedApower;
    public float savedVpower;
    private List<double> savedSeries = new List<double>(); //List of saved spectrum values
    public GameObject frequencySeriesParent;  //Saved heartbeat container
    */

    //GameObject Links
    public GameObject timeSeriesParent;
    public GameObject barTemplate;

    //UI Connection (Only used for the save functions)
    /*
    public Text debugText;
    public Text toggleText;
    public Button toggleButton;
    public Button recordButton;
    */

    //State
    private bool enter;
    private bool playRecording;

    //Getter and Setter methods for variables dials
    public float getBPM()
    {
        Debug.Log("sending " + BPM);
        return BPM;
    }

    public void setBPM(float newBPM)
    {
        BPM = newBPM;
        Debug.Log("setting BPM as " + BPM);
    }

    public float getApower()
    {
        Debug.Log("sending " + Apower);
        return Apower;     
    }

    public void setApower(float newApower)
    {
        Apower = newApower;
        Debug.Log("setting Apower as " + Apower);
    }

    public float getVpower()
    {
        Debug.Log("sending " + Vpower);
        return Vpower;
    }

    public void setVpower(float newVpower)
    {
        Vpower = newVpower;
        Debug.Log("setting Vpower as " + Vpower);
    }

    private void drawChart(List<double> values, GameObject parent)
    {
        //Clear spectrum chart
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
          for (int i = 0; i < values.Count; i ++)
            {
                //Spectrum width (How long it is)
                GameObject g = Instantiate(barTemplate);
            g.transform.position = new Vector3(width * (float) i / (float) (numReading / 2) + timeOffset, 0, 0);
            g.transform.localScale = new Vector3(width / (float) (numReading / (2 * timeStep)), 
                                                0.5f * (float) values[i],
                                                width / (float)(numReading / (2 * timeStep)));
            g.transform.SetParent(parent.transform, false);
        }
        beat();
    }



    //Saves current view of the frequency for later use. (Old and unused for final solution, can alter the code in the future so that BPM, Apower and Vpower are saved)
    /*
    //overload for drawchart when it is called without values
    private void drawChart()
	{
    drawChart(savedSeries, timeSeriesParent);
	}

    public void RecordHeartbeat()
    {
        debugText.text = "Current heartrate view saved";
        toggleButton.interactable = true;


        savedBPM = BPM;
        savedApower = Apower;
        savedVpower = Vpower;

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

            BPM = savedBPM;
            Apower = savedApower;
            Vpower = savedVpower;
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
    */

    private void Awake()
    {
        StartCoroutine(Display(2.0f));
    }

    void FixedUpdate()
    {
        //Sample acceleration
        Vector3 acc = Input.acceleration;

        if (timeSeries.Count < numReading)
		{
            timeSeries.Add(acc.magnitude);
		}
        timeSeries.Add(0.1f);
        switch (difficulty)
        {
            case "easy":
                timeSeries.Add(0.1f);
                break;
            case "hard":
                timeSeries.Add(acc.magnitude);
                break;            
        }
        


        while (timeSeries.Count > numReading)
        {
            timeSeries.RemoveAt(0);
        }
        if (timeSeries.Count == numReading)
        {
            //if (!playRecording)
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

                for (int i = 1; i < numReading / 2 ; i += timeStep)
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
                smoothedPeak = smoothingFactor * smoothedPeak + (1.0f - smoothingFactor) * peakFreq;
             
                drawChart(timeSeries, timeSeriesParent);
            }
            //Deprecated save code
            //else
			{
                //Play recording that's saved
                InvokeRepeating("drawChart", 1.0f, 0.5f);
                //drawChart(savedSeries, timeSeriesParent);
			}
        }
        else
		{
            debugBPM.text = "Starting: " + (100.0f * timeSeries.Count / numReading) + "%";     //This
        }
    }

    //Delay between bpm updates, just like a real ECG
    IEnumerator Display(float time)
    {
        while (true)
        {
            if (!enter) debugBPM.text = BPM.ToString() + " bpm";       //This
            yield return new WaitForSeconds(time);
            enter = false;
        }
    }


    /* ----------------------------------------------------------------------
     * CODE BELOW WAS OBTAINED ONLINE
     * IT IS NOT CODE DEVELOPED BY CARDIAC ARREST
     * It was however edited by Phillip, original code can be found in the HCHeartbeat folder.
     * Source: https://unitylist.com/p/j7f/Heartbeat-Open-Script-Unity
     * 
     * Its purpose is to count an editable, precise and realistic intreval for a heartbeat to play
     * ----------------------------------------------------------------------
     */

    //Frequency
    private float Hertz;
    private float PeriodT; //FrequencyHertz
    private float remainPeriod;
    private float remainPeriodMillisec;
    public float returnTime = .01f;
    private float startReturnTime;
    private float startReturnTimeMillisec;

    //State
    public int stateIndex = 0;
    public int isBeating;
    private bool Lub;
    public GameObject lubShow;
    
    //Potential future improvment: Extend how long the pulse lasts via Vpower to simulate different heart conditions
    public void beat()
	{
        Hertz = BPM / 60f;
        PeriodT = 1 / Hertz; //https://www.quora.com/How-do-you-convert-Hertz-to-seconds

        if (!Lub)
        {
            remainPeriod -= Time.deltaTime;
            remainPeriodMillisec -= Time.deltaTime * 1000;
            startReturnTime = returnTime;
            startReturnTimeMillisec = returnTime * 1000;
            if (remainPeriodMillisec <= 0f)
            {
                stateIndex = 1;
                timeSeries.Add((Apower / 10));    //<---- Gonna divide this by an arbitrary value to see if it fixes monitor issues
                //sound.beep(); //Comenting this out because it causes null ref
                Lub = true;
            }
        }
        else
        {
            remainPeriod = PeriodT;
            remainPeriodMillisec = PeriodT * 1000;
            startReturnTime -= Time.deltaTime;
            startReturnTimeMillisec -= Time.deltaTime * 1000;
            if (startReturnTimeMillisec <= Time.deltaTime / 2)
            {
                stateIndex = 0;
                timeSeries.Add((Vpower / 10));   //<- Gonna divide this by an arbitrary value to see if it fixes monitor issues
                Lub = false;
            }
        }
    }
}