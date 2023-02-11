using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomQuest : AbstractQuest
{
    public BuildRoomQuest(BuildRoomData buildRoomData, QuestLine questLine)
    {
        this.buildRoomData = buildRoomData;
        this.questLine = questLine;
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
        if(GlobalStats.Instance.PlyaerRooms.FindAll(a => a.RoomType == buildRoomData.roomToBuild && !a.IsUnderConstruction).Count > 0)
        {
            CompleatQuest();
            SoundManager.Instance.PlaySound(SoundManager.Sound.QuestCompleted);
            AlertManager.Instance.SendAlert(new Alert("Quest Complet",QuestData.Title, OpenAlertQuest, Alert.AlertPrority.low, Icons.GetMiscUIIcon(UIIcons.QuestComplete)));
            questStaus = QuestStatus.Completed;
            TimeTickSystem.OnTick -= CheckIfRequimentsAreMetOnTick;
        }
    }
}
