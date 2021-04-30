using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activitySelector : MonoBehaviour
{
    private bool rateIsActive, atrialIsActive, ventricalIsActive;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickRate()
    {
        if (!rateIsActive)
        {
            rateIsActive = true;
            atrialIsActive = false;
            ventricalIsActive = false;

            GetComponent<hudDisplay>().setActiveTask("heartrate");
        }
    }

    public void onClickAtrial()
    {
        if (!atrialIsActive)
        {
            rateIsActive = false;
            atrialIsActive = true;
            ventricalIsActive = false;

            GetComponent<hudDisplay>().setActiveTask("atrial");
        }
    }

    public void onClickVentrical()
    {
        if (!ventricalIsActive)
        {
            rateIsActive = false;
            atrialIsActive = false;
            ventricalIsActive = true;

            GetComponent<hudDisplay>().setActiveTask("ventrical");
        }
    }

    public bool getRateIsActive()
    {
        return rateIsActive;
    }

    public bool getAtrialIsActive()
    {
        return atrialIsActive;
    }

    public bool getVentricalIsActive()
    {
        return ventricalIsActive;
    }

    public string getActiveActivity()
    {
        if (rateIsActive) return "Heartrate";
        if (atrialIsActive) return "Atrial";
        if (ventricalIsActive) return "Ventrical";
        return "Unknown";
    }

}
