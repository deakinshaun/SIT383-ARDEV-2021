using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroControl : MonoBehaviour
{
    public Text RotationMessage;
    private float maxRotation;
    private float minRotation;


    void Start()
    {
        transform.rotation = Quaternion.identity;
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        minRotation = 60.0f;
        maxRotation = 300.0f;
    }

    void Update()
    {
        //Quaternion deviceRotation = DeviceRotation.Get();
        Quaternion originalRotation = Quaternion.identity;
        Quaternion deviceRotation = Rotation();
        Quaternion dispensingXY = Quaternion.Inverse(Quaternion.FromToRotation(originalRotation * Vector3.forward, deviceRotation * Vector3.forward));
        Quaternion rotationz = dispensingXY * deviceRotation;

        transform.rotation = rotationz;

        RotationMessage.text = (deviceRotation.eulerAngles.z.ToString());

        if (minRotation < deviceRotation.eulerAngles.z && deviceRotation.eulerAngles.z < maxRotation)
        {
            Vibration.Vibrate(250);
        }

    }

    public Quaternion Rotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
