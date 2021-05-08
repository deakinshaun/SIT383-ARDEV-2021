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
    public Text debugMessageText;
    public Text maximumSliderValueText;
    public Text minumumSliderValueText;
    public Text targetValueText;
    public GameObject activeItemIcon;
    public Text sensitivityValueText;
    public Text currentValueText;

    public Slider trackingSlider;

    private string activeTask;
    private string debugMessage;

    private float activeSensitivity, activeGap, minSliderValue, maxSliderValue, currentSliderValue;

    // Start is called before the first frame update
    void Start()
    {
        activeTask = "heartrate";
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
        GetSliderValues();

        if (activeTask == "heartrate") displayForHeart();
        else if (activeTask == "atrial") DisplayForAtrial();
        else if (activeTask == "ventrical") DisplayForVentricle();
        else DisplayForDebug();
    }

    public void displayForHeart()
    {
        activeActivityText.text = "Adjusting Heartrate";
        debugMessageText.text = debugMessage;
        maximumSliderValueText.text = minSliderValue.ToString();
        minumumSliderValueText.text = maxSliderValue.ToString();
        targetValueText.text = GetComponent<HeartDetails>().GetTargetHeartrate().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(51, 0, 0, 255);
        sensitivityValueText.text = GetComponent<HeartDetails>().GetHeartSensitivityAmount().ToString();
        currentValueText.text = GetComponent<HeartDetails>().GetCurrentHeartrate().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
    }

    public void DisplayForAtrial()
    {
        activeActivityText.text = "Dampening Atrial Vascular Rhythm";
        debugMessageText.text = debugMessage;
        maximumSliderValueText.text = minSliderValue.ToString();
        minumumSliderValueText.text = maxSliderValue.ToString();
        targetValueText.text = GetComponent<AtrialDetails>().GetTargetAtrial().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(0, 13, 11, 255);
        sensitivityValueText.text = GetComponent<AtrialDetails>().GetAtrialSensitivityAmount().ToString();
        currentValueText.text = GetComponent<AtrialDetails>().GetCurrentAtrial().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
    }

    public void DisplayForVentricle()
    {
        activeActivityText.text = "Dampening Ventrical Vascular Rhythm";
        debugMessageText.text = debugMessage;
        maximumSliderValueText.text = minSliderValue.ToString();
        minumumSliderValueText.text = maxSliderValue.ToString();
        targetValueText.text = GetComponent<VentricleDetails>().GetTargetVentricleValue().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(44, 51, 0, 255);
        sensitivityValueText.text = GetComponent<VentricleDetails>().GetVentricleSensitivityAmount().ToString();
        currentValueText.text = GetComponent<VentricleDetails>().GetCurrentVentricleValue().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
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
            return GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<AtrialDetails>().GetCurrentAtrial();
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
