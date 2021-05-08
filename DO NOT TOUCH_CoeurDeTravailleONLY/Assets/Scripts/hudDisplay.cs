using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudDisplay : MonoBehaviour
{
    //public Text TimerDisplay;
    //private float countDownValue = 60f * 1f;
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
        //countDownValue -= Time.deltaTime;
        //TimerDisplay.text = string.Format("{0:00}:{1:00}", ((int)(countDownValue / 60) % 60).ToString("d2"), ((int)(countDownValue % 60)).ToString("d2"));
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
        targetValueText.text = GetComponent<heartDetails>().getTarget().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(51, 0, 0, 255);
        sensitivityValueText.text = GetComponent<heartDetails>().getSensitivity().ToString();
        currentValueText.text = GetComponent<heartDetails>().getCurrent().ToString();
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
        targetValueText.text = GetComponent<atrialDetails>().getTarget().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(0, 13, 11, 255);
        sensitivityValueText.text = GetComponent<atrialDetails>().getSensitivity().ToString();
        currentValueText.text = GetComponent<atrialDetails>().getCurrent().ToString();
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
        targetValueText.text = GetComponent<ventricalDetails>().getTarget().ToString();
        activeItemIcon.GetComponent<Image>().color = new Color32(44, 51, 0, 255);
        sensitivityValueText.text = GetComponent<ventricalDetails>().getSensitivity().ToString();
        currentValueText.text = GetComponent<ventricalDetails>().getCurrent().ToString();
        trackingSlider.minValue = minSliderValue;
        trackingSlider.maxValue = maxSliderValue;
        trackingSlider.value = currentSliderValue;
    }

    public void getSliderValues()
    {

        //Initialise for the deafult Heartrate adjustment activity
        activeSensitivity = getSensitivity();
        activeGap = getGap();

        if (activeTask == "heartrate") minSliderValue = -(GetComponent<heartDetails>().getMax() / activeSensitivity);
        else if (activeTask == "atrial") minSliderValue = -(GetComponent<atrialDetails>().getMax() / activeSensitivity);
        else if (activeTask == "ventrical") minSliderValue = -(GetComponent<ventricalDetails>().getMax() / activeSensitivity);

        maxSliderValue = 0.0f;

        currentSliderValue = Mathf.Abs(getGap() / activeSensitivity);
    }


    public float getGap()
    {
        //Atrial and Ventrical gaps not yet calculated
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<atrialDetails>().getCurrent();
        }
        else if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<ventricalDetails>().getCurrent();
        }
        else
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<heartDetails>().getCurrent();
        }
    }

    public float getSensitivity()
    {
        if (GetComponent<activitySelector>().getAtrialIsActive())
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<atrialDetails>().getSensitivityIndex();
        }
        else if (GetComponent<activitySelector>().getVentricalIsActive())
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<ventricalDetails>().getSensitivityIndex();
        }
        else
        {
            return GetComponent<heartDetails>().getTarget() - GetComponent<heartDetails>().getSensitivityIndex();
        }
    }

    public void displayForDebug()
    {

    }

}
