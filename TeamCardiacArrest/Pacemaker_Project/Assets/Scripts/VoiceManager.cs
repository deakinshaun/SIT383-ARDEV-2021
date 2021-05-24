using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Realtime;
using Photon.Pun;

public class VoiceManager : MonoBehaviourPunCallbacks
{
    [Tooltip("TextMeshPro object for displaying call status")]
    public TMPro.TextMeshPro status;

    [Tooltip("Maximum length of status message in characters")]
    public int statusMaxLength = 150;

    public Text connectionInfo;


    public bool micOn = true;

   
    //int count = 1;

    //Commented out for prefab, however we might want to bring these back in tandem with the mute button

    //public GameObject microphoneIndicator;
    //public Material microphoneOn;
    //public Material microphoneOff;


    private string previousMessage = " ";

    

    /*
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
    */

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


    void Start()
    {
        //this is causing an error, but im not sure why tbh
        //status.text = " ";
        setStatusText("Photon Voice Started");

        //photonView = GetComponent<PhotonView>();



        PhotonNetwork.ConnectUsingSettings();

        VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.Client.AddCallbackTarget(this);
        vc.ConnectUsingSettings();

        muteMic(vc);
    }



    public override void OnConnectedToMaster()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        RoomOptions roomopt = new RoomOptions();
        TypedLobby lobby = new TypedLobby("ApplicationLobby", LobbyType.Default);
        vc.Client.OpJoinOrCreateRoom(new EnterRoomParams { RoomName = "Caridac Arrest Mic Test", RoomOptions = roomopt, Lobby = lobby });
        connectionInfo.text = "Connected to master";

        //VoiceConnection vc = GetComponent<VoiceConnection>();

    }

    public override void OnJoinedRoom()
    {
        //setStatusText("Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " particpants.");

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        setStatusText("Disconnected " + cause);
        //connectionInfo.text = "Disconnected";

        /*
        PhotonNetwork.ConnectUsingSettings();

        VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.Client.AddCallbackTarget(this);
        vc.ConnectUsingSettings();
        */
    }

    public void switchMicrophone()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        //if ((OVRInput.Get (button)) || (Input.GetAxis ("Fire1") > 0.0f)) 
        //button = true;
        if (micOn == true)
        {
            vc.PrimaryRecorder.TransmitEnabled = false;
            vc.PrimaryRecorder.DebugEchoMode = false;
            micOn = false;
            Debug.Log("MIC ON");
            status.text = "MIC ON";
        }
        else
        {
            vc.PrimaryRecorder.TransmitEnabled = true;
//            vc.PrimaryRecorder.DebugEchoMode = true;
            micOn = true;
            Debug.Log("MIC OFF");
            status.text = "MIC OFF";
        }

    }

    public void muteMic(VoiceConnection vc)
    {
        //VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.PrimaryRecorder.TransmitEnabled = false;
        vc.PrimaryRecorder.DebugEchoMode = false;
        micOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //button = Input.anyKeyDown;

        //mc = Input.anyKey;
        //if (photonView.IsMine == true)
        //{

        //count++;

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
            
            Debug.Log(vc.Client.State.ToString() + "server: " + vc.Client.CloudRegion + ":" + vc.Client.CurrentServerAddress + " room: " + room + " participants: " + otherParticipants);
             connectionInfo.text = vc.Client.CurrentServerAddress + " room: " + room + " participants: " + otherParticipants;
            //if (count == 100 || count == 200 || count == 300 || count == 400 || count == 600 || count == 700 )
            // {

        //setStatusText("OEFJNOJDNF");
        //}

        /*
        if (count == 100 || count == 200 || count == 300 || count == 400 || count == 600 || count == 700)
        {
            switchMicrophone();
        }
        */

        //}
    }

}
