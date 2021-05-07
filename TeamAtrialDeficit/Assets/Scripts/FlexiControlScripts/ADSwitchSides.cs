using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSwitchSides : MonoBehaviour
{
    public GameObject DesktopVerSD;
    public GameObject MobileVerSD;

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            DesktopVerSD.SetActive(false);
            MobileVerSD.SetActive(true);
        }
        else
        {
            DesktopVerSD.SetActive(true);
            MobileVerSD.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
