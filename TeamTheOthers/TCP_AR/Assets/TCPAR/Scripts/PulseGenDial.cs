using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Genericised button
// Uses messaging to the pulse generator

public class PulseGenDial : MonoBehaviour
{
    public GameObject pulseGenerator;

    public string pulseGeneratorDialLeftFunction = "UnknownButtonFunction";
    public string pulseGeneratorDialRightFunction = "UnknownButtonFunction";

    // Start is called before the first frame update
    void Start()
    {
        if (pulseGenerator == null)
        {
            GameObject foundPulseGenerator = GameObject.FindWithTag("pulse_generator");
            if (foundPulseGenerator != null)
            {
                pulseGenerator = foundPulseGenerator;
            }
        }
    }

    public void ARDragRight()
    {
        pulseGenerator.transform.SendMessage(pulseGeneratorDialRightFunction);
       
    }

    public void ARDragLeft()
    {
        pulseGenerator.transform.SendMessage(pulseGeneratorDialLeftFunction);
       
    }
}
