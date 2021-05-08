using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    AudioSource audiosource;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name+"has entered the box");
        audiosource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name + "is still in the box");
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name+"has exsit the box");
    }
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
