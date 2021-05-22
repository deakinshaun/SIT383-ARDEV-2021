using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundations
using Photon.Pun;
using Photon.Realtime;



public class RoomManager : MonoBehaviourPunCallbacks
{
    public Text DebugText;
    public Text message;
    public GameObject SetupPrefab;
    public GameObject TeacherPrefab;

    public Canvas TeacherUI;
    public GameObject RoomCanvas;
    public GameObject RoomSelect;
    public GameObject RoomPrefab;

    public Button LeaveButton;

    public Texture LobbyRoom;
    public Texture GroupRoom;
    public Texture SoloRoom;

    public Dropdown ddType;
    public Dropdown ddGroup;

    private int type;
    private int diff;
    private int group;
    private SoundManager soundsMan;
    public GameObject ARcontainer;

    

    //This Game Object should be the Voice manager prefab, used for photon voice - Chris
    public GameObject voiceManager;

    private bool allowJoin = false;
    List <GameObject> displayRooms = new List<GameObject>();


    List <string> typeDD = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Photon manager starting.");
        PhotonNetwork.ConnectUsingSettings();

        // this is an attempt to instantiate the voice manager prefab, it is returning a null reference exception when i try to instantiate it,
        // Despite voiceManager.name returning the correct value, i have no idea why. Also this should be taking place in onJoinedRoom for the final product. -Chris

        Debug.Log(voiceManager.name);
        GameObject PV = GameObject.Find(voiceManager.name);
        Debug.Log(PV);
        if (PV != null)
        {
            GameObject voiceTest = PhotonNetwork.Instantiate(PV.name, new Vector3(), Quaternion.identity, 0);
            if (voiceTest != null)
            {
            DebugText.Text = "You found me!";
            }
        }
        else
        {
            Debug.Log("PV was not found");
        }
    }

/*
     public DropdownItemSelected(Dropdown dropdown, int value)
    {
        int index = dropdown.selection;
        int value = dropdown.options[index].value;
        return value;
    }
*/

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();       
    }
    // Update is called once per frame



    public override void OnJoinedLobby ()
    {
        Debug.Log("You made to the lobby!");
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
        setRoomTexture(room);
        room.transform.SetParent(RoomCanvas.transform);
        room.GetComponent<DisplayRoom>().setName(name);
        room.GetComponent<LocalRoomBehaviour>().SetManager(this);
        displayRooms.Add (room);
        return room;
    }

    public void setRoomTexture(GameObject room)
    {
        Texture tex = room.GetComponent<Texture>();
        switch (ddGroup.value)
        {
            case 0:
            tex = LobbyRoom;
            break;

            case 1:
            tex = SoloRoom;
            break;

            case 2:
            tex = GroupRoom;
            break;
            
            default:
            tex = LobbyRoom;
            break;
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room " + returnCode + " " + message);
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

    public void JoinRoom(string roomName)
    {
        allowJoin = true;
        PhotonNetwork.JoinRoom(roomName);
    }



    public override void OnJoinedRoom()
    {
        Room r = PhotonNetwork.CurrentRoom;
        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
        p["notices"] = RoomManager.getName(this.gameObject) + " : " + Time.time + ":joined\n";
        r.SetCustomProperties(p);
        if (!allowJoin)
        {
            PhotonNetwork.LeaveRoom();
        } 
        else if (ddGroup.value == 0)
        {
            PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(0.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), 0.0f), Quaternion.identity, 0);
            ARcontainer.transform.position = new Vector3(0.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), 0.0f);
            DebugText.text = "Just you on this run";
            Debug.Log("Just you on this run");
            allowJoin = false;
        }
        else
        {
            if (ddType.value == 0)
            {
                PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(0.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), 0.0f), Quaternion.identity, 0);
                // Arsession origin.transform position = point;
                message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse";
                Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse");
            }
            else
            {
                PhotonNetwork.Instantiate(TeacherPrefab.name, new Vector3(1.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), 0.0f), Quaternion.identity, 0);
                // Arsession origin.transform position = point;
                message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor";
                Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor");                
            }
        }
        RoomSelect.SetActive(false);
        RoomCanvas.SetActive(true);       
    }

    public void LeaveRoom()
    {
        soundsMan.EndSimulation();
        PhotonNetwork.LeaveRoom();
        RoomSelect.SetActive(true);
        RoomCanvas.SetActive(false);               
    }

    private void removeRoomObject (GameObject room)
    {
        displayRooms.Remove (room);
        Destroy(room);
    }

    void Update()
    {
        
    }
}
