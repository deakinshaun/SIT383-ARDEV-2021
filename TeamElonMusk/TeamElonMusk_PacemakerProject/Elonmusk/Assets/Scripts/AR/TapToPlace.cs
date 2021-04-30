using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public PhotonConnect NetworkManager;

    public ARRaycastManager raycastManager;
    public GameObject aRPointer;

    public List<GameObject> objectToPlace;

    private GameObject aRPointerObject;
    private bool placeObject = false;


    //canvas buttons
    [Header("Canvas Buttons")]
    public GameObject backButton;
    public GameObject placeObjectsButton;
    public GameObject PlaceObjectsPanel;



    private void Start()
    {
        aRPointerObject = Instantiate(aRPointer, transform.position, Quaternion.identity);
        NetworkManager = FindObjectOfType<PhotonConnect>();

        backButton.SetActive(false);
        PlaceObjectsPanel.SetActive(false);
        placeObjectsButton.SetActive(true);
    }

    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (placeObject)
        { 
                aRPointerObject.SetActive(true);
                aRPointerObject.transform.position = hits[0].pose.position;
                aRPointerObject.transform.rotation = hits[0].pose.rotation;
        }   

    }

    public void PlaceMonitor()
    {
        NetworkManager.SpawnObject(objectToPlace[0], aRPointerObject.transform.position, Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - this.gameObject.transform.position.x, 0, Camera.main.transform.position.z - this.gameObject.transform.position.z)));
    }

    public void PlaceOperationTable()
    {
        NetworkManager.SpawnObject(objectToPlace[1], aRPointerObject.transform.position, Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - this.gameObject.transform.position.x, 0, Camera.main.transform.position.z - this.gameObject.transform.position.z)));
    }

    public void PlaceWhiteBoard()
    {
        NetworkManager.SpawnObject(objectToPlace[2], aRPointerObject.transform.position, Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - this.gameObject.transform.position.x, 0, Camera.main.transform.position.z - this.gameObject.transform.position.z)));
    }

    public void PlaceObjects()
    {
        placeObject = true;
        PlaceObjectsPanel.SetActive(true);
        backButton.SetActive(true);
        placeObjectsButton.SetActive(false);
    }

    public void BackButton()
    {
        placeObject = false;
        PlaceObjectsPanel.SetActive(false);
        placeObjectsButton.SetActive(true);
        backButton.SetActive(false);
    }

}
