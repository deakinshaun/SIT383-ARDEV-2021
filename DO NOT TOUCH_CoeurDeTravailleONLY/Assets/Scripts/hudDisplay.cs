using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudDisplay : MonoBehaviour
{
    /*
     * This is the main control script for the Canvas headup display.  It is called via various methods 
     * to update HUD information.
     * 
     * For each of the three display types (Heart, Atrial, Ventricle) it will retrieve and display the 
     * required set of data values for the active component.
     * 
     * Developed by Stephen Caines
     */

    public Text TimerDisplay;
    private float countDownValue = 60.0f * 1.0f;

    public Text activeActivityText;
    public GameObject activeItemIcon;
    public Text sensitivityValueText;
    public Text debugMessageText;

    private string activeTask;
    private string debugMessage;

    private Color colorHeart = new Color32(234, 82, 211, 255);
    private Color colorAtrial = new Color32(252, 175, 56, 255);
    private Color colorVentricle = new Color32(249, 83, 53, 255);

    private float activeSensitivity, activeGap, minSliderValue, maxSliderValue, currentSliderValue;

    public Text valueTop;
    public Text valueBottom;
    public Text labelTop;
    public Text labelBottom;

    private Vector3 topLabelPos;
    private Vector3 topValuePos;
    private Vector3 bottomLabelPos;
    private Vector3 bottomValuePos;

    private Color32 targetColour = new Color32(0, 255, 0, 255);
    private Color32 currentColour = new Color32(0, 0, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        activeTask = "heartrate";
        topLabelPos = labelTop.transform.localPosition;
        topValuePos = valueTop.transform.localPosition;
        bottomLabelPos = labelBottom.transform.localPosition;
        bottomValuePos = valueBottom.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Added by Ken - displays a one minute countdown
        countDownValue -= Time.deltaTime;
        TimerDisplay.text = string.Format("{0:00}:{1:00}", ((int)(countDownValue / 60) % 60).ToString("d2"), ((int)(countDownValue % 60)).ToString("d2"));
    }

    public void UpdateHUD()
    {
        //Slider moved off screen after UX testing
        //GetSliderValues();

        if (activeTask == "heartrate") displayForHeart();
        else if (activeTask == "atrial") DisplayForAtrial();
        else if (activeTask == "ventrical") DisplayForVentricle();
        else DisplayForDebug();
    }

    public void displayForHeart()
    {
        activeActivityText.text = "Adjusting Heartrate (R)";
        activeItemIcon.GetComponent<Image>().color = colorHeart;
        debugMessageText.text = debugMessage;
        sensitivityValueText.text = GetComponent<HeartDetails>().GetHeartSensitivityAmount().ToString("0.0");

        if(GetComponent<HeartDetails>().GetTargetHeartrate() > GetComponent<HeartDetails>().GetCurrentHeartrate())
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Target:";
            valueTop.text = GetComponent<HeartDetails>().GetTargetHeartrate().ToString("0.0");
            labelTop.color = targetColour;
            valueTop.color = targetColour;

            labelBottom.text = "Current:";
            valueBottom.text = GetComponent<HeartDetails>().GetCurrentHeartrate().ToString("0.0");
            labelBottom.color = currentColour;
            valueBottom.color = currentColour;
        }
        else
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Current:";
            valueTop.text = GetComponent<HeartDetails>().GetCurrentHeartrate().ToString("0.0");
            labelTop.color = currentColour;
            valueTop.color = currentColour;

            labelBottom.text = "target:";
            valueBottom.text = GetComponent<HeartDetails>().GetTargetHeartrate().ToString("0.0");
            labelBottom.color = targetColour;
            valueBottom.color = targetColour;
        }
    }

    public void DisplayForAtrial()
    {
        activeActivityText.text = "Dampening Atrial Vascular Rhythm (A)";
        activeItemIcon.GetComponent<Image>().color = colorAtrial;
        debugMessageText.text = debugMessage;
        sensitivityValueText.text = GetComponent<AtrialDetails>().GetAtrialSensitivityAmount().ToString("0.0");

        if (GetComponent<AtrialDetails>().GetTargetAtrialValue() > GetComponent<AtrialDetails>().GetCurrentAtrialValue())
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Target:";
            valueTop.text = GetComponent<AtrialDetails>().GetTargetAtrialValue().ToString("0.0");
            labelTop.color = targetColour;
            valueTop.color = targetColour;

            labelBottom.text = "Current:";
            valueBottom.text = GetComponent<AtrialDetails>().GetCurrentAtrialValue().ToString("0.0");
            labelBottom.color = currentColour;
            valueBottom.color = currentColour;
        }
        else
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Current:";
            valueTop.text = GetComponent<AtrialDetails>().GetCurrentAtrialValue().ToString("0.0");
            labelTop.color = currentColour;
            valueTop.color = currentColour;

            labelBottom.text = "target:";
            valueBottom.text = GetComponent<AtrialDetails>().GetTargetAtrialValue().ToString("0.0");
            labelBottom.color = targetColour;
            valueBottom.color = targetColour;
        }
    }

    public void DisplayForVentricle()
    {
        activeActivityText.text = "Dampening Ventrical Vascular Rhythm (V)";
        debugMessageText.text = debugMessage;
        activeItemIcon.GetComponent<Image>().color = colorVentricle;
        sensitivityValueText.text = GetComponent<VentricleDetails>().GetVentricleSensitivityAmount().ToString("0.0");

        if (GetComponent<VentricleDetails>().GetTargetVentricleValue() > GetComponent<VentricleDetails>().GetCurrentVentricleValue())
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Target:";
            valueTop.text = GetComponent<VentricleDetails>().GetTargetVentricleValue().ToString("0.0");
            labelTop.color = targetColour;
            valueTop.color = targetColour;

            labelBottom.text = "Current:";
            valueBottom.text = GetComponent<VentricleDetails>().GetCurrentVentricleValue().ToString("0.0");
            labelBottom.color = currentColour;
            valueBottom.color = currentColour;
        }
        else
        {
            //Target is higher than current so is placed on top next to the up button
            labelTop.text = "Current:";
            valueTop.text = GetComponent<VentricleDetails>().GetCurrentVentricleValue().ToString("0.0");
            labelTop.color = currentColour;
            valueTop.color = currentColour;

            labelBottom.text = "target:";
            valueBottom.text = GetComponent<VentricleDetails>().GetTargetVentricleValue().ToString("0.0");
            labelBottom.color = targetColour;
            valueBottom.color = targetColour;
        }
    }

    public void GetSliderValues()
    {

        //Initialise for the deafult Heartrate adjustment activity
        activeSensitivity = GetActiveTaskSensitivity();
        activeGap = GetCurrentTargetGap();

        if (activeTask == "heartrate") minSliderValue = -(GetComponent<HeartDetails>().GetMaximumHeartrate() / activeSensitivity);
        else if (activeTask == "atrial") minSliderValue = -(GetComponent<AtrialDetails>().GetAtrialMaximum() / activeSensitivity);
        else if (activeTask == "ventrical") minSliderValue = -(GetComponent<VentricleDetails>().GetMaximumVentricleValue() / activeSensitivity);

        maxSliderValue = 0.0f;

        currentSliderValue = Mathf.Abs(GetCurrentTargetGap() / activeSensitivity);
    }


    public float GetCurrentTargetGap()
    {
        //Atrial and Ventrical gaps not yet calculated
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<AtrialDetails>().GetCurrentAtrialValue();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<VentricleDetails>().GetCurrentVentricleValue();
        }
        else
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<HeartDetails>().GetCurrentHeartrate();
        }
    }

    public float GetActiveTaskSensitivity()
    {
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<AtrialDetails>().GetAtrialSensitivityIndex();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<VentricleDetails>().GetVentricleSensitivityIndex();
        }
        else
        {
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<HeartDetails>().GetHeartSensitivityIndex();
        }
    }

    public string GetActiveTask()
    {
        return activeTask;
    }

    public void SetActiveTask(string task)
    {
        activeTask = task;
        UpdateHUD();
    }

    public void DisplayForDebug()
    {

    }

}
