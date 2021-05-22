/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(PulseEngineDriver), true)]
public class PulseEngineDriverEditor : Editor
{
    SerializedProperty stateFileProperty;   // serialized initial state file
    SerializedProperty timeStepProperty;
    SerializedProperty formatProperty;

    void OnEnable()
    {
        stateFileProperty = serializedObject.FindProperty("initialStateFile");
        timeStepProperty = serializedObject.FindProperty("timeStepProperty");
        formatProperty = serializedObject.FindProperty("serializationFormat");
    }

    public override void OnInspectorGUI()
    {
        // Ensure serialized properties are up to date with component
        serializedObject.Update();

        // Check that pulse data files are in the streaming asset folder
        var driver = target as PulseEngineDriver;
        string path = Application.streamingAssetsPath + "/" + driver.engineDataPath;
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        bool valid = false;
        if (directoryInfo.Exists)
        {
            DirectoryInfo[] pulseDirectories = directoryInfo.GetDirectories();
            bool ecg = false,
            environments = false,
            nutrition = false,
            substances = false;
            foreach (DirectoryInfo dir in pulseDirectories)
            {
                if (dir.Name.Equals("ecg"))
                    ecg = true;
                else if (dir.Name.Equals("environments"))
                    environments = true;
                else if (dir.Name.Equals("nutrition"))
                    nutrition = true;
                else if (dir.Name.Equals("substances"))
                    substances = true;
            }
            valid = ecg && environments && nutrition && substances;
        }
        if (!valid)
        {
            string message = "Missing data files expected at '" + path + "'.\n" +
                "Copy the '" + driver.engineDataPath + "' folder located in the " +
                "'StreamingAssets' of the Pulse asset to the top-level 'StreamingAssets' " +
                "folder of your Unity project (in 'Assets/StreamingAssets').";

            EditorGUILayout.HelpBox(message, MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
            return;
        }

        // Draw UI to select initial state file
        EditorGUILayout.PropertyField(stateFileProperty,
                                      new GUIContent("Initial State File"));
        var state = stateFileProperty.objectReferenceValue as TextAsset;
        if (state == null)
        {
            string message = "A state file is required to initialize the " +
                "Pulse engine. You can find an example file in 'Data/states' " +
                "or generate one by running the Pulse engine and save a " +
                "state to file (Unity will only accept '.txt' files).";
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            return;
        }

        // Show the default inspector property editor without the script field
        DrawPropertiesExcluding(serializedObject, "m_Script", "initialStateFile");

        // Apply modifications back to the component
        serializedObject.ApplyModifiedProperties();
    }
}
