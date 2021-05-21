using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGenController : MonoBehaviour
{
    /* Editor Vars */
    [Header("Objects")]
    public GameObject screen;
    public GameObject screen2;
    [Header("Materials")]
    public Material screenOffMaterial;
    public Material screenOnMaterial;
    public Material ledGreenMaterial;
    public Material ledBlueMaterial;
    public Material ledOffMaterial;
    [Header("Sounds")]
    public AudioClip soundClick;
    public AudioClip soundSoftButton;

    /* Device State */
    private bool deviceRunning = true;
    
    void Start()
    {
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

        deviceRunning = newState;
    }

    private void _ShowAndroidToastMessage(string message)
    {
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
    }
}
