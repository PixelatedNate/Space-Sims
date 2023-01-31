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

    public PersonInfo SelectedPerson = null;
    public WaittingQuest questSelected;

    //public QuestView MainQuestView { private get; set; }

    private void Start()
    {
        AddListener();
    }

    public void SetPerson(PersonInfo personInfo, WaittingQuest quest)
    {
        if (quest.questStaus == QuestStatus.InProgress || quest.questStaus == QuestStatus.Completed)        
        {
            GetComponent<Button>().interactable = false;
        }

        questSelected = quest;
        SelectedPerson = personInfo;
        personIcon.SetActive(false);
        PersonDisplay.SetActive(true);
        Head.sprite = personInfo.Head;
        Head.color = personInfo.SkinColor;
        Body.sprite = personInfo.Body;
        Body.color = personInfo.SkinColor;
        Cloths.sprite = personInfo.Clothes;
        PersonName.text = personInfo.Name;
        if (personInfo.skills.GetSkill(quest.requiments.SkillRequiment) < quest.requiments.skillValueMin)
        {
            Background.color = Color.red;
        }
        else
        {
            Background.color = Color.green;
        }
        RequimentValue.text = personInfo.skills.GetSkill(quest.requiments.SkillRequiment).ToString();
    }

    public void UnSetPerson()
    {
        SetRequiments(questSelected);
    }

    public void SetRequiments(WaittingQuest quest)
    {
        Background.color = Color.gray;
        questSelected = quest;
        SelectedPerson = null;
        personIcon.SetActive(true);
        PersonDisplay.SetActive(false);
        Requiment.sprite = Icons.GetSkillIcon(quest.requiments.SkillRequiment);
        RequimentValue.text = quest.requiments.skillValueMin.ToString() + "+";
    }

    public void AddListener()
    {
        // GetComponent<Button>().onClick.AddListener(() => SelectPersonForQuest());
    }

    public void DeselectRequimentBox()
    {
        Outline.SetActive(false);
    }

    public void SelectRequimentBox()
    {
        Outline.SetActive(true);
    }


    /*
    private void SelectPersonForQuest()
    {
        Outline.SetActive(true);
        MainQuestView.SetActiveRequimentBoxView(this);
        UIManager.Instance.OpenSelectPersonForQuestListView(PersonSelected);
    }

    private void PersonSelected(PersonInfo newPersonInfo)
    {
        Outline.SetActive(false);
        if(SelectedPerson != null)
        {
            questSelected.UnassginPerson(SelectedPerson);
        }
        questSelected.AssginPerson(newPersonInfo);
        SetPerson(newPersonInfo,questSelected);
    }
    */
}
