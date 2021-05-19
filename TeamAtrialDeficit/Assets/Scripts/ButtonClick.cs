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
    public float buttonPressCD = -1;
    public GameObject SoundManager;
    private bool ButtonPressedDown = false;

    void onRecognized()
    {
        if (Vector3.Distance(GestureDetector.GetComponent<GestureDetector>().skeleton.transform.position, this.gameObject.transform.position) < buttonPressingDistance
            && buttonPressCD <= 0 
            && GestureDetector.GetComponent<GestureDetector>().currentGesture.name == "Button push Gesture L") //position less than 0.3 from button, buttonPressCD = 0 AND currentGesture.name = button push
        {
                buttonPressed();
                buttonPressCD = 1.0f;
        }
    }

    private void Update()
    {
        //Debug to make sure it works:
        if (Input.GetKeyDown(KeyCode.O) && Direction == "Up" && buttonPressCD <= 0)
        {
            buttonPressed();
            buttonPressCD = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.P) && Direction == "Down" && buttonPressCD <= 0)
        {
            buttonPressed();
            buttonPressCD = 1.0f;
        }
        
        if (buttonPressCD >= -1)
            buttonPressCD -= Time.deltaTime;

        if (buttonPressCD <= 0 && ButtonPressedDown)
        {
            ButtonPressedDown = false;
            this.transform.position += this.transform.up * 0.01f;
        }
    }

    void buttonPressed()
    {
        ButtonPressedDown = true;
        this.transform.position += this.transform.up * -0.01f;
        if (Direction == "Up" || Direction == "up")
            SoundManager.GetComponent<SoundManager>().button1SoundPlay();
        else
            SoundManager.GetComponent<SoundManager>().button2SoundPlay();

        Monitor1.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor2.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor3.GetComponent<MonitorScript>().buttonPress(Direction);
    }
}
