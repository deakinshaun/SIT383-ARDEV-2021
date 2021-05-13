using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBuzzer : MonoBehaviour
{
    public float minimumBPM = 60;
    public float maximumBPM = 100;
    private float currentBPM = 90;

    // Start is called before the first frame update

    //Needs to keep track of the current Dial value, and 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBPM >= maximumBPM || currentBPM <= minimumBPM)
        {
            Debug.Log("BPM outside acceptable range");
            Buzz(100, 200);
        }
    }

    public void SetBPM(float input)
    {
        currentBPM = input;
 //       Debug.Log("Set BPM = " + input + "New BPM is " + currentBPM);
    }

    public void Buzz(int miliseconds, int intensity)
    {
        Vibration2.Vibrate(miliseconds, intensity);
    }
}
