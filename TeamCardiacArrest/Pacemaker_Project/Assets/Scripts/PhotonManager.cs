using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Text message;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Photon manager starting.");
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("ApplicationRoom", roomopt, new TypedLobby("ApplicationLobby", LobbyType.Default));
    }
    // Update is called once per frame
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            message.text = "Just you on this run";
            Debug.Log("Just you on this run");
        }
        else
        {
            message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people";
            Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people");
        }
    }
    void Update()
    {
        
    }
}
