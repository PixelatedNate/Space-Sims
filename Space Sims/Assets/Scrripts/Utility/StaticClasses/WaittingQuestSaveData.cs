using System;

[Serializable]
public class WaittingQuestSaveData : AbstractQuestSaveData
{
    public bool Inprogress;
    public string[] PeopleAssginedId;
    public string timmerId;


    public WaittingQuestSaveData(WaittingQuest quest)
    {
        PopulateData(quest);
        this.PeopleAssginedId = new string[quest.PeopleAssgined.Count];


        int index = 0;
        foreach (PersonInfo person in quest.PeopleAssgined)
        {
            if (SaveSystem.SavedPeople.ContainsKey(person))
            {
                PeopleAssginedId[index] = SaveSystem.SavedPeople[person];
            }
            else
            {
                PersonSaveData personData = person.Save();
                PeopleAssginedId[index] = personData.personId;
            }
            index++;
        }

        if (quest.questStaus == QuestStatus.InProgress)
        {
            TimerSaveData data = quest.QuestTimer.Save();
            this.timmerId = data.ID;
        }
    }


}
