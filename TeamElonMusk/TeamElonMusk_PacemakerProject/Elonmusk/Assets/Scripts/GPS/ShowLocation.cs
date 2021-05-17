using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLocation : MonoBehaviour
{
    public GPSTracking locationService;
    public GameObject marker;

    public float globeRadius = 10.0f;
    public float rotationSpeed = 30.0f;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up);

        float latitude;
        float longitude;
        float altitude;

        if (locationService.retrieveLocation(out latitude, out longitude, out altitude))
        {
            Vector3 position = globeRadius * new Vector3(Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad), Mathf.Sin(latitude * Mathf.Deg2Rad), Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad));
            marker.transform.localPosition = position;
        }
    }
}
