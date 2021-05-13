using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Text message;
    public int roomtype;
    public GameObject SetupPrefab;
    public GameObject TeacherPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Photon manager starting.");
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("ApplicationRoom", roomopt, new TypedLobby("ApplicationLobby", LobbyType.Default));
    }
    // Update is called once per frame


    // AR core card on the floor create environment, simulation
    // !Surface based generation
    // marker or placed plane

    //Lobby and room abstraction
    // !two boxes in one scene
    // !same photon room

    //Room a or b
    // Solo run
    // mentor and nurses
    public override void OnJoinedRoom()
    {
        switch (roomtype)
        {
            case 1:
                PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(), Quaternion.identity, 0);
                //get camera componenet, look though
                message.text = "Just you on this run";
                Debug.Log("Just you on this run");
                break;            

            case 2:
                PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(), Quaternion.identity, 0);
                //get camera componenet, look though
                //camera inactive on later intances
                message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse";
                Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a nurse");
                break;            

            case 3:
                PhotonNetwork.Instantiate(TeacherPrefab.name, new Vector3(), Quaternion.identity, 0);
                //get camera componenet, look though
                //camera inactive on later intances
                message.text = "You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor";
                Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other people as a mentor");
                break;            


            default:
                PhotonNetwork.Instantiate(SetupPrefab.name, new Vector3(), Quaternion.identity, 0);
                message.text = "Just you on this run";
                Debug.Log("Just you on this run");
                break;
        }
    }
    void Update()
    {
        
    }
}
