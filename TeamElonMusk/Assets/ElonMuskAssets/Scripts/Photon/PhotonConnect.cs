using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public GameObject DesktopController;
    public GameObject AR_Controller;
    public GameObject AR_Avatar;

    void Start()
    {
        Debug.Log("Starting network");
        PhotonNetwork.ConnectUsingSettings();

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
        Debug.Log("Server has acknowledged my existance");
        RoomOptions roompts = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("SIT383Room", roompts, new TypedLobby("SIT383Lobby", LobbyType.Default));

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
}
