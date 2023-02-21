using System;

[Serializable]
public class BuildRoomSaveData : AbstractQuestSaveData
{

    public BuildRoomSaveData(BuildRoomQuest quest)
    {
        base.PopulateData(quest);
    }

}
