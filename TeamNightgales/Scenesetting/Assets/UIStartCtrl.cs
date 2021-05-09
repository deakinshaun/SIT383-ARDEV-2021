using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartCtrl : MonoBehaviour
{
    public Button btnStart;

    private void Awake()
    {
        btnStart.onClick.AddListener(ClickBtnStart);
    }

    void ClickBtnStart()
    {
        SceneManager.LoadScene("Game");
    }
}
