using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRequimentBoxView : MonoBehaviour
{
    [SerializeField]
    GameObject PersonDisplay;
    [SerializeField]
    GameObject personIcon;
    [SerializeField]
    GameObject Outline;

    [SerializeField]
    Image Head, Body, Cloths, Requiment, Background;

    [SerializeField]
    TextMeshProUGUI RequimentValue, PersonName;

    public PersonInfo PersonInfo = null;
    public Quest questSelected;

    public QuestView MainQuestView { private get; set; }

    private void Start()
    {
        AddListener();
    }

    public void SetPerson(PersonInfo personInfo, Quest quest)
    {
        questSelected = quest;
        PersonInfo = personInfo;
        personIcon.SetActive(false);
        PersonDisplay.SetActive(true);
        Head.sprite = personInfo.Head;
        Body.sprite = personInfo.Body;
        Cloths.sprite = personInfo.Clothes;
        PersonName.text = personInfo.Name; 
        if(personInfo.skills.GetSkill(quest.requiments.SkillRequiment) < quest.requiments.skillValueMin)
        {
            Background.color = Color.red;
        }
        else
        {
            Background.color = Color.green;
        }
        RequimentValue.text = personInfo.skills.GetSkill(quest.requiments.SkillRequiment).ToString();
    }
    public void SetRequiments(Quest quest)
    {
        questSelected = quest;
        PersonInfo = null;
        personIcon.SetActive(true);
        PersonDisplay.SetActive(false);
        Requiment.sprite = Icons.GetSkillIcon(quest.requiments.SkillRequiment);
        RequimentValue.text = quest.requiments.skillValueMin.ToString() + "+";
    }

    public void AddListener()
    {
        GetComponent<Button>().onClick.AddListener(() => SelectPersonForQuest());
    }

    public void DeselectRequimentBox()
    {
        Outline.SetActive(false);
    }

    private void SelectPersonForQuest()
    {
        Outline.SetActive(true);
        MainQuestView.SetActiveRequimentBoxView(this);
        UIManager.Instance.OpenSelectPersonForQuestListView(PersonSelected);
    }

    private void PersonSelected(PersonInfo newPersonInfo)
    {
        Outline.SetActive(false);
        if(PersonInfo != null)
        {
            questSelected.UnassginPerson(PersonInfo);
        }
        questSelected.AssginPerson(newPersonInfo);
        SetPerson(newPersonInfo,questSelected);
    }
}
