using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ObjScan : MonoBehaviour
{
	//UI
	public GameObject roomSelection;
	//Object References
    ARTrackedImageManager trackedManager;

    public GameObject roomManagerContainer;
    RoomManager roomManager;

	//Room Cube
	public GameObject roomCube;

    void Awake()
    {
        trackedManager = GetComponent<ARTrackedImageManager>();
        roomManager = roomManagerContainer.GetComponent<RoomManager>();
    }

	private void OnEnable()
	{
		trackedManager.trackedImagesChanged += onChangedImage;
	}

	private void OnDisable()
	{
		trackedManager.trackedImagesChanged -= onChangedImage;
	}

	void onChangedImage (ARTrackedImagesChangedEventArgs args)
	{
		foreach (ARTrackedImage trackedImage in args.added)
		{
			roomSelection.SetActive(false);
			roomSelection.SetActive(true);
			roomCube.SetActive(true);
			//---Room Insertion?
		}
	}

}