using UnityEngine;

public abstract class AbstractQuest
{

    public abstract QuestData QuestData { get; }

    public QuestStatus questStaus { get; set; }
    public QuestLine questLine;

    public abstract bool StartQuest();

    public virtual void CompleatQuest()
    {
        if(QuestData.reward.ClothUnlock)
        {
            if(QuestData.reward.RandomCloths)
            {
                Sprite cloth = ResourceHelper.PersonHelper.GetRandomCloths(Gender.Male, QuestData.reward.CLothRarity);
                UnlocksManager.UnlockCloth(cloth);
            }
            else if (QuestData.reward.Cloth != null)
            {
                UnlocksManager.UnlockCloth(QuestData.reward.Cloth);
            }
        }



        if (CustomEventTriggers.OnQuestCompleted.onEventDelaget != null)
        {
            CustomEventTriggers.OnQuestCompleted.onEventDelaget.Invoke(this);
        }
        if (questLine != null)
        {
            questLine.AddNextQuest();
        }
        if (QuestData.reward.roomBlueprintUnlock)
        {
            UnlocksManager.UnlockRoom(QuestData.reward.roomBlueprint);
        }
        if (QuestData.DialogIndex != -1)
        {
            Debug.LogError("This sould be fixed post alpha");
            //Debug.l "This should be fixed post alpha"
            DialogManager.Instance.StartDiaglogIndex(QuestData.DialogIndex);
        }
    }

    protected void populateFromSave(AbstractQuestSaveData saveData)
    {
        this.questStaus = (QuestStatus)saveData.questStatus;
        if (saveData.QuestLineId != null)
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
