using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TransportQuestData), true)]
public class TransportQuestDataEditor : QuestDataEditor
{

    bool showRequiments;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        TransportQuestData questDataTarget = (TransportQuestData)target;
        var m_IntProperty = serializedObject.FindProperty("TargetPlanetData");
            
        EditorGUILayout.PropertyField(m_IntProperty, includeChildren: true);

        serializedObject.ApplyModifiedProperties();

               
    }

}


