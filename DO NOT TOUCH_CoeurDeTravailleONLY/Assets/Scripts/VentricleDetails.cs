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

    [Tooltip("Sets a minimum value when generating a random number for Ventricle Noise")]
    public float minimumVentricleValue = 0.0f;
    [Tooltip("Sets a maximum value when generating a random number for Ventricle Noise")]
    public float maximumVentricleValue = 180.0f;

    [Tooltip("Connect this to Canvas element valueCurrentValue")]
    public Text textCurrent;
    [Tooltip("Connect this to Canvas element valueTargetValue")]
    public Text textTarget;

    private float currentVentricleValue;
    private float targetVentricleValue;
    private List<float> ventricleSensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
    private int ventricleSensitivityIndex = 0;
    private float ventricleSensitivityAmount;

    // Start is called before the first frame update
    void Start()
    {
        //current = NEED CODE TO PRODUCE WAVEFORM
        //target = INVERSE OF WAVEFORM TO CANCEL NOISE

        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
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


    public float GetCurrentVentricalValue()
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
        currentVentricleValue += ventricleSensitivityAmount;
    }

    public void DecreaseCurrentVentricleValue()
    {
        currentVentricleValue -= ventricleSensitivityAmount;
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

    public void RaiseVentricleSensitivityAmount()
    {
        Debug.Log("Raising Ventrical Sensitivity");
        ventricleSensitivityIndex += 1;
        if (ventricleSensitivityIndex > 9)
        {
            ventricleSensitivityIndex = 9;
        }

        ventricleSensitivityAmount = ventricleSensitivityArray[ventricleSensitivityIndex];
    }

    public void LowerVentricleSensitivityAmount()
    {
        Debug.Log("Lowering Ventrical Sensitivity");
        ventricleSensitivityIndex -= 1;
        if (ventricleSensitivityIndex < 9)
        {
            ventricleSensitivityIndex = 0;
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

}
