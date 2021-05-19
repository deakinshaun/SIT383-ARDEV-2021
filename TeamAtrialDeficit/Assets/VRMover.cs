using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMover : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 constrainDirection = new Vector3(0, 1, 0);
    public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.01f)
        {
            this.transform.position += speed * Vector3.ProjectOnPlane(pointer.transform.forward, constrainDirection);
        }
    }
}
