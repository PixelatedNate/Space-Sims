using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingQuestViewComponet : MonoBehaviour
{

    [SerializeField]
    Transform RequimentPanel, ProgressBar;

    [SerializeField]
    private GameObject RequimentTemplate;

    public WaittingQuest questSelected { get; private set; }

    [SerializeField]
    Button startBtn;

    [SerializeField]
    TextMeshProUGUI TimeLeft, TotalTime;

    private List<QuestRequimentBoxView> QuestRequimentBoxViews = new List<QuestRequimentBoxView>();
    private QuestRequimentBoxView SelectedQuestRequiment;

    [SerializeField]
    Sprite ReadyButtonImg;
    [SerializeField]
    Sprite UnreadyButtonImg;

    [SerializeField]
    Image buttonImge;

    [SerializeField]
    GameObject LogUIPrefab;

    private int LogsTotal;

    [SerializeField]
    Transform LogPanal;

    public void SelectQuest(WaittingQuest waittingQuest)
    {
        questSelected = waittingQuest;
        SetRequiments();
        if (waittingQuest.questStaus == QuestStatus.Available)
        {
            ProgressBar.parent.parent.gameObject.SetActive(false);
            SetButton();
        }
        else if (waittingQuest.questStaus == QuestStatus.InProgress)
        {
            TimeTickSystem.OnTick += OnTick;
            startBtn.interactable = false;
            SetProgressBarAndText();
        }
        else if (waittingQuest.questStaus == QuestStatus.Completed)
        {
            ProgressBar.parent.parent.gameObject.SetActive(true);
            TimeTickSystem.OnTick -= OnTick;
            startBtn.interactable = false;
            TimeLeft.text = "Completed";
            ProgressBar.localScale = new Vector3(1, 1, 1);
        }
        setLogs();
    }

    void SetRequiments()
    {
        foreach (Transform child in RequimentPanel)
        {
            Destroy(child.gameObject);
        }
        TotalTime.text = "";
        if(questSelected.questStaus == QuestStatus.Completed)
        {
            return;
        }
        else if(questSelected.questStaus == QuestStatus.Available)
        {
            TotalTime.text = questSelected.WaittingQuestData.Duration.ToString("h'h 'm'm 's's'");
        }

        for (int i = 0; i < questSelected.WaittingQuestData.QuestRequiments.Numpeople; i++)
        {
            GameObject QuestRequimentItem = GameObject.Instantiate(RequimentTemplate, RequimentPanel);
            QuestRequimentBoxView questRequimentBoxView = QuestRequimentItem.GetComponent<QuestRequimentBoxView>();
            //    questRequimentBoxView.MainQuestView = this;
            if (questSelected.PeopleAssgined.Count >= i + 1)
            {
                questRequimentBoxView.SetPerson(questSelected.PeopleAssgined[i], questSelected);
            }
            else
            {
                questRequimentBoxView.SetRequiments(questSelected);
            }
            QuestRequimentItem.GetComponent<Button>().onClick.AddListener(() => SelectRequimentBox(questRequimentBoxView));
            QuestRequimentBoxViews.Add(questRequimentBoxView);
        }
    }
    public void setLogs()
    {
        foreach (Transform child in LogPanal)
        {
            Destroy(child.gameObject);
        }
        foreach (var log in questSelected.QuestLog)
        {
            var LogUI = GameObject.Instantiate(LogUIPrefab, LogPanal);
            LogUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = log.Discription;
        }
        LogsTotal = questSelected.QuestLog.Count;
    }
    private void SelectRequimentBox(QuestRequimentBoxView requimentBox)
    {
        if (SelectedQuestRequiment == requimentBox)
        {
            SelectedQuestRequiment.DeselectRequimentBox();
            SelectedQuestRequiment = null;
            UIManager.Instance.OpenQuestViewOnQuest(questSelected);
            //UIManager.Instance.OpenQuestListView();              
            return;
        }
        else if (SelectedQuestRequiment != null)
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
        if (SelectedQuestRequiment.SelectedPerson == newPersonInfo || newPersonInfo == null)
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

        SetButton();
    }

    public void SetButton()
    {
        if (questSelected.WaittingQuestData.QuestRequiments.Ismet(questSelected.PeopleAssgined.ToArray()))
        {
            startBtn.interactable = true;
            buttonImge.sprite = ReadyButtonImg;
        }
        else
        {
            startBtn.interactable = false;
            buttonImge.sprite = UnreadyButtonImg;
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
        if (questSelected.questStaus == QuestStatus.InProgress)
        {
            SetProgressBarAndText();
            if (LogsTotal != questSelected.QuestLog.Count)
            {
                setLogs();
            }
        }
        else
        {
            TimeLeft.text = "Completed";
            ProgressBar.localScale = new Vector3(1, 1, 1);
            TimeTickSystem.OnTick -= OnTick;
        }
    }

}
