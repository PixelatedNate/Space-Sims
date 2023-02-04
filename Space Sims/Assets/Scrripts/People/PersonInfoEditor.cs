using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PersonInfo), true)]
public class PersonInfoEditor : Editor
{

    string clothesPath = "Artwork/Clothes/Male";

    Texture2D HeadTexture;
    Texture2D BodyTexture;
    Texture2D ClothingTexture;


    bool ShowHeads   = false;
    bool ShowCloths  = false;
    bool showBodies = false;
    bool showSkills = false;

    int HeadSelection = 0;
    int BodySelection = 0;
    int clothingSelection = 0;

    public override void OnInspectorGUI()
    {
       PersonInfo personInfoTarget = (PersonInfo)target;

      
       string HeadPath = "ArtWork/People/" + personInfoTarget.Race + "/" + personInfoTarget.Gender + "/Heads";
       string BodyPath = "ArtWork/People/" + personInfoTarget.Race + "/" + personInfoTarget.Gender + "/Bodies";



        Texture2D[] heads = GetTextureFromPath(HeadPath);
        Sprite[] headSprites = GetSpriteFromPath(HeadPath);
        HeadSelection = FindMatchingIndex(personInfoTarget.Head, headSprites);
        HeadTexture = heads[HeadSelection];

        Texture2D[] bodies = GetTextureFromPath(BodyPath);
        Sprite[] bodySprites = GetSpriteFromPath(BodyPath);
        BodySelection = FindMatchingIndex(personInfoTarget.Body, bodySprites);
        BodyTexture = bodies[BodySelection];

        Texture2D[] cloths = GetTextureFromPath(clothesPath);
        Sprite[] clothsSprites = GetSpriteFromPath(clothesPath);
        clothingSelection = FindMatchingIndex(personInfoTarget.Clothes, clothsSprites);
        ClothingTexture = cloths[clothingSelection];


        GUI.DrawTexture(new Rect(50, 0, 256, 256), HeadTexture);
        GUI.DrawTexture(new Rect(50, 0, 256, 256),BodyTexture);
        GUI.DrawTexture(new Rect(50, 0, 256, 256),ClothingTexture);


        GUILayout.Space(200);


       personInfoTarget.Race = (Race)EditorGUILayout.EnumPopup("Race", personInfoTarget.Race);
       personInfoTarget.Gender = (Gender)EditorGUILayout.EnumPopup("Gender", personInfoTarget.Gender);

        bool RandomizeAll = GUILayout.Button("Randomize All");

        ShowHeads = EditorGUILayout.Foldout(ShowHeads,"Head");
        if(ShowHeads) {
            HeadSelection = GUILayout.SelectionGrid(HeadSelection, heads, 6);
            personInfoTarget.Head = headSprites[HeadSelection];
        }
 
        showBodies= EditorGUILayout.Foldout(showBodies,"Body");
        if (showBodies)
        {
            BodySelection = GUILayout.SelectionGrid(BodySelection, bodies, 6);
            personInfoTarget.Body = bodySprites[BodySelection];
        }

 
        ShowCloths = EditorGUILayout.Foldout(ShowCloths,"Cloths");
        if (ShowCloths)
        {
            clothingSelection = GUILayout.SelectionGrid(clothingSelection, cloths, 6);
            personInfoTarget.Clothes = clothsSprites[clothingSelection];
        }

        showSkills = EditorGUILayout.Foldout(showSkills,"skills");
        if (showSkills)
        {
           var skills = Enum.GetValues(typeof(SkillsList));


            foreach (SkillsList skill in skills)
            {
                var value = EditorGUILayout.IntSlider(skill.ToString(), (int)personInfoTarget.skills.GetSkill(skill), 0, 10);
                personInfoTarget.skills.SetSkill(skill, value);
            }

        }


    }



    public int FindMatchingIndex(Sprite sprite, Sprite[] list)
    {
        if(sprite == null)
        {
            return 0;
        }
        for(int i = 0; i < list.Length; i++)
        {
            if(list[i].texture == sprite.texture)
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
