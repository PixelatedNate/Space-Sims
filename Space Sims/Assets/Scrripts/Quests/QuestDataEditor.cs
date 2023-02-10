using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(QuestData), true)]
public class QuestDataEditor : Editor
{
    bool showRewareds;

    public override void OnInspectorGUI()
    {

        QuestData questDataTarget = (QuestData)target;

         questDataTarget.Title = EditorGUILayout.TextField("Quest Title", questDataTarget.Title);
         EditorGUILayout.LabelField("Quest Discription");
         questDataTarget.Description = EditorGUILayout.TextArea(questDataTarget.Description);

         showRewareds = EditorGUILayout.Foldout(showRewareds, "Rewareds");

        if (showRewareds)
        {

            var m_IntProperty = serializedObject.FindProperty("reward").FindPropertyRelative("people");

            questDataTarget.reward.GameResourcesReward.Minerals = EditorGUILayout.IntField("Minerals", questDataTarget.reward.GameResourcesReward.Minerals);
            questDataTarget.reward.GameResourcesReward.Minerals = questDataTarget.reward.GameResourcesReward.Minerals < 0 ? 0 : questDataTarget.reward.GameResourcesReward.Minerals;

            EditorGUILayout.BeginHorizontal();
            questDataTarget.reward.roomBlueprintUnlock = EditorGUILayout.Toggle("Blueprint", questDataTarget.reward.roomBlueprintUnlock);
            if (questDataTarget.reward.roomBlueprintUnlock)
            {
                questDataTarget.reward.roomBlueprint = (RoomType)EditorGUILayout.EnumPopup("Room BluePrints", questDataTarget.reward.roomBlueprint);
            }
            else
            {
                questDataTarget.reward.roomBlueprint = RoomType.CrewQuaters;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(m_IntProperty, includeChildren: true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

