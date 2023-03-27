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
        GUILayout.Label("Save Tools");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("ClearSave"))
        {
            SaveSystem.ClearSave();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;

        if (GUILayout.Button("Load From After Toutoriol"))
        {
            SaveSystem.LoadPostToutorial();
        }

        GUILayout.EndHorizontal();


        EditorGUILayout.EndVertical();


        GUILayout.EndArea();

        Handles.EndGUI();
    }
}
