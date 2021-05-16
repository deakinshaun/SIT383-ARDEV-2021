using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MonitorScript : MonoBehaviour
{
    public int difficulty; //assigned per dimension?
    public GameObject pulseLine;
    public GameObject SoundManager;
    public GameObject spawnLocation;
    public GameObject BPMText;
    
    //Rahul: Added to include Mobile UI
    public GameObject PaceMakerBPMText;

    private List<GameObject> pulseList;
    private float BPM = 90;
    private float timeSinceNextPulse = 0.0f;
    private int pulseCounter; //used for creation of visual pulse
    private int[] pulseChangeArray = new int[10]{0,-2,-4,-2,0,2,4,6,4,2}; //changes in height when a pulse happens
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        pulseList = new List<GameObject>();
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine == true)
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject pulse = Instantiate(pulseLine, spawnLocation.transform.position + new Vector3(0.0005f * i, 0, 0), spawnLocation.transform.rotation);
                pulseList.Add(pulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == true)
        {
            BPMText.GetComponent<TextMesh>().text = BPM.ToString() + " BMP";
            float BeatsPerSecond = BPM / 60;

            //Rahul: Added to include Mobile UI
            if (PaceMakerBPMText.activeSelf)
            {
                PaceMakerBPMText.GetComponent<Text>().text = BPM.ToString() + " BMP";
            }
            


            if (timeSinceNextPulse > BeatsPerSecond)
            {
                //pulse
                SoundManager.GetComponent<SoundManager>().monitorSoundPlay();
            }
            else
            {
                //no pulse
                timeSinceNextPulse += Time.deltaTime;
                if (pulseCounter > 0)
                    pulseCounter--;
            }
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
    }

    void pulseGraph()
    {
        //use pulsecounter;
        pulseList.RemoveAt(0);
        foreach(GameObject pulseListObject in pulseList)
        {
            pulseListObject.transform.position += new Vector3(0.0005f, 0, 0); //Move along the line.
        }

        GameObject pulse = Instantiate(pulseLine, spawnLocation.transform.position + new Vector3(0, 0, 0.005f * pulseChangeArray[pulseCounter]), spawnLocation.transform.rotation);
        pulseList.Add(pulse);
    }

    public void buttonPress(string direction)
    {
        if (photonView.IsMine == true)
        {
            if (direction == "up")
            {
                //bpm increase
            }
            else
            {
                //bpm decrease
            }
        }
    }
}
