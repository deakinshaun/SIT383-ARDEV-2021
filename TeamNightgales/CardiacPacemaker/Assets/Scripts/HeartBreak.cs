using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HeartBreak : MonoBehaviour
{
    public float speed = 3.5f;
    public GameObject Heart;
    private float scaleValue = 2.0f;
    public bool running = true;
    private Vector3 initScale;
    public static HeartBreak instance;
    public float HeartSpeed = 1.0f;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        running = false;
        initScale = Heart.transform.localScale;
        scaleValue = 2 * initScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
           
            Pinpong();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            running = false;
        }
    }
    public void StopPingpong()
    {
        running = false;
    }
    public void Pinpong(float v=3.5f)
    {
        running = true;
        speed = v;
        Heart.transform.DOScale(scaleValue, 2.0f/speed);
        StartCoroutine(PingBack());
    }
    IEnumerator PingBack()
    {
        yield return new WaitForSeconds(2.0f/speed);
        Heart.transform.DOScale(initScale.x,2.0f/speed);
        yield return new WaitForSeconds(2.0f/speed);
        if (running)
        {
            Pinpong(speed);
        }
    }
}
