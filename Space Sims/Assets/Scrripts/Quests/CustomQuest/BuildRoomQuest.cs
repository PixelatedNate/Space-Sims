using System;

public class BuildRoomQuest : AbstractQuest, ISaveable<BuildRoomSaveData>
{
    public BuildRoomQuest(BuildRoomData buildRoomData, QuestLine questLine)
    {
        this.buildRoomData = buildRoomData;
        this.questLine = questLine;
    }

    public BuildRoomQuest(BuildRoomSaveData buildRoomSaveData)
    {
        base.populateFromSave(buildRoomSaveData);
        this.buildRoomData = ResourceHelper.QuestHelper.GetBuildQuestData(buildRoomSaveData.QuestDataName);
        if (questStaus == QuestStatus.InProgress)
        {
            TimeTickSystem.OnTick += CheckIfRequimentsAreMetOnTick;
        }
    }

    public override QuestData QuestData => buildRoomData;
    public BuildRoomData buildRoomData;

    public override bool StartQuest()
    {
        questStaus = QuestStatus.InProgress;
        TimeTickSystem.OnTick += CheckIfRequimentsAreMetOnTick;
        return true;
    }

    public void CheckIfRequimentsAreMetOnTick(object source, EventArgs e)
    {
        if (GlobalStats.Instance.PlyaerRooms.FindAll(a => a.RoomType == buildRoomData.roomToBuild && !a.IsUnderConstruction).Count > 0)
        {
            CompleatQuest();
            AlertManager.Instance.SendAlert(new Alert("Quest Complet", QuestData.Title, OpenAlertQuest, Alert.AlertPrority.low, Icons.GetMiscUIIcon(UIIcons.QuestComplete)));
            questStaus = QuestStatus.Completed;
            TimeTickSystem.OnTick -= CheckIfRequimentsAreMetOnTick;
        }
    }

    public BuildRoomSaveData Save()
    {
        BuildRoomSaveData data = new BuildRoomSaveData(this);
        data.Save();
        return data;
    }

    public void Load(string Path)
    {
        throw new NotImplementedException();
    }

    public void Load(BuildRoomSaveData data)
    {
        throw new NotImplementedException();
    }
}
