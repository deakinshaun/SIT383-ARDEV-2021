using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroControl : MonoBehaviour
{
    public Text RotationMessage;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion deviceRotation = DeviceRotation.Get();
        Quaternion deviceRotation = Rotation();

        transform.rotation = deviceRotation;

        RotationMessage.text = (deviceRotation.eulerAngles.z.ToString());

    }

    public Quaternion Rotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
