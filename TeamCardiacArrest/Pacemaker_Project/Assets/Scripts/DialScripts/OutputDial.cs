using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutputDial : MonoBehaviour
{
    //this is public so that it can be easily checked in the editor, number and name is arbitrary at the moment
    // Once the pacemaker simulation comes together more they will be used to affect the patients heaart.
    public float output = 0f;

    public CapsuleCollider outputBox;

    private bool forward = true;
    private Ray ray;
    private RaycastHit hit;
    public TextMeshPro text3D;
    public TextMeshPro reverse;

    // gives access to required floats for heartbeat manager, set in the editor
    public HeartMonitor heartMonitorReference;


    void Start()
    {
        output = heartMonitorReference.getApower();
        text3D.text = output.ToString("0.00") + " mA";
        Debug.Log(output);
    }

    public void reverseOutput()
    {
        if (forward == true)
        {
            forward = false;
        }
        else
        {
            forward = true;
        }
    }


    //Method to check whether dial var is close enough value where vibration occurs, to cover for "fuzzy" floats
    public static void checkAproxOutput(float output)
    {
        if (Mathf.Abs(0.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(50.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        if (Mathf.Abs(100.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(150.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(200.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(250.00f - output) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }

    }



    // Update is called once per frame
    private void Update()
 {
     //Using raycast to see if the user mouse if over the dial object
     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
     if (Physics.Raycast(ray, out hit))
     {
         if (hit.collider.name == outputBox.name)
         {
             if (forward == true)
             {
                 if (output < 250.20)
                 {
                     output += 0.2f;
                     //updating Apower in heart monitor
                     heartMonitorReference.setApower(output);
                     transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, 0.5f, 0.0f));
                     text3D.text = output.ToString("0.00") + " mA";
                     checkAproxOutput(output);
                        /*
                     if ((output % 50) == 0)
                     {
                         Vibration2.CreateOneShot(3, 255);
                     }
                        */
                 }
             }
             else
             {
                 if (output > 0)
                 {
                     output -= 0.2f;
                     //updating Apower in heart monitor
                     heartMonitorReference.setApower(output);
                     transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, -0.5f, 0.0f));
                     text3D.text = output.ToString("0.00") + " mA";
                     checkAproxOutput(output);
                        /*
                     if ((output % 50) == 0)
                     {
                         Vibration2.CreateOneShot(3, 255);
                     }
                        */
                    }
             }
         }

         if (hit.collider.name == reverse.name)
         {
             if (forward == true)
             {
                 forward = false;
                 reverse.text = "Swap direcition: Currently reverse";
             }
             else
             {
                 forward = true;
                 reverse.text = "Swap direcition: Currently forward";
             }
         }

     }

 }
}

