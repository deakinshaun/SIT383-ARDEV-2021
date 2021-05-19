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

    public void OnPlusPressed()
    {
        //Increase the step change of the adjustment buttons
        if (GetComponent<ActivitySelector>().GetHeartrateIsActive())
        {
            GetComponent<HeartDetails>().RaiseHeartStepAmount();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().RaiseAtrialStepAmount();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().RaiseVentricleStepAmount();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }

    public void OnMinusPressed()
    {
        //Decrease the step change of the adjustment buttons
        if (GetComponent<ActivitySelector>().GetHeartrateIsActive())
        {
            GetComponent<HeartDetails>().LowerHeartStepAmount();
        }
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().LowerAtrialStepAmount();
        }
        if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            GetComponent<VentricleDetails>().LowerVentricleStepAmount();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }

}
