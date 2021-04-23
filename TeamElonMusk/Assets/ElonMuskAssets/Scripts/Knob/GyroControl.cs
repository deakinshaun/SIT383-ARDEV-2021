using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;

    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;
        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }

    void Update()
    {
        if (gyroSupported)
        {
            this.transform.rotation = Quaternion.Euler(90, 0, 90) * gyro.attitude * Quaternion.Euler(180, 180, 0);
        }

    }
}
