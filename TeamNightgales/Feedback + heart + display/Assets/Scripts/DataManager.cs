using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text heartRate1;
    public TMP_Text bloodPressure1;
    public TMP_Text heartRate2;
    public TMP_Text bloodPressure2;
     
    public float heartRate;
    public float bloodPressure;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heartRate = 50 + Mathf.Sin(Time.time) * 5;
        heartRate = Mathf.Round(heartRate);
        bloodPressure = 70 + Mathf.Sin(Time.time) * 5;
        bloodPressure = Mathf.Round(heartRate);
        heartRate1.text = heartRate.ToString();
        heartRate2.text = heartRate.ToString();
        bloodPressure1.text = bloodPressure.ToString();
        bloodPressure2.text = bloodPressure.ToString();
    }
}
