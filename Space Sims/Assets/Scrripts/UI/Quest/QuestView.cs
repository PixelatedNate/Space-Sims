using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title;
    [SerializeField]
    TextMeshProUGUI Discription, TimeLeft, Reward;
    [SerializeField]
    Transform RequimentPanel, ProgressBar;

    [SerializeField]
    Button startBtn;

    [SerializeField]
    Image buttonImge;

    [SerializeField]
    Sprite ReadyButtonImg;
    [SerializeField]
    Sprite UnreadyButtonImg;

    [SerializeField]
    Transform LogPanal;

    [SerializeField]
    GameObject LogUIPrefab;

    [SerializeField]
    PageSwiper pageSwiper;


    [SerializeField]
    WaitingQuestViewComponet waitingQuestView;
    [SerializeField]
    TramsprotQuestViewComponet transprotQuestViewComponet;


  //  public WaittingQuest questSelected { get; private set; }
    public AbstractQuest questSelected { get; private set; }

    [SerializeField]
    private GameObject RequimentTemplate;

    public void SelectQuest(AbstractQuest quest)
    {
        if (quest.GetType() == typeof(WaittingQuest))
        {
            waitingQuestView.gameObject.SetActive(true);
            transprotQuestViewComponet.gameObject.SetActive(false);
            waitingQuestView.SelectQuest((WaittingQuest)quest);
        }
        if(quest.GetType() == typeof(TransportQuest))
        {
            waitingQuestView.gameObject.SetActive(false);
            transprotQuestViewComponet.gameObject.SetActive(true);
            transprotQuestViewComponet.SelectQuest((TransportQuest)quest);
            buttonImge.sprite = ReadyButtonImg;
        }
        questSelected = quest;
        Title.text = quest.Title;
        Discription.text = quest.Description;
        setRewaredText();
    }

    public void StartQuest()
    {
        if (questSelected.StartQuest())
        {
            startBtn.enabled = false;
            SelectQuest(questSelected);
            UIManager.Instance.OpenAvalibalQuestListView();
        }


    }

    private void setRewaredText()
    {
        Reward.text = null;
        GameResources rewaredResources = questSelected.reward.GameResourcesReward;
        foreach (ResourcesEnum re in Enum.GetValues(typeof(ResourcesEnum)))
        {
            if (rewaredResources.GetResorce(re) != 0)
            {
                Reward.text = Icons.GetResourceIconForTextMeshPro(re) + ": " + rewaredResources.GetResorce(re).ToString() + " <br>";
            }
        }
        if (questSelected.reward.NumberOfPeopleReward != 0)
        {
            Reward.text = Reward.text + Icons.GetPersonIconForTextMeshPro() + ": " + questSelected.reward.NumberOfPeopleReward + " <br>";
        }
    }
}
