using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDetails : MonoBehaviour
{
    [Tooltip ("Minimum Heartrate allowed, zero means stopped")]
    public float minimumHeartrate = 0.0f;
    [Tooltip("Maximum Heartrate allowed, 180 is a very fit athlete")]
    public float maximumHeartrate = 180.0f;
    [Tooltip("Connect this to the newECGDisplay component in the Canvas")]
    public GameObject ECG_Reference;

    [Tooltip("Connect this to Canvas element valueCurrentValue")]
    public Text textCurrentHeartrate;
    [Tooltip("Connect this to Canvas element valueTargetValue")]
    public Text textTargetHeartrate;

    private float currentHeartrate;
    private float targetHeartrate;
    private List<float> heartSensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
    private int heartSensitivityIndex = 0;
    private float heartSensitivityAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentHeartrate = RandomGaussian(30.0f, 180.0f);
        targetHeartrate = RandomGaussian(60.0f, 90.0f);

        heartSensitivityAmount = heartSensitivityArray[heartSensitivityIndex];

        //Force click Heartrate Actvity to get us started
        GetComponent<ActivitySelector>().OnClickHeartrate();
    }

    public void Restart()
    {
        currentHeartrate = RandomGaussian(30.0f, 180.0f);
        targetHeartrate = RandomGaussian(60.0f, 90.0f);

        heartSensitivityAmount = heartSensitivityArray[heartSensitivityIndex];

        //Force click Heartrate Actvity to get us started
        GetComponent<ActivitySelector>().OnClickHeartrate();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCurrentValueText()
    {
        GetComponent<HudDisplay>().SetActiveTask("heartrate");

        if (currentHeartrate == targetHeartrate)
        {
            textCurrentHeartrate.color = new Color32(0, 255, 0, 255);
            textTargetHeartrate.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            textCurrentHeartrate.color = new Color32(255, 255, 0, 255);
            textTargetHeartrate.color = new Color32(255, 255, 0, 255);
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

    public float GetCurrentHeartrate()
    {
        return currentHeartrate;
    }
    public void SetCurrentHeartrate(float newValue)
    {
        currentHeartrate = newValue;
        UpdateCurrentValueText();
    }

    public void IncreaseCurrentHeartrate()
    {
        currentHeartrate += heartSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckForHeartSync();
    }

    public void DecreaseCurrentHeartrate()
    {
        currentHeartrate -= heartSensitivityAmount;
        ECG_Reference.GetComponent<EcgVisualiser>().CheckForHeartSync();
    }

    public float GetTargetHeartrate()
    {
        return targetHeartrate;
    }

    public void SetTargetHeartrate(float newValue)
    {
        targetHeartrate = newValue;
        UpdateCurrentValueText();
    }

    public int GetHeartSensitivityIndex()
    {
        return heartSensitivityIndex;
    }

    public void SetHeartSensitivityIndex(int newValue)
    {
        heartSensitivityIndex = newValue;
        SetHeartSensitivityAmount(newValue);
    }

    public float GetHeartSensitivityAmount()
    {
        return heartSensitivityAmount;
    }

    public void SetHeartSensitivityAmount(int newIndex)
    {
        heartSensitivityAmount = heartSensitivityArray[newIndex];
    }

    public void RaiseHeartSensitivityAmount()
    {
        heartSensitivityIndex += 1;
        if (heartSensitivityIndex > 9)
        {
            heartSensitivityIndex = 9;
        }

        heartSensitivityAmount = heartSensitivityArray[heartSensitivityIndex];
    }

    public void LowerHeartSensitivityAmount()
    {
        heartSensitivityIndex -= 1;
        if (heartSensitivityIndex < 9)
        {
            heartSensitivityIndex = 0;
        }

        heartSensitivityAmount = heartSensitivityArray[heartSensitivityIndex];
    }

    public float GetMinimumHeartrate()
    {
        return minimumHeartrate;
    }

    public float GetMaximumHeartrate()
    {
        return maximumHeartrate;
    }



}
