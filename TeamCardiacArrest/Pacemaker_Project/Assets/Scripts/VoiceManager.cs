using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Realtime;
using Photon.Pun;

public class VoiceManager : MonoBehaviourPunCallbacks
{
    [Tooltip("TextMeshPro object for displaying call status")]
    public TMPro.TextMeshPro status;

    [Tooltip("Maximum length of status message in characters")]
    public int statusMaxLength = 150;


    //public bool button = true;
    //public bool mc = true;
    public bool micOn = true;

    //Commented out for prefab, however we might want to bring these back in tandem with the mute button
    //public GameObject microphoneIndicator;
    //public Material microphoneOn;
    //public Material microphoneOff;

    private string previousMessage = " ";

    public void micButton()
    {
        if (micOn)
        {
            Debug.Log("Microphone switched off");
            micOn = false;
            //change button to indicate muted?
        }
        else
        {
            Debug.Log("Microphone switched on");
            micOn = true;
            //chnage button to indicate unmuted?
        }

    }

    private void setStatusText(string message)
    {
        if (!message.Equals(previousMessage))
        {
            Debug.Log(message);
            status.text += "\n" + message;
            if (status.text.Length > statusMaxLength)
            {
                status.text = status.text.Remove(0, status.text.Length - statusMaxLength);
            }
            previousMessage = message;
        }
    }


    void Awake()
    {
        //this is causing an error, but im not sure why tbh
        status.text = " ";
        setStatusText("Photon Voice Started");

        PhotonNetwork.ConnectUsingSettings();

        VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.Client.AddCallbackTarget(this);
        vc.ConnectUsingSettings();
    }



    public override void OnConnectedToMaster()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        RoomOptions roomopt = new RoomOptions();
        TypedLobby lobby = new TypedLobby("ApplicationLobby", LobbyType.Default);
        //romm name should maybe be changed to pCardiac Arrest?
        vc.Client.OpJoinOrCreateRoom(new EnterRoomParams { RoomName = "SIT383PhotonVoice", RoomOptions = roomopt, Lobby = lobby });

    }

    public override void OnJoinedRoom()
    {
        setStatusText("Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " particpants.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        setStatusText("Disconnected " + cause);
    }

    void switchMicrophone()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        //if ((OVRInput.Get (button)) || (Input.GetAxis ("Fire1") > 0.0f)) 
        //button = true;
        if (micOn)
        {
            vc.PrimaryRecorder.TransmitEnabled = true;
        }



        //if (button || mc)
        //{ 
        //vc.PrimaryRecorder.TransmitEnabled = !vc.PrimaryRecorder.TransmitEnabled; 
        // }
        //this can probably be removed, but might come in handy for debugging so ill keep it in for now
        /*
         if (microphoneIndicator != null)
         {
             if (vc.PrimaryRecorder.TransmitEnabled)
             {
                 //Debug.Log("im on!");
                 microphoneIndicator.GetComponent<MeshRenderer>().material = microphoneOn;
             }
             else
             {
                 microphoneIndicator.GetComponent<MeshRenderer>().material = microphoneOff;
             }
         }   
        */
    }

    // Update is called once per frame
    void Update()
    {
        //button = Input.anyKeyDown;

        //mc = Input.anyKey;

        VoiceConnection vc = GetComponent<VoiceConnection>();

        string otherParticipants = "";
        if (vc.Client.InRoom)
        {
            Dictionary<int, Player>.ValueCollection pts = vc.Client.CurrentRoom.Players.Values;

            foreach (Player p in pts)
            {
                otherParticipants += p.ToStringFull();
            }
        }
        string room = "not in room";
        if (vc.Client.CurrentRoom != null)
        {
            room = vc.Client.CurrentRoom.Name;
        }

        setStatusText(vc.Client.State.ToString() + "server: " + vc.Client.CloudRegion + ":" + vc.Client.CurrentServerAddress + " room: " + room + " participants: " + otherParticipants);

        switchMicrophone();
    }

}
