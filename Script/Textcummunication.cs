using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    public Dropdown dropdown;
    public InputField inputField;
    public Text talkText;
    string title = "";
    void Start()
    {
        ChangePlayer(0);
        talkText.text = "";
        LayoutRebuilder.ForceRebuildLayoutImmediate(talkText.transform.parent.GetComponent<RectTransform>());
    }

    public void ChangePlayer(int v)
    {
        title = "[" + dropdown.options[v].text + "]:";
    }

    public void Send()
    {

        string send = title + inputField.text;
        inputField.text = "";
        if (talkText.text.Equals(""))
        {
            talkText.text += send;
        }
        else
        {
            talkText.text += "\n" + send;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(talkText.transform.parent.GetComponent<RectTransform>());
    }
}
