using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button but;
    void Start()
    {
        but.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
