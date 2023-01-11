using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListView : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform QuestScrollPanal;
    private List<AbstractQuest> QuestInView = null;

    [SerializeField]
    GameObject QuestListItemTemplate;

    [SerializeField]
    Image availableBtnImg, ActiveBtnImg, CompletedBtmImg;

    AbstractQuest SelectedQuest;

    public void SetView(QuestStatus status)
    {
        QuestInView = QuestManager.GetQuestsByStaus(status);
        SelectedQuest = null;
        PopulateList();
        SetBtnColours(status);
    }

    public void OpenOnQuest(AbstractQuest quest)
    {
        QuestInView = QuestManager.GetQuestsByStaus(quest.questStaus);
        SetBtnColours(quest.questStaus);
        if (SelectedQuest != quest)
        {
            OpenQuest(quest);
        }

    }

    public void SetBtnColours(QuestStatus staus)
    {
        availableBtnImg.color = Color.gray;
        ActiveBtnImg.color = Color.gray;
        CompletedBtmImg.color = Color.gray;
        if (staus == QuestStatus.Available)
        {
            availableBtnImg.color = Color.white;
        }
        else if (staus == QuestStatus.InProgress)
        {
            ActiveBtnImg.color = Color.white;
        }
        else if (staus == QuestStatus.Completed)
        {
            CompletedBtmImg.color = Color.white;
        }

    }




    private void PopulateList()
    {
        foreach (Transform child in QuestScrollPanal)
        {
            Destroy(child.gameObject);
        }
        foreach (WaittingQuest quest in QuestInView)
        {
            GameObject questViewItem = GameObject.Instantiate(QuestListItemTemplate, QuestScrollPanal);
           questViewItem.GetComponent<QuestListItemView>().setQuest(quest);
            if (SelectedQuest == quest)
            {
                questViewItem.GetComponent<Image>().color = Color.yellow;
                questViewItem.GetComponent<Button>().onClick.AddListener(() => CloseQuest(quest));
            }
            else
            {
                questViewItem.GetComponent<Button>().onClick.AddListener(() => OpenQuest(quest));
            }
        }
    }


    private void OpenQuest(AbstractQuest quest)
    {
        TipsAndToutorial.Instance.OpenTipMenu(TipsAndToutorial.ToutorialMenus.Quest);
        UIManager.Instance.OpenQuestView(quest);
        SelectedQuest = quest;
        PopulateList();
    }

    private void CloseQuest(WaittingQuest quest)
    {
        UIManager.Instance.ClearRightPanal();
        SelectedQuest = null;
        PopulateList();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
