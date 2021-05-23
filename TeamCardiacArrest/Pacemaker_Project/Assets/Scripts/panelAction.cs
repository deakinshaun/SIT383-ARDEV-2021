using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class panelAction : MonoBehaviour
{

    [SerializeField]
    private GameObject WelcomeWindow;

    [SerializeField]
    private GameObject DismissButton;

    public GameObject studentPrefab;
    private Camera cam;

    public void Dismiss()
    {
        WelcomeWindow.SetActive(false);
/*
        Instantiate(studentPrefab);
        cam = studentPrefab.GetComponent<UserCam>();
        cam.setActive();
*/
    }
}
