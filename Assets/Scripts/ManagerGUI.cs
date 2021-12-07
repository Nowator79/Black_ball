using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Manager))]
public class ManagerGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Manager manager = (Manager)target;
        if (GUILayout.Button("Created XML Level"))
        {
            manager.CreatedXMLLevel();
        }
    }
}
