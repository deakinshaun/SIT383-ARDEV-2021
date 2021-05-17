using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControls : MonoBehaviour
{
    public GameObject VoiceControlsPanel;

    public GameObject ShowVoiceControlsButton;




    public void ShowVoiceControls()
    {
        VoiceControlsPanel.SetActive(true);
        ShowVoiceControlsButton.SetActive(false);
    }

    public void HideVoiceControls()
    {
        VoiceControlsPanel.SetActive(false);
        ShowVoiceControlsButton.SetActive(true);
    }
}
