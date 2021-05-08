using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialSound : MonoBehaviour
{
    public GameObject soundSource;
    public GameObject viewer;
    public float decayFactor = 1.0f;
    public float dropoff = 0.1f;

    private float lastDistance;
   
    void Update()
    {
        float distance = (soundSource.transform.position - viewer.transform.position).magnitude;
        soundSource.GetComponent<AudioSource>().volume = 1.0f / Mathf.Pow(distance * dropoff, decayFactor);

        float s = (distance - lastDistance) / Time.deltaTime;
        float c = 330.0f;
        soundSource.GetComponent<AudioSource>().pitch = (c + s) / (c - s);
        lastDistance = distance;

    }
}
