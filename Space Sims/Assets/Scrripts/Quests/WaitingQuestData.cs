using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/WaittingQuest", order = 2)]
public class WaitingQuestData : QuestData
{
    [Serializable]
    public class Requiments
    {
        public int Numpeople;
        public SkillsList SkillRequiment;
        public int skillValueMin;

        public bool Ismet(PersonInfo[] people)
        {
            if (people.Length != Numpeople)
            {
                return false;
            }
            foreach (PersonInfo p in people)
            {
                if (p.skills.GetSkill(SkillRequiment) < skillValueMin)
                {
                    return false;
                }
            }
            return true;

        }
    }
    [SerializeField]
    public Requiments QuestRequiments = new Requiments();
    [SerializeField]
    public QuestEncounter[] PossibleEncounters;
    

    public TimeSpan Duration { get { return new TimeSpan(DurationHour, DurationMinuits, 00);  } }

    [SerializeField]
    public int DurationHour;
    [SerializeField]
    public int DurationMinuits;

    public override AbstractQuest CreateQuest(QuestLine questline = null)
    {
        WaittingQuest quest = new WaittingQuest(this, questline);
        return quest;
    }
}
