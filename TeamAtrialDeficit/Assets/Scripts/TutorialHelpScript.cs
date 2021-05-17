using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelpScript : MonoBehaviour
{
    public GameObject TutorialCanvas;
    private bool AState;
    
    // Start is called before the first frame update
    void Start()
    {
        AState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AState = !AState;
        }
        TutorialCanvas.SetActive(AState);
    }
}
