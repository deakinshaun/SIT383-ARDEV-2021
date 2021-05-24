using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

public enum PacingMode
{
    DDD,
    DDI,
    DOO,
    AAI,
    AOO,
    VVI,
    VOO,
    OOO
}

public enum LEDColor
{
    Blue,
    Green
}

public class PulseGenController : MonoBehaviour
{
    /* Editor Vars */
    [Header("Objects")]
    public GameObject screen;
    public GameObject screen2;
    public GameObject atrialPulseLED;
    public GameObject ventPulseLED;
    public GameObject atrialSenseLED;
    public GameObject ventSenseLED;

    [Header("Materials")]
    public Material screenOffMaterial;
    public Material screenOnMaterial;
    public Material ledGreenMaterial;
    public Material ledBlueMaterial;
    public Material ledOffMaterial;
    [Header("Sounds")]
    public AudioClip soundClick;
    public AudioClip soundSoftButton;

    [Header("Operation")]
    public bool connectedToPatient = true;
    public PacingMode pacingMode = PacingMode.DDD; // The only pacing mode that will work in this simulation
    public int deviceBPM = 80;

    /* Device State */
    private bool devicePower = true;
    private bool deviceRunning = true;

    private int lastPulse = 0;
    private int lastVentPulse = 0;

    private int currentControllerBPM = 80;

    private int currentAvInterval = 160; // A-V Interval milliseconds
    private int signalLengthTime = 500; // Time that the signal raises for (not accurate, but determines LED timing

    //private Timer nextPulseTimer = null;
    //private Timer nextVentPulseTimer = null;

    private DateTime nextPulseTime = DateTime.Now;
    private DateTime nextVentPulseTime = DateTime.Now;

    private DateTime lastAtrialPulseTime = DateTime.Now;
    private DateTime lastVentPulseTime = DateTime.Now;

    private bool pulsingStateChange = false;
    public bool _pulsingAtrial = false;
    public bool _senseAtrial = false;
    public bool _pulsingVent = false;
    public bool _senseVent = false;

    void Start()
    {
        _ShowAndroidToastMessage("Simulation limited to DDD mode");
        InitiatePacing();
    }

    void Update()
        // TODO: Move timing into update loop... SIGH!
    {
        if (pulsingStateChange) {
            if (_pulsingAtrial)
            {
                UpdateLED(LEDColor.Green, atrialPulseLED, true);
                
            } else
            {
                UpdateLED(LEDColor.Green, atrialPulseLED, false);
            }
            if (_pulsingVent)
            {
                UpdateLED(LEDColor.Green, ventPulseLED, true);
            } else
            {
                UpdateLED(LEDColor.Green, ventPulseLED, false);
            }
            if (_senseAtrial)
            {
                UpdateLED(LEDColor.Blue, atrialSenseLED, true);
    
            } else
            {
                UpdateLED(LEDColor.Green, atrialSenseLED, false);
            }
            if (_senseVent)
            {
                UpdateLED(LEDColor.Blue, ventSenseLED, true);
               
            } else
            {
                UpdateLED(LEDColor.Green, ventSenseLED, false);
            }
        }

        pulsingStateChange = false;
    }

    private void FixedUpdate()
    {
        //Debug.Log("Comparing " + (System.DateTime.Now.Millisecond) + " with " + nextPulseTime.Millisecond);
        if (nextPulseTime != System.DateTime.MaxValue)
        {
            if (System.DateTime.Compare(System.DateTime.Now, nextPulseTime) > 0)
            {
                lastAtrialPulseTime = nextPulseTime;
                // next pulse time has lapsed
                nextPulseTime = System.DateTime.MaxValue;
                

                OnPulseEvent();
            }
        }
        if (nextVentPulseTime != System.DateTime.MaxValue)
        {
            if (System.DateTime.Compare(System.DateTime.Now, nextVentPulseTime) > 0)
            {
                lastVentPulseTime = nextVentPulseTime;
                nextVentPulseTime = System.DateTime.MaxValue;
               
                // next vent pulse time has lapsed
                OnVentricularPulseEvent();
            }
        }

        if (System.DateTime.Compare(System.DateTime.Now, lastAtrialPulseTime.AddMilliseconds(signalLengthTime)) > 0) {
            // LED should be switched off
            _senseAtrial = false;
            _pulsingAtrial = false;
            pulsingStateChange = true;
        }

        if (System.DateTime.Compare(System.DateTime.Now, lastVentPulseTime.AddMilliseconds(signalLengthTime)) > 0)
        {
            // LED should be switched off
            _senseVent = false;
            _pulsingVent = false;
            pulsingStateChange = true;
        }
    }

