using System;
using System.Collections.Generic;
using UnityEngine;
using static TimeDelayManager;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/WaittingQuest", order = 1)]
public class WaittingQuest : AbstractQuest
{


    [Serializable]
    public class Requiments
    {
        [SerializeField]
        public int Numpeople;
        [SerializeField]
        public SkillsList SkillRequiment;
        [SerializeField]
        public int skillValueMin;

        public bool Ismet(PersonInfo[] people)
        {
            if (people.Length != Numpeople)
            {
                return false;
            }
            foreach (PersonInfo p in people)
            {
                if (p.skills.GetSkill(SkillRequiment) < skillValueMin)
                {
                    return false;
                }
            }
            return true;

        }
    }
    
    private bool Inprogress = false;

    [SerializeField]
    public Requiments requiments;
    [SerializeField]
    float Duration;
  
    [SerializeField]
    public List<PersonInfo> PeopleAssgined = new List<PersonInfo>();

    public List<QuestEncounter> QuestLog { get; private set; } = new List<QuestEncounter>();


    [SerializeField]
    private QuestEncounter[] PossibleEncounters;

    public Timer QuestTimer { get; private set; }

    public void AssginPerson(PersonInfo person)
    {
        if (Inprogress)
        {
            throw new Exception("Trying to assginPerson to a quest in progress");
        }
        person.AssignQuest(this);
        PeopleAssgined.Add(person);
        bool requimentsMet = requiments.Ismet(PeopleAssgined.ToArray());
    }

    public void UnassginPerson(PersonInfo person)
    {
        if (!PeopleAssgined.Contains(person))
        {
            throw new Exception("trying to UnassginPerson not assgined to quest");
        }
        PeopleAssgined.Remove(person);
    }

    public void UnassginAllPeopople()
    {
        PeopleAssgined.Clear();
    }

    public bool DosePersonMeetRequiment(PersonInfo person)
    {
        return person.skills.GetSkill(requiments.SkillRequiment) > requiments.skillValueMin;
    }

    public override bool StartQuest()
    {
        if (!requiments.Ismet(PeopleAssgined.ToArray()))
        {
            return false;
        }
        questStaus = QuestStatus.InProgress;
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.StartQuest(this);
        }
        TimeTickSystem.OnMajorTick += onMajorTick;
        QuestTimer = new Timer(DateTime.Now.AddMinutes(Duration), new Action(CompleatQuest));
        TimeDelayManager.Instance.AddTimer(QuestTimer);
        return true;
    }


    private void onMajorTick(object source, EventArgs e)
    {
        Dictionary<QuestEncounter, int> events = new Dictionary<QuestEncounter, int>();
        int valueOffset = 0;
        foreach (QuestEncounter qe in PossibleEncounters)
        {
            int value = valueOffset + qe.frequancy;
            events.Add(qe, value);
            valueOffset = value;
        }
        int randomnum = Random.Range(1, valueOffset + 1);

        foreach (var qe in events)
        {
            if (randomnum <= qe.Value)
            {
                TriggerEncounter(qe.Key);
                return;
            }
        }
    }


    public void TriggerEncounter(QuestEncounter qe)
    {
        QuestLog.Add(qe);
    }

    public override void CompleatQuest()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.QuestCompleted);

        //add stuff like reweards for quest compleation
        GlobalStats.Instance.PlayerResources += reward.GameResourcesReward;

        TimeTickSystem.OnMajorTick -= onMajorTick;
        questStaus = QuestStatus.Completed;
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.CompleteQuest(new PersonInfo.Skills());
        }

        for (int i = 0; i < reward.NumberOfPeopleReward; i++)
        {
            PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom);
        }

        AlertManager.Instance.SendAlert(new Alert("Quest Complet", Title, OpenAlertQuest, Alert.AlertPrority.low,Icons.GetMiscUIIcon(UIIcons.QuestComplete)));
    }

    public override void ResetQuest()
    {
        UnassginAllPeopople();
        questStaus = QuestStatus.Available;
        QuestLog.Clear();
    }
}

