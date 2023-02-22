using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonInfo : ISaveable<PersonSaveData>
{

    string HeadPath;
    string BodyPath;

    string hairPath;
    string clothesPath;




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
    public short Age { get; set; }
    [SerializeField]
    public Skills skills = new Skills();
    public GameResources Upkeep { get; set; } = new GameResources { Food = -5 };
    public Sprite Head { get; set; }
    public Sprite Body { get; set; }
    public Sprite Hair { get; set; }
    public Sprite Clothes { get; set; }

    public Color SkinColor { get; set; }
    public Color HairColor { get; set; }


    private WaittingQuest AssginedQuest = null; // is a person aassgined to a quest that hasn't started yet

    public AbstractQuest CurrentQuest { get; set; } = null; // is a person on a quest that is in progregress or part of a transprot quest;
                                                            //   public bool isCargoForTransprotQuest { get }
    public bool IsCargoForTransportQust
    {
        get
        {
            if (CurrentQuest != null)
            {
                return CurrentQuest.GetType() == typeof(TransportQuest);
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

    public AbstractRoom Room { get; set; }

    public PersonInfo(PersonSaveData saveData)
    {
        SaveSystem.LoadedPeople.Add(saveData.personId, this);


        this.Name = saveData.PersonName;
        this.HomePlanet = saveData.HomePlanet;
        this.Job = saveData.Job;
        this.Likes = saveData.Likes;
        this.Dislikes = saveData.Dislikes;

        this.Gender = (Gender)saveData.Gender;
        this.Race = (Race)saveData.Race;
        this.Age = saveData.Age;

        this.skills = new Skills()
        {
            Strength = saveData.strength,
            Dexterity = saveData.dexterity,
            Intelligence = saveData.intelligence,
            Wisdom = saveData.wisdom,
            Charisma = saveData.charisma,
        };

        //this.SkinColor = PersonSkin.GetRandomColor(this.Race);

        this.SkinColor = new Color(saveData.r, saveData.g, saveData.b);

        this.Head = ResourceHelper.PersonHelper.GetHeadFromSpriteName(this, saveData.HeadName);
        this.Body = ResourceHelper.PersonHelper.GetBodyFromSpriteName(this, saveData.BodyName);
        this.Clothes = ResourceHelper.PersonHelper.GetClothFromSpriteName(this, saveData.ClothsName);

    }

    public PersonInfo(PersonTemplate template)
    {
        this.Race = template.RandomRace ? Tools.GetRandomEnumValue<Race>(typeof(Race)) : template.Race;
        this.Gender = template.RandomGender ? Tools.GetRandomEnumValue<Gender>(typeof(Gender)) : template.Gender;
        this.Name = template.RandomName ? ResourceHelper.PersonHelper.GetRandomName(this.Gender) : template.PersonName;
        this.Age = (short)Random.Range(template.MinAge, template.MaxAge);

        this.SkinColor = PersonSkin.GetRandomColor(this.Race);
        this.Head = template.RandomHead ? ResourceHelper.PersonHelper.GetRandomHead(this) : template.Head;
        this.Body = template.RandomBody ? ResourceHelper.PersonHelper.GetRandomBody(this) : template.Body;
        this.Clothes = template.RandomCloths ? ResourceHelper.PersonHelper.GetRandomCloths(this) : template.Clothes;

        UnlocksManager.UnlockCloth(this.Clothes);

        foreach (SkillsList skill in Enum.GetValues(typeof(SkillsList)))
        {
            this.skills.SetSkill(skill, (int)Random.Range(template.MinSkills.GetSkill(skill), template.MaxSkills.GetSkill(skill)));
        }
        SetBioInfo();

    }


    private void SetBioInfo()
    {
        HomePlanet = ResourceHelper.PickRandomFromLocation(PlanetNamePath)[0];
        Job = ResourceHelper.PickRandomFromLocation(JobNamePath)[0];

        int numberofHobbies = Random.Range(1, 3);
        int numberofDislikeHobbies = Random.Range(1, 3);

        Likes = ResourceHelper.PickRandomFromLocation(HobbiesNamePath, numberofHobbies);
        Dislikes = ResourceHelper.PickRandomFromLocation(HobbiesNamePath, numberofDislikeHobbies);

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

    public void UnassignQuest()
    {
        AssginedQuest = null;
    }


    public void StartQuest(WaittingQuest quest)
    {
        if (CurrentQuest != null)
        {
            throw new Exception("Trying to start a quest on a person when a quest is allready active/inprogress");
        }
        else
        {
            if (PersonMonoBehaviour != null)
            {
                GameObject.Destroy(PersonMonoBehaviour.gameObject);
                Room.RemoveWorker(PersonMonoBehaviour);
            }
            Room = GlobalStats.Instance.QuestRoom;
            CurrentQuest = quest;
        }
    }

    public void CompleteQuest(Skills skills)
    {
        CurrentQuest = null;
        var newTemplate = PrefabSpawner.Instance.SpawnPerson();
        newTemplate.transform.position = Room.transform.position;
        PersonMonoBehaviour = newTemplate.GetComponent<Person>();
        Room.Workers.Add(this);
        PersonMonoBehaviour.AssginPerson(this);
    }

    public void SetCloths(Sprite cloths)
    {
        Clothes = cloths;
        if (PersonMonoBehaviour != null)
        {
            PersonMonoBehaviour.ReRenderPerson();
        }
    }

    public PersonSaveData Save()
    {
        PersonSaveData saveData = new PersonSaveData(this);
        saveData.Save(this);
        return saveData;
    }

    public void Load(string Path)
    {
        throw new NotImplementedException();
    }

    public void Load(PersonSaveData data)
    {
        throw new NotImplementedException();
    }
}
