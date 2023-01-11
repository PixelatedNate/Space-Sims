using System.Collections.Generic;

public static class QuestManager
{
    private static List<AbstractQuest> Quests { get; } = new List<AbstractQuest>();

    public static List<AbstractQuest> GetQuestsByStaus(QuestStatus status)
    {
        return Quests.FindAll((a) => a.questStaus == status);
    }

    public static void SetAvalibleQuest(WaittingQuest[] quests)
    {


        Quests.RemoveAll((a) => a.questStaus == QuestStatus.Available);

        foreach (WaittingQuest q in quests)
        {
            q.UnassginAllPeopople();
            q.questStaus = QuestStatus.Available;
            q.QuestLog.Clear();
        }
        Quests.AddRange(quests);
    }
}
