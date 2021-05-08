using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitySelector : MonoBehaviour
{
    /*
     * Allows player to select which "Temporary Pacemaker" aspect (Rate, Atrial, Ventricle) is active.  
     * Is attached to btnSelectRate, btnSelectAtrial and btnSelectVentricle on maim HUD Canvas.  
     * Has three “On Click” methods to activate individual aspects along with methods for accessing the active
     * aspect as either a boolean or string value.
     * 
     * Develop by Stephen Caines for SIT383 Augemented Reality Systems
     */


    private bool rateIsActive, atrialIsActive, ventricalIsActive;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRate()
    {
        if (!rateIsActive)
        {
            rateIsActive = true;
            atrialIsActive = false;
            ventricalIsActive = false;

            GetComponent<HudDisplay>().SetActiveTask("heartrate");
        }
    }

    public void OnClickAtrial()
    {
        if (!atrialIsActive)
        {
            rateIsActive = false;
            atrialIsActive = true;
            ventricalIsActive = false;

            GetComponent<HudDisplay>().SetActiveTask("atrial");
        }
    }

    public void OnClickVentrical()
    {
        if (!ventricalIsActive)
        {
            rateIsActive = false;
            atrialIsActive = false;
            ventricalIsActive = true;

            GetComponent<HudDisplay>().SetActiveTask("ventrical");
        }
    }

    public bool GetHeartrateIsActive()
    {
        return rateIsActive;
    }

    public bool GetAtrialIsActive()
    {
        return atrialIsActive;
    }

    public bool GetVentricalIsActive()
    {
        return ventricalIsActive;
    }

    public string GetActiveActivity()
    {
        if (rateIsActive) return "Heartrate";
        if (atrialIsActive) return "Atrial";
        if (ventricalIsActive) return "Ventricle";
        return "Unknown";
    }

}
