using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Tooltip("Connect this to the newECGDisplay component in the Canvas")]
    public GameObject ECG_Reference;

    public Button heartButton;
    public Button atrialButton;
    public Button ventricleButton;

    [Tooltip("Connect this to the btnNewGame component in the Canvas")]
    public GameObject goAgain;
    [Tooltip("Connect this to the SyncCompleteText component in the Canvas")]
    public GameObject endGameText;

    private bool b_Heart = false;
    private bool b_Atrial = false;
    private bool b_Ventricle = false;

    private Color colorHeart = new Color32(234, 82, 211, 255);
    private Color colorAtrial = new Color32(252, 175, 56, 255);
    private Color colorVentricle = new Color32(249, 83, 53, 255);

    // Start is called before the first frame update
    void Start()
    {
        endGameText.SetActive(false);
        goAgain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        b_Heart = ECG_Reference.GetComponent<EcgVisualiser>().GetHeartSync();
        b_Atrial = ECG_Reference.GetComponent<EcgVisualiser>().GetAtrialSync();
        b_Ventricle = ECG_Reference.GetComponent<EcgVisualiser>().GetVentricleSync();

        if (b_Heart) heartButton.GetComponent<Image>().color = Color.green;
        else heartButton.GetComponent<Image>().color = colorHeart;

        if (b_Atrial) atrialButton.GetComponent<Image>().color = Color.green;
        else atrialButton.GetComponent<Image>().color = colorAtrial;

        if (b_Ventricle) ventricleButton.GetComponent<Image>().color = Color.green;
        else ventricleButton.GetComponent<Image>().color = colorVentricle;

        if (b_Heart && b_Atrial && b_Ventricle)
        {
            EndOfGame();
        }
    }

    public void EndOfGame()
    {
        endGameText.SetActive(true);
        goAgain.SetActive(true);
    }

    public void OnClickRestart()
    {
        ECG_Reference.GetComponent<EcgVisualiser>().GameReset();
        goAgain.SetActive(false);
    }
}
