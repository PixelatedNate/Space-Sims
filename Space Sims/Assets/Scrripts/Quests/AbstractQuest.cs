public abstract class AbstractQuest
{

    public abstract QuestData QuestData { get; }

    public QuestStatus questStaus { get; set; }
    public QuestLine questLine;

    public abstract bool StartQuest();

    public virtual void CompleatQuest()
    {
        if (questLine != null)
        {
            questLine.AddNextQuest();
        }
        if (QuestData.reward.roomBlueprintUnlock)
        {
            UnlocksManager.UnlockRoom(QuestData.reward.roomBlueprint);
        }
    }

    protected void populateFromSave(AbstractQuestSaveData saveData)
    {
        this.questStaus = (QuestStatus)saveData.questStatus;
        if(saveData.QuestLineId != null)
        {
          QuestLineSaveData questLineData = SaveSystem.LoadData<QuestLineSaveData>(SaveSystem.QuestLinePath + "/" + saveData.QuestLineId + SaveSystem.QuestLinePrefix);
            questLine = new QuestLine(questLineData);
        }
    }

    protected void OpenAlertQuest()
    {
        GlobalStats.Instance.QuestRoom.FocusRoom();
        UIManager.Instance.OpenQuestViewOnQuest(this);
    }

}
