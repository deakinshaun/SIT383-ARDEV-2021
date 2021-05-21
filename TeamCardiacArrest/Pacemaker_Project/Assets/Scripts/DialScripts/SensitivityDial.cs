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

    // Start is called before the first frame update
    void Start()
    {

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
                    if (sensitivty < 200.00)
                    {
                        sensitivty ++ ;
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, 0.5f, 0.0f));
                        text3D.text = sensitivty.ToString("0.00") + " mV";
                        if ((sensitivty % 50) == 0)
                        {
                            Vibration2.CreateOneShot(3, 255);
                        }
                    }
                }
                else
                {
                    if (sensitivty > 0)
                    {
                        sensitivty--;
                        transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, -0.5f, 0.0f));
                        text3D.text = sensitivty.ToString("0.00") + " mV";
                        if ((sensitivty % 50) == 0)
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
