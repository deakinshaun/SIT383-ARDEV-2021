using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class GyroAndMove : MonoBehaviour
{

    public float speed = 10.0f;
    public GameObject nurse;
    public GameObject teacher;
    

    // Update is called once per frame
    void Update()
    {
       if (SystemInfo.supportsGyroscope && GetComponent <PhotonView> ().IsMine == true || PhotonNetwork.IsConnected == false)
        {
            Input.gyro.enabled = true;
            transform.rotation = 
                Quaternion.Euler (90, 0, 90) * 
                Input.gyro.attitude * 
                Quaternion.Euler (180, 180, 0);


            Camera camera = GameObject.Find("UserCam").GetComponent<Camera>();

            if (camera == null)
            {
                Debug.Log("Looking for a camera");
            }
            else
            {
                float v = Input.GetAxis("Vertical");
                transform.position += v * speed * Time.deltaTime * camera.transform.forward;
            }

            

        }        

    }
}
