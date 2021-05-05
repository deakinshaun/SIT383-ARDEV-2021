using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroControl : MonoBehaviour
{
    public Text RotationMessage;
    private float maxRotation;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        maxRotation = 90.0f;
        //Vibrator.Vibrate();
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion deviceRotation = DeviceRotation.Get();
        Quaternion deviceRotation = Rotation();

        transform.rotation = deviceRotation;

        RotationMessage.text = (deviceRotation.eulerAngles.z.ToString());

        if (deviceRotation.eulerAngles.z > maxRotation)
        {
            Vibrator.Vibrate();
        }

    }

    public Quaternion Rotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
