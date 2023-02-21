using System;

[Serializable]
public class QuestLineSavieData
{
    public int index;
    public string QuestLineDataName;


    public QuestLineSavieData(QuestLine questLine)
    {
        this.index = questLine.index;
        this.QuestLineDataName = questLine.questLineData.name;
    }




}
