using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkScript : MonoBehaviourPunCallbacks
{
    public bool ConnectToOnline;
    public bool usingVR;
    public GameObject Avatar;
    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject Portal3;
    public GameObject SoundManager;
    public GameObject vrAvatar;
    private GameObject avatarPlayer; //The user
    public GameObject Monitor;
    public GameObject Bed;


    //For the Flexible Controller: Variables
    public GameObject ControlPointer;

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
        if (usingVR)
        {
            avatarPlayer = PhotonNetwork.Instantiate(vrAvatar.name, new Vector3(), Quaternion.identity, 0);
            SoundManager.GetComponent<SoundManager>().vrListener = vrAvatar;
        }
        avatarPlayer = PhotonNetwork.Instantiate(Avatar.name, new Vector3(), Quaternion.identity, 0);
        avatarPlayer.GetComponent<ChangeUniverse>().portal = Portal1; //Portals are unassigned at spawn for some reason, assigning them.
        avatarPlayer.GetComponent<ChangeUniverse>().portal2 = Portal2;
        avatarPlayer.GetComponent<ChangeUniverse>().portal3 = Portal3;
        avatarPlayer.GetComponent<ChangeUniverse>().soundManager = SoundManager;
        SoundManager.GetComponent<SoundManager>().avatarListener = avatarPlayer;


        GameObject[] ListOfMonitors = GameObject.FindGameObjectsWithTag("Monitor");
        Debug.Log("List Of Monitors: " + ListOfMonitors.Length.ToString());
        if (ListOfMonitors.Length == 0)
        {
            Monitor = PhotonNetwork.Instantiate(Monitor.name, new Vector3(-0.72f, 0.16f, 21.51f), Quaternion.Euler(-90, -180, 0), 0);
            Monitor.GetComponent<MonitorScript>().SoundManager = SoundManager;
            Debug.Log("Spawned a Monitor");
        }


        GameObject[] ListOfBeds = GameObject.FindGameObjectsWithTag("Bed");
        Debug.Log("List Of Bed: " + ListOfMonitors.Length.ToString());
        if (ListOfBeds.Length == 0)
        {
            Bed = PhotonNetwork.Instantiate(Bed.name, new Vector3(0.27f, 0.16f, 17.03f), Quaternion.Euler(0, 90, 0), 0);
            Debug.Log("Spawned a Bed");
        }
    }

    private void OnApplicationQuit()
    {
        PhotonNetwork.Destroy(avatarPlayer); //So their model doesn't stay around on server after they dc.
    }

    //The Following sections of code is used for the Flexible Controller, if it Causing issues Uncomment the /* */ given above and below the section of code.
    //Currently, for it to work there should only be two versions of the same person in the join, this might get in the way of other components as is.
    //Therefore for the moment please disable the Flexi Controller through the Main Scene Heirachy if it becomes a problem
    // The below code should not affect anything else - Rahul
    // /*
    [PunRPC]
    void ControllerUpdate(Quaternion ControllerOrientation, bool ButtonAState, bool ButtonXState)
    {
        Debug.Log("Recieved Controller info: " + ControllerOrientation + " " + ButtonAState + " " + ButtonXState);
        ControlPointer.transform.rotation = ControllerOrientation;
    }

    public void SendControllerState(Quaternion ControllerOrientation, bool ButtonAState, bool ButtonXState)
    {
        photonView.RPC("ControllerUpdate", RpcTarget.All, ControllerOrientation, ButtonAState, ButtonXState);
    }
    // */
}
