using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class atrialDetails : MonoBehaviour
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
        //current = NEED CODE TO PRODUCE WAVEFORM
        //target = INVERSE OF WAVEFORM TO CANCEL NOISE

        sensitivityArray = new List<float> { 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.2f, 0.1f, 0.05f, 0.02f, 0.01f };
        sensitivityIndex = 0;
        sensitivityAmount = sensitivityArray[sensitivityIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateCVTText()
    {
        GetComponent<hudDisplay>().setActiveTask("atrial");

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

