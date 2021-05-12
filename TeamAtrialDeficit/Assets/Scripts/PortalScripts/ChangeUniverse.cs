using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUniverse : MonoBehaviour
{
    public Material phyCamMaterial;
    public Material virCamMaterial;
    public Material virCamMaterial2;
    public Material virCamMaterial3;
    public Material virCamMaterial4;
    public GameObject portal;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject virtualview1;
    public GameObject virtualview2;
    public GameObject virtualview3;

    public bool inPhysical = true;
    //public bool inPhysical2 = true;
    //public bool inPhysical3 = true;

    public GameObject soundManager; //Brendan: Added so portal sound can be played when user enters.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inPhysical)
        {
            phyCamMaterial.SetInt("_Stencil_Level", 0);
            virCamMaterial.SetInt("_Stencil_Level", 1);
            virCamMaterial2.SetInt("_Stencil_Level", 2);
            virCamMaterial3.SetInt("_Stencil_Level", 3);
        }
        else
        {
            phyCamMaterial.SetInt("_Stencil_Level", 1);
            virCamMaterial.SetInt("_Stencil_Level", 0);
            virCamMaterial2.SetInt("_Stencil_Level", 0);
            virCamMaterial3.SetInt("_Stencil_Level", 0);
            //virCamMaterial.SetInt("_Stencil_Level", 0);
            //virCamMaterial2.SetInt("_Stencil_Level", 0);
            //virCamMaterial3.SetInt("_Stencil_Level", 0);
        }
        /*if (inPhysical2)
        {
            phyCamMaterial.SetInt("_Stencil_Level", 0);
            virCamMaterial2.SetInt("_Stencil_Level", 2);
        }
        else
        {
            phyCamMaterial.SetInt("_Stencil_Level", 2);
            virCamMaterial2.SetInt("_Stencil_Level", 0);
        }
        if (inPhysical3)
        {
            phyCamMaterial.SetInt("_Stencil_Level", 0);
            virCamMaterial3.SetInt("_Stencil_Level", 3);
        }
        else
        {
            phyCamMaterial.SetInt("_Stencil_Level", 3);
            virCamMaterial3.SetInt("_Stencil_Level", 0);
        }*/
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == portal)
        {
            Debug.Log("Changing Universe");
            inPhysical = !inPhysical;
            //inPhysical2 = !inPhysical2;
            //inPhysical3 = !inPhysical3;
            soundManager.GetComponent<SoundManager>().advancedPortalSoundPlay(); //Brendan: Plays portal sound
        }
        if (other.gameObject == portal2)
        {
            Debug.Log("Changing Universe2");
            //inPhysical2 = !inPhysical2;
            inPhysical = !inPhysical;
            //inPhysical3 = !inPhysical3;
            soundManager.GetComponent<SoundManager>().intermediatePortalSoundPlay(); //Brendan: Plays portal sound
        }
        if (other.gameObject == portal3)
        {
            Debug.Log("Changing Universe3");
            //inPhysical3 = !inPhysical3;
            //inPhysical2 = !inPhysical2;
            inPhysical = !inPhysical;
            soundManager.GetComponent<SoundManager>().beginnerPortalSoundPlay(); //Brendan: Plays portal sound
        }
    }
}
