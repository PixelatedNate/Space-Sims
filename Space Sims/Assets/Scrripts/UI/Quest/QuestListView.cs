using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListView : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform QuestScrollPanal;
    private List<Quest> QuestInView = null;

    [SerializeField]
    GameObject QuestListItemTemplate;

    [SerializeField]
    Image availableBtnImg, ActiveBtnImg, CompletedBtmImg;

    Quest SelectedQuest;

    public void SetView(Quest.Status status)
    {
        //questView.gameObject.SetActive(false);
        //QuestInView = GlobalStats.Instance.GetQuestsByStaus(status);
        QuestInView = QuestManager.GetQuestsByStaus(status);
        SelectedQuest = null;
        PopulateList();
        SetBtnColours(status);
    }

    public void OpenOnQuest(Quest quest)
    {
        QuestInView = QuestManager.GetQuestsByStaus(quest.questStaus);
        SetBtnColours(quest.questStaus);
        if (SelectedQuest != quest)
        {
            OpenQuest(quest);
        }

    }

    public void SetBtnColours(Quest.Status staus)
    {
        availableBtnImg.color = Color.gray;
        ActiveBtnImg.color = Color.gray;
        CompletedBtmImg.color = Color.gray;
        if (staus == Quest.Status.Available)
        {
            availableBtnImg.color = Color.white;
        }
        else if (staus == Quest.Status.InProgress)
        {
            ActiveBtnImg.color = Color.white;
        }
        else if (staus == Quest.Status.Completed)
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
        foreach (Quest quest in QuestInView)
        {
            GameObject questViewItem = GameObject.Instantiate(QuestListItemTemplate, QuestScrollPanal);
            // questViewItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.Title;
            questViewItem.GetComponent<QuestListItemView>().setQuest(quest);
            if (SelectedQuest == quest)
            {
                questViewItem.transform.GetChild(1).gameObject.SetActive(true);
                questViewItem.GetComponent<Image>().color = Color.yellow;
                questViewItem.GetComponent<Button>().onClick.AddListener(() => CloseQuest(quest));
            }
            else
            {
                questViewItem.transform.GetChild(1).gameObject.SetActive(false);
                questViewItem.GetComponent<Button>().onClick.AddListener(() => OpenQuest(quest));
            }
        }
    }


    private void OpenQuest(Quest quest)
    {
        UIManager.Instance.OpenQuestView(quest);
        SelectedQuest = quest;
        PopulateList();
    }

    private void CloseQuest(Quest quest)
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
