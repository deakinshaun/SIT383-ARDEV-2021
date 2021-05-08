using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Roomprelisting : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        roomInfo = roomInfo;
        _text.text = roomInfo.Name;
    }

}
