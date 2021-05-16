using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField SessionID;
    public string SessionName;
    public void StartSim()
    {
        int RandomID;
        if (SessionID == null)
        {
            RandomID = Random.Range(1000, 9999);
            SessionName = RandomID.ToString();
        }
        else
        {
            SessionName = SessionID.ToString();
        }

        SceneManager.LoadScene("SampleScene"); // change when we get a better name for this scene.
    }
}
