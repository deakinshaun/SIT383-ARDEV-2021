using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class RoomManger : MonoBehaviourPunCallbacks
{
    //DownGrade to Unity4.21
    public Text TimerDisplay;
    private float countDownValue = 60f * 1f;
    public Text MessageText;
    public GameObject MessagePanel;
    public bool GameStart = false;
    [SerializeField]
    private Text _roomName;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private Roomprelisting _roomListing;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Login");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("We have connected");
        //RoomOptions roomopt = new RoomOptions();
        //PhotonNetwork.JoinOrCreateRoom("AppRoom", roomopt, new TypedLobby("AppLobby", LobbyType.Default));
        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You are in a room with  " + PhotonNetwork.CurrentRoom.PlayerCount + "  Other Players.");
        //showMessageStatus("You have join the room", 3);
    }




    // Update is called once per frame
    void Update()
    {
        if (GameStart == true)
            {
            StartCounting();
        }
        //countDownValue -= Time.deltaTime;
        //TimerDisplay.text = string.Format("{0:00}:{1:00}", ((int)(countDownValue / 60) % 60).ToString("d2"), ((int)(countDownValue % 60)).ToString("d2"));
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

    public void OnClick_CreateRoom()
    {
        Debug.Log("We have connected");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("AppRoom", roomopt, new TypedLobby("AppLobby", LobbyType.Default));
        GameStart = true;
        showMessageStatus("You have join the room", 3);

    }

    public override void OnCreatedRoom()
    {
        //MasterManager.DebugConsole.AddText("Created  room sucessfully", this);
         //Debug.Log("You are in a room with  " + PhotonNetwork.CurrentRoom.PlayerCount + "  Other Players.");
    }

    public override void OnCreateRoomFailed(short returncode, string message)
    {
        //MasterManager.DebugConsole.AddText("Created  room failed", this);
         Debug.Log("Failed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            Roomprelisting listing = Instantiate(_roomListing, _content);
            if (listing != null)
                listing.SetRoomInfo(info);
        }
    }
    public void StartCounting()
    {
        if (GameStart == true)
            {
            countDownValue -= Time.deltaTime;
            TimerDisplay.text = string.Format("{0:00}:{1:00}", ((int)(countDownValue / 60) % 60).ToString("d2"), ((int)(countDownValue % 60)).ToString("d2"));
        }
    }


}
