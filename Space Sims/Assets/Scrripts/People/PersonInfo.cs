using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PersonInfo
{


    [Serializable]
    public class Skills
    {

        [SerializeField]
        private float _strength = 2;
        public float Strength { get { return Mathf.Floor(_strength); } set { _strength = value; } }

        [SerializeField]
        private float _dexterity = 2;
        public float Dexterity { get { return Mathf.Floor(_dexterity); } set { _dexterity = value; } }
        [SerializeField]
        private float _intelligence = 2;
        public float Intelligence { get { return Mathf.Floor(_intelligence); } set { _intelligence = value; } }

        [SerializeField]
        private float _wisdom = 2;
        public float Wisdom { get { return Mathf.Floor(_wisdom); } set { _wisdom = value; } }

        [SerializeField]
        private float _charisma = 2;
        public float Charisma { get { return Mathf.Floor(_charisma); } set { _charisma = value; } }

        private float _s2 = 2;
        public float S2 { get { return Mathf.Floor(_s2); } set { S2 = value; } }

        public float GetSkill(SkillsList skill)
        {
            if (skill == SkillsList.Strength)
            {
                return Strength;
            }
            if (skill == SkillsList.Wisdom)
            {
                return Wisdom;
            }
            if (skill == SkillsList.Dexterity)
            {
                return Dexterity;
            }
            if (skill == SkillsList.Intelligence)
            {
                return Intelligence;
            }
            if (skill == SkillsList.Charisma)
            {
                return Charisma;
            }
            else return 0;
        }

        public void AddToSkill(SkillsList skill, float value)
        {
            if (skill == SkillsList.Strength)
            {
                Strength += value;
            }
            if (skill == SkillsList.Wisdom)
            {
                Wisdom += value;
            }
        }

        #region Comparators
        public static bool operator >(Skills a, Skills b)
        {
            return ((a.Strength > b.Strength) &&
                    (a.Dexterity > b.Dexterity) &&
                    (a.Intelligence > b.Intelligence) &&
                    (a.Wisdom > b.Wisdom) &&
                    (a.Charisma > b.Charisma));
        }

        public static bool operator <(Skills a, Skills b)
        {
            return ((a.Strength < b.Strength) &&
                    (a.Dexterity < b.Dexterity) &&
                    (a.Intelligence < b.Intelligence) &&
                    (a.Wisdom < b.Wisdom) &&
                    (a.Charisma < b.Charisma));
        }

        public static bool operator ==(Skills a, Skills b)
        {
            return ((a.Strength == b.Strength) &&
                    (a.Dexterity == b.Dexterity) &&
                    (a.Intelligence == b.Intelligence) &&
                    (a.Wisdom == b.Wisdom) &&
                    (a.Charisma == b.Charisma));
        }

        public static bool operator !=(Skills a, Skills b)
        {
            return ((a.Strength != b.Strength) &&
                    (a.Dexterity != b.Dexterity) &&
                    (a.Intelligence != b.Intelligence) &&
                    (a.Wisdom != b.Wisdom) &&
                    (a.Charisma != b.Charisma));
        }

        public static bool operator <=(Skills a, Skills b)
        {
            return ((a.Strength < b.Strength) || (a.Strength == b.Strength) &&
                    (a.Dexterity < b.Dexterity) || (a.Dexterity == b.Dexterity) &&
                    (a.Intelligence < b.Intelligence) || (a.Intelligence == b.Intelligence) &&
                    (a.Wisdom < b.Wisdom) || (a.Wisdom == b.Wisdom) &&
                    (a.Charisma < b.Charisma) || (a.Charisma == b.Charisma));
        }
        public static bool operator >=(Skills a, Skills b)
        {
            return ((a.Strength > b.Strength) || (a.Strength == b.Strength) &&
                    (a.Dexterity > b.Dexterity) || (a.Dexterity == b.Dexterity) &&
                    (a.Intelligence > b.Intelligence) || (a.Intelligence == b.Intelligence) &&
                    (a.Wisdom > b.Wisdom) || (a.Wisdom == b.Wisdom) &&
                    (a.Charisma > b.Charisma) || (a.Charisma == b.Charisma));
        }

        #endregion

    }


    string HeadPath;
    string BodyPath;

    string hairPath;
    string clothesPath;

    const string MaleNamePath = "TextData/Names/People/male";
    const string FemaleNamePath = "TextData/Names/People/female";






    //  [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    // [SerializeField]
    private Gender _gender;
    public Gender Gender { get { return _gender; } }

    private Race _race;
    public Race Race { get { return _race; } }


    private short _age;
    public short Age { get { return _age; } }

    [SerializeField]
    public Skills skills = new Skills();

    // [SerializeField]
    public GameResources Upkeep { get; set; } = new GameResources { Food = -5 };
    //  [SerializeField]
    private Sprite _head;
    public Sprite Head { get { return _head; } }

    //   [SerializeField]
    private Sprite _body;
    public Sprite Body { get { return _body; } }

    //   [SerializeField]
    private Sprite _hair;
    public Sprite Hair { get { return _hair; } }

    //   [SerializeField]
    private Sprite _clothes;
    public Sprite Clothes { get { return _clothes; } }

    private Color _skinColor;
    public Color SkinColor { get { return _skinColor; } }

    private Color _hairColor;

    public Color HairColor { get { return _hairColor; } }

    public bool IsQuesting { get { return CurrentQuest != null; } }
    public Quest CurrentQuest { get; set; }

    public Person PersonMonoBehaviour { get; set; }

    private AbstractRoom _room;
    public AbstractRoom Room { get; set; }


    public void Randomize()
    {
        _gender = (Random.Range(0f, 1f) < 0.5f) ? Gender.Male : Gender.Female;
        _race = (Race)Random.Range(0, 10);
        _age = (short)Random.Range(20, 80);
        RandomizeName();
        RandomizeAppereance();
        RandomizeSkills();
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
        skills.Strength = Random.Range(1, 11);
        skills.Intelligence = Random.Range(1, 11);
        skills.Wisdom = Random.Range(1, 11);
        skills.Charisma = Random.Range(1, 11);
        skills.Dexterity = Random.Range(1, 11);
    }

    private void RandomizeAppereance()
    {
        if (Gender == Gender.Female)
        {
            HeadPath = "ArtWork/People/" + _race + "/Female/Heads";
            BodyPath = "ArtWork/People/" + _race + "/Female/Bodies";
            hairPath = "Artwork/Hair/Female";
            clothesPath = "Artwork/Clothes/Female";
        }
        else
        {
            HeadPath = "ArtWork/People/" + _race + "/Male/Heads";
            BodyPath = "ArtWork/People/" + _race + "/Male/Bodies";
            hairPath = "Artwork/Hair/Male";
            clothesPath = "Artwork/Clothes/Male";
        }


        _head = GetRandomSpriteFromPath(HeadPath);
        _body = GetRandomSpriteFromPath(BodyPath);
        _hair = GetRandomSpriteFromPath(hairPath);
        _clothes = GetRandomSpriteFromPath(clothesPath);
        _skinColor = PersonSkin.GetRandomColor(Race);
        _hairColor = GetRandomColor();

        //      Debug.Log("I have produced a " + Gender.ToString() + " " + _race);
    }


    private Sprite GetRandomSpriteFromPath(string path)
    {
        Sprite[] sprits = Resources.LoadAll<Sprite>(path);
        int index = Random.Range(0, sprits.Length);
        return sprits[index];
    }

    private Color GetRandomColor()
    {
        Color returnColor = new Color();
        returnColor = Random.ColorHSV(0f, 1f, 0.2f, 1f, 0.6f, 1f, 1f, 1f);
        return returnColor;
    }

}
