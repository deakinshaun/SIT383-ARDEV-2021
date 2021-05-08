using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public GameObject DesktopController;
    public GameObject AR_Controller;
    public GameObject AR_Avatar;

    void Start()
    {
        Debug.Log("Starting network");
        PhotonNetwork.ConnectUsingSettings();

        /*VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.Client.AddCallbackTarget(this);
        vc.ConnectUsingSettings();*/


        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            GameObject ar_controller = Instantiate(AR_Controller);

        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Instantiate(DesktopController);
        }
    }

    public override void OnConnectedToMaster()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        Debug.Log("Server has acknowledged my existance");
        RoomOptions roompts = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("SIT383Room", roompts, new TypedLobby("SIT383Lobby", LobbyType.Default));
        /*TypedLobby lobby = new TypedLobby("SIT383Room", LobbyType.Default);
        PhotonNetwork.JoinOrCreateRoom("SIT383Room", roompts, lobby);
        vc.Client.OpJoinOrCreateRoom(new EnterRoomParams { RoomName = "SIT383Room", RoomOptions = roompts, Lobby = lobby });*/

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined with " + PhotonNetwork.CurrentRoom.PlayerCount + " others.");
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            GameObject ARpoint = PhotonNetwork.Instantiate(AR_Avatar.name, new Vector3(), Quaternion.identity, 0);

        }
    }
    public void SpawnObject(GameObject obj,Vector3 transformPosition, Quaternion transformRotation)
    {
        Debug.Log("Requesting spawn Object");
        GameObject spawnedObject = PhotonNetwork.Instantiate(obj.name, transformPosition, transformRotation, 0);

        Debug.Log("Spawned Object");
    }

    /*public void turnOnMic()
    {
        VoiceConnection vc = GetComponent<VoiceConnection>();
        vc.PrimaryRecorder.TransmitEnabled = true;
    }*/

}
