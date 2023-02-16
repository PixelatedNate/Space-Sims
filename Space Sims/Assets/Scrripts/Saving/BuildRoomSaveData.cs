using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildRoomSaveData : AbstractQuestSaveData
{

    public BuildRoomSaveData(BuildRoomQuest quest)
    {
        base.PopulateData(quest);
    }

}
