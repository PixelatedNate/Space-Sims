using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildRoomData), true)]
public class BuildRoomQuestDataEditor : QuestDataEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BuildRoomData roomQuestTarget  = (BuildRoomData)target;
        roomQuestTarget.roomToBuild = (RoomType)EditorGUILayout.EnumPopup("Room BluePrints", roomQuestTarget.roomToBuild);
    }
}
