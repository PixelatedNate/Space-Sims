using System;

[Serializable]
public abstract class AbstractQuestSaveData
{

    public string QuestDataName;
    public int questStatus;


    public void PopulateData(AbstractQuest quest)
    {
        QuestDataName = quest.QuestData.name;
        questStatus = (int)quest.questStaus;
    }


}
