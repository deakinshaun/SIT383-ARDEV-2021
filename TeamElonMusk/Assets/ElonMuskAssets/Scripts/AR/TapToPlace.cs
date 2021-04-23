using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public PhotonConnect NetworkManager;

    public ARRaycastManager raycastManager;
    public GameObject aRPointer;

    public List<GameObject> objectToPlace;
    private int objectPlaced = 0;

    private GameObject aRPointerObject;
    private void Start()
    {
        aRPointerObject = Instantiate(aRPointer, transform.position, Quaternion.identity);
        NetworkManager = FindObjectOfType<PhotonConnect>();
    }

    void Update()
    {

        
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0 && objectPlaced < objectToPlace.Count)
        {
            aRPointerObject.SetActive(true);
            aRPointerObject.transform.position = hits[0].pose.position;
            aRPointerObject.transform.rotation = hits[0].pose.rotation;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                
                NetworkManager.SpawnObject(objectToPlace[objectPlaced], aRPointerObject.transform.position, Quaternion.LookRotation(new Vector3(Camera.main.transform.position.x - this.gameObject.transform.position.x, 0 , Camera.main.transform.position.z - this.gameObject.transform.position.z)));
                objectPlaced++;
            }
        }
        else
        {
            aRPointerObject.SetActive(false);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 10, 100, 20), "Object Number: " + objectToPlace.Count + " Object Placed: " + objectPlaced);

    }
}
