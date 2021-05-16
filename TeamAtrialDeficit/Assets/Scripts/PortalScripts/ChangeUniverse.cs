using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUniverse : MonoBehaviour
{
    public Material virCamMaterial;
    public Material virCamMaterial2;
    public Material virCamMaterial3;
    public GameObject portal;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject monitor;
    public GameObject bed;
    public GameObject VirtualCameraView;
    public GameObject VirtualCameraView2;
    public GameObject VirtualCameraView3;

    public bool inPhysical = true;
    public bool inVirtual = false;
    public bool inVirtual2 = false;
    public bool inVirtual3 = false;

    public GameObject soundManager; //Brendan: Added so portal sound can be played when user enters.

    // Start is called before the first frame update
    void Start()
    {
        inPhysical = true;

    }

    // Update is called once per frame
    void Update()
    {
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
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == portal)
        {
            Debug.Log("Entering Advanced Practice");
            inPhysical = !inPhysical;
            inVirtual3 = true;
            soundManager.GetComponent<SoundManager>().advancedPortalSoundPlay(); //Brendan: Plays portal sound
            monitor.layer = 12;
            foreach (GameObject child in monitor.transform)
                child.gameObject.layer = 12;
            bed.layer = 12;
            foreach (GameObject child in bed.transform)
                child.gameObject.layer = 12;
        }
        if (other.gameObject == portal2)
        {
            inPhysical = !inPhysical;
            inVirtual2 = true;
            Debug.Log("Entering Intermediate Practice");
            soundManager.GetComponent<SoundManager>().intermediatePortalSoundPlay(); //Brendan: Plays portal sound
            monitor.layer = 11;
            foreach (GameObject child in monitor.transform)
                child.gameObject.layer = 11;
            bed.layer = 11;
            foreach (GameObject child in bed.transform)
                child.gameObject.layer = 11;
        }
        if (other.gameObject == portal3)
        {
            inPhysical = !inPhysical;
            inVirtual = true;
            Debug.Log("Entering Basic Practice");
            soundManager.GetComponent<SoundManager>().beginnerPortalSoundPlay(); //Brendan: Plays portal sound
            monitor.layer = 10;
            foreach (GameObject child in monitor.transform)
                child.gameObject.layer = 10;
            bed.layer = 10;
            foreach (GameObject child in bed.transform)
                child.gameObject.layer = 10;
        }
    }
}
