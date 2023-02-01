using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            case Milestones.QuestCompleted: return QuestManager.GetQuestsByStaus(QuestStatus.Completed).Count;
            default: throw new Exception("no value found for milston enum");
        }
    }



}



