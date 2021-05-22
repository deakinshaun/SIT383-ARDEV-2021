using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharactors : MonoBehaviour
{
    public GameObject[] Humans;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, Humans.Length-1);
        Humans[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
