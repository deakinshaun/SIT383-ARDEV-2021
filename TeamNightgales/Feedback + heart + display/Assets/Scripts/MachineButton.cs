using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineButton : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject display;
    public GameObject display2;
    public Machine machine;
    private void OnMouseDown()
    {
        audioSource.Play();
        print(1);
        display.SetActive(!machine.on);
        machine.on = !machine.on;
        if (!machine.on)
        {
            display2.SetActive(false);
        }
    }
}
