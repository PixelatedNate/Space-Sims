using System;

[Serializable]
public class TransportQuestSaveData : AbstractQuestSaveData
{

    public string[] peopleOnShipId;



    public TransportQuestSaveData(TransportQuest quest)
    {
        PopulateData(quest);


        int index = 0;
        peopleOnShipId = new string[quest.peopleOnShip.Count];

        foreach (Person person in quest.peopleOnShip)
        {
            if (SaveSystem.SavedPeople.ContainsKey(person.PersonInfo))
            {
                peopleOnShipId[index] = SaveSystem.SavedPeople[person.PersonInfo];
            }
            else
            {
                PersonSaveData personData = person.PersonInfo.Save();
                peopleOnShipId[index] = personData.personId;
            }
            index++;
        }

    }

}
