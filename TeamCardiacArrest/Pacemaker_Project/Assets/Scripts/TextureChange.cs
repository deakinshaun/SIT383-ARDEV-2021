using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TextureChange : MonoBehaviour
{
    // Class will be placed in the 360 cube prefab.
    // On Awake user will be prompted if they're with a tutor or solo
    // Texture will change depending on selection, activating the photon script to join that room

    private GameObject RoomSelection;

    public GameObject displayObject;
    public Material texture;

    public Texture[] roomTextures;

    void Awake()
    {

        //Make room selection panel visible
        RoomSelection.SetActive(true);
    }
}
