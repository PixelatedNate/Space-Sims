using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestManager
{
    private static List<Quest> Quests { get; } = new List<Quest>();

    public static List<Quest> GetQuestsByStaus(Quest.Status status)
    {
        return Quests.FindAll((a) => a.questStaus == status);
    }

    public static void SetAvalibleQuest(Quest[] quests)
    {


        Quests.RemoveAll((a) => a.questStaus == Quest.Status.Available);

        foreach (Quest q in quests)
        {
            q.UnassginAllPeopople();
            q.questStaus = Quest.Status.Available;
            q.QuestLog.Clear();
        }
        Quests.AddRange(quests);
    }
}
