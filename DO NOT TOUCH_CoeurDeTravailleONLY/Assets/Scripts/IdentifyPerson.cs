using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyPerson : MonoBehaviour
{
    // boolean to check if there is a camera on the device
    private bool IsCameraAvailable;

    // WebCamTexture for the camera on the back of the device
    private WebCamTexture backFacingCamera;
    // Texture to set the default background of the scene
    private Texture defBackground;

    // Will serve as the background of the experience.
    // Allows the camera to be present on the whole device screen.
    public RawImage background;
    // Allows for the aspect ratio to be changed and how it looks when it is resized to fit the screen.
    public AspectRatioFitter ScreenFit;

    // Start is called before the first frame update
    private void Start()
    {
        // Sets the default background to the raw image background.
        defBackground = background.texture;
        // Looking for cameras on the device that is running the application.
        WebCamDevice[] devices = WebCamTexture.devices;

        // If no cameras are on the device sets the boolean to false and doesn't set the background to a camera view.
        if (devices.Length == 0)
        {
            Debug.Log("This device has no camera.");
            IsCameraAvailable = false;
            return;
        }

        // checks if the camera is not front facing and places the back facing camera as the new webcam texture.
        for (int i = 0; i < devices.Length; i++)
        {
            if(!devices[i].isFrontFacing)
            {
                backFacingCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        // Null check for if the back facing camera is null/zero. 
        // Receives a debug message stating the devices back facing camera is not detected.
        if (backFacingCamera == null)
        {
            Debug.Log("This device doesn't have a back facing camera.");
            return;
        }

        // Sets the back facing camera to play.
        backFacingCamera.Play();
        // Sets the raw image to the back facing camera.
        background.texture = backFacingCamera;

        // Sets the IsCameraAvailable to true.
        IsCameraAvailable = true;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the camera isn't available don't do anything
        if (!IsCameraAvailable)
        {
            return;
        }

        // Creates a screen ratio variable which takes in the height and width from the camera.
        float screenRatio = (float)backFacingCamera.width / (float)backFacingCamera.height;
        // Sets the aspect ratio to the screen ratio variable created above.
        ScreenFit.aspectRatio = screenRatio;

        // sets the scale for the device if it orientated in Portrait.
        float scaleY = backFacingCamera.videoVerticallyMirrored ? -1f : 1f;
        // Sets the Rect transform taking in the new scale for the Y value created above.
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backFacingCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
