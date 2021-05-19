using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentricleDetails : MonoBehaviour
{
    /*
     * Ventricle Component
     * 
     * 
     * Developed by Stephen Caines for SIT383 Augmented Reality Systems
     */

    [Tooltip("Connect this to Canvas element valueCurrentValue")]
    public Text textCurrent;
    [Tooltip("Connect this to Canvas element valueTargetValue")]
    public Text textTarget;
    [Tooltip("Connect this to the newECGDisplay component in the Canvas")]
    public GameObject ECG_Reference;

    private float minimumVentricleValue = 8.0f;
    private float maximumVentricleValue = 30.0f;

    private float initialVentricleValue;
    private float currentVentricleValue;
    private float targetVentricleValue;

    private List<float> ventricleSensitivityArray = new List<float> { 0.1f, 0.2f, 0.5f, 1.0f, 2.0f, 5.0f, 10.0f };
    private int ventricleSensitivityIndex = 6;
    private float ventricleSensitivityAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentVentricleValue = RandomGaussian(minimumVentricleValue, maximumVentricleValue);
        initialVentricleValue = currentVentricleValue;
        targetVentricleValue = initialVentricleValue * RandomGaussian(1.2f, 1.8f);

        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Restart()
    {
        initialVentricleValue = RandomGaussian(30.0f, 180.0f);
        targetVentricleValue = initialVentricleValue * RandomGaussian(1.2f, 1.5f);

        ventricleSensitivityIndex = 6;
        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];

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
        GetComponent<HudDisplay>().SetActiveTask("ventrical");

        if (currentVentricleValue == targetVentricleValue)
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


    public float GetCurrentVentricleValue()
    {
        return currentVentricleValue;
    }
    public void SetCurrentVentricleValue(float newValue)
    {
        currentVentricleValue = newValue;
        textCurrent.text = newValue.ToString();
    }

    public void IncreaseCurrentVentricleValue()
    {
        Debug.Log("Increasing Current Ventricle Value");
        currentVentricleValue += ventricleSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckCancelBreathing();
    }

    public void DecreaseCurrentVentricleValue()
    {
        Debug.Log("Decreasing Current Ventricle Value");
        currentVentricleValue -= ventricleSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckCancelBreathing();
    }

    public float GetTargetVentricleValue()
    {
        return targetVentricleValue;
    }

    public void SetTargetVentricleValue(float newValue)
    {
        targetVentricleValue = newValue;
        textTarget.text = newValue.ToString();
    }

    public int GetVentricleSensitivityIndex()
    {
        return ventricleSensitivityIndex;
    }

    public void SetVentricleSensitivityIndex(int newValue)
    {
        ventricleSensitivityIndex = newValue;
        setVentricleSensitivityAmount(newValue);
    }

    public float GetVentricleSensitivityAmount()
    {
        return ventricleSensitivityAmount;
    }

    public void setVentricleSensitivityAmount(int newIndex)
    {
        ventricleSensitivityAmount = ventricleSensitivityArray[newIndex];
    }

    public void RaiseVentricleStepAmount()
    {

        if (ventricleSensitivityIndex < ventricleSensitivityArray.Count -1)
        {
            ventricleSensitivityIndex += 1;
        }

        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];
    }

    public void LowerVentricleStepAmount()
    {
        if (ventricleSensitivityIndex > 0)
        {
            ventricleSensitivityIndex -= 1;
        }

        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];
    }

    public float GetMinimumVentricleValue()
    {
        return minimumVentricleValue;
    }

    public float GetMaximumVentricleValue()
    {
        return maximumVentricleValue;
    }

    public float GetInitialVentricleValue()
    {
        return initialVentricleValue;
    }
}
