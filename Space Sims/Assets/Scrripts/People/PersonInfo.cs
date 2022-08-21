using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo : ScriptableObject
{

    const string HeadPath = "ArtWork/People/Heads";
    const string BodyPath = "ArtWork/People/Bodys";

    const string MaleNamePath = "TextData/Names/People/male";
    const string FemaleNamePath = "TextData/Names/People/female";

    public string Name;
    public Gender Gender;
    public float s1;
    public float s2;
    public float s3;
    public float s4; 
    public float s5;
    public float s6;
   public Sprite Head;
   public Sprite Body; 



    public void Randomize()
    {
        Gender = (Random.Range(0f, 1f) < 0.5f) ? Gender.Male : Gender.Female ;
        RandomizeName();
        RandomizeAppereance();
    }


    private void RandomizeName()
    {
        string namePath = (Gender == Gender.Male) ? MaleNamePath : FemaleNamePath;
        TextAsset rawNamesFile = Resources.Load<TextAsset>(namePath);
        string[] names = rawNamesFile.text.Split('\n');
        int index = Random.Range(0, names.Length);
        Name = names[index];

    }

    private void RandomizeSkills()
    {

    }

    private void RandomizeAppereance()
    {
        Head = GetRandomSpriteFromPath(HeadPath);
        Body = GetRandomSpriteFromPath(BodyPath);


    }


    private Sprite GetRandomSpriteFromPath(string path)
    {
        Sprite[] sprits = Resources.LoadAll<Sprite>(path);
        int index = Random.Range(0, sprits.Length);
        return sprits[index];
    }


}
