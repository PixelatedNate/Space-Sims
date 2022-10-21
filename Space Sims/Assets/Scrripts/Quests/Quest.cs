using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/Quest", order = 1)]
public class Quest : ScriptableObject
{

    public enum Status
    {
       Available,
       InProgress,
       Completed,
    }

    [Serializable]
    class Requiments
    {
        [SerializeField]
        int Numpeople;
        [SerializeField]
        PersonInfo.Skills PeopleRequiments;
    
        public bool Ismet(PersonInfo[] people)
        {
            if (people.Length != Numpeople)
            {
                return false;
            }
            foreach (PersonInfo p in people)
            {
                if (p.skills < PeopleRequiments)
                {
                    return false;
                }
            }
            return true;

        }
    }
    [Serializable]
    class Reward
    {
        [SerializeField]
        GameResources gameResourcesReward;
        [SerializeField]
        PersonInfo[] peopleReward;
    }

    private bool Inprogress = false;

    [SerializeField]
    public string Title;
    [TextArea(15, 20), SerializeField]
    public string Description;
    [SerializeField]
    Requiments requiments;
    [SerializeField]
    int Duration;
    [SerializeField]
    Reward reward;

    [SerializeField]
    List<PersonInfo> PeopleAssgined = new List<PersonInfo>();

    List<QuestEncounter> QuestLog;

    Status questStaus;

    [SerializeField]
    private QuestEncounter[] PossibleEncounters;

    public void AssginPerson(PersonInfo person)
    {
        if (Inprogress)
        {
            throw new Exception("Trying to assginPerson to a quest in progress");
        }
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



    public bool StartQuest()
    {

        TimeTickSystem.OnMajorTick += onMajorTick;
        if (!requiments.Ismet(PeopleAssgined.ToArray()))
        {
            return false;
        }
        TimeDelayManager.Instance.AddTimer(new TimeDelayManager.Timer(Duration, new Action(CompleatQuest)));
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.StartQuest(this);
        }
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

    public void CompleatQuest()
    {
        //add stuff like reweards for quest compleation

        TimeTickSystem.OnMajorTick -= onMajorTick;
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.CompleteQuest(new PersonInfo.Skills());
        }
        GlobalStats.Instance.MarkQuestCompleted(this);
    }
}

