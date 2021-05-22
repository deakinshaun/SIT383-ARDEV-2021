using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class MyPunManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogError("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

             //   LoadArena();
            }
        }
        /// <summary>
        /// Called when a Photon Player got disconnected. We need to load a smaller scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogError("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

             //   LoadArena();
            }
        }
        public void PunShowNormal()
        {
            photonView.RPC("ShowNormal", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void ShowNormal()
        {
            GameController.instance.ShowNormal();
        }

        public void PunShowEmergency()
        {
            photonView.RPC("ShowEmergency", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void ShowEmergency()
        {
            GameController.instance.ShowEmergency();
        }

        public void PunResetButtons()
        {
            photonView.RPC("ResetButtons", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void ResetButtons()
        {
            GameController.instance.ResetButtons();
        }
        public void PunIncreaseCardiogram()
        {
            photonView.RPC("IncreaseCardiogram", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void IncreaseCardiogram()
        {
            GameController.instance.IncreaseCardiogram();
        }

        public void PunDecreaseCardiogram()
        {
            photonView.RPC("DecreaseCardiogram", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void DecreaseCardiogram()
        {
            GameController.instance.DecreaseCardiogram();
        }
        public void PunCheckValue()
        {
            photonView.RPC("CheckValue", RpcTarget.AllViaServer);
        }
        [PunRPC]
        public void CheckValue()
        {
            GameController.instance.CheckValue();
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("PunBasics-Launcher");
        }
    }
}
