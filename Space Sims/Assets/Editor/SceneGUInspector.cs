using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomSceenEditor))]
    public class SceneGUInspector : Editor
    {

        void OnSceneGUI()
        {
            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(20, 20, 150, 60));

            var rect = EditorGUILayout.BeginVertical();
            GUI.color = Color.yellow;
            GUI.Box(rect, GUIContent.none);

            GUI.color = Color.white;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Save");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.red;

            if (GUILayout.Button("ClearSave"))
            {
            SaveSystem.ClearSave();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();


            GUILayout.EndArea();

            Handles.EndGUI();
        }
    }
