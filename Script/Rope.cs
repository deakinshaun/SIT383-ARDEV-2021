using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform startPoint2;
    public Transform endTarget;
    public float ropeThickness = 0.1f;
    Vector3 startPoint;
    Vector3 endPoint;
    float distance = 0f;
    void Start()
    {
        startPoint = startPoint2.position;
        transform.localScale = Vector3.one * ropeThickness;
    }

    void Update()
    {
        endPoint = endTarget.transform.position;
        distance = Vector3.Distance(endPoint, startPoint);
        transform.position = (startPoint + endPoint) / 2f;
        Vector3 loc = transform.localScale;
        loc.y = distance / 2f;
        transform.localScale = loc;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, );
        //transform.LookAt(endTarget, transform.position- endPoint);

        transform.LookAt(endTarget.transform.position);
        transform.Rotate(Vector3.right * 90f);
    }
}
