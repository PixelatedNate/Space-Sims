using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PersonInfo {

    const string HeadPath = "ArtWork/People/Heads";
    const string BodyPath = "ArtWork/People/Bodys";

    const string MaleNamePath = "TextData/Names/People/male";
    const string FemaleNamePath = "TextData/Names/People/female";


    //  [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

   // [SerializeField]
    private Gender _gender;
    public Gender Gender { get { return _gender; } }

    [SerializeField]
    private float _s1;
    public float S1 { get { return Mathf.Floor(_s1); } set { S1 = value; } }

    // [SerializeField]
    public GameResources Upkeep { get; set; } = new GameResources { Food = 5 };
  //  [SerializeField]
    private Sprite _head;
    public Sprite Head { get { return _head; } }
    
 //   [SerializeField]
    private Sprite _body; 
    public Sprite Body { get { return _body; } }




    public void Randomize()
    {
        _gender = (Random.Range(0f, 1f) < 0.5f) ? Gender.Male : Gender.Female ;
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
        _head = GetRandomSpriteFromPath(HeadPath);
        _body = GetRandomSpriteFromPath(BodyPath);
    }


    private Sprite GetRandomSpriteFromPath(string path)
    {
        Sprite[] sprits = Resources.LoadAll<Sprite>(path);
        int index = Random.Range(0, sprits.Length);
        return sprits[index];
    }


}
