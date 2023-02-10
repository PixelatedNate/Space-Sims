using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListItemView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title, people, requiment, rewared;
    [SerializeField]
    Image requimentImage;


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
    }

    public void SetRewaredText(AbstractQuest quest)
    {
        GameResources rewaredResources = quest.QuestData.reward.GameResourcesReward;
        rewared.text = rewaredResources.ToString();
        if (quest.QuestData.reward.people.Length != 0)
        {
            rewared.text = rewared.text + "\n" + Icons.GetPersonIconForTextMeshPro() + ": " + quest.QuestData.reward.people.Length + " <br>";
        }

    }
}
