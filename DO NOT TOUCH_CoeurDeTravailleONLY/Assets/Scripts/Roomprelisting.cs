using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Roomprelisting : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private RoomInfo _roomInfo;
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        _text.text = roomInfo.Name;
    }

}
