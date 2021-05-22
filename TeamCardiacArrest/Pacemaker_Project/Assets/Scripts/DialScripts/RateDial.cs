using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RateDial : MonoBehaviour
{
    //this is public so that it can be easily checked in the editor, number and name is arbitrary at the moment
    // Once the pacemaker simulation comes together more they will be used to affect the patients heaart.
    public float rate = 30.00f;

    public CapsuleCollider rateBox;

    //public GameObject dial;

    private bool forward = true;
    private Ray ray;
    private RaycastHit hit;
    public TextMeshPro text3D;
    public TextMeshPro reverse;
    
    
    // gives access to required floats for heartbeat manager, set in the editor
    public HeartMonitor heartMonitorReference;

    // Start is called before the first frame update
    void Start()
    {
        rate = heartMonitorReference.getBPM();
        Debug.Log(rate);
    }


    //This is not being used, but will be used if i manage to get a button working within the prefab properly
   
    public void reverseRate()
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
   


    // Update is called once per frame
    private void Update()
    {
        //Using raycast to see if the user mouse if over the dial object
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //this if loop is to make sure only this dial rotates
            if (hit.collider.name == rateBox.name)
            {
                if (forward == true)
                {
                    if (rate < 200.00)
                    {
                        rate++;
                        //updating BPM in heart monitor
                        heartMonitorReference.setBPM(rate);
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, 0.5f, 0.0f));
                        text3D.text = rate.ToString("0.00") + " ppm";
                        if ((rate % 50) == 0)
                        {
                            Vibration2.CreateOneShot(3, 255);
                        }
                    }

                }
                else
                {
                    if (rate > 0)
                    {
                        rate--;
                        //updating BPM in heart monitor
                        heartMonitorReference.setBPM(rate);
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, -0.5f, 0.0f));
                        text3D.text = rate.ToString("0.00") + " ppm";
                        if ((rate % 50) == 0)
                        {
                            Vibration2.CreateOneShot(3, 255);
                        }
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
