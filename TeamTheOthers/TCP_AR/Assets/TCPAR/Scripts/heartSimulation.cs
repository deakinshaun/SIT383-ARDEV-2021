using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using TMPro;

public class heartSimulation : MonoBehaviour
{
    private int bpm = -1;
    private bool pulseOn = false;
    private bool alive = true;
    private double newSense;
    private Timer senseTimer;
    public TMP_Text t;
    public int targetBpm, aOutput;
    public double bOutput;

    void Start()
    {
        senseTimer = new System.Timers.Timer();
    }

    void Update()
    {
        HeartRateVal(targetBpm, aOutput, bOutput);
    }

    public void HeartRateVal(int targetBpm, int output, double sense)
    {
        int difficulty, beat, milSeconds;
        if (alive == true)
        {
            if (bpm == -1)
            {
                difficulty = Random.Range(1, 4);
                if (difficulty == 1)
                {
                    bpm = 40 + Random.Range(0, 9);
                }
                if (difficulty == 2)
                {
                    bpm = 60 + Random.Range(0, 9);
                }
                if (difficulty == 3)
                {
                    bpm = 80 + Random.Range(0, 9);
                }
                if (difficulty == 4)
                {
                    bpm = 120 + Random.Range(0, 9);
                }
                newSense = sense;
                milSeconds = (int)sense * 100;
            }
            else
            {
                if(newSense != sense)
                {
                    milSeconds = (int)sense * 100;
                    senseTimer.Enabled = true;
                }

                if (pulseOn == true)
                {
                    if (bpm < targetBpm)
                    {
                        bpm = bpm - output;
                    }
                    else
                    {
                        bpm = bpm + output;
                    }
                    pulseOn = false;
                }

                beat = Random.Range(1, 35);
                if (beat == 1)
                {
                    bpm = bpm + Random.Range(-1, 2);
                }
            }

            if (bpm <= 0 || bpm >= 180)
            {
                Debug.Log("Dead");
                bpm = 0;
                alive = false;
            }
        }
        t.text = bpm.ToString();
    }

    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        pulseOn = true;
        senseTimer.Enabled = true;
    }
}
