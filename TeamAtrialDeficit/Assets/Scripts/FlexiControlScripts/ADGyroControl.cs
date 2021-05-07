using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADGyroControl : MonoBehaviour
{

    private Gyroscope MobileGyro;
    private bool IsGyroSupported;
    public NetworkScript NetConnector;

    // Start is called before the first frame update
    void Start()
    {
        IsGyroSupported = SystemInfo.supportsGyroscope;
        if (IsGyroSupported)
        {
            MobileGyro = Input.gyro;
            MobileGyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGyroSupported)
        {
            this.transform.rotation = Quaternion.Euler(90, 0, 90) * MobileGyro.attitude * Quaternion.Euler(180, 180, 0);

            NetConnector.SendControllerState(this.transform.rotation, true, false);
        }
    }
}
