using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    public Material camTexMaterial;
    public Text outputText;
    private int currentCamera = 0;
    private WebCamTexture webcamTexture;

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
        currentCamera = (currentCamera + 1) %WebCamTexture.devices.Length;

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
