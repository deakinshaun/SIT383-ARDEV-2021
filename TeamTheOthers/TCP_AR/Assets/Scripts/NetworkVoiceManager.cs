using Photon.Pun;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoiceConnection))]
public class NetworkVoiceManager : MonoBehaviour
{
    public Transform remoteVoiceParent;

    private VoiceConnection voiceConnection;
    public Recorder rec;
    public Button col;
    private bool isMute = false;

    void Awake()
    {
        voiceConnection = GetComponent<VoiceConnection>();
    }

    public void Mute()
    {
        int on = 1;
        if(isMute)
        {
            OnEnable();
            isMute = false;
            rec.TransmitEnabled = true;
            on = 2;
            col.GetComponent<Image>().color = Color.green;
        }
        if (on == 1)
        {
            if (!isMute)
            {
                OnDisable();
                rec.TransmitEnabled = false;
                isMute = true;
                col.GetComponent<Image>().color = Color.red;
            }
        }
    }

    private void OnEnable()
    {
        voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
    }

    private void OnDisable()
    {
        voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
    }

    private void OnSpeakerCreated(Speaker speaker)
    {
        speaker.transform.SetParent(this.remoteVoiceParent);
        speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
    }

    private void OnRemoteVoiceRemove(Speaker speaker)
    {
        if (speaker != null)
        {
            Destroy(speaker.gameObject);
        }
    }
}