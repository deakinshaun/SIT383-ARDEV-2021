using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeUniverse : MonoBehaviour
{
    public Material virCamMaterial;
    public Material virCamMaterial2;
    public Material virCamMaterial3;
    public GameObject portal;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject monitor1;
    public GameObject monitor2;
    public GameObject monitor3;
    public GameObject VirtualCameraView;
    public GameObject VirtualCameraView2;
    public GameObject VirtualCameraView3;

    public bool inPhysical = true;
    public bool inVirtual = false;
    public bool inVirtual2 = false;
    public bool inVirtual3 = false;

    public GameObject soundManager; //Brendan: Added so portal sound can be played when user enters.
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        inPhysical = true;

        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (photonView.IsMine == true)
        //{
            if (inPhysical)
            {
                virCamMaterial.SetInt("_Stencil_Level", 1);
                virCamMaterial2.SetInt("_Stencil_Level", 2);
                virCamMaterial3.SetInt("_Stencil_Level", 3);
                inVirtual3 = false;
                inVirtual = false;
                inVirtual2 = false;
                portal.SetActive(true);
                portal2.SetActive(true);
                portal3.SetActive(true);
                VirtualCameraView.SetActive(true);
                VirtualCameraView2.SetActive(true);
                VirtualCameraView3.SetActive(true);
            }
            else
            {
                virCamMaterial.SetInt("_Stencil_Level", 0);
                virCamMaterial2.SetInt("_Stencil_Level", 0);
                virCamMaterial3.SetInt("_Stencil_Level", 0);
            }
            if (inVirtual3)
            {
                portal2.SetActive(false);
                portal3.SetActive(false);
                VirtualCameraView.SetActive(false);
                VirtualCameraView2.SetActive(false);
            }
            if (inVirtual2)
            {
                portal.SetActive(false);
                portal3.SetActive(false);
                VirtualCameraView.SetActive(false);
                VirtualCameraView3.SetActive(false);
            }
            if (inVirtual)
            {
                portal2.SetActive(false);
                portal.SetActive(false);
                VirtualCameraView2.SetActive(false);
                VirtualCameraView3.SetActive(false);
            }
        //}
    }
    public void OnTriggerEnter(Collider other)
    {
        //if (photonView.IsMine == true)
        //{
            if (other.gameObject == portal)
            {
                Debug.Log("Entering Advanced Practice");
                inPhysical = !inPhysical;
                inVirtual3 = true;
                soundManager.GetComponent<SoundManager>().advancedPortalSoundPlay(); //Brendan: Plays portal sound
                monitor1.GetComponent<MonitorScript>().Run = false; //Brendan, turns off monitor in outside player area
                monitor2.GetComponent<MonitorScript>().Run = false; //Brendan, turns off monitor in outside player area
                monitor3.GetComponent<MonitorScript>().Run = true; //Brendan, runs monitor in player area
            }
            if (other.gameObject == portal2)
            {
                inPhysical = !inPhysical;
                inVirtual2 = true;
                Debug.Log("Entering Intermediate Practice");
                soundManager.GetComponent<SoundManager>().intermediatePortalSoundPlay(); //Brendan: Plays portal sound
                monitor1.GetComponent<MonitorScript>().Run = false; //Brendan, turns off monitor in outside player area
                monitor2.GetComponent<MonitorScript>().Run = true; //Brendan, runs monitor in player area
                monitor3.GetComponent<MonitorScript>().Run = false; //Brendan, turns off monitor in outside player area
            }
            if (other.gameObject == portal3)
            {
                inPhysical = !inPhysical;
                inVirtual = true;
                Debug.Log("Entering Basic Practice");
                soundManager.GetComponent<SoundManager>().beginnerPortalSoundPlay(); //Brendan: Plays portal sound
                monitor1.GetComponent<MonitorScript>().Run = true; //Brendan, turns off monitor in outside player area
                monitor2.GetComponent<MonitorScript>().Run = false; //Brendan, turns off monitor in outside player area
                monitor3.GetComponent<MonitorScript>().Run = false; //Brendan, runs monitor in player area
            }
        //}
    }
}
