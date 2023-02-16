using TMPro;
using UnityEngine;

public class QuestListItemView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title, people, requiment, rewared, Time;


    public void setQuest(AbstractQuest quest)
    {
        title.text = quest.QuestData.Title;

        if (quest.GetType() == typeof(WaittingQuest))
        {
            SetRequiment((WaittingQuest)quest);
        }

        SetRewaredText(quest);
    }

    public void SetRequiment(WaittingQuest quest)
    {
        WaitingQuestData questData = (WaitingQuestData)quest.QuestData;
        requiment.text = null;
        requiment.text = Icons.GetPersonIconForTextMeshPro() + ": " + questData.QuestRequiments.Numpeople + " <br>";
        requiment.text = requiment.text + Icons.GetSkillIconForTextMeshPro(questData.QuestRequiments.SkillRequiment) + ": " + questData.QuestRequiments.skillValueMin;
        Time.text = quest.WaittingQuestData.Duration.ToString(@"hh\:mm\:ss");
    }

    public void SetRewaredText(AbstractQuest quest)
    {
        //./GameResources rewaredResources = quest.QuestData.reward.GameResourcesReward;
        rewared.text = quest.QuestData.reward.ToString();
    }
}
