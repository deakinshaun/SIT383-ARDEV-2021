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
        if (GetComponent<ActivitySelector>().GetHeartrateIsActive())
        {
            GetComponent<HeartDetails>().RaiseHeartSensitivityAmount();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().IncreaseAtrialSensitivityIndex();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().RaiseVentricleSensitivityAmount();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }

    public void OnMinusSensitivity()
    {
        if (GetComponent<ActivitySelector>().GetHeartrateIsActive())
        {
            GetComponent<HeartDetails>().LowerHeartSensitivityAmount();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().DecreaseAtrialSensitivityIndex();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().LowerVentricleSensitivityAmount();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }

}
