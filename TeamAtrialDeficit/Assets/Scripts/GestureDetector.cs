using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Class without function, is used to hold data
[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    //Passes tracked bone data including bone index, parent relation and index and transform which can give position of bone.

    // Start is called before the first frame update
    void Start()
    {
        previousGesture = new Gesture();
        //List gets filled with all bones that can be accessed in ovr skeleton with the bones.
    }

    // Update is called once per frame
    void Update()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        //Debug.Log("RecognisingGesture");
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("New Gesture Found : " + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();

        }
        
    }

    void Save()
    {
        Gesture ButtonPush = new Gesture();
        ButtonPush.name = "Button push Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            //create using finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        ButtonPush.fingerData = data;
        gestures.Add(ButtonPush);
    }
    //Function to compare gestures based on closeness of position
    Gesture Recognize()
    {
        Gesture currentgesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i <fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerData[i]);
                if(distance>threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentgesture = gesture;
            }
        }
        return currentgesture;
    }
}
