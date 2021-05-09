using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputDial : MonoBehaviour
{
    //this is public so that it can be easily checked in the editor, number and name is arbitrary at the moment
    // Once the pacemaker simulation comes together more they will be used to affect the patients heaart.
    public float output;

    public CapsuleCollider outputBox;


    private Ray ray;
    private RaycastHit hit;

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
            //this if loop is to make sure only this dial rotates
            if (hit.collider.name == outputBox.name)
            {
                print("rate hit");

                transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.0f, 0.5f, 0.0f));
                if (output < 50.00) { output++; }
                print("rate hit2");


                //THis is meant to feel like a pulse, CreateWavefrom SHOULD be more effective here, but it has not been so far (im probs doing something wrong)
                Vibration2.CreateOneShot(10, 500);
                Vibration2.CreateOneShot(2, 255);
            }

        }

    }
}
