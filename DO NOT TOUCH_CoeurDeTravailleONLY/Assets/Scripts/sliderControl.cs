using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    /*
     * Slider Control – allows player to raise or lower active rate by one sensitivity 
     * unit per click and updates HUD with new information.  Has two methods OnClickUp 
     * and OnClickDown.
     */

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickUp() 
    {
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().IncreaseCurrentAtrial();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            Debug.Log("Calling Increase Ventricle Value");
            GetComponent<VentricleDetails>().IncreaseCurrentVentricleValue();
        }
        else
        {
            GetComponent<HeartDetails>().IncreaseCurrentHeartrate();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }

    public void OnClickDown()
    {
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            GetComponent<AtrialDetails>().DecreaseCurrentAtrial();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            Debug.Log("Calling Decrease Ventricle Value");
            GetComponent<VentricleDetails>().DecreaseCurrentVentricleValue();
        }
        else
        {
            GetComponent<HeartDetails>().DecreaseCurrentHeartrate();
        }

        GetComponent<HudDisplay>().UpdateHUD();
    }
}
