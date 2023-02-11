using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WaitingQuestData), true)]
public class WaittingQuestDataEditor : QuestDataEditor
{

    bool showRequiments;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        WaitingQuestData questDataTarget = (WaitingQuestData)target;

        EditorGUILayout.LabelField("Duration");
        int hours = EditorGUILayout.IntField("hh:", questDataTarget.DurationHour);
        questDataTarget.DurationHour = hours < 0 ? 0 : hours;
        int minuits = EditorGUILayout.IntField("mm:", questDataTarget.DurationMinuits);
        questDataTarget.DurationMinuits = minuits < 0 ? 0 : minuits;
        questDataTarget.DurationMinuits = minuits > 60 ? 60 : minuits;
        int seconds = EditorGUILayout.IntField("ss:", questDataTarget.DurationSeconds);
        questDataTarget.DurationSeconds = seconds < 0 ? 0 : seconds;
        questDataTarget.DurationSeconds = seconds > 60 ? 60 : seconds;


        showRequiments = EditorGUILayout.Foldout(showRequiments, "Requiments");
        if (showRequiments)
        {
            questDataTarget.QuestRequiments.Numpeople = EditorGUILayout.IntField("People", questDataTarget.QuestRequiments.Numpeople);
            questDataTarget.QuestRequiments.SkillRequiment = (SkillsList)EditorGUILayout.EnumPopup("Skill Requiments", questDataTarget.QuestRequiments.SkillRequiment);
            questDataTarget.QuestRequiments.skillValueMin = EditorGUILayout.IntField("Skill min", questDataTarget.QuestRequiments.skillValueMin);
        }
               
    }

}


