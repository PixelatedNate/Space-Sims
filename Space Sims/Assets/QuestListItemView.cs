using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListItemView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title, people, requiment, rewared;
    [SerializeField]
    Image requimentImage;


    public void setQuest(WaittingQuest quest)
    {
        title.text = quest.Title;
        SetRequiment(quest);
        SetRewaredText(quest);
    }

    public void SetRequiment(WaittingQuest quest)
    {
        requiment.text = null;
        requiment.text =  Icons.GetPersonIconForTextMeshPro() + ": " + quest.requiments.Numpeople + " <br>";
        requiment.text = requiment.text + Icons.GetSkillIconForTextMeshPro(quest.requiments.SkillRequiment) + ": " + quest.requiments.skillValueMin;
    }

    public void SetRewaredText(WaittingQuest quest)
    {
        rewared.text = null;
        GameResources rewaredResources = quest.reward.GameResourcesReward;
        foreach (ResourcesEnum re in Enum.GetValues(typeof(ResourcesEnum)))
        {
            if (rewaredResources.GetResorce(re) != 0)
            {
                rewared.text = rewared.text + Icons.GetResourceIconForTextMeshPro(re) + ": " + rewaredResources.GetResorce(re).ToString() + " <br>";
            }
        }
        if (quest.reward.NumberOfPeopleReward != 0)
        {
         rewared.text = rewared.text + Icons.GetPersonIconForTextMeshPro() + ": " + quest.reward.NumberOfPeopleReward + " <br>";
        }
    }
}
