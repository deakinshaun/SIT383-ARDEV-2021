using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VB1Scrpt : MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject VirButObject;
    public GameObject ObjectToAlter;

    // Start is called before the first frame update
    void Start()
    {
        VirButObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPressed(VirtualButtonBehaviour VB)
    {

        ObjectToAlter.SetActive(true);
        Debug.Log("BTN Pressed: Gyro Temporarily Activated");
    }

    public void OnButtonReleased(VirtualButtonBehaviour VB)
    {
        ObjectToAlter.SetActive(false);
        Debug.Log("BTN Released: Gyro De-Activated");
    }

}
