using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float distance = 20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ivMachine()
    {
        
    }
    public static void hospitalTrolley()
    {
        //
        float x = -10;
        float z = 2;
        Instantiate(TrolleyWaker,Vector3(x,-2,z));
        while (x<10)
        {
        x = distance * Time.time;
        transform position = new Vector3(transform.position.x);
        }
    }
    public static void manInPain()
    {
        
    }
    public static void HospitalQuiet()
    {
        
    }
    public static void HospitalLoud()
    {
        
    }
    public static void beep()
    {
        
    }
    public static void flatLine()
    {
        
    }

}
