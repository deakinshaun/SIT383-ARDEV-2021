using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonConnect: MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Starting network");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Acknowledged");
        CreateRoom();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("MedRoom"); //hardcoded for now will be randomly generated for each session.
            
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joind with " + PhotonNetwork.CurrentRoom.PlayerCount + " others." );
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail");
    }

    void Update()
    {

    }
}
