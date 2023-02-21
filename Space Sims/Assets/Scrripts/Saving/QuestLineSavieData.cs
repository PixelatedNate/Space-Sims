using System;

[Serializable]
public class QuestLineSaveData
{
    public string Id;
    public int index;
    public string QuestLineDataName;


    public QuestLineSaveData(QuestLine questLine)
    {
        this.Id = Guid.NewGuid().ToString();

        this.index = questLine.index;
        this.QuestLineDataName = questLine.questLineData.name;
    }




}
