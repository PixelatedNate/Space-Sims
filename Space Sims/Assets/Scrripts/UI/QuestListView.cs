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

    public void SetView(Quest.Status status)
    {
        questView.gameObject.SetActive(false);
        QuestInView = GlobalStats.Instance.GetQuestsByStaus(status);
        PopulateList();
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
