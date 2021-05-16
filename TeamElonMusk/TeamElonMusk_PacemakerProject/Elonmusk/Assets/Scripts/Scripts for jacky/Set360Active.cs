using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set360Active : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void SetObjectActive()
    {
        this.gameObject.SetActive(true);
    }
}
