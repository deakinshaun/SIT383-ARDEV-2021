using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Genericised button
// Uses messaging to the pulse generator

public class PulseGenButton : MonoBehaviour
{
    public GameObject pulseGenerator;

    public string pulseGeneratorButtonFunction = "UnknownButtonFunction";

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

    public void ARTap()
    {
        pulseGenerator.transform.SendMessage(pulseGeneratorButtonFunction);

        Debug.unityLogger.Log("TCPAR", "Sending Pulse Gen message:" + pulseGeneratorButtonFunction + ", to:" + pulseGenerator.name);
    }
}
