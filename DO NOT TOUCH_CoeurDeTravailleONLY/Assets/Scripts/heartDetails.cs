using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDetails : MonoBehaviour
{

    public float min, max;
    private float current, target;
    private List<float> sensitivityArray;
    private int sensitivityIndex;
    private float sensitivityAmount;

    public Text textCurrent;
    public Text textTarget;

    // Start is called before the first frame update
    void Start()
    {
        min = 0;
        max = 180;
        current = RandomGaussian(30.0f, 180.0f);
        target = RandomGaussian(60.0f, 90.0f);

        sensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
        sensitivityIndex = 0;
        sensitivityAmount = sensitivityArray[sensitivityIndex];

        //Force click Heartrate Actvity to get us started
        GetComponent<ActivitySelector>().OnClickRate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateCVTText()
    {
        GetComponent<HudDisplay>().setActiveTask("heartrate");

        if (current == target)
        {
            textCurrent.color = new Color32(0, 255, 0, 255);
            textTarget.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            textCurrent.color = new Color32(255, 255, 0, 255);
            textTarget.color = new Color32(255, 255, 0, 255);
        }
    }

    public float RandomGaussian(float minValue, float maxValue)
    {
        // Code by Oneiros90 - This will produce random heart rates aligned to a
        // standard distribution curve - e.g. more in normal range
        float u, v, S;
        float temp;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;

        temp = Mathf.Clamp(std * sigma + mean, minValue, maxValue);

        return temp;
    }

    public float getCurrent()
    {
        return current;
    }
    public void setCurrent(float newValue)
    {
        current = newValue;
        updateCVTText();
    }

    public void increaseCurrent()
    {
        current += sensitivityAmount;
    }

    public void decreaseCurrent()
    {
        current -= sensitivityAmount;
    }

    public float getTarget()
    {
        return target;
    }

    public void setTarget(float newValue)
    {
        target = newValue;
        updateCVTText();
    }

    public int getSensitivityIndex()
    {
        return sensitivityIndex;
    }

    public void setSensitivityIndex(int newValue)
    {
        sensitivityIndex = newValue;
        setSensitivity(newValue);
    }

    public float getSensitivity()
    {
        return sensitivityAmount;
    }

    public void setSensitivity(int newIndex)
    {
        sensitivityAmount = sensitivityArray[newIndex];
    }

    public void raiseSensitivity()
    {
        sensitivityIndex += 1;
        if (sensitivityIndex > 9)
        {
            sensitivityIndex = 9;
        }

        sensitivityAmount = sensitivityArray[sensitivityIndex];
    }

    public void lowerSensitivity()
    {
        sensitivityIndex -= 1;
        if (sensitivityIndex < 9)
        {
            sensitivityIndex = 0;
        }

        sensitivityAmount = sensitivityArray[sensitivityIndex];
    }

    public float getMin()
    {
        return min;
    }

    public float getMax()
    {
        return max;
    }



}
