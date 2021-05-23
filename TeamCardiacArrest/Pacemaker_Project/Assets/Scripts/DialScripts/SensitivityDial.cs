using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SensitivityDial : MonoBehaviour
{
    //this is public so that it can be easily checked in the editor, number and name is arbitrary at the moment
    // Once the pacemaker simulation comes together more they will be used to affect the patients heaart.
    public float sensitivty = 0f;

    public CapsuleCollider sensitvityBox;

    private bool forward = true;
    private Ray ray;
    private RaycastHit hit;
    public TextMeshPro text3D;
    public TextMeshPro reverse;

    public HeartMonitor heartMonitorReference;


    // Start is called before the first frame update
    void Start()
    {
        sensitivty = heartMonitorReference.getVpower();
        text3D.text = sensitivty.ToString("0.00") + " mV";
        Debug.Log(sensitivty);
    }

    public void reverseSensitivty()
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
    public static void checkAproxSensitivty(float sensivity)
    {
        if (Mathf.Abs(0.00f - sensivity) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(40.00f - sensivity) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        if (Mathf.Abs(80.00f - sensivity) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(120.00f - sensivity) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(160.00f - sensivity) <= 0.01)
        {
            //Debug.Log("Numbers are aprox the same");
            Vibration2.CreateOneShot(3, 255);
        }
        else if (Mathf.Abs(200.00f - sensivity) <= 0.01)
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
            if (hit.collider.name == sensitvityBox.name)
            {
                if (forward == true)
                {
                    if (sensitivty < 200.20)
                    {
                        sensitivty += 0.2f ;
                        heartMonitorReference.setVpower(sensitivty);
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, 0.5f, 0.0f));
                        text3D.text = sensitivty.ToString("0.00") + " mV";
                        checkAproxSensitivty(sensitivty);
                    }
                }
                else
                {
                    if (sensitivty > 0)
                    {
                        sensitivty -= 0.2f;
                        heartMonitorReference.setVpower(sensitivty);
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, -0.5f, 0.0f));
                        text3D.text = sensitivty.ToString("0.00") + " mV";
                        checkAproxSensitivty(sensitivty);
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
