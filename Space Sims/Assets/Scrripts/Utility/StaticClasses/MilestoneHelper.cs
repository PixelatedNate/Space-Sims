using System;


public static class MilestoneHelper
{

    public enum Milestones
    {

        RoomsBuilt,
        PlanetsVisited,
        TotalPeopleInShip,
        QuestCompleted,

    }

    public static int GetMilstoneValue(Milestones milstone)
    {
        switch (milstone)
        {
            case Milestones.RoomsBuilt: return GlobalStats.Instance.PlyaerRooms.Count;
            case Milestones.TotalPeopleInShip: return GlobalStats.Instance.PlayersPeople.Count;
            case Milestones.QuestCompleted: return QuestManager.GetQuestsByStaus(QuestStatus.Completed).Count;
            default: throw new Exception("no value found for milston enum");
        }
    }



}



