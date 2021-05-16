using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public GameObject handLeft;
    public GameObject handRight;
    public GameObject GestureDetector;
    public float buttonPressingDistance = 0.3f;
    public GameObject Monitor1;
    public GameObject Monitor2;
    public GameObject Monitor3;
    public string Direction; //up or down
    private float buttonPressCD = 0;

    void onRecognized()
    {
        if (Vector3.Distance(GestureDetector.GetComponent<GestureDetector>().skeleton.transform.position, this.gameObject.transform.position) < buttonPressingDistance
            && buttonPressCD == 0 
            && GestureDetector.GetComponent<GestureDetector>().currentGesture.name == "Button push Gesture L") //position less than 0.3 from button, buttonPressCD = 0 AND currentGesture.name = button push
        {
                buttonPressed();
                buttonPressCD = 1.0f;
        }

        if (buttonPressCD > 0)
            buttonPressCD -= Time.deltaTime;
    }

    void buttonPressed()
    {
        Monitor1.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor2.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor3.GetComponent<MonitorScript>().buttonPress(Direction);
    }
}