    void StopPacing()
    {
        //nextPulseTimer.Stop();
        //nextVentPulseTimer.Stop();
        deviceRunning = false;
    }

    void InitiatePacing()
    {
        deviceRunning = true;
        Debug.unityLogger.Log("TCPAR", "Starting device pacing at " + currentControllerBPM + " BPM");

        _ShowAndroidToastMessage("Starting device pacing at " + currentControllerBPM + " BPM");
        /* Set start time to now */
        lastPulse = DateTime.Now.Millisecond;

        Debug.unityLogger.Log("TCPAR", "Last pulse set.");

        /* Schedule next pacing */
        // This code is absolutely for simulation purposes only and you would 
        // never use this form of scheduling for a real device HAHA!

        // Create a timer with a interval based on the intended BPM
        // Timer (in millisenconds) is set to rate (BPM) / 60 (mins) * 1000 (ms)
        nextPulseTime = System.DateTime.Now;
        nextPulseTime = nextPulseTime.AddMilliseconds(currentControllerBPM / 60 * 1000);


        //nextPulseTimer = new System.Timers.Timer(currentControllerBPM / 60 * 1000);
        // Hook up the Elapsed event for the timer. 
        //nextPulseTimer.Elapsed += OnPulseEvent;
        //nextPulseTimer.AutoReset = false;

        //nextPulseTimer.Start();

        Debug.unityLogger.Log("TCPAR", "Atrial pulsing scheduled.");

        // Setup a timer for the ventricular pacing that occurs after the Atrial Depolorization
        nextVentPulseTime = System.DateTime.Now;
        nextVentPulseTime = nextVentPulseTime.AddMilliseconds(currentAvInterval);

        //nextVentPulseTimer = new System.Timers.Timer(currentAvInterval);
        // Hook up the Elapsed event for the ventricular pulsing
        //nextVentPulseTimer.Elapsed += OnVentricularPulseEvent;
        //nextVentPulseTimer.AutoReset = false;
    }

    void SensedPulse()
    {
        //nextPulseTimer.Stop();
       // nextVentPulseTimer.Stop();

        Debug.unityLogger.Log("TCPAR", "Device sensed atrial event.");
        lastPulse = DateTime.Now.Millisecond;

        UpdateDeviceStates();

        // Schedule Ventricular pulse
        nextVentPulseTime = System.DateTime.Now;
        nextVentPulseTime = nextVentPulseTime.AddMilliseconds(currentAvInterval);
       // nextVentPulseTimer.Start();

        // Timer (in millisenconds) is set to rate (BPM) * 1000 (ms)
        nextPulseTime = System.DateTime.Now;
        nextPulseTime = nextPulseTime.AddMilliseconds(currentControllerBPM / 60 * 1000);

        // Next pulse is scheduled

        pulsingStateChange = true;
        _senseAtrial = true;
    }

    void OnPulseEvent()
    {
       // nextPulseTimer.Stop();
        // Ensure Ventricular pulse has been cancelled if scheduled
        //nextVentPulseTimer.Stop();

        Debug.unityLogger.Log("TCPAR", "Device atrial pulse event.");
        lastPulse = DateTime.Now.Millisecond;

        Debug.unityLogger.Log("TCPAR", "Attempt next schedule...");

        UpdateDeviceStates();

        // Schedule Ventricular pulse
        //nextVentPulseTimer.Start();
        nextVentPulseTime = System.DateTime.Now;
        nextVentPulseTime = nextVentPulseTime.AddMilliseconds(currentAvInterval);

        // Timer (in millisenconds) is set to rate (BPM) * 1000 (ms)
        nextPulseTime = System.DateTime.Now;
        nextPulseTime = nextPulseTime.AddMilliseconds(currentControllerBPM / 60 * 1000);

        Debug.unityLogger.Log("TCPAR", "Next pulse set for in " + currentControllerBPM / 60 * 1000 + "ms");
        //nextPulseTimer.Interval = (currentControllerBPM / 60 * 1000);
        //nextPulseTimer.Elapsed += OnPulseEvent;

        // Next pulse is scheduled
        //nextPulseTimer.Start();
        Debug.unityLogger.Log("TCPAR", "Device next atrial pulse scheduled.");

        pulsingStateChange = true;
        _pulsingAtrial= true;
    }

