using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class RoomManger : MonoBehaviourPunCallbacks
{
    //DownGrade to Unity4.21
    public Text MessageText;
    public GameObject MessagePanel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Login");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We have connected");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("AppRoom", roomopt, new TypedLobby("AppLobby", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You are in a room with  " + PhotonNetwork.CurrentRoom.PlayerCount + "  Other Players.");
        showMessageStatus("You have join the room", 3);
    }




    // Update is called once per frame
    void Update()
    {

    }

    public void showMessageStatus(string message, int seconds)
    {

        // Set the game status message, show it, and hide it after user defined seconds
        MessageText.text = message;
        MessagePanel.SetActive(true);
        Invoke("hideMessageStatus", seconds);
    }

    public void hideMessageStatus ()
    {
        MessagePanel.SetActive(false);
    }

}
