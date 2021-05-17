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
    public GameObject Monitor1;
    private GameObject networkedMonitor1;
    public GameObject Monitor2;
    private GameObject networkedMonitor2;
    public GameObject Monitor3;
    private GameObject networkedMonitor3;
    public GameObject InformationText1; //mobile UI BPM
    public GameObject UpButton;
    public GameObject DownButton;

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
        else
        {
            avatarPlayer = PhotonNetwork.Instantiate(Avatar.name, new Vector3(), Quaternion.identity, 0);
            avatarPlayer.GetComponent<ChangeUniverse>().portal = Portal1; //Portals are unassigned at spawn for some reason, assigning them.
            avatarPlayer.GetComponent<ChangeUniverse>().portal2 = Portal2;
            avatarPlayer.GetComponent<ChangeUniverse>().portal3 = Portal3;
            avatarPlayer.GetComponent<ChangeUniverse>().soundManager = SoundManager;
            SoundManager.GetComponent<SoundManager>().avatarListener = avatarPlayer;
            avatarPlayer.GetComponent<ChangeUniverse>().VirtualCameraView = avatarPlayer.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject;
            avatarPlayer.GetComponent<ChangeUniverse>().VirtualCameraView2 = avatarPlayer.transform.GetChild(6).gameObject.transform.GetChild(2).gameObject;
            avatarPlayer.GetComponent<ChangeUniverse>().VirtualCameraView3 = avatarPlayer.transform.GetChild(6).gameObject.transform.GetChild(3).gameObject;
            avatarPlayer.GetComponent<ChangeUniverse>().monitor1 = Monitor1;
            avatarPlayer.GetComponent<ChangeUniverse>().monitor2 = Monitor2;
            avatarPlayer.GetComponent<ChangeUniverse>().monitor3 = Monitor3;

        }
        
        GameObject[] ListOfMonitors = GameObject.FindGameObjectsWithTag("Monitor");
        Debug.Log("List Of Monitors: " + ListOfMonitors.Length.ToString());
        if (ListOfMonitors.Length == 3)
        {
            networkedMonitor1 = PhotonNetwork.Instantiate(Monitor1.name, Monitor1.transform.position, Monitor1.transform.rotation, 0);
            networkedMonitor1.layer = Monitor1.layer;
            SoundManager.GetComponent<SoundManager>().monitor1 = networkedMonitor1;
            networkedMonitor1.GetComponent<MonitorScript>().SoundManager = SoundManager;
            networkedMonitor1.GetComponent<MonitorScript>().BPMText = networkedMonitor1.transform.GetChild(0).gameObject;
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorBeep1 = networkedMonitor1.GetComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(3); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(3, networkedMonitor1.GetComponent<AudioSource>());
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine1 = networkedMonitor1.AddComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine1.clip = SoundManager.GetComponent<SoundManager>().FlatLine;
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(6); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(6, SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine1);
            GameObject.Destroy(Monitor1);
            foreach(Transform child in networkedMonitor1.transform)
                child.gameObject.layer = networkedMonitor1.layer;

            networkedMonitor2 = PhotonNetwork.Instantiate(Monitor2.name, Monitor2.transform.position, Monitor2.transform.rotation, 0);
            networkedMonitor2.layer = Monitor2.layer;
            SoundManager.GetComponent<SoundManager>().monitor2 = networkedMonitor2;
            networkedMonitor2.GetComponent<MonitorScript>().SoundManager = SoundManager;
            networkedMonitor2.GetComponent<MonitorScript>().BPMText = networkedMonitor2.transform.GetChild(0).gameObject;
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorBeep2 = networkedMonitor2.GetComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(4); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(4, networkedMonitor2.GetComponent<AudioSource>());
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine2 = networkedMonitor2.AddComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine2.clip = SoundManager.GetComponent<SoundManager>().FlatLine;
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(7); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(7, SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine2);
            GameObject.Destroy(Monitor2);
            foreach (Transform child in networkedMonitor2.transform)
                child.gameObject.layer = networkedMonitor2.layer;

            networkedMonitor3 = PhotonNetwork.Instantiate(Monitor3.name, Monitor3.transform.position, Monitor3.transform.rotation, 0);
            networkedMonitor3.layer = Monitor3.layer;
            SoundManager.GetComponent<SoundManager>().monitor3 = networkedMonitor3;
            networkedMonitor3.GetComponent<MonitorScript>().SoundManager = SoundManager;
            networkedMonitor3.GetComponent<MonitorScript>().BPMText = networkedMonitor3.transform.GetChild(0).gameObject;
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorBeep3 = networkedMonitor3.GetComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(5); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(5, networkedMonitor3.GetComponent<AudioSource>());
            //--------------------
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine3 = networkedMonitor3.AddComponent<AudioSource>();
            SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine3.clip = SoundManager.GetComponent<SoundManager>().FlatLine;
            SoundManager.GetComponent<SoundManager>().soundList.RemoveAt(8); //remove old audio object.
            SoundManager.GetComponent<SoundManager>().soundList.Insert(8, SoundManager.GetComponent<SoundManager>().soundMonitorFlatLine3);
            GameObject.Destroy(Monitor3);
            foreach (Transform child in networkedMonitor3.transform)
                child.gameObject.layer = networkedMonitor3.layer;


            avatarPlayer.GetComponent<ChangeUniverse>().monitor1 = networkedMonitor1;
            avatarPlayer.GetComponent<ChangeUniverse>().monitor2 = networkedMonitor2;
            avatarPlayer.GetComponent<ChangeUniverse>().monitor3 = networkedMonitor3;

            networkedMonitor1.GetComponent<MonitorScript>().PaceMakerBPMText = InformationText1;
            networkedMonitor2.GetComponent<MonitorScript>().PaceMakerBPMText = InformationText1;
            networkedMonitor3.GetComponent<MonitorScript>().PaceMakerBPMText = InformationText1;

            UpButton.GetComponent<ButtonClick>().Monitor1 = networkedMonitor1;
            UpButton.GetComponent<ButtonClick>().Monitor2 = networkedMonitor2;
            UpButton.GetComponent<ButtonClick>().Monitor3 = networkedMonitor3;

            DownButton.GetComponent<ButtonClick>().Monitor1 = networkedMonitor1;
            DownButton.GetComponent<ButtonClick>().Monitor2 = networkedMonitor2;
            DownButton.GetComponent<ButtonClick>().Monitor3 = networkedMonitor3;
        }
        else if (ListOfMonitors.Length > 3)
        {
            GameObject.Destroy(Monitor1);
            GameObject.Destroy(Monitor2);
            GameObject.Destroy(Monitor3);
        }
        
        /*
        GameObject[] ListOfBeds = GameObject.FindGameObjectsWithTag("Bed");
        Debug.Log("List Of Bed: " + ListOfMonitors.Length.ToString());
        if (ListOfBeds.Length == 0)
        {
            
            Bed = PhotonNetwork.Instantiate(Bed.name, new Vector3(0.27f, 0.16f, 17.03f), Quaternion.Euler(0, 90, 0), 0);
            //vatarPlayer.GetComponent<ChangeUniverse>().bed = Bed;
            Debug.Log("Spawned a Bed");
            
        }
        */
    }

    private void OnApplicationQuit()
    {
        if (ConnectToOnline)
        {
            PhotonNetwork.Destroy(avatarPlayer); //So their model doesn't stay around on server after they dc.
            //PhotonNetwork.Destroy(Bed);
            //PhotonNetwork.Destroy(Monitor);
        }
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