    // Get any new device settings and apply them to state
    void UpdateDeviceStates()
    {
        // Update 'current' bpm to new setting
        currentControllerBPM = deviceBPM;

        // TODO: Update AV interval?
    }

    void SensedVentricularPulse()
    {
       // nextVentPulseTimer.Stop();
        nextVentPulseTime = DateTime.MaxValue;

       //nextVentPulseTime.AddMilliseconds(currentControllerBPM / 60 * 1000);

        Debug.unityLogger.Log("TCPAR", "Device sensed ventricular event.");
        lastVentPulse = DateTime.Now.Millisecond;

        UpdateDeviceStates();

        pulsingStateChange = true;
        _senseVent = true;
    }

    void OnVentricularPulseEvent()
    {
        Debug.unityLogger.Log("TCPAR", "Device ventricular pulse event.");
        lastVentPulse = DateTime.Now.Millisecond;

        pulsingStateChange = true;
        _pulsingVent = true;
    }


    /* Message 'Action' Callable Functions */

    void PulseGenPowerButtonPressed()
    {
        SetDevicePower(!deviceRunning);
        _ShowAndroidToastMessage("Power Button Pressed");
        PlaySound(soundClick);
    }

    void UnknownButtonFunction()
    {
        Debug.unityLogger.Log("TCPAR", "Button clicked with unknown function!");
        _ShowAndroidToastMessage("Button clicked with unknown function!");
    }

    /* Local Functions */

    private void PlaySound(AudioClip soundToPlay)
    {

        Debug.unityLogger.Log("TCPAR", "Attempting to play sound " + soundToPlay.name);
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = soundToPlay;
        audio.Play();
    }

    private void UpdateLED(LEDColor colour, GameObject led, bool state)
    {
        try
        {
            if (state)
            {
                    switch (colour)
                    {
                        case (LEDColor.Blue):
                            led.GetComponent<Renderer>().material = ledBlueMaterial;
                            break;
                        case (LEDColor.Green):
                            led.GetComponent<Renderer>().material = ledGreenMaterial;
                            break;
                    }
            
            } else
            {
                led.GetComponent<Renderer>().material = ledOffMaterial;
            }
        }
        catch (Exception e)
        {
            Debug.unityLogger.Log("TCPAR", "Unable to affect LED: " + e);
        }
    }

    private void SetDevicePower(bool newState)
    {
        Debug.unityLogger.Log("TCPAR", "Setting Device Power State to " + newState.ToString());

        if (newState == true)
        {
            screen.GetComponent<Renderer>().material = screenOnMaterial;
            screen2.GetComponent<Renderer>().material = screenOnMaterial;
        }
        else
        {
            screen.GetComponent<Renderer>().material = screenOffMaterial;
            screen2.GetComponent<Renderer>().material = screenOffMaterial;
        }

        devicePower = newState;

        if (devicePower)
        {
            StopPacing(); // Just in case somehow this is not already the case... 
            InitiatePacing();
        } else
        {
            StopPacing();
        }
    }

    private void SetDevicePause(bool newState)
    {
        if (devicePower)
        {
            if (deviceRunning)
            {
                InitiatePacing();
            }
            else
            {
                StopPacing();
            }
        }
    }

    private void _ShowAndroidToastMessage(string message)
    {

        Debug.unityLogger.Log("TCPAR", "*ANDROID TOAST*:" + message);
#if !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
#endif
    }

}
