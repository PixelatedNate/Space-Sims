using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PersonTemplate), true)]
public class PersonTemplateEditor : Editor
{

    string clothesPath = "Artwork/Clothes/Male/Basic";

    Texture2D HeadTexture;
    Texture2D BodyTexture;
    Texture2D ClothingTexture;

    Texture2D[] heads, bodies, cloths;
    Sprite[] headSprites, bodySprites, clothsSprites;


    bool ShowHeads = false;
    bool ShowCloths = false;
    bool showBodies = false;
    bool showSkills = false;

    int HeadSelection = 0;
    int BodySelection = 0;
    int clothingSelection = 0;



#warning Do not forget to refactor code to use ResourceHelper.

    public override void OnInspectorGUI()
    {


        PersonTemplate personTemplateTarget = (PersonTemplate)target;
        EditorUtility.SetDirty(personTemplateTarget);

        string HeadPath = "ArtWork/People/" + personTemplateTarget.Race + "/" + personTemplateTarget.Gender + "/Heads";
        string BodyPath = "ArtWork/People/" + personTemplateTarget.Race + "/" + personTemplateTarget.Gender + "/Bodies";


        heads = GetTextureFromPath(HeadPath);
        headSprites = GetSpriteFromPath(HeadPath);
        HeadSelection = FindMatchingIndex(personTemplateTarget.Head, headSprites);
        HeadTexture = heads[HeadSelection];

        bodies = GetTextureFromPath(BodyPath);
        bodySprites = GetSpriteFromPath(BodyPath);
        BodySelection = FindMatchingIndex(personTemplateTarget.Body, bodySprites);
        BodyTexture = bodies[BodySelection];

        cloths = GetTextureFromPath(clothesPath);
        clothsSprites = GetSpriteFromPath(clothesPath);
        clothingSelection = FindMatchingIndex(personTemplateTarget.Clothes, clothsSprites);
        ClothingTexture = cloths[clothingSelection];



        GUI.DrawTexture(new Rect(50, 0, 256, 256), HeadTexture);
        GUI.DrawTexture(new Rect(50, 0, 256, 256), BodyTexture);
        GUI.DrawTexture(new Rect(50, 0, 256, 256), ClothingTexture);


        GUILayout.Space(200);

        #region Name
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(personTemplateTarget.RandomName);
        personTemplateTarget.PersonName = EditorGUILayout.TextField("Name", personTemplateTarget.PersonName);
        EditorGUI.EndDisabledGroup();
        personTemplateTarget.RandomName = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomName);
        EditorGUILayout.EndHorizontal();
        #endregion


        #region Race
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(personTemplateTarget.RandomRace);
        personTemplateTarget.Race = (Race)EditorGUILayout.EnumPopup("Race", personTemplateTarget.Race);
        EditorGUI.EndDisabledGroup();
        personTemplateTarget.RandomRace = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomRace);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Gender
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(personTemplateTarget.RandomGender);
        personTemplateTarget.Gender = (Gender)EditorGUILayout.EnumPopup("Gender", personTemplateTarget.Gender);
        EditorGUI.EndDisabledGroup();
        personTemplateTarget.RandomGender = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomGender);
        EditorGUILayout.EndHorizontal();
        #endregion


        #region Age
        float minAge = personTemplateTarget.MinAge;
        float maxAge = personTemplateTarget.MaxAge;
        EditorGUILayout.LabelField("Age min val", minAge.ToString());
        EditorGUILayout.LabelField("Age max val", maxAge.ToString());
        EditorGUILayout.MinMaxSlider("Age", ref minAge, ref maxAge, 16, 100);
        personTemplateTarget.MinAge = (short)minAge;
        personTemplateTarget.MaxAge = (short)maxAge;
        #endregion

        #region Head
        EditorGUILayout.BeginHorizontal();
        ShowHeads = EditorGUILayout.Foldout(ShowHeads, "Head");
        EditorGUI.BeginDisabledGroup(personTemplateTarget.RandomRace || personTemplateTarget.RandomGender);
        if (personTemplateTarget.RandomRace || personTemplateTarget.RandomGender)
        {
            personTemplateTarget.RandomHead = true;
        }
        personTemplateTarget.RandomHead = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomHead);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
        if (ShowHeads && !personTemplateTarget.RandomHead)
        {
            HeadSelection = GUILayout.SelectionGrid(HeadSelection, heads, 6);
            personTemplateTarget.Head = headSprites[HeadSelection];
        }
        #endregion

        #region Body
        EditorGUILayout.BeginHorizontal();
        showBodies = EditorGUILayout.Foldout(showBodies, "Body");
        EditorGUI.BeginDisabledGroup(personTemplateTarget.RandomRace || personTemplateTarget.RandomGender);
        if (personTemplateTarget.RandomRace || personTemplateTarget.RandomGender)
        {
            personTemplateTarget.RandomBody = true;
        }
        personTemplateTarget.RandomBody = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomBody);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
        if (showBodies && !personTemplateTarget.RandomBody)
        {
            BodySelection = GUILayout.SelectionGrid(BodySelection, bodies, 6);
            personTemplateTarget.Body = bodySprites[BodySelection];
        }
        #endregion

        #region cloths
        EditorGUILayout.BeginHorizontal();
        ShowCloths = EditorGUILayout.Foldout(ShowCloths, "Cloths");
        personTemplateTarget.RandomCloths = EditorGUILayout.Toggle("Random", personTemplateTarget.RandomCloths);
        EditorGUILayout.EndHorizontal();
        if (ShowCloths && !personTemplateTarget.RandomCloths)
        {
            clothingSelection = GUILayout.SelectionGrid(clothingSelection, cloths, 6);
            personTemplateTarget.Clothes = clothsSprites[clothingSelection];
        }
        #endregion


        #region Skills
        showSkills = EditorGUILayout.Foldout(showSkills, "skills");
        if (showSkills)
        {
            var skills = Enum.GetValues(typeof(SkillsList));


            foreach (SkillsList skill in skills)
            {

                float minSkill = personTemplateTarget.MinSkills.GetSkill(skill);
                float maxSkill = personTemplateTarget.MaxSkills.GetSkill(skill);
                EditorGUILayout.LabelField(skill.ToString() + "min val", minSkill.ToString());
                EditorGUILayout.LabelField(skill.ToString() + "max val", maxSkill.ToString());
                EditorGUILayout.MinMaxSlider(skill.ToString(), ref minSkill, ref maxSkill, 0, 10);

                personTemplateTarget.MinSkills.SetSkill(skill, minSkill);
                personTemplateTarget.MaxSkills.SetSkill(skill, maxSkill);
            }
        }

        #endregion
    }

    public int FindMatchingIndex(Sprite sprite, Sprite[] list)
    {
        if (sprite == null)
        {
            return 0;
        }
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].texture == sprite.texture)
            {
                return i;
            }
        }
        return 0;

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
