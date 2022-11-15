using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    QuestView questView;

    [SerializeField]
    Image availableBtnImg, ActiveBtnImg, CompletedBtmImg;

    public void SetView(Quest.Status status)
    {
        //questView.gameObject.SetActive(false);
        QuestInView = GlobalStats.Instance.GetQuestsByStaus(status);
        PopulateList();
        SetBtnColours(status);
    }

    public void OpenOnQuest(Quest quest)
    {
        QuestInView = GlobalStats.Instance.GetQuestsByStaus(quest.questStaus);
        PopulateList();
        SetBtnColours(quest.questStaus);
        OpenQuest(quest);

    }

    public void SetBtnColours(Quest.Status staus)
    {
        availableBtnImg.color = Color.gray;
        ActiveBtnImg.color = Color.gray;
        CompletedBtmImg.color = Color.gray;
        if(staus == Quest.Status.Available)
        {
            availableBtnImg.color = Color.white;
        }
        else if(staus == Quest.Status.InProgress)
        {
            ActiveBtnImg.color = Color.white;
        }
        else if(staus == Quest.Status.Completed)
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
        foreach(Quest quest in QuestInView)
        {
           GameObject questViewItem = GameObject.Instantiate(QuestListItemTemplate, QuestScrollPanal);
            questViewItem.GetComponent<Button>().onClick.AddListener(() => OpenQuest(quest));
            questViewItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.Title;        
        }
    }


    private void OpenQuest(Quest quest)
    {
        questView.gameObject.SetActive(true);
        questView.SelectQuest(quest);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
