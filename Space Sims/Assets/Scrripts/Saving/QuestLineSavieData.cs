using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
