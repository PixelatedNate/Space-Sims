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


    public void setQuest(Quest quest)
    {
        title.text = quest.Title;
        //people.text = quest.requiments.Numpeople.ToString();
        // requimentImage.sprite = Icons.GetSkillIcon(quest.requiments.SkillRequiment);
        //requiment.text = quest.requiments.skillValueMin.ToString();

        SetRequiment(quest);
        SetRewaredText(quest);
    }

    public void SetRequiment(Quest quest)
    {
        requiment.text = null;
        requiment.text =  Icons.GetPersonIconForTextMeshPro() + ": " + quest.requiments.Numpeople + " <br>";
        requiment.text = requiment.text + Icons.GetSkillIconForTextMeshPro(quest.requiments.SkillRequiment) + ": " + quest.requiments.skillValueMin;
    }

    public void SetRewaredText(Quest quest)
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
