using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtrialDetails : MonoBehaviour
{
    [Tooltip("Connect this to Canvas element valueCurrentValue")]
    public Text textCurrentAtrial;
    [Tooltip("Connect this to Canvas element valueTargetValue")]
    public Text textTargetAtrial;
    [Tooltip("Connect this to the newECGDisplay component in the Canvas")]
    public GameObject ECG_Reference;

    private float minimumAtrialValue = 8.0f;
    private float maximumAtrialValue = 30.0f;

    private float initialAtrialValue;
    private float currentAtrialValue;
    private float targetAtrialValue;

    private List<float> atrialSensitivityArray = new List<float> { 0.1f, 0.2f, 0.5f, 1.0f, 2.0f, 5.0f, 10.0f };
    private int atrialSensitivityIndex = 6;
    private float atrialSensitivityAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentAtrialValue = RandomGaussian(minimumAtrialValue, maximumAtrialValue);
        initialAtrialValue = currentAtrialValue;
        targetAtrialValue = initialAtrialValue * RandomGaussian(1.2f, 1.8f);

        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Restart()
    {
        initialAtrialValue = RandomGaussian(30.0f, 180.0f);
        targetAtrialValue = initialAtrialValue * RandomGaussian(1.2f, 1.5f);

        atrialSensitivityIndex = 6;
        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];

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

        //Return it with only one decimal place
        return Mathf.Round(temp * 10) / 10;
    }

    public void UpdateCurrentValueText()
    {
        GetComponent<HudDisplay>().SetActiveTask("atrial");

        if (currentAtrialValue == targetAtrialValue)
        {
            textCurrentAtrial.color = new Color32(0, 255, 0, 255);
            textTargetAtrial.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            textCurrentAtrial.color = new Color32(255, 255, 0, 255);
            textTargetAtrial.color = new Color32(255, 255, 0, 255);
        }
    }

    public float GetInitialAtrialValue()
    {
        return initialAtrialValue;
    }
    public void SetInitialAtrialValue(float newValue)
    {
        initialAtrialValue = newValue;
    }

    public float GetCurrentAtrialValue()
    {
        return currentAtrialValue;
    }
    public void SetCurrentAtrialValue(float newValue)
    {
        currentAtrialValue = newValue;
        textCurrentAtrial.text = newValue.ToString();
    }

    public void IncreaseCurrentAtrialValue()
    {
        currentAtrialValue += atrialSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckCancelNoise();
    }

    public void DecreaseCurrentAtrialValue()
    {
        currentAtrialValue -= atrialSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckCancelNoise();
    }

    public float GetTargetAtrialValue()
    {
        return targetAtrialValue;
    }

    public void SetTargetAtrialValue(float newValue)
    {
        targetAtrialValue = newValue;
        textTargetAtrial.text = newValue.ToString();
    }


    public int GetAtrialSensitivityIndex()
    {
        return atrialSensitivityIndex;
    }

    public void SetAtrialSensitivityIndex(int newValue)
    {
        atrialSensitivityIndex = newValue;
        SetAtrialSensitivityAmount(newValue);
    }

    public float GetAtrialSensitivityAmount()
    {
        return atrialSensitivityAmount;
    }

    public void SetAtrialSensitivityAmount(int newIndex)
    {
        atrialSensitivityAmount = atrialSensitivityArray[newIndex];
    }


    public void RaiseAtrialStepAmount()
    {

        if (atrialSensitivityIndex < atrialSensitivityArray.Count -1)
        {
            atrialSensitivityIndex += 1;
        }

        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    public void LowerAtrialStepAmount()
    {

        if (atrialSensitivityIndex > 0)
        {
            atrialSensitivityIndex -= 1;
        }

        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    public float GetAtrialMinimum()
    {
        return minimumAtrialValue;
    }

    public float GetAtrialMaximum()
    {
        return maximumAtrialValue;
    }

}

