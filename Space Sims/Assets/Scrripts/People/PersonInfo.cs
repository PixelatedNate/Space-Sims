using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PersonInfo {


    [Serializable]
    public class Skills
    {
        [SerializeField]
        private float _s1 = 2;
        public float S1 { get { return Mathf.Floor(_s1); } set { S1 = value; } }
        
        [SerializeField]
        private float _s2 = 2;
        public float S2 { get { return Mathf.Floor(_s2); } set { S2 = value; } }
        
        public float GetSkill(SkillsList skill)
        {
            if(skill == SkillsList.s1)
            {
                return S1;
            }
            if (skill == SkillsList.s2)
            {
                return S2;
            }
            else return 0;         
        }

        public void AddToSkill(SkillsList skill,  float value)
        {
            if (skill == SkillsList.s1)
            {
                S1 += value;
            }
            if (skill == SkillsList.s2)
            {
                S2 += value;
            }
        }

        #region Comparators
        public static bool operator >(Skills a, Skills b)
        {
            return ((a.S1 > b.S2) &&
                    (a.S2 > b.S2));
        }

        public static bool operator <(Skills a, Skills b)
        {
            return ((a.S2 < b.S1) &&
                     (a.S2 < b.S2));
        }

        public static bool operator ==(Skills a, Skills b)
        {
            return ((a.S1 == b.S1) &&
                     (a.S2 == b.S2));
        }

        public static bool operator !=(Skills a, Skills b)
        {
            return ((a.S1 != b.S1) &&
                    (a.S2 != b.S2));
        }

        public static bool operator <=(Skills a, Skills b)
        {
            return (a < b || a == b);
        }
        public static bool operator >=(Skills a, Skills b)
        {
            return (a > b || a == b);
        }

        #endregion

    }


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
    public Skills skills = new Skills();

    // [SerializeField]
    public GameResources Upkeep { get; set; } = new GameResources { Food = 5 };
  //  [SerializeField]
    private Sprite _head;
    public Sprite Head { get { return _head; } }
    
 //   [SerializeField]
    private Sprite _body; 
    public Sprite Body { get { return _body; } }

    public bool IsQuesting { get { return CurrentQuest != null; } }
    public Quest CurrentQuest { get; set; }

    public Person PersonMonoBehaviour { get; set; }



    private Room _room;

    public Room Room { get; set; }
    public void Randomize()
    {
        _gender = (Random.Range(0f, 1f) < 0.5f) ? Gender.Male : Gender.Female ;
        RandomizeName();
        RandomizeAppereance();
    }


    public void StartQuest(Quest quest)
    {
        if (CurrentQuest != null)
        {
            throw new Exception("Trying to start a quest on a person when a quest is allready active/inprogress");
        }
        else
        {
            GameObject.Destroy(PersonMonoBehaviour.gameObject);
            CurrentQuest = quest;
        }
    }
    public void CompleteQuest(Skills skills)
    {
        CurrentQuest = null;
        GameObject newTemplate = PrefabSpawner.Instance.SpawnPerson();
        PersonMonoBehaviour = newTemplate.GetComponent<Person>();
        PersonMonoBehaviour.AssginPerson(this);

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
