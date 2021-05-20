using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


    //Lobby and room abstraction
    // !two boxes in one scene
    // !same photon room

    //Room a or b
    // Solo run
    // mentor and nurses




public class RoomManager : MonoBehaviourPunCallbacks
{
    public Text message;
    public GameObject SetupPrefab;
    public GameObject TeacherPrefab;
//    public Canvas TeacherUI;
    public Canvas RoomCanvas;
    public Canvas RoomSelect;
    public GameObject RoomPrefab;
    public bool Nurse;
    public bool Hard;
    public bool Solo;

    private bool allowJoin = false;
    List <GameObject> displayRooms = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Photon manager starting.");
        PhotonNetwork.ConnectUsingSettings();
    }

        public static string getName (GameObject o)
    {
        if (o.GetComponent<PhotonView>() != null)
        {
            if ((o.GetComponent<PhotonView>().Owner.NickName != null) && (o.GetComponent<PhotonView>().Owner.NickName.Equals ("")))
            {
                return o.GetComponent<PhotonView>().Owner.NickName;
            }
            else
            {
                return o.GetComponent<PhotonView>().Owner.UserId;
            }
        }
        else
        {
            return "X" + PhotonNetwork.AuthValues.UserId;
        }
        
    }

    private GameObject getRoomObject (string name)
    {
        foreach (GameObject g in displayRooms)
        {
            DisplayRoom dr = g.GetComponent<DisplayRoom>();
            if (dr.getName().Equals(name))
            {
                return g;
            }
        }
        GameObject room = Instantiate(RoomPrefab);
        room.transform.SetParent(RoomCanvas.transform);
        room.GetComponent<DisplayRoom>().setName(name);
        room.GetComponent<LocalRoomBehaviour>().SetManager(this);
        displayRooms.Add (room);
        return room;
    }

    private void removeRoomObject (GameObject room)
    {
        displayRooms.Remove (room);
        Destroy(room);
    }

    private void UpdateRooms()
    {
        int row = 0;
        int col = 0;
        int columnLimit = 2;
        foreach(GameObject room in displayRooms)
        {
            room.transform.localPosition = new Vector3 (col * 20 - 5, row * 20, 0);
            col +=1 ;
            if (col >= columnLimit)
            {
                col = 0;

            }
        }
    }

    public void JoinRoom(string roomName)
    {
        allowJoin = true;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnRoomListUpdate (List<RoomInfo> roomList)
    {
        foreach (RoomInfo ri in roomList)
        {
            GameObject room = getRoomObject(ri.Name);
            if (ri.RemovedFromList)
            {
                removeRoomObject(room);
            }
            else
            {
                room.GetComponent<DisplayRoom>().display (ri.Name + "\n\nwith " +
                                                          ri.PlayerCount + " other people\n" +
                                                          ri.CustomProperties["notices"]);
            }
        }
        UpdateRooms();
    }


    public override void OnJoinedLobby ()
    {
        Debug.Log("You made to the lobby!");
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();       
    }
    // Update is called once per frame


    public override void OnJoinedRoom()
    {
        Room r = PhotonNetwork.CurrentRoom;
        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
        p["notices"] = RoomManager.getName(this.gameObject) + " : " + Time.time + ":joined\n";
        r.SetCustomProperties(p);

        
        if (Nurse == true && Solo == true)
        {
            PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(), Quaternion.identity, 0);
//          cam Camera
            message.text = "Just you on this run";
            Debug.Log("Just you on this run");
            allowJoin = false;
        }
        else if (Nurse == true && Solo == false)
        {
            PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(), Quaternion.identity, 0);
// get camera componenet, look though
// camera inactive on later intances
            message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse";
            Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse");
        }
        else if (Nurse == false && Solo == false)
        {
            PhotonNetwork.Instantiate(TeacherPrefab.name, new Vector3(), Quaternion.identity, 0);
            //get camera componenet, look though
            //camera inactive on later intances
            message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor";
            Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor");

        }
        else if (!allowJoin)
        {
            PhotonNetwork.LeaveRoom();
        }

    }

    private GameObject SetNurse()
    {
        return true;
    }

    public GameObject SetMentor()
    {
        return false;
    }
    public GameObject SetSolo()
    {
        return true;
    }
    public GameObject SetGroup()
    {
        return false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room " + returnCode + " " + message);
    }

    public void addRoom(Text name)
    {
        Debug.Log("Adding new room: " + name.text);
        RoomOptions ro = new RoomOptions();
        ro.EmptyRoomTtl = 100000;


        string[] roomPropsInLobby = {"notices"};
        ro.CustomRoomPropertiesForLobby = roomPropsInLobby;
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() 
        {
            {"notices", "RoomStart\n"}
        };
        ro.CustomRoomProperties = customRoomProperties;
        PhotonNetwork.JoinOrCreateRoom (name.text, ro, null);
    }

    void Update()
    {
        
    }
}
