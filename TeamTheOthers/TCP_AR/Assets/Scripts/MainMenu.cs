using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField SessionID;
    public TMP_Text t;
    private string SessionName;

    public void StartSim()
    {
        int RandomID;
        SessionName = SessionID.text;
        if (SessionID.text == "")
        {
            RandomID = UnityEngine.Random.Range(1000, 9999);
            SessionName = RandomID.ToString();
            Debug.Log("New Session");
        }

        SetSessionName();
        SceneManager.LoadScene("BaseScene"); 
    }

    public void SetSessionName()
    {
        string path = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;// + "SessionName.txt";
        }
        else
        {
            path = Application.dataPath;
            //path = path + "/Saves/SessionName.txt";
        }
        
        path = path + "SessionName.txt";
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path,true);
        writer.WriteLine(SessionName);
        writer.Close();
    }
}
