using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLineHolder : MonoBehaviour
{
    public int pulseHeightChangeValue = 0; //check MonitorScript.cs : pulseChangeArray
    public Vector3 defaultPosition;

    private void Start()
    {
        defaultPosition = this.gameObject.transform.position;
    }
}
