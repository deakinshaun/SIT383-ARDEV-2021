using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARRaycast : MonoBehaviour
{

    private Camera arCamera;

    private Vector2 touchPosition = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;
            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                //Debug.Log(hitObject);
                /*
                if (Physics.Raycast(ray, out hitObject))
                {
                    //
                }
                */
            }
        }

        
    }
}
