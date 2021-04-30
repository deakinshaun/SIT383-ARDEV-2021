using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class adjustSensitivity : MonoBehaviour
{
    /*
    * This class is used by the plus and minus buttons to set the adjustment sensitivity
    * for the active item
    */

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onPlusSensitivity()
    {
        if (GetComponent<activitySelector>().getRateIsActive())
        {
            GetComponent<heartDetails>().raiseSensitivity();
        }
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            GetComponent<atrialDetails>().raiseSensitivity();
        }
        if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            GetComponent<ventricalDetails>().raiseSensitivity();
        }

        GetComponent<hudDisplay>().updateHUD();
    }

    public void onMinusSensitivity()
    {
        if (GetComponent<activitySelector>().getRateIsActive())
        {
            GetComponent<heartDetails>().lowerSensitivity();
        }
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            GetComponent<atrialDetails>().lowerSensitivity();
        }
        if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            GetComponent<ventricalDetails>().lowerSensitivity();
        }

        GetComponent<hudDisplay>().updateHUD();
    }

}
