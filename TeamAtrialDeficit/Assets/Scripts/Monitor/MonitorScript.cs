using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MonitorScript : MonoBehaviour
{
    public int difficulty; //assigned per dimension (0 = beginner, 1 = intermediate, 2 = advanced)
    public GameObject pulseLine;
    public GameObject SoundManager;
    public GameObject spawnLocation;
    public GameObject BPMText;
    public bool Run = true; //set to true if user enters dimension.
    
    //Rahul: Added to include Mobile UI
    public GameObject PaceMakerBPMText;
    public GameObject[] pulseList = new GameObject[72]; //public for testing reasons. (to make sure it works using unity editor.)
    private float BPM = 90;
    private float timeSinceLastPulse = 0.0f;
    private int[] pulseChangeArray = new int[10]{0,-2,-4,-2,0,2,4,6,4,2}; //changes in height when a pulse happens
    private PhotonView photonView;
    private float heightOfLine = 0.05f;
    private int framePerChange = 10; //This is used for the speed of the monitor beep line. Lower it is, faster it changes/goes across the screen.
    private int frameCount = 0; // Counter for ^
    private int currentFrameSinceStart = 0; //used for totalFrames since user started this scenario.

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //if (photonView.IsMine == true)
        //{
        int counter = 0;
        foreach(Transform child in transform)
        {
            if(child.gameObject.tag == "PulseLine")
            {
                pulseList[counter] = child.gameObject;
                counter++;
            }
        }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Run)
        {
            //BPMText.GetComponent<TextMesh>().text = BPM.ToString() + " BMP\nDifficulty:" + difficulty.ToString();            //This is debug text, if you want to see the difficulty for each monitor.
            BPMText.GetComponent<TextMesh>().text = BPM.ToString() + " BMP";
            float BeatsPerSecond = 3 - (BPM / 60);

            //Rahul: Added to include Mobile UI
            if (PaceMakerBPMText.activeSelf)
            {
                PaceMakerBPMText.GetComponent<Text>().text = BPM.ToString() + " BMP";
            }

            if (timeSinceLastPulse > BeatsPerSecond)
            {
                //pulse
                SoundManager.GetComponent<SoundManager>().monitorSoundPlay();
                timeSinceLastPulse = 0.0f;
                pulseList[0].GetComponent<PulseLineHolder>().pulseHeightChangeValue = 10;
            }
            else
            {
                //no pulse
                timeSinceLastPulse += Time.deltaTime;
            }
            
            frameCount++;
            if(frameCount >= framePerChange)
            {
                pulseGraph(); //run every few frames, otherwise it is basically instant.
                frameCount = 0;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                BPM += 1;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                BPM -= 1;
            }

            this.GetComponent<PhoneBuzzer>().SetBPM(BPM);
            
            difficultyModifier();
        }
        else
        {
            BPM = 90;
            currentFrameSinceStart = 0;
        }
    }

    void pulseGraph() //This is the heart beat code for the monitor, controlling the wave generated. Be careful when touching or modifying!
    {
        for (int i = 0; i < pulseList.Length - 1; i++)
        {
            if(pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue < 10)
                pulseList[i].transform.position = pulseList[i].GetComponent<PulseLineHolder>().defaultPosition + new Vector3(0, heightOfLine * pulseChangeArray[pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue], 0); //if change value is between 0 and 9, change.
            else
                pulseList[i].transform.position = pulseList[i].GetComponent<PulseLineHolder>().defaultPosition + new Vector3(0, heightOfLine * pulseChangeArray[0], 0); //if 10, go to 0, but don't pass on yet.
            
            if(pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue == 10)
            {
                if(i == 0)
                {
                    //noone can pass it to me, so pass along
                    pulseList[i + 1].GetComponent<PulseLineHolder>().pulseHeightChangeValue = 10;
                    pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue--;
                }
                else
                {
                    if (pulseList[i - 1].GetComponent<PulseLineHolder>().pulseHeightChangeValue == 9)
                    {
                        //I was just passed this value! Don't pass it on!
                    }
                    else if (i != pulseList.Length - 1)
                    {
                        pulseList[i + 1].GetComponent<PulseLineHolder>().pulseHeightChangeValue = 10;
                        pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue--;
                    }
                }
            }
            else
            {
                if(pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue > 0)
                    pulseList[i].GetComponent<PulseLineHolder>().pulseHeightChangeValue--;
            }
        }
    }

    public void buttonPress(string direction)
    {
        //if (photonView.IsMine == true)
        //{
            if (direction == "Up" || direction == "up")
            {
                BPM++;
                //bpm increase
            }
            else
            {
                BPM--;
                //bpm decrease
            }
        //}
    }

    public void difficultyModifier()
    {
        currentFrameSinceStart++;
        //Change difficulty.
    }
}
