using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudDisplay : MonoBehaviour
{
    public Text TimerDisplay;
    private float countDownValue = 60f * 1f;
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
        countDownValue -= Time.deltaTime;
        TimerDisplay.text = string.Format("{0:00}:{1:00}", ((int)(countDownValue / 60) % 60).ToString("d2"), ((int)(countDownValue % 60)).ToString("d2"));
    }

    public void updateHUD()
    {
        getSliderValues();

        if (activeTask == "heartrate") displayForHeart();
        else if (activeTask == "atrial") displayForAtrial();
        else if (activeTask == "ventrical") displayForVentrical();
        else displayForDebug();
    }

public string getActiveTask()
    {
        return activeTask;
    }

    public void setActiveTask (string task)
    {
        activeTask = task;
        updateHUD();
    }

    public void displayForHeart()
    {
        activeActivityText.text = "Adjusting Heartrate";
        debugMessageText.text = debugMessage;
        maximumSliderValueText.text = minSliderValue.ToString();
        minumumSliderValueText.text = maxSliderValue.ToString();
        targetValueText.text = GetComponent<HeartDetails>().getTarget().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(51, 0, 0, 255);
        sensitivityValueText.text = GetComponent<HeartDetails>().getSensitivity().ToString();
        currentValueText.text = GetComponent<HeartDetails>().getCurrent().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
    }

    public void displayForAtrial()
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

    public void displayForVentrical()
    {
        activeActivityText.text = "Dampening Ventrical Vascular Rhythm";
        debugMessageText.text = debugMessage;
        maximumSliderValueText.text = minSliderValue.ToString();
        minumumSliderValueText.text = maxSliderValue.ToString();
        targetValueText.text = GetComponent<VentricleDetails>().getTarget().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(44, 51, 0, 255);
        sensitivityValueText.text = GetComponent<VentricleDetails>().getSensitivity().ToString();
        currentValueText.text = GetComponent<VentricleDetails>().getCurrent().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
    }

    public void getSliderValues()
    {

        //Initialise for the deafult Heartrate adjustment activity
        activeSensitivity = getSensitivity();
        activeGap = getGap();

        if (activeTask == "heartrate") minSliderValue = -(GetComponent<HeartDetails>().getMax() / activeSensitivity);
        else if (activeTask == "atrial") minSliderValue = -(GetComponent<AtrialDetails>().GetAtrialMaximum() / activeSensitivity);
        else if (activeTask == "ventrical") minSliderValue = -(GetComponent<VentricleDetails>().getMax() / activeSensitivity);

        maxSliderValue = 0.0f;

        currentSliderValue = Mathf.Abs(getGap() / activeSensitivity);
    }


    public float getGap()
    {
        //Atrial and Ventrical gaps not yet calculated
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<AtrialDetails>().GetCurrentAtrial();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<VentricleDetails>().getCurrent();
        }
        else
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<HeartDetails>().getCurrent();
        }
    }

    public float getSensitivity()
    {
        if (GetComponent<ActivitySelector>().GetAtrialIsActive())
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<AtrialDetails>().GetAtrialSensitivityIndex();
        }
        else if (GetComponent<ActivitySelector>().GetVentricalIsActive())
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<VentricleDetails>().getSensitivityIndex();
        }
        else
        {
            return GetComponent<HeartDetails>().getTarget() - GetComponent<HeartDetails>().getSensitivityIndex();
        }
    }

    public void displayForDebug()
    {

    }

}
