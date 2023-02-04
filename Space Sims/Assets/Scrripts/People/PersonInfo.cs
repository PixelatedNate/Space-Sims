using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/People/NewPerson", order = 1)]
public class PersonInfo : ScriptableObject
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
        public void SetSkill(SkillsList skill, float value)
        {
            if (skill == SkillsList.Strength)
            {
                _strength = value;
            }
            if (skill == SkillsList.Wisdom)
            {
                _wisdom = value;
            }
            if (skill == SkillsList.Dexterity)
            {
                _dexterity = value;
            }
            if (skill == SkillsList.Intelligence)
            {
                _intelligence = value;
            }
            if (skill == SkillsList.Charisma)
            {
                _charisma = value;
            }
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


    const string JobNamePath = "TextData/Names/Jobs";
    const string PlanetNamePath = "TextData/Names/Planets/PlanetNames";

    const string HobbiesNamePath = "TextData/Hobbies";

    //  [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    public Gender Gender { get; set; }


    public String HomePlanet;
    public String Job;
    public String[] Likes;
    public String[] Dislikes;

    public Race Race { get; set; }


    private short _age;
    public short Age { get { return _age; } }

    [SerializeField]
    public Skills skills = new Skills();

    // [SerializeField]
    public GameResources Upkeep { get; set; } = new GameResources { Food = -5 };
    public Sprite Head { get; set; } 
    public Sprite Body { get; set; }

    //   [SerializeField]
    private Sprite _hair;
    public Sprite Hair { get { return _hair; } }

    //   [SerializeField]
    //private Sprite _clothes;
    public Sprite Clothes { get; set; }

    private Color _skinColor;
    public Color SkinColor { get { return _skinColor; } }

    private Color _hairColor;

    public Color HairColor { get { return _hairColor; } }


    private WaittingQuest AssginedQuest = null; // is a person aassgined to a quest that hasn't started yet

    public AbstractQuest CurrentQuest { get; set; } = null; // is a person on a quest that is in progregress or part of a transprot quest;
                                                            //   public bool isCargoForTransprotQuest { get }
    public bool IsCargoForTransportQust
    {
        get
        {
            if (CurrentQuest != null)
            {
                return CurrentQuest .GetType() == typeof(TransportQuest);
            }
            else return false;
        }
    }

    public bool IsQuesting
    {
        get
        {
            if (CurrentQuest != null)
            {
                return CurrentQuest.GetType() == typeof(WaittingQuest);
            }
            else return false;
        }
    }


    public Person PersonMonoBehaviour { get; set; }

    private AbstractRoom _room;
    public AbstractRoom Room { get; set; }


    public void Randomize()
    {
        Gender = (Random.Range(0f, 1f) < 0.5f) ? Gender.Male : Gender.Female;
        Race = (Race)Random.Range(0, 10);
        _age = (short)Random.Range(20, 80);
        RandomizeTextValues();
        RandomizeAppereance();
        RandomizeSkills();
    }


    public void FirstTimeIntilize()
    {
        _skinColor = PersonSkin.GetRandomColor(Race);
    }


    public void SetAsCargoForTransprotQuest(TransportQuest quest)
    {
        CurrentQuest = quest;
    }

    public void AssignQuest(WaittingQuest quest)
    {
        if (AssginedQuest != null)
        {
            if (AssginedQuest == quest)
            {
                Debug.LogWarning("Assgineding a person to a quest they are allready Assigend to");
            }
            AssginedQuest.UnassginPerson(this);
        }
        AssginedQuest = quest;
    }
    public void StartQuest(WaittingQuest quest)
    {
        if (CurrentQuest != null)
        {
            throw new Exception("Trying to start a quest on a person when a quest is allready active/inprogress");
        }
        else
        {
            GameObject.Destroy(PersonMonoBehaviour.gameObject);
            Room.RemoveWorker(PersonMonoBehaviour);
            Room = GlobalStats.Instance.QuestRoom;
            CurrentQuest = quest;
        }
    }
    public void CompleteQuest(Skills skills)
    {
        CurrentQuest = null;
        GameObject newTemplate = PrefabSpawner.Instance.SpawnPerson();
        newTemplate.transform.position = Room.transform.position;
        PersonMonoBehaviour = newTemplate.GetComponent<Person>();
        PersonMonoBehaviour.AssginPerson(this);

    }

    private void RandomizeTextValues()
    {
        string namePath = (Gender == Gender.Male) ? MaleNamePath : FemaleNamePath;
        Name = PickFromListAtRandom(namePath)[0];
        HomePlanet = PickFromListAtRandom(PlanetNamePath)[0];
        Job = PickFromListAtRandom(JobNamePath)[0];

        int numberofHobbies = Random.Range(1, 3);
        int numberofDislikeHobbies = Random.Range(1, 3);

        Likes = PickFromListAtRandom(HobbiesNamePath, numberofHobbies);
        Dislikes = PickFromListAtRandom(HobbiesNamePath, numberofDislikeHobbies);
    }

    private String[] PickFromListAtRandom(String path, int number = 1)
    {
        string[] selectedValues = new string[number];
        TextAsset rawFile = Resources.Load<TextAsset>(path);
        string[] values = rawFile.text.Split('\n');

        int[] selectedValuesIndex = new int[number];
        for (int i = 0; i < number; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, values.Length);
            } while (Array.Exists(selectedValuesIndex, element => element == index));

            selectedValues[i] = values[index];
        }
        return selectedValues;

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
            HeadPath = "ArtWork/People/" + Race + "/Female/Heads";
            BodyPath = "ArtWork/People/" + Race + "/Female/Bodies";
            hairPath = "Artwork/Hair/Female";
            clothesPath = "Artwork/Clothes/Female";
        }
        else
        {
            HeadPath = "ArtWork/People/" + Race + "/Male/Heads";
            BodyPath = "ArtWork/People/" + Race + "/Male/Bodies";
            hairPath = "Artwork/Hair/Male";
            clothesPath = "Artwork/Clothes/Male";
        }


        Head = GetRandomSpriteFromPath(HeadPath);
        Body = GetRandomSpriteFromPath(BodyPath);
        _hair = GetRandomSpriteFromPath(hairPath);
        Clothes = GetRandomSpriteFromPath(clothesPath);
        _skinColor = PersonSkin.GetRandomColor(Race);
        _hairColor = GetRandomColor();


    }

    public void SetCloths(Sprite cloths)
    {
        cloths = cloths;
        if (PersonMonoBehaviour != null)
        {
            PersonMonoBehaviour.ReRenderPerson();
        }
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
