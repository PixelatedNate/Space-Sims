using System;

[Serializable]
public abstract class AbstractQuestSaveData
{

    public string QuestDataName;
    public int questStatus;
    public bool IsGeneric;

    public string QuestLineId;

    public void PopulateData(AbstractQuest quest)
    {
        this.QuestDataName = quest.QuestData.name;
        this.questStatus = (int)quest.questStaus;
        this.IsGeneric = quest.QuestData.IsGeneric;

        if (quest.questLine != null)
        {
            QuestLineSaveData questlineSaveData = quest.questLine.Save();
            this.QuestLineId = questlineSaveData.Id;
        }
        else
        {
            this.QuestLineId = null;
        }

    }


}
