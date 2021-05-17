using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class DisplayRoom : MonoBehaviour
{

    private string roomName;

    public string getName()
    {
        return roomName;
    }

    public void setName (string name)
    {
        roomName = name;
    }

    public void display (string message)
    {
        GetComponentInChildren <Text>().text = message;
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
