using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineButton2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public AudioSource audioSource;
    public Machine machine;

    private void OnMouseDown()
    {
        if(!machine.on)
            return;
        audioSource.Play();
        text1.fontSize += 0.001f;
        text2.fontSize += 0.001f;
        text3.fontSize += 0.001f;
        text4.fontSize += 0.001f;
    }
}
