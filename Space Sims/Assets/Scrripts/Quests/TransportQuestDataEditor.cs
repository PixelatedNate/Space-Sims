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

        questDataTarget.reward.GameResourcesReward.Minerals = EditorGUILayout.IntField("Minerals", questDataTarget.reward.GameResourcesReward.Minerals);
        questDataTarget.reward.GameResourcesReward.Minerals = questDataTarget.reward.GameResourcesReward.Minerals < 0 ? 0 : questDataTarget.reward.GameResourcesReward.Minerals;
        questDataTarget.reward.roomBlueprint = (RoomType)EditorGUILayout.EnumPopup("Room BluePrints", questDataTarget.reward.roomBlueprint);
            
        EditorGUILayout.PropertyField(m_IntProperty, includeChildren: true);

        serializedObject.ApplyModifiedProperties();

               
    }

}


