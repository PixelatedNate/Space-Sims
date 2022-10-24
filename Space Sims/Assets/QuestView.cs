using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title;
    [SerializeField]
    TextMeshProUGUI Discription;
    [SerializeField]
    Transform RequimentPanel;

    [SerializeField]
    Button startBtn;

    private QuestRequimentBoxView SelectedQestRequiment;

    private Quest questSelected;
    // Start is called before the first frame update
   
    [SerializeField]
    private GameObject RequimentTemplate;
   
    public void SelectQuest(Quest quest)
    {
        questSelected = quest;
        Title.text = quest.Title;
        Discription.text = quest.Description;
        SetRequiments();
        startBtn.enabled =enabled;
        if(quest.questStaus == Quest.Status.InProgress)
        { 
            startBtn.enabled = false;
        }
    }

    public void SetActiveRequimentBoxView(QuestRequimentBoxView selectedQuestRequiment)
    {
        if(SelectedQestRequiment != null)
        {
            SelectedQestRequiment.DeselectRequimentBox();
        }
        SelectedQestRequiment = selectedQuestRequiment;
    }
    
    public void SetRequiments()
    {
        foreach (Transform child in RequimentPanel)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < questSelected.requiments.Numpeople; i++)
        {
            GameObject QuestRequimentItem = GameObject.Instantiate(RequimentTemplate, RequimentPanel);
            QuestRequimentBoxView questRequimentBoxView = QuestRequimentItem.GetComponent<QuestRequimentBoxView>();
            questRequimentBoxView.MainQuestView = this;
            if (questSelected.PeopleAssgined.Count >= i + 1)
            {
                questRequimentBoxView.SetPerson(questSelected.PeopleAssgined[i],questSelected);
            }
            else
            {
                questRequimentBoxView.SetRequiments(questSelected);
            }
        }
    }

    public void StartQuest()
    {
        if(questSelected.StartQuest())
        {
            startBtn.enabled = false;
        }


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
