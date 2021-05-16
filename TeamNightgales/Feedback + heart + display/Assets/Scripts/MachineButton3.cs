using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButton3 : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject display;
    public Machine machine;

    private void OnMouseDown()
    {
        if(!machine.on)
            return;
        audioSource.Play();
      
            display.SetActive(!display.activeInHierarchy);
    }
}
