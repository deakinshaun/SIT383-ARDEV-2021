using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkScript : MonoBehaviourPunCallbacks
{
    public bool ConnectToOnline;
    public GameObject Avatar;
    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject Portal3;
    // Start is called before the first frame update
    void Start()
    {
        if(ConnectToOnline)
        {
            Debug.Log("Starting network");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("AtrialDeficit", roomopt, new TypedLobby("MainLobby", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " others");

        GameObject avatarPlayer = PhotonNetwork.Instantiate(Avatar.name, new Vector3(), Quaternion.identity, 0);
        avatarPlayer.GetComponent<ChangeUniverse>().portal = Portal1; //Portals are unassigned at spawn for some reason, assigning them.
        avatarPlayer.GetComponent<ChangeUniverse>().portal2 = Portal2;
        avatarPlayer.GetComponent<ChangeUniverse>().portal3 = Portal3;

    }
}
