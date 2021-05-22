using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;




public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static float TimeScale = 1.0f;//控制波峰间隔时间
    public static float HeartSpeed = 1.0f;//控制心跳速度
    public static float CardiogramScale = 1.0f;//控制波峰值
    public GameObject[] Cardiogram;

    public Button[] Buttons;
    public GameObject Notice;
    public Text NoticeText;
    public GameObject Timer;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncreaseCardiogram()
    {

        if (CardiogramScale >1.6f)
        {
            return;// cannot increase while heartbreak too high
        }
        if (Mathf.Abs(CardiogramScale - 1.2f) < 0.1f)
        {
            SoundManager.instance.PlayQuick();
        }
        else if (Mathf.Abs(CardiogramScale - 0.8f) < 0.1f)
        {
            SoundManager.instance.PlayNormal();
        }
        if (Mathf.Abs(CardiogramScale - 1.4f) < 0.1f)
        {
            Notice.SetActive(true);
            StartCoroutine(CloseNotice());
            NoticeText.text = "Heartbreak Too High!";
        }
        CardiogramScale += 0.1f;
        HeartBreak.instance.speed += 0.3f;
        Cardiogram[1].transform.Find("Notice").GetComponent<PulseDataNumberRenderer>().multiplier += 0.04f; ;
    }
    public void DecreaseCardiogram()
    {
        if (CardiogramScale < 0.6f)
        {
            return;// cannot decrease while heartbreak too low
        }
        if (Mathf.Abs(CardiogramScale - 1.2f) < 0.1f)
        {
            SoundManager.instance.PlayNormal();
        }
        else if (Mathf.Abs(CardiogramScale - 0.8f) < 0.1f)
        {
            SoundManager.instance.PlayEmergency();
        }
        
        CardiogramScale -= 0.1f;
        HeartBreak.instance.speed -= 0.3f;
        Cardiogram[1].transform.Find("Notice").GetComponent<PulseDataNumberRenderer>().multiplier -= 0.04f; ;
    }
    /// <summary>
    /// Open Normal Panel.
    /// </summary>
    public void ShowNormal()
    {
        Buttons[1].gameObject.SetActive(false);// hide emergency button
        Buttons[2].gameObject.SetActive(false);// hide about us button
        Buttons[3].gameObject.SetActive(true);// show back button
        HeartBreak.instance.Pinpong(3.5f);
        Cardiogram[0].SetActive(true);
        Cardiogram[1].SetActive(false);
        SoundManager.instance.PlayNormal();
    }
    /// <summary>
    /// Open Emergency Panel.
    /// </summary>
    public void ShowEmergency()
    {
        Buttons[0].gameObject.SetActive(false);//hide normal button
        Buttons[2].gameObject.SetActive(false);//hide about us button
        Buttons[3].gameObject.SetActive(true);//show back button
        Buttons[4].gameObject.SetActive(true);//show increase button
        Buttons[5].gameObject.SetActive(true);//show decrease button
        Buttons[6].gameObject.SetActive(true);//show done button
        CardiogramScale = 0.5f; // scale the cardiogram

        HeartBreak.instance.Pinpong(2.0f);
        Cardiogram[0].SetActive(false);
        Cardiogram[1].SetActive(true);
        SoundManager.instance.PlayEmergency();

        Timer.SetActive(true);
        Timer.GetComponent<TimeCounter>().StartCount();
        Cardiogram[1].transform.Find("Notice").GetComponent<PulseDataNumberRenderer>().multiplier = 0.8f;

    }

    /// <summary>
    /// Reset all buttons.
    /// </summary>
    public void ResetButtons()
    {
        SoundManager.instance.Stop();
        HeartBreak.instance.StopPingpong();
        Cardiogram[0].SetActive(false);
        Cardiogram[1].SetActive(false);
        Timer.SetActive(false);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].gameObject.SetActive(false);
        }
        Buttons[0].gameObject.SetActive(true);
        Buttons[1].gameObject.SetActive(true);
        Buttons[2].gameObject.SetActive(true);
    }
    public void CheckValue()
    {
        if (CardiogramScale <= 0.9f)
        {
            Notice.SetActive(true);
            NoticeText.text = "Warning! Heartbreak Too Low!";
        }
        else if (CardiogramScale >= 1.1f)
        {
            Notice.SetActive(true);
            NoticeText.text = "Warning! Heartbreak Too High!";
        }
        else
        {
            Notice.SetActive(true);
            NoticeText.text = "Congratulations!";
            Invoke("ResetButtons", 2.0f);// back to main menu
        }
        StartCoroutine(CloseNotice());
    }

    public void ShowUpNotice(string message)
    {
        Notice.SetActive(true);
        NoticeText.text = message;
        StartCoroutine(CloseNotice());

    }
    IEnumerator CloseNotice()
    {
        yield return new WaitForSeconds(2.0f);
        Notice.SetActive(false);
    }




}

