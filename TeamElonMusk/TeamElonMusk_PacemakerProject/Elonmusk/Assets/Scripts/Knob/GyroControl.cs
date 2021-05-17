using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroControl : MonoBehaviour
{
    public Text RotationMessage;
    public static float rotationOutput;
    //private float maxRotation;
    //private float firstClick1;
    // private float firstClick2;


    void Start()
    {
        transform.rotation = Quaternion.identity;
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        //firstClick1 = 60.0f;
        //firstClick2 = 70.0f;
        //maxRotation = 300.0f;
    }

    void Update()
    {
        //Quaternion deviceRotation = DeviceRotation.Get();
        Quaternion originalRotation = Quaternion.identity;
        Quaternion deviceRotation = Rotation();
        Quaternion dispensingXY = Quaternion.Inverse(Quaternion.FromToRotation(originalRotation * Vector3.forward, deviceRotation * Vector3.forward));
        Quaternion rotationz = dispensingXY * deviceRotation;

        transform.rotation = rotationz;

        rotationOutput = deviceRotation.eulerAngles.z;

        RotationMessage.text = (deviceRotation.eulerAngles.z.ToString());


        #region gyroVibration
        //int buzz = (int)(3.0f * deviceRotation.eulerAngles.z);
        if (deviceRotation.eulerAngles.z < 50.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (50.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 60.0f)
        {
            Vibration.Cancel();
        }
        else if (60.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 110.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (110.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 120.0f)
        {
            Vibration.Cancel();
        }
        else if (120.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 170.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (170.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 180.0f)
        {
            Vibration.Cancel();
        }
        else if (180.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 230.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (230.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 240.0f)
        {
            Vibration.Cancel();
        }
        else if (240.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 290.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (290.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 300.0f)
        {
            Vibration.Cancel();
        }
        else if (300.0f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 350.0f)
        {
            Vibration2.Vibrate(50, 80);
        }
        else if (350f < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < 360.0f)
        {
            Vibration.Cancel();
        }
        #endregion
        while (deviceRotation.eulerAngles.z == 10.0f)
        {
            AudioSource Turn = GameObject.Find("Turn").GetComponent<AudioSource>();
            Turn.Play();
        }
    }

    public Quaternion Rotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
