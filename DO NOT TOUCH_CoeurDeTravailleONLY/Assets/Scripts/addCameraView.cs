using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addCameraView : MonoBehaviour
{
    //This Component retrieves image from a selected connected camera and provides it as a Material that can be applied to a Scene Object

    /*
    * This code was initially taken from The Hitchhikers Guide to Augmented Reality by C. S. Steinbeck.
    * 
    * Additional modifications, comments and instructions by Stephen Caines to create a set of single-function
    * components for future AR Projects.
    * 
    */

    /* 
     * To use:
     * 1. Create a new material(Create/Material) and rename this as PhysicalCameraViewMaterial
     * 2. Create this (getPhysicalCameraView) Script
     * 3. Add the material (from 1) to your main object (the one you wish to display the camera texture on)
     * 4. Add this Script to the same  main object
     * 5. Select the main object to display its properties in the inspector window
     * 6. Drag the PhysicalCameraViewMaterial onto the Cam Tex Material field of the Physical Camera Texture (Script) property
     * 
     * To activate the switch camera functionality (for phones)
     * 7. On your screen Canvas add a UI/Text element and a UI/Button element
     * 8. Select this script and drag your new UI/Text element to the outputText field
     * 9. Connect the Button as follows:
     * (a) Select the ChangeCameraButton so its properties appear in the Inspector window. Press the + button under the On Click () 
     *     list to add an extra event handler when the button is clicked.
     * (b) Press the circle next to the None (Object) to select the object that contains the component with the function we require.
     * (c) Select from the “No Function” drop down list to choose the nextCamera function on the PhysicalmeraTexture component.
    */

    public Material camTexMaterial;
    public Text outputText;

    private WebCamTexture webcamTexture;
    private int currentCamera = 0;

    private void showCameras()
    {
        outputText.text = "";
        foreach (WebCamDevice d in WebCamTexture.devices)
        {
            outputText.text += d.name + (d.name == webcamTexture.deviceName ? "*" : "") + "\n";
        }
    }

    public void nextCamera()
    {
        currentCamera = (currentCamera + 1) % WebCamTexture.devices.Length;
        // Change camera only works if the camera is stopped. 
        webcamTexture.Stop();
        webcamTexture.deviceName = WebCamTexture.devices[currentCamera].name;
        webcamTexture.Play();
        showCameras();
    }


    void Start()
    {
        webcamTexture = new WebCamTexture();
        showCameras();
        camTexMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
