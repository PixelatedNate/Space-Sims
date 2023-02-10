using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(QuestData), true)]
public class QuestDataEditor : Editor
{
    bool showRewareds;
    bool roomBluePrints = false;

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

            RoomType selectedRoomBluePrint;
            selectedRoomBluePrint = questDataTarget.reward.roomBlueprint != null ? (RoomType)questDataTarget.reward.roomBlueprint : RoomType.Food;

            EditorGUILayout.BeginHorizontal();

            roomBluePrints = EditorGUILayout.Toggle("Blueprint", roomBluePrints);
            if (roomBluePrints)
            {
                questDataTarget.reward.roomBlueprint = (RoomType)EditorGUILayout.EnumPopup("Room BluePrints", selectedRoomBluePrint);
            }
            else
            {
                questDataTarget.reward.roomBlueprint = null;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(m_IntProperty, includeChildren: true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

