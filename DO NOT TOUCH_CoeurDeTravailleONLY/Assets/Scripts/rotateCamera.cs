using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCamera : MonoBehaviour
{
    float initialRotationX, initialRotationY, initialRotationZ;
    float newRotationZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickCameraRotate()
    {
        initialRotationX = this.transform.localRotation.x;
        initialRotationY = this.transform.localRotation.y;
        initialRotationZ = this.transform.localRotation.z;

        newRotationZ = initialRotationZ + 90.0f;

        if (newRotationZ > 360.0f) newRotationZ -= 360.0f;

       this.transform.Rotate(initialRotationX, initialRotationY, newRotationZ, Space.Self);

    }


}
