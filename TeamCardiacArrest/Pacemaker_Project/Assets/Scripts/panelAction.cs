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

    public void Dismiss()
    {
        WelcomeWindow.SetActive(false);
    }
}
