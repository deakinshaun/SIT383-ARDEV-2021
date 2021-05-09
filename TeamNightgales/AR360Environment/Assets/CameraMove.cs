using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            transform.rotation = Quaternion.Euler(90, 0, 90) * Input.gyro.attitude * Quaternion.Euler(180, 180, 0);
        }
    }
}
