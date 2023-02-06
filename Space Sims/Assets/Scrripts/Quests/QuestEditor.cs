using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AbstractQuest), true)]
public class QuestEditor : Editor
{
    bool showRewareds;
    bool showRewaredsPeople;
    bool dropDown;

    string[] rewaredOptions = new string[] { "Minerals", "BluePrints", "Cosmetics" };

    List<int> RwaredType;
    int rewaredcounts = 0;

    public override void OnInspectorGUI()
    {

        AbstractQuest questTarget = (AbstractQuest)target;

        questTarget.Title = EditorGUILayout.TextField("Title test", questTarget.Title);
        EditorGUILayout.LabelField("Quest Discription");
        questTarget.Description = EditorGUILayout.TextArea(questTarget.Description);

        showRewareds = EditorGUILayout.Foldout(showRewareds, "Rewareds");

        if (showRewareds)
        {
            questTarget.reward.GameResourcesReward.Minerals = EditorGUILayout.IntField("Minerals", questTarget.reward.GameResourcesReward.Minerals);
            questTarget.reward.roomBlueprint = (RoomType)EditorGUILayout.EnumPopup("Room BluePrints", questTarget.reward.roomBlueprint);
            questTarget.reward.roomBlueprint = (RoomType)EditorGUILayout.EnumPopup("Cosmetics", questTarget.reward.roomBlueprint);

            questTarget.reward._numberofpeopleReward = EditorGUILayout.IntField("Number of people", questTarget.reward._numberofpeopleReward);


            // showRewaredsPeople = EditorGUILayout.Foldout(showRewaredsPeople,"people");


            /*
            bool pressed = GUILayout.Button("Add Person");
            if(pressed)
             {
                 AddRewared();
             }
             */
        }
        else
        {

        }
    }

    public void AddRewared()
    {
        Debug.Log("here");
        rewaredcounts++;
    }
}

