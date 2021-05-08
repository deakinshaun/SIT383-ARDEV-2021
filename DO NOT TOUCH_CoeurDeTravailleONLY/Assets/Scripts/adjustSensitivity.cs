using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSensitivity : MonoBehaviour
{
    /*
    * This class is used by the plus and minus buttons on the manin HUD canvas to set the 
    * adjustment sensitivity for the active item.
    * 
    * 
    * Develop by Stephen Caines for SIT383 Augemented Reality Systems
    */

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlusSensitivity()
    {
        if (GetComponent<ActivitySelector>().GetRateIsActive())
        {
            GetComponent<HeartDetails>().raiseSensitivity();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().IncreaseAtrialSensitivityIndex();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().raiseSensitivity();
        }

        GetComponent<HudDisplay>().updateHUD();
    }

    public void OnMinusSensitivity()
    {
        if (GetComponent<ActivitySelector>().GetRateIsActive())
        {
            GetComponent<HeartDetails>().lowerSensitivity();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().DecreaseAtrialSensitivityIndex();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().lowerSensitivity();
        }

        GetComponent<HudDisplay>().updateHUD();
    }

}
