using System;
using System.Collections.Generic;

public static class QuestManager
{
    private static List<AbstractQuest> Quests { get; } = new List<AbstractQuest>();
    private static List<QuestLine> QuestLines { get; } = new List<QuestLine>();

    public static event Action OnQuestAdded;

    public static void AddNewQuest(QuestData[] questDatas)
    {
        foreach (QuestData questData in questDatas)
        {
            AddNewQuest(questData);
          //  var quest = questData.CreateQuest();
           // Quests.Add(quest);
        }
    }

    public static void AddNewQuest(QuestData questData)
    {
        Quests.Add(questData.CreateQuest());
        OnQuestAdded?.Invoke();

    }

    public static void AddNewQuest(AbstractQuest quest)
    {
        Quests.Add(quest);
        OnQuestAdded?.Invoke();
    }

    public static void RemoveQuest(AbstractQuest quest)
    {
        Quests.Remove(quest);
    }


    public static void SaveQuests()
    {
        foreach (AbstractQuest quest in Quests)
        {
            if (quest.GetType() == typeof(WaittingQuest))
            {
                WaittingQuest waittingQuest = (WaittingQuest)quest;
                waittingQuest.Save();
            }
            if (quest.GetType() == typeof(TransportQuest))
            {
                TransportQuest transportQuest = (TransportQuest)quest;
                transportQuest.Save();
            }
            if (quest.GetType() == typeof(BuildRoomQuest))
            {
                BuildRoomQuest buildroomQuest = (BuildRoomQuest)quest;
                buildroomQuest.Save();
            }



        }
    }

    public static void LoadQuests()
    {
        WaittingQuestSaveData[] SaveQuests = SaveSystem.GetAllSavedQuest<WaittingQuestSaveData>(SaveSystem.WaittingQuestPath);
        foreach (WaittingQuestSaveData savequest in SaveQuests)
        {
            WaittingQuest quest = new WaittingQuest(savequest);
            AddNewQuest(quest);
        }
        TransportQuestSaveData[] transportQuestSaveDatas = SaveSystem.GetAllSavedQuest<TransportQuestSaveData>(SaveSystem.TransportQuestPath);
        foreach (TransportQuestSaveData savequest in transportQuestSaveDatas)
        {
            TransportQuest quest = new TransportQuest(savequest);
            AddNewQuest(quest);
        }

        BuildRoomSaveData[] buildRoomQuestData = SaveSystem.GetAllSavedQuest<BuildRoomSaveData>(SaveSystem.BuildRoomQuestPath);
        foreach (BuildRoomSaveData savequest in buildRoomQuestData)
        {
            BuildRoomQuest quest = new BuildRoomQuest(savequest);
            AddNewQuest(quest);
        }



    }




    public static void AddNewQuestLine(QuestLineData questLineData)
    {
        QuestLine questline = new QuestLine(questLineData);
        QuestLines.Add(questline);
        questline.AddNextQuest();
    }

    public static List<AbstractQuest> GetQuestsByStaus(QuestStatus status)
    {
        return Quests.FindAll((a) => a.questStaus == status);
    }

    public static void SetAvalibleQuest(AbstractQuest[] quests)
    {
        Quests.RemoveAll((a) => a.questStaus == QuestStatus.Available);

        foreach (AbstractQuest q in quests)
        {
            //q.ResetQuest();
        }
        Quests.AddRange(quests);
    }
}
