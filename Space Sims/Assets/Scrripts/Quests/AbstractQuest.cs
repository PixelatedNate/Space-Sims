using System;
using UnityEngine;

public abstract class AbstractQuest {

    public abstract QuestData QuestData { get; }

    public QuestStatus questStaus { get; set; } = QuestStatus.Available;

    public QuestLine questLine;

    public abstract bool StartQuest();

    public virtual void CompleatQuest()
    {
        GlobalStats.Instance.PlayerResources += QuestData.reward.GameResourcesReward;
        if(questLine != null)
        {
            questLine.AddNextQuest();
        }
        if (QuestData.reward.roomBlueprintUnlock)
        {
            UnlocksManager.UnlockRoom(QuestData.reward.roomBlueprint);
        }
    }

    protected void OpenAlertQuest()
    {
        GlobalStats.Instance.QuestRoom.FocusRoom();
        UIManager.Instance.OpenQuestViewOnQuest(this);
    }
}
