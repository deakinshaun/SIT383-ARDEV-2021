using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADGyroControl : MonoBehaviour
{

    private Gyroscope MobileGyro;
    private bool IsGyroSupported;
    public NetworkScript NetConnector;
    public bool GyroActive;
    public GameObject PointerSet;

    // Start is called before the first frame update
    void Start()
    {
        IsGyroSupported = SystemInfo.supportsGyroscope;
        if (IsGyroSupported && GyroActive)
        {
            MobileGyro = Input.gyro;
            MobileGyro.enabled = true;
            PointerSet.SetActive(true);
            Debug.LogWarning("GYROARM ACTIVE");
        }
        else
        {
            PointerSet.SetActive(false);
            Debug.LogWarning("GYRO ARM INACTIVE");
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

    public void SwitchActiveStatePoinetrSet()
    {
        GyroActive = !GyroActive;
    }
}
