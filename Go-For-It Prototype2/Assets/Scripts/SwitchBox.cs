using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBox : MonoBehaviour
{
    public Transform boy;
    public GameObject[] UpAndDownCanvas = new GameObject[2];
    public GameObject[] RObj = new GameObject[3];
    public Text[] RContext = new Text[3];
    public float Rspeed = 1f;
    public Text[] DownContext = new Text[7];
    public Transform selectUI;


    int selectIndex = 0;
    float[] data = new float[3] { 135, 30, 30 };
    bool isLock = false;
    bool isSwitch = true;
    Color startColor;

    bool isV = false;

    private void Start()
    {
        Switch();
        startColor = DownContext[selectIndex].color;
        Chose();
    }

    public void RotateRight(int i)
    {
        if (!isSwitch || isLock)
        {
            return;
        }

        if (data[i] >= 360f)
        {
            return;
        }
        Debug.Log("StartRight");
        RObj[i].transform.Rotate(Vector3.up * Rspeed);
        data[i] += Rspeed;
        RefreshContext(i);
        if (data[i] > 5f)
        {
            boy.GetComponent<Animator>().SetBool("Start",true);
            if (!isV)
            {
                Handheld.Vibrate();
                isV = true;
            }
        }

    }

    public void RotateLeft(int i)
    {
        if (!isSwitch || isLock)
        {
            return;
        }

        if (data[i] <= 0f)
        {
            return;
        }
        Debug.Log("StartLeft");
        RObj[i].transform.Rotate(-Vector3.up * Rspeed);
        data[i] -= Rspeed;
        RefreshContext(i);
        if (data[i] <= 5f)
        {
            boy.GetComponent<Animator>().SetBool("Start", false);
        }
    }

    private void RefreshContext(int i)
    {
        switch (i)
        {
            case 0: RContext[i].text = "Rate(" + (int)(data[i] % 360f * 200f / 360f + (int)(data[i] / 360f) * 200) + ")"; break;
            case 1: RContext[i].text = "A\nOutput(" + (int)(data[i] % 360f * 200f / 360f + (int)(data[i] / 360f) * 200) + ")"; break;
            case 2: RContext[i].text = "V\nOutput(" + (int)(data[i] % 360f * 200f / 360f + (int)(data[i] / 360f) * 200) + ")"; break;
            default:
                break;
        }
    }

    public void Lock()
    {
        if (!isSwitch)
        {
            return;
        }
        isLock = !isLock;
        Debug.Log("isLock:" + isLock);
    }

    private void Chose()
    {
        foreach (var item in DownContext)
        {
            item.color = startColor;
        }
        selectUI.position = DownContext[selectIndex].transform.position;
        DownContext[selectIndex].color = Color.white;
    }

    public void Select(int i)
    {
        if (!isSwitch)
        {
            return;
        }
        selectIndex += i;
        selectIndex = Mathf.Clamp(selectIndex, 0, DownContext.Length - 1);
        Chose();
    }

    public void Switch()
    {
        isSwitch = !isSwitch;
        foreach (var item in UpAndDownCanvas)
        {
            item.SetActive(isSwitch);
        }
        if (!isSwitch)
        {
            boy.GetComponent<Animator>().SetBool("Start", false);
        }
    }

    
}
