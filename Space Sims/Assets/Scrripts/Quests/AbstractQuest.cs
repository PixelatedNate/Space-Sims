using System;
using UnityEngine;

public abstract class AbstractQuest {

    protected QuestData QuestData;

    public QuestStatus questStaus { get; set; } = QuestStatus.Available;

    public abstract bool StartQuest();
    public abstract void CompleatQuest();

    protected void OpenAlertQuest()
    {
        GlobalStats.Instance.QuestRoom.FocusRoom();
        UIManager.Instance.OpenQuestViewOnQuest(this);
    }
}
