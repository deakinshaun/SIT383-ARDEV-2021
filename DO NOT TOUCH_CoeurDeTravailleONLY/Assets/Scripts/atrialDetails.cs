using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtrialDetails : MonoBehaviour
{
    public float atrialMinimum, atrialMaximum;
    private float currentAtrial, targetAtrial;
    private List<float> atrialSensitivityArray;
    private int atrialSensitivityIndex;
    private float atrialSensitivityAmount;

    public Text textCurrentAtrial;
    public Text textTargetAtrial;

    // Start is called before the first frame update
    void Start()
    {
        atrialMinimum = 0;
        atrialMaximum = 180;
        //current = NEED CODE TO PRODUCE WAVEFORM
        //target = INVERSE OF WAVEFORM TO CANCEL NOISE

        atrialSensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
        atrialSensitivityIndex = 0;
        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateCVTText()
    {
        GetComponent<HudDisplay>().setActiveTask("atrial");

        if (currentAtrial == targetAtrial)
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


    public float GetCurrentAtrial()
    {
        return currentAtrial;
    }
    public void SetCurrentAtrial(float newValue)
    {
        currentAtrial = newValue;
        textCurrentAtrial.text = newValue.ToString();
    }

    public void IncreaseCurrentAtrial()
    {
        currentAtrial += atrialSensitivityAmount;
    }

    public void DecreaseCurrentAtrial()
    {
        currentAtrial -= atrialSensitivityAmount;
    }

    public float GetTargetAtrial()
    {
        return targetAtrial;
    }

    public void SetTargetAtrial(float newValue)
    {
        targetAtrial = newValue;
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


    public void IncreaseAtrialSensitivityIndex()
    {
        atrialSensitivityIndex += 1;
        if (atrialSensitivityIndex > 9)
        {
            atrialSensitivityIndex = 9;
        }

        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    public void DecreaseAtrialSensitivityIndex()
    {
        atrialSensitivityIndex -= 1;
        if (atrialSensitivityIndex < 9)
        {
            atrialSensitivityIndex = 0;
        }

        atrialSensitivityAmount = atrialSensitivityArray[atrialSensitivityIndex];
    }

    public float GetAtrialMinimum()
    {
        return atrialMinimum;
    }

    public float GetAtrialMaximum()
    {
        return atrialMaximum;
    }

}

