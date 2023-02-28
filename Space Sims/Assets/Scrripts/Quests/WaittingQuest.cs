using System;
using System.Collections.Generic;
using UnityEngine;
using static TimeDelayManager;
using Random = UnityEngine.Random;

public class WaittingQuest : AbstractQuest, ISaveable<WaittingQuestSaveData>
{

    public List<PersonInfo> PeopleAssgined = new List<PersonInfo>();
    public List<QuestEncounter> QuestLog { get; private set; } = new List<QuestEncounter>();

    public Timer QuestTimer { get; private set; }
    public override QuestData QuestData => WaittingQuestData;

    public WaitingQuestData WaittingQuestData { get; }


    public WaittingQuest(WaitingQuestData questdata, QuestLine questLine = null)
    {
        this.WaittingQuestData = questdata;
        this.questLine = questLine;
    }

    public WaittingQuest(WaittingQuestSaveData saveData)
    {
        populateFromSave(saveData);
        this.WaittingQuestData = ResourceHelper.QuestHelper.GetWaittingQuestData(saveData.QuestDataName);
        foreach (string personId in saveData.PeopleAssginedId)
        {

            PersonInfo person;
            if (SaveSystem.LoadedPeople.ContainsKey(personId))
            {
                person = SaveSystem.LoadedPeople[personId];
            }
            else
            {
                person = new PersonInfo(SaveSystem.GetPersonData(personId));
            }
            AssginPerson(person);
            if (questStaus == QuestStatus.InProgress)
            {
                Debug.LogWarning("in progress");
                this.QuestTimer = TimeDelayManager.Timer.ReconstructTimer(saveData.timmerId, new Action(CompleatQuest));
                person.StartQuest(this);
            }

        }
    }



    public void AssginPerson(PersonInfo person)
    {
        person.AssignQuest(this);
        PeopleAssgined.Add(person);
        bool requimentsMet = WaittingQuestData.QuestRequiments.Ismet(PeopleAssgined.ToArray());
    }

    public void UnassginPerson(PersonInfo person)
    {
        if (!PeopleAssgined.Contains(person))
        {
            throw new Exception("trying to UnassginPerson not assgined to quest");
        }
        person.UnassignQuest();
        PeopleAssgined.Remove(person);
    }

    public void UnassginAllPeopople()
    {
        PeopleAssgined.Clear();
    }

    public bool DosePersonMeetRequiment(PersonInfo person)
    {
       return person.skills.GetSkill(WaittingQuestData.QuestRequiments.SkillRequiment) >= WaittingQuestData.QuestRequiments.skillValueMin;
    }

    public override bool StartQuest()
    {
        if (questStaus != QuestStatus.Available)
        {
            Debug.LogWarning("trying to start a quest in progress or completed");
            return false;
        }
        if (!WaittingQuestData.QuestRequiments.Ismet(PeopleAssgined.ToArray()))
        {
            return false;
        }
        questStaus = QuestStatus.InProgress;
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.StartQuest(this);
        }
        TimeTickSystem.OnMajorTick += onMajorTick;
        QuestTimer = new Timer(WaittingQuestData.Duration, new Action(CompleatQuest));
        TimeDelayManager.Instance.AddTimer(QuestTimer);
        return true;
    }


    private void onMajorTick(object source, EventArgs e)
    {
        Dictionary<QuestEncounter, int> events = new Dictionary<QuestEncounter, int>();
        int valueOffset = 0;
        foreach (QuestEncounter qe in WaittingQuestData.PossibleEncounters)
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
        base.CompleatQuest();

        SoundManager.Instance.PlaySound(SoundManager.Sound.QuestCompleted);

        //add stuff like reweards for quest compleation
        GlobalStats.Instance.PlayerResources += QuestData.reward.GameResourcesReward;

        TimeTickSystem.OnMajorTick -= onMajorTick;
        questStaus = QuestStatus.Completed;
        foreach (PersonInfo p in PeopleAssgined)
        {
            p.CompleteQuest(new Skills());
        }

        for (int i = 0; i < QuestData.reward.people.Length; i++)
        {
            PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom, QuestData.reward.people[i]);
        }

        AlertManager.Instance.SendAlert(new Alert("Quest Complet", QuestData.Title, OpenAlertQuest, Alert.AlertPrority.low, Icons.GetMiscUIIcon(UIIcons.QuestComplete)));
    }

    public WaittingQuestSaveData Save()
    {
        WaittingQuestSaveData saveData = new WaittingQuestSaveData(this);
        saveData.Save();
        return saveData;
    }

    public void Load(string path)
    {
        // if(data.)

    }

    public void Load(WaittingQuestSaveData data)
    {

    }

}

