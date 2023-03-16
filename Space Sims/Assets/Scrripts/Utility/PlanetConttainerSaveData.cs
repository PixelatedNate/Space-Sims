using System;
using System.Collections.Generic;

[Serializable]
public class PlanetConttainerSaveData
{

    public string planetId;
    public string planetDataName;
    public bool HasPlayerVisted;
    public float[] planetPosition = new float[3];

    public string timmerId;

    public string[] WaittingQuestsName;
    public string[] TransportQuestsName;

    public PlanetConttainerSaveData(PlanetContainer planetContainer)
    {
        this.planetId = System.Guid.NewGuid().ToString();
        this.HasPlayerVisted = planetContainer.HasPlayerVisted;
        this.planetDataName = planetContainer.planetData.name;
        this.planetPosition[0] = planetContainer.PlanetPosition.x;
        this.planetPosition[1] = planetContainer.PlanetPosition.y;
        this.planetPosition[2] = planetContainer.PlanetPosition.z;

        List<string> waittingquestNamesList = new List<string>();
        List<string> transportquestNamesList = new List<string>();
        foreach(AbstractQuest quest in planetContainer.quests)
        {
            if(quest.questType == QuestType.Waitting)
            {
                waittingquestNamesList.Add(quest.QuestData.name);
            }
            else if(quest.questType == QuestType.Transport)
            {
                transportquestNamesList.Add(quest.QuestData.name);
            }
        }

        WaittingQuestsName = waittingquestNamesList.ToArray();
        TransportQuestsName = transportquestNamesList.ToArray();

        TimerSaveData data = planetContainer.NewQuestIn.Save();
        this.timmerId = data.ID;

    }


}
