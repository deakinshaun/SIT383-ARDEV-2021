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

    public Button adjustUpButton;
    public Button adjustDownButton;

    public Text activeActivityText;
    public GameObject activeItemIcon;
    public Text sensitivityValueText;
    public Text debugMessageText;

    private string activeTask;
    private string debugMessage;

    private Color colorHeart = new Color32(234, 82, 211, 255);
    private Color colorAtrial = new Color32(252, 175, 56, 255);
    private Color colorVentricle = new Color32(249, 83, 53, 255);
    private Color32 colorGreen = new Color32(0, 255, 0, 255);
    private Color32 colorYellow = new Color32(255, 255, 0, 255);

    private float activeSensitivity, activeGap, minSliderValue, maxSliderValue, currentSliderValue;

    public Text valueTarget;
    public Text valueCurrent;
    //public Text labelTarget;
    //public Text labelCurrent;

    public Sprite greenUp;
    public Sprite greenDown;
    public Sprite yellowUp;
    public Sprite yellowDown;

    private float gap = 0.0f;

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
        valueTarget.text = GetComponent<HeartDetails>().GetTargetHeartrate().ToString("0.0");
        valueCurrent.text = GetComponent<HeartDetails>().GetCurrentHeartrate().ToString("0.0");

        gap = Mathf.Abs(GetComponent<HeartDetails>().GetTargetHeartrate() - GetComponent<HeartDetails>().GetCurrentHeartrate());
        gap = Mathf.Round(gap * 10) / 10;
        debugMessage = "Heart gap is " + gap;
        debugMessageText.text = debugMessage;
        if (GetComponent<HeartDetails>().GetTargetHeartrate() > GetComponent<HeartDetails>().GetCurrentHeartrate())
        {
            //up button green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = yellowDown;
        }
        else if (gap == 0.0f)
        {
            //we have a match so both to green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
        }
        else
        {
            //down button green
            adjustUpButton.GetComponent<Image>().sprite = yellowUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
        }
    }

    public void DisplayForAtrial()
    {
        activeActivityText.text = "Dampening Atrial Vascular Rhythm (A)";
        activeItemIcon.GetComponent<Image>().color = colorAtrial;
        debugMessageText.text = debugMessage;
        sensitivityValueText.text = GetComponent<AtrialDetails>().GetAtrialSensitivityAmount().ToString("0.0");
        valueTarget.text = GetComponent<AtrialDetails>().GetTargetAtrialValue().ToString("0.0");
        valueCurrent.text = GetComponent<AtrialDetails>().GetCurrentAtrialValue().ToString("0.0");

        gap = Mathf.Abs(GetComponent<AtrialDetails>().GetTargetAtrialValue() - GetComponent<AtrialDetails>().GetCurrentAtrialValue());
        gap = Mathf.Round(gap * 10) / 10;
        debugMessage = "Atrial gap is " + gap;
        debugMessageText.text = debugMessage;

        if (GetComponent<AtrialDetails>().GetTargetAtrialValue() > GetComponent<AtrialDetails>().GetCurrentAtrialValue())
        {
            //up button green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = yellowDown;
        }
        else if (gap == 0.0f)
        {
            //we have a match so both to green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
        }
        else
        {
            //down button green
            adjustUpButton.GetComponent<Image>().sprite = yellowUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
        }
    }

    public void DisplayForVentricle()
    {
        activeActivityText.text = "Dampening Ventricle Vascular Rhythm (V)";
        debugMessageText.text = debugMessage;
        activeItemIcon.GetComponent<Image>().color = colorVentricle;
        sensitivityValueText.text = GetComponent<VentricleDetails>().GetVentricleSensitivityAmount().ToString("0.0");
        valueTarget.text = GetComponent<VentricleDetails>().GetTargetVentricleValue().ToString("0.0");
        valueCurrent.text = GetComponent<VentricleDetails>().GetCurrentVentricleValue().ToString("0.0");

        gap = Mathf.Abs(GetComponent<VentricleDetails>().GetTargetVentricleValue() - GetComponent<VentricleDetails>().GetCurrentVentricleValue());
        gap = Mathf.Round(gap * 10) / 10;
        debugMessage = "Ventricle gap is " + gap;
        debugMessageText.text = debugMessage;

        if (GetComponent<VentricleDetails>().GetTargetVentricleValue() > GetComponent<VentricleDetails>().GetCurrentVentricleValue())
        {
            //up button green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = yellowDown;
        }
        else if (gap == 0.0f)
        {
            //we have a match so both to green
            adjustUpButton.GetComponent<Image>().sprite = greenUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
        }
        else
        {
            //down button green
            adjustUpButton.GetComponent<Image>().sprite = yellowUp;
            adjustDownButton.GetComponent<Image>().sprite = greenDown;
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
