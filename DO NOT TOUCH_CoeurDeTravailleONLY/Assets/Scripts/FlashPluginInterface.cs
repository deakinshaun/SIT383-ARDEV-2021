using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashPluginInterface : MonoBehaviour
{
    // UI Text that is used to display debugging messages on phone
    public Text debugText;
    // UI Image that is used to display the web cam texture
    public Image camImage;
    public Canvas canvas;

    // package name of created plugin
    private const string pluginName = "com.appliedalgorithms.unity.MyPlugin";

    // created plugin class
    private static AndroidJavaClass _pluginClass;
    private static AndroidJavaObject _pluginInstance;
    AndroidJavaObject _context;
    AndroidJavaObject _activity;

    // used for debugging. ensures there is communication between the plugin and this script.
    //private float elapsedTime = 0;
    // boolean which indicates if the camera flash is on or not. false by default.
    private bool flashOn = false;
    // the id of the back facing camera
    //private string camName = "";
    private int camIndex = -1;
    // the webcam texture of the back camera
    private WebCamTexture camTex;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            SetContext();
            PluginInstance.Call("OnStart", _context, _activity, (int)canvas.pixelRect.width, (int)canvas.pixelRect.height);

        }
        GetBackFacingCameraImage();
        PutCamOnUIImage();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && Application.platform == RuntimePlatform.Android)
        {
            //PluginInstance.Call("OnPause");
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && Application.platform == RuntimePlatform.Android)
        {
            //PluginInstance.Call("OnResume", (int)camImage.flexibleWidth, (int)camImage.flexibleHeight);
        }
    }

    private void OnApplicationQuit()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //PluginInstance.Call("OnQuit");
        }
    }

    /// <summary>
    /// Retrieves the plugin class that coresponds to the package name.
    /// </summary>
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if(_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    /// <summary>
    /// Gets an instance of the plugin class corresponding to the package name.
    /// </summary>
    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null) 
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    /// <summary>
    /// Used for debugging. If deployed on android, it will get the time since the 
    /// plugin instance was initiated. Ensures continued communication is acived.
    /// </summary>
    /// <returns></returns>
    /*private double GetElapsedTime()
    {
        if (Application.platform == RuntimePlatform.Android) 
        {
            return PluginInstance.Call<double>("getElapsedTime");
        }
        Debug.LogWarning("Wrong Platform");
        return 0;
    }*/

    /// <summary>
    /// Only works when on an android device.
    /// Finds the default unity android java class and use it to get the 
    /// application context from the current activity.
    /// </summary>
    /// <returns>Returns the Context as an Android Java Object</returns>
    private void SetContext() 
    {
        // only do for android
        if (Application.platform == RuntimePlatform.Android) 
        {
            // get default unity player android java class
            var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
            // get activity object from the unity java class
            _activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            // get context from unity activity
            _context = _activity.Call<AndroidJavaObject>("getApplicationContext");
            return;
        }
        _context = null;
        _activity = null;
    }

    /// <summary>
    /// When called, will change the state of the flash from on to
    /// off and vice versa. Will only work if the plugin instance
    /// is set.
    /// </summary>
    public void ToggleFlash()
    {
        // chacks if plugin instance is not null
        if(PluginInstance != null) 
        {
            // check if the flash should be on
            if (flashOn) 
            {
                // flash is on so turn it off
                PluginInstance.CallStatic("FlashOff", camIndex.ToString());
                debugText.text = "\nFlash should now be off";
            }
            else
            {
                // flash is off so turn it on
                PluginInstance.CallStatic("FlashOn", camIndex.ToString());
                debugText.text = "\nFlash should now be on";
            }
            // make the flash On bool the opposite of its current value.
            flashOn = !flashOn;
        }
        else
        {
            // notify user that the plugin instance cannot be found
            debugText.text += "\nPlugin Instance is null";
        }
        
    }

    /// <summary>
    /// Goes through the available webcam devices and find the first 
    /// one that is not front facing. Saves the index of the camera
    /// as the cam name string to be used elsewhere.
    /// </summary>
    /// <returns>Returns True if a non-front facing camera.</returns>
    private bool GetBackFacingCameraImage()
    {
        // only do stuff if there is a camera
        if (WebCamTexture.devices.Length == 0)
            return false;
        // go through each device to find the back facing one
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            if (!WebCamTexture.devices[i].isFrontFacing)
            {
                // save the camera index as a string
                camIndex = i;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// If a back facing camera was found, then apply the image as the 
    /// material texture of the UI image if it is set.
    /// </summary>
    private void PutCamOnUIImage() 
    {
        // make sure cam image is set
        if (camImage == null)
            return;

        // chacks if plugin instance is not null
        if (PluginInstance != null)
        {
            // set up the camera
            PluginInstance.Call("SetupCamera", (int)camImage.flexibleWidth, 
                (int)camImage.flexibleHeight);
            debugText.text = "\nCamera should be set up";
        }
        /*
        if (camIndex == -1)
        {
            if (!GetBackFacingCameraImage())
            {
                debugText.text = "Back camera cannot be found";
                return;
            }
        }
        camTex = new WebCamTexture(WebCamTexture.devices[camIndex].name);

        if (camImage != null)
            camImage.material.mainTexture = camTex;*/
    }

    /// <summary>
    /// If the WebCam Texture is playing, stop it. If it is stopped, make it play.
    /// </summary>
    public void ToggleCamTexture()
    {
        // check if the cam tex is playing
        if (camTex.isPlaying)
        {
            // cam tex is playing so it needs to be stopped
            camTex.Stop();
        }
        else
        {
            // camera is stopped so it needs to be stopped
            camTex.Play();
        }
            
    }


}