using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingQuestData : QuestData
{
    public class Requiments
    {
        [SerializeField]
        public int Numpeople;
        [SerializeField]
        public SkillsList SkillRequiment;
        [SerializeField]
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

    public readonly Requiments QuestRequiments;
    public readonly QuestEncounter[] PossibleEncounters;
    public readonly float Duration;


}
