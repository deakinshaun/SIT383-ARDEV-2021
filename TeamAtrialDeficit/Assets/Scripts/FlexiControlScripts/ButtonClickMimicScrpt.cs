using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickMimicScrpt : MonoBehaviour
{

    public GameObject Monitor1;
    public GameObject Monitor2;
    public GameObject Monitor3;
    public string Direction; 
    public GameObject SoundManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WhenPressed()
    {
        SoundManager.GetComponent<SoundManager>().button1SoundPlay();

        Monitor1.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor2.GetComponent<MonitorScript>().buttonPress(Direction);
        Monitor3.GetComponent<MonitorScript>().buttonPress(Direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
