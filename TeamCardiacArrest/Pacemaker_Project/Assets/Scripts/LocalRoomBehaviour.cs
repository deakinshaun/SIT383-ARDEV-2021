using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime; 

public class LocalRoomBehaviour : MonoBehaviour
{
    private RoomManager RoomManager;

    public void setManager(RoomManager manager)
    {
//        roomManager = manager;
    }

    public void enterRoom()
    {
 //       string roomName = GetComponent<DisplayRoom>().getName();
 //       Debug.Log = ("Entering room: " + roomName);
 //       roomManager.JoinRoom(roomName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}