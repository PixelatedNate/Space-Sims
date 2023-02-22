using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestData), true)]
public class QuestDataEditor : Editor
{
    bool showRewareds;
    bool ShowCloths = false;

    int clothingSelection = 0;

    Texture2D[] cloths;
    Sprite[] clothsSprites;

    string clothesPath = "Artwork/Clothes/Male/";

    public override void OnInspectorGUI()
    {

        QuestData questDataTarget = (QuestData)target;

        EditorUtility.SetDirty(questDataTarget);

        string fullPath = clothesPath + questDataTarget.reward.CLothRarity.ToString();

        cloths = GetTextureFromPath(fullPath);
        clothsSprites = GetSpriteFromPath(fullPath);

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

            EditorGUILayout.BeginHorizontal();
            questDataTarget.reward.ClothUnlock = EditorGUILayout.Toggle("Enable Cloth Rewared",questDataTarget.reward.ClothUnlock);
            EditorGUILayout.EndHorizontal();
            if (questDataTarget.reward.ClothUnlock)
            { 
                ShowCloths = EditorGUILayout.Foldout(ShowCloths, "Cloths");
                if (ShowCloths)
                {
                    questDataTarget.reward.CLothRarity = (ClothRarity)EditorGUILayout.EnumPopup("Rarity", questDataTarget.reward.CLothRarity);
                    questDataTarget.reward.RandomCloths = EditorGUILayout.Toggle("Random", questDataTarget.reward.RandomCloths);
                    if (!questDataTarget.reward.RandomCloths)
                    {
                        clothingSelection = GUILayout.SelectionGrid(clothingSelection, cloths, 6);
                        questDataTarget.reward.Cloth = clothsSprites[clothingSelection];
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.HelpBox("Leave as -1 if no dialog wanted", MessageType.Warning);
        questDataTarget.DialogIndex = EditorGUILayout.IntField("DialogAtEndOfQuest", questDataTarget.DialogIndex);


    }


    private Texture2D[] GetTextureFromPath(string path)
    {
        Texture2D[] sprits = Resources.LoadAll<Texture2D>(path);
        return sprits;
    }
    private Sprite[] GetSpriteFromPath(string path)
    {
        Sprite[] sprits = Resources.LoadAll<Sprite>(path);
        return sprits;
    }

}

