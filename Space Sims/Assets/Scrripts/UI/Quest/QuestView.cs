using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TimeDelayManager;

public class QuestView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title;
    [SerializeField]
    TextMeshProUGUI Discription, TimeLeft;
    [SerializeField]
    Transform RequimentPanel, ProgressBar;

    [SerializeField]
    Button startBtn;

    private List<QuestRequimentBoxView> QuestRequimentBoxViews = new List<QuestRequimentBoxView>();
    private QuestRequimentBoxView SelectedQuestRequiment;

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
        startBtn.enabled = enabled;
        if (quest.questStaus == Quest.Status.Available)
        {
            ProgressBar.parent.parent.gameObject.SetActive(false);
        }
        else if(quest.questStaus == Quest.Status.InProgress)
        { 
            TimeTickSystem.OnTick += OnTick;
            startBtn.enabled = false;
            SetProgressBarAndText();
        }
        else if(quest.questStaus == Quest.Status.Completed)
        {
            ProgressBar.parent.parent.gameObject.SetActive(true);
            TimeTickSystem.OnTick -= OnTick;
            startBtn.enabled = false;
            TimeLeft.text = "Completed";
            ProgressBar.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetActiveRequimentBoxView(QuestRequimentBoxView selectedQuestRequiment)
    {
        if(SelectedQuestRequiment != null)
        {
            SelectedQuestRequiment.DeselectRequimentBox();
        }
        SelectedQuestRequiment = selectedQuestRequiment;
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
            QuestRequimentItem.GetComponent<Button>().onClick.AddListener(() => SelectRequimentBox(questRequimentBoxView));
            QuestRequimentBoxViews.Add(questRequimentBoxView);
        }
    }


    private void SelectRequimentBox(QuestRequimentBoxView requimentBox)
    {
        if(SelectedQuestRequiment == requimentBox)
        {
            SelectedQuestRequiment.DeselectRequimentBox();
            SelectedQuestRequiment = null;
            UIManager.Instance.OpenQuestListView();              
            return;
        }
        else if(SelectedQuestRequiment != null)
        {
            SelectedQuestRequiment.DeselectRequimentBox();
        }
        SelectedQuestRequiment = requimentBox;
        requimentBox.SelectRequimentBox();
        SelectPersonForQuest();
    }

    private void SelectPersonForQuest()
    {
        UIManager.Instance.OpenSelectPersonForQuestListView(PersonSelected, questSelected);
    }   
    private void PersonSelected(PersonInfo newPersonInfo)
    {
        if(SelectedQuestRequiment.SelectedPerson == newPersonInfo || newPersonInfo == null)
        {
            questSelected.UnassginPerson(SelectedQuestRequiment.SelectedPerson);
            SelectedQuestRequiment.DeselectRequimentBox();
            SelectedQuestRequiment.UnSetPerson();
            SelectedQuestRequiment = null;
            UIManager.Instance.OpenQuestListView();
            return;
        }
        if (SelectedQuestRequiment.SelectedPerson != null)
        {
            questSelected.UnassginPerson(SelectedQuestRequiment.SelectedPerson);
        }
        questSelected.AssginPerson(newPersonInfo);
        SelectedQuestRequiment.SetPerson(newPersonInfo, questSelected);
        SelectedQuestRequiment.DeselectRequimentBox();
        SelectedQuestRequiment = null;
        UIManager.Instance.OpenQuestListView();              
    }



    public void StartQuest()
    {
        if(questSelected.StartQuest())
        {
            startBtn.enabled = false;
            SelectQuest(questSelected);
            UIManager.Instance.OpenAvalibalQuestListView();
        }


    }

    
    private void SetProgressBarAndText()
    {
        ProgressBar.parent.parent.gameObject.SetActive(true);
        TimeLeft.text = questSelected.QuestTimer.RemainingDuration.ToString("h'h 'm'm 's's'");
        double ProgressBarPercent = (questSelected.QuestTimer.RemainingDuration.TotalSeconds / (questSelected.QuestTimer.TotalDuration.TotalSeconds / 100));
        ProgressBar.localScale = new Vector3(1 - (float)ProgressBarPercent / 100, 1, 1);
    }


    private void OnTick(object source, EventArgs e)
    {
        if (questSelected.questStaus == Quest.Status.InProgress)
        {
            SetProgressBarAndText();
        }
        else
        {           
            TimeLeft.text = "Completed";
            ProgressBar.localScale = new Vector3(1, 1, 1);
            TimeTickSystem.OnTick -= OnTick;
        }
    }
}
