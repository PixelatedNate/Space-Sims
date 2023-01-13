using System.Collections.Generic;

public static class QuestManager
{
    private static List<AbstractQuest> Quests { get; } = new List<AbstractQuest>();

    public static List<AbstractQuest> GetQuestsByStaus(QuestStatus status)
    {
        return Quests.FindAll((a) => a.questStaus == status);
    }

    public static void SetAvalibleQuest(AbstractQuest[] quests)
    {
        Quests.RemoveAll((a) => a.questStaus == QuestStatus.Available);

        foreach (AbstractQuest q in quests)
        {
            q.ResetQuest();
        }
        Quests.AddRange(quests);
    }
}
