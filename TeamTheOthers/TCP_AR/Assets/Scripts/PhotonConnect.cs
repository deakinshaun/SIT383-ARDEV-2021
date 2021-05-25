using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using TMPro;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public GameObject avatarPrefab;
    private string SessionName;
    public TMP_Text t;

    void Start()
    {
        Debug.Log("Starting network");
        SetRoomName();
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetRoomName()
    {
        string path = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else
        {
            path = Application.dataPath;
        }

        path = path + "SessionName.txt";
        StreamReader reader = new StreamReader(path);
        SessionName = reader.ReadToEnd();
        t.text = "Session ID:" + SessionName;
        reader.Close();
        //SessionName = "Room" + SessionName;
    }


    public override void OnConnectedToMaster()
    {

        Debug.Log("Acknowledged");
        Debug.Log("Session Name: " + SessionName);
        RoomOptions roomopts = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("room", roomopts,
            new TypedLobby(SessionName + "Lobby", LobbyType.Default));

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joind with " + PhotonNetwork.CurrentRoom.PlayerCount + " others.");
        //PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0);
    }

    void Update()
    {

    }
}
