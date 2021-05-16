using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerDetection : MonoBehaviour
{
    public GameObject plane;
    public Text debugText;
    WebCamTexture webCam;
    bool checkingPulse = false;
    bool fingerOverCam = false;
    int numberOfChecks = 200;
    int currentCheck = 0;
    int[] redLevels;
    int width;
    int height;
    int red;
    int blue;
    int green;
    int BPM_L = 40;
    int BMM_H = 230;

    // Start is called before the first frame update
    void Start()
    {
        redLevels = new int[numberOfChecks];
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingPulse)
        {
            CheckWebCam();
            CheckFinger();

            if(fingerOverCam)
                CheckPulse();
            else
                redLevels = new int[numberOfChecks];
        }
    }

    void CheckWebCam()
    {
        if (webCam == null)
        {
            webCam = new WebCamTexture();
            if (plane != null)
                plane.GetComponent<MeshRenderer>().material.mainTexture = webCam;
        }
        if (!webCam.isPlaying)
            webCam.Play();

        width = webCam.width;
        height = webCam.height;
    }

    void CheckFinger()
    {
        red = 0;
        blue = 0;
        green = 0;
        Color32[] colors = webCam.GetPixels32();
        for (int i = 0; i < colors.Length; i++)
        {
            Color32 c = colors[i];
            red += (int)c.r;
            blue += (int)c.b;
            green += (int)c.g;
        }
        debugText.text = "Red: " + red + " Blue: " + blue + " Green: " + green;
        if (blue < red / 1000 && green < red / 1000)
        {
            fingerOverCam = true;
        }
        else
            fingerOverCam = false;
        
        debugText.text += "\nFinger Over Cam: " + fingerOverCam;
    }

    void CheckPulse()
    {
        redLevels[currentCheck] = red / (width * height);
        debugText.text += "\nRed bightness: " + AverageOfArray(redLevels);
        //debugText.text += "\nPeak difference: " + MaxToMinOFArray(redLevels) * Time.deltaTime;
        debugText.text += "\n";
        List<int> maxIndexes = HighPointsOfArray(redLevels);
        for (int i = 0; i < maxIndexes.Count; i++)
        {
            debugText.text += maxIndexes[i] + ", ";
        }
        currentCheck = (currentCheck + 1) % numberOfChecks;
    }

    public void ToggelCheckingPulse()
    {
        checkingPulse = !checkingPulse;
    }

    private int AverageOfArray(int[] array)
    {
        int sum = 0;
        int average = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        average = sum / array.Length;
        return average;
    }

    private int MaxOfArray(int[] array)
    {
        int max = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }
        return max;
    }

    private int MinOfArray(int[] array)
    {
        int min = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < min)
            {
                min = array[i];
            }
        }
        return min;
    }

    private int FindIndexOfValueInArray(int[] array, int value)
    {
        int index = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                index = i;
            }
        }
        return index;
    }

    private int MaxToMinOFArray(int[] array)
    {
        int max = MaxOfArray(array);
        int min = MinOfArray(array);
        int maxindex = FindIndexOfValueInArray(array, max);
        int minindex = FindIndexOfValueInArray(array, min);

        return (Mathf.Abs(maxindex - minindex));
    }

    private List<int> HighPointsOfArray(int[] array)
    {
        int max = MaxOfArray(array);
        int min = MinOfArray(array);
        int diff = max - min;
        int topTenPerc = max - (diff / 10);
        List<int> maxIndexs = new List<int>();

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > topTenPerc)
            {
                maxIndexs.Add(i);
            }
        }

        return maxIndexs;
    }
}
