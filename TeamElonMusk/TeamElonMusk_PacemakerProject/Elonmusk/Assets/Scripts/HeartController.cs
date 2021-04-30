using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public GameObject theHeart;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Camera.main)
        {
            theHeart.GetComponent<Material>().SetColor("red", Color.white);
        }
    }
}
