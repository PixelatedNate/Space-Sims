using System.Collections.Generic;

public static class QuestManager
{
    private static List<AbstractQuest> Quests { get; } = new List<AbstractQuest>();
    private static List<QuestLine> QuestLines { get; } = new List<QuestLine>();


    public static void AddNewQuest(QuestData[] questDatas)
    {
        foreach(QuestData questData in questDatas)
        {
            var quest = questData.CreateQuest();
            Quests.Add(quest);
        }
    }

    public static void AddNewQuest(QuestData questData)
    {
        Quests.Add(questData.CreateQuest());
    }

    public static void AddNewQuest(AbstractQuest quest)
    {
        Quests.Add(quest);
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
