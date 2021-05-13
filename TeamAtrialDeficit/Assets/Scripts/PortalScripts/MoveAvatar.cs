using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAvatar : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public float rotationSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.position += v * transform.forward * Time.deltaTime * 100.0f * movementSpeed;
        transform.rotation *= Quaternion.AngleAxis(h * 1000.0f * Time.deltaTime * rotationSpeed, transform.up);
    }
}
