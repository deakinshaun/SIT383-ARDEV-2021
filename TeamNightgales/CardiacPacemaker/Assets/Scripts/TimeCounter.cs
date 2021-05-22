using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{

    private float timer = 0.0f;
    public Text WarningText;
    public int TotalSeconds;
    public bool running;

    public static TimeCounter instance;
    // Start is called before the first frame update
    void Start()
    {
       // running = false;
        //instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
            WarningText.text = "Time Left: " + Mathf.CeilToInt(TotalSeconds - timer);
            if (timer > TotalSeconds)
            {
                running = false;
                GameController.instance.ShowUpNotice("GAME OVER!");
                Invoke("GameOver",2.0f);
            }
        }
    }
    public void GameOver()
    {
        GameController.instance.ResetButtons();
    }
    public void StartCount()
    {
        timer = 0f;
        running = true;
    }
}
