using System;
using System.Collections.Generic;
using UnityEngine;
using static TimeDelayManager;
using Random = UnityEngine.Random;

public class PlanetContainer : ISaveable<PlanetConttainerSaveData>
{

    public AbstractQuest[] quests;

    public bool HasPlayerVisted = false;

    public Timer NewQuestIn;

    public PlanetContainer(PlanetData data, Vector3 pos)
    {
        this.planetData = data;
        this.PlanetPosition = pos;
        SetQuests();
    }


    public AbstractQuest[] getAvalibleQuests()
    {
        List<AbstractQuest> avalibleQuest = new List<AbstractQuest>();
        foreach (AbstractQuest quest in quests)
        {
            if(quest.questStaus == QuestStatus.Available)
            {
                avalibleQuest.Add(quest);
            }
        }
        return avalibleQuest.ToArray();
    }

    private void SetQuests()
    {
        int numberOfQuest = Random.Range(planetData.minNumberOfQuest, planetData.maxNumberOfQuest+1);
        quests = ResourceHelper.QuestHelper.getRandomGenericWaittingQuests(numberOfQuest, planetData.planetType);
        NewQuestIn = new Timer(DateTime.Now.AddHours(planetData.QuestResetPeriodHours), SetQuests);
        TimeDelayManager.Instance.AddTimer(NewQuestIn,true);
    }

    public PlanetContainer(PlanetConttainerSaveData saveData)
    {
        this.planetData = ResourceHelper.PlanetHelper.GetPlanetData(saveData.planetDataName);
        this.PlanetPosition = new Vector3(saveData.planetPosition[0], saveData.planetPosition[1], saveData.planetPosition[2]);
        this.HasPlayerVisted = saveData.HasPlayerVisted;
        this.NewQuestIn = TimeDelayManager.Timer.ReconstructTimer(saveData.timmerId, new Action(SetQuests));
    }

    public PlanetData planetData { get; private set; }
    public Vector3 PlanetPosition;

    public PlanetConttainerSaveData Save()
    {
        PlanetConttainerSaveData savedata = new PlanetConttainerSaveData(this);
        savedata.Save();
        return savedata;
    }


    public void ArriveAtPlanet()
    {
        HasPlayerVisted = true;
        foreach (AbstractQuest quest in quests)
        {
            if(quest.questStaus == QuestStatus.Available)
            QuestManager.AddNewQuest(quest);
        }           
    }
    public void LeavePlanet()
    {
        foreach (AbstractQuest quest in quests)
        {
            if(quest.questStaus == QuestStatus.Available)
            QuestManager.RemoveQuest(quest);
        }           
    }


    public void Load(string Path)
    {
        throw new System.NotImplementedException();
    }

    public void Load(PlanetConttainerSaveData data)
    {
        throw new System.NotImplementedException();
    }
}
