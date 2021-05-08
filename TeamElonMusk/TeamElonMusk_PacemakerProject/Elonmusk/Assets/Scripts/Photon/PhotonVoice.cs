using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Realtime;


public class PhotonVoice : MonoBehaviour
{
    public VoiceConnection voiceConnection;

    void Start()
    {
        voiceConnection = GetComponent<VoiceConnection>();
        voiceConnection.Client.AddCallbackTarget(this);
        voiceConnection.ConnectUsingSettings();
        //vc.PrimaryRecorder.TransmitEnabled = true;
    }

    public void OnConnectedToMaster()
    {
        voiceConnection = GetComponent<VoiceConnection>();
        Debug.Log("Server has acknowledge voice existance");
        RoomOptions roompts = new RoomOptions();
        TypedLobby lobby = new TypedLobby("SIT383Room", LobbyType.Default);
        voiceConnection.Client.OpJoinOrCreateRoom(new EnterRoomParams {RoomName = "SIT383Room", RoomOptions = roompts, Lobby = lobby});

        
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Voice Enabled");
        /*if (this.voiceConnection.PrimaryRecorder == null)
        {
            this.voiceConnection.PrimaryRecorder = this.gameObject.AddComponent<Recorder>();
        }
        if (this.autoTransmit)
        {
            this.voiceConnection.PrimaryRecorder.TransmitEnabled = this.autoTransmit;
        }*/
    }
    void Update()
    {
        voiceConnection = GetComponent<VoiceConnection>();
        voiceConnection.PrimaryRecorder.TransmitEnabled = true;
    }
}
