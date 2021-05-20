using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveAvatar : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public float rotationSpeed = 0.2f;
    private PhotonView photonView;
    public GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == true)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            transform.position += v * transform.forward * Time.deltaTime * 100.0f * movementSpeed;
            transform.rotation *= Quaternion.AngleAxis(h * 1000.0f * Time.deltaTime * rotationSpeed, transform.up);
        }
        else
        {
            myCamera.SetActive(false); //disables camera if not player avatar
            this.transform.GetChild(2).gameObject.SetActive(false); //disable portalview camera if not player avatar
            this.transform.GetChild(3).gameObject.SetActive(false); //disable portalview camera if not player avatar
            this.transform.GetChild(4).gameObject.SetActive(false); //disable portalview camera if not player avatar
            this.transform.GetChild(5).gameObject.SetActive(false); //disable portalview camera if not player avatar
        }
    }
}
