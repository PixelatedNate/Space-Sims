using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title;
    [SerializeField]
    TextMeshProUGUI Discription, TimeLeft, Reward, OverFlowReward;
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
    TransprotQuestViewComponet transprotQuestViewComponet;


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
        if (quest.GetType() == typeof(TransportQuest))
        {
            waitingQuestView.gameObject.SetActive(false);
            transprotQuestViewComponet.gameObject.SetActive(true);
            transprotQuestViewComponet.SelectQuest((TransportQuest)quest);
            startBtn.interactable = true;
            buttonImge.sprite = ReadyButtonImg;
        }
        if (quest.GetType() == typeof(BuildRoomQuest))
        {
            waitingQuestView.gameObject.SetActive(false);
            transprotQuestViewComponet.gameObject.SetActive(false);
            //transprotQuestViewComponet.SelectQuest((TransportQuest)quest);
            startBtn.interactable = true;
            buttonImge.sprite = ReadyButtonImg;
        }
        questSelected = quest;
        Title.text = quest.QuestData.Title;
        Discription.text = quest.QuestData.Description;
        setRewaredText();
    }

    public void StartQuest()
    {
        if (questSelected.StartQuest())
        {
            startBtn.interactable = false;
            SelectQuest(questSelected);
            UIManager.Instance.OpenAvalibalQuestListView();
        }


    }

    private void setRewaredText()
    {
        Reward.text = "";
        OverFlowReward.text = "";

        string rewardStr = questSelected.QuestData.reward.ToString();
        string[] lines = rewardStr.Split("\n");
        for (int i = 0; i < lines.Length; i++)
        {
            if (i < 2)
            {
                Reward.text += lines[i] + "\n" + "\n";
            }
            else
            {
                OverFlowReward.text += lines[i] + "\n" + "\n";
            }
        }
    }
}
