using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class NetworkPosition : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    Vector3 realPosition;
    Quaternion realRotation;
    
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(photonView.IsMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 1f);
            Debug.Log("hit");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("yes");
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
