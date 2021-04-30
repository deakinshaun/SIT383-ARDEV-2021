using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClickUp() 
    {
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            GetComponent<atrialDetails>().increaseCurrent();
        }
        else if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            GetComponent<ventricalDetails>().increaseCurrent();
        }
        else
        {
            GetComponent<heartDetails>().increaseCurrent();
        }

        GetComponent<hudDisplay>().updateHUD();
    }

    public void onClickDown()
    {
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            GetComponent<atrialDetails>().decreaseCurrent();
        }
        else if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            GetComponent<ventricalDetails>().decreaseCurrent();
        }
        else
        {
            GetComponent<heartDetails>().decreaseCurrent();
        }

        GetComponent<hudDisplay>().updateHUD();
    }
}
