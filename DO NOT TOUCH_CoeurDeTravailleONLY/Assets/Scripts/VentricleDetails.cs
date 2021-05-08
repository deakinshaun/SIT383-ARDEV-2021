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

    [Tooltip("Sets a minimum value when generating a random number for Atrial Noise")]
    public float min = 0;
    [Tooltip("Sets a maximum value when generating a random number for Atrial Noise")]
    public float max = 180;

    private float current, target;
    private List<float> sensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
    private int sensitivityIndex = 0;
    private float sensitivityAmount;

    public Text textCurrent;
    public Text textTarget;

    // Start is called before the first frame update
    void Start()
    {
        //current = NEED CODE TO PRODUCE WAVEFORM
        //target = INVERSE OF WAVEFORM TO CANCEL NOISE

        sensitivityAmount = sensitivityArray[sensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateCVTText()
    {
        GetComponent<HudDisplay>().setActiveTask("ventrical");

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


    public float getCurrent()
    {
        return current;
    }
    public void setCurrent(float newValue)
    {
        current = newValue;
        textCurrent.text = newValue.ToString();
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
        textTarget.text = newValue.ToString();
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
        Debug.Log("Raising Ventrical Sensitivity");
        sensitivityIndex += 1;
        if (sensitivityIndex > 9)
        {
            sensitivityIndex = 9;
        }

        sensitivityAmount = sensitivityArray[sensitivityIndex];
    }

    public void lowerSensitivity()
    {
        Debug.Log("Lowering Ventrical Sensitivity");
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
