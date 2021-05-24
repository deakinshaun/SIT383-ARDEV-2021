using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
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

    //public GameObject VoiceConnection;     - MIC WIP

    //private GameObject voiceTest;

    public GameObject CubeRoom;
    

    //This Game Object should be the Voice manager prefab, used for photon voice - Chris
    //public GameObject voiceManager;

    private bool allowJoin = false;
    List <GameObject> displayRooms = new List<GameObject>();


    List <string> typeDD = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        //OnStart();
    }
    
    public void OnStart()
    {
        Debug.Log ("Photon manager starting.");
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected!");

        //GameObject vc = GetComponent<VoiceConnection>();   - MIC WIP 
        //vc.Client.AddCallbackTarget(this);                    MIC WIP
        //vc.ConnectUsingSettings();                          MIC WIP
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        //GameObject vc = GetComponent<VoiceConnection>();    - MIC WIP
    }
    // Update is called once per frame



    public override void OnJoinedLobby ()
    {
        Debug.Log("You made to the lobby!");
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

    


    private void UpdateRooms()
    {
        int row = 0;
        int col = 1;
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

    public void setRoomTexture(GameObject room)
    {
        Texture tex = room.GetComponent<Texture>();
        switch (ddGroup.value)
        {
            case 0:
            tex = SoloRoom;
            break;

            case 1:
            tex = GroupRoom;
            break;

            case 2:
            tex = LobbyRoom;
            break;
            
            default:
            tex = LobbyRoom;
            break;
        }
    }

    /*
    private void voiceInstance()
    {
        PhotonNetwork.Instantiate("voiceManager", new Vector3(), Quaternion.identity, 0);
    }
    */


    public override void OnJoinedRoom()
    {
        setRoomTexture(CubeRoom);
        //voiceTest = new GameObject(voiceInstance());
        
        //Debug.Log(voiceManager.name);
        //Vibration2.CreateOneShot(3, 255);
        //if (photonView.IsMine == true)
        //{
            //GameObject voiceTest = (GameObject) PhotonNetwork.Instantiate("voiceManager", new Vector3(), Quaternion.identity, 0);
        //}
        /*
        if (voiceTest != null)
        {
            message.text = "You found me!";
            Debug.Log("You found me");
        }
        else
        {
            message.text = "You DID NOT FINE ME IDIOT!!!!! me!";
        }
        */
        Room r = PhotonNetwork.CurrentRoom;
        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
        p["notices"] = RoomManager.getName(this.gameObject) + " : " + Time.time + ":joined\n";
        r.SetCustomProperties(p);
        //message.text= (r.Name);

        if (!allowJoin)
        {
            PhotonNetwork.LeaveRoom();
        } 
        else if (ddGroup.value == 0)
        {
            GameObject avatar = new GameObject(); 
            setRoomTexture(CubeRoom);
            avatar = PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), 0.0f, 0.0f), Quaternion.identity, 0);
            avatar.transform.SetParent(ARcontainer.transform);
            //voiceTest.transform.SetParent(avatar.transform);
            DebugText.text = "Just you on this run";
            Debug.Log("Just you on this run");
            allowJoin = false;
        }
        else
        {
            if (ddType.value == 0)
            {
                //PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(0.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount * 0.1f), 0f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(0.0f, ((float)PhotonNetwork.CurrentRoom.PlayerCount*0.1f), -9.0f), Quaternion.identity, 0); //making z -10 instwad of 0 for test
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
        PhotonNetwork.LeaveRoom();
        message.text = "Leaving room";
        ddGroup.value = 2;
        PhotonNetwork.JoinLobby();
        RoomCanvas.SetActive(false);               
        RoomSelect.SetActive(true);
        ddGroup.value = 0;
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
