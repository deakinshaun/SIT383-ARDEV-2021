using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    public GameObject Knob;
    // Start is called before the first frame update
    void Start()
    {
        Knob.transform.parent = this.transform;
    }

}