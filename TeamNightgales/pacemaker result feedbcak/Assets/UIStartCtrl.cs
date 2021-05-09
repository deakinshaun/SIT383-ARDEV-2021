using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartCtrl : MonoBehaviour
{
    public Button btnStart;

    public GameObject show1;
    public GameObject show2;
    public GameObject show3;

    private void Awake()
    {
        btnStart.onClick.AddListener(ClickBtnStart);
    }

    void ClickBtnStart()
    {
        show1.SetActive(true);
        show2.SetActive(true);
        show3.SetActive(true);
    }
}
