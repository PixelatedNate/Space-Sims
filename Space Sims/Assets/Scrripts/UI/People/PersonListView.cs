using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonListView : MonoBehaviour
{
    [SerializeField]
    Transform PersonScrollPanal;
    List<PersonInfo> people = null;

    [SerializeField]
    GameObject PersonListItemTemplate;
    [SerializeField]
    GameObject RemoveFromQuestBtn;

    [SerializeField]
    TextMeshProUGUI Filterlable;

    SkillsList? skillfilter = null;

    private bool Inverted { get; set; }

    private Action<PersonInfo> OnSelectMethod = null;
    private WaittingQuest QuestSelected = null;


    public void SetView()
    {
        QuestSelected = null;
        OnSelectMethod = null;
        people = GlobalStats.Instance.PlayersPeople;
        if (skillfilter == null)
        {
            FilterListByAge(Inverted);
        }
        else
        {
            FilterListBySkill((SkillsList)skillfilter, Inverted);
        }
    }

    public void GetPersonForQuest(Action<PersonInfo> onSelectMethod, WaittingQuest quest)
    {
        QuestSelected = quest;
        OnSelectMethod = onSelectMethod;
        people = GlobalStats.Instance.PlayersPeople;
        FilterListBySkill(quest.requiments.SkillRequiment);
        // ApplyDetailForQuest(quest);

    }




    private void ApplyDetailForQuest(WaittingQuest quest)
    {

        foreach (Transform child in PersonScrollPanal)
        {
            child.GetComponent<PersonListViewItem>().SetQuestText(quest);
        }
        //    GameObject ClearPersonBtn = GameObject.Instantiate(RemoveFromQuestBtn,PersonScrollPanal);
        //  ClearPersonBtn.GetComponent<Button>().onClick.AddListener(() => OnSelectMethod(null));
    }

    private void FilterListBySkill(SkillsList skill, bool inverse = false)
    {
        skillfilter = skill;
        Filterlable.text = skill.ToString() + " " + Icons.GetSkillIconForTextMeshPro(skill);

        if (inverse)
        {
            people.Sort((a, b) => a.skills.GetSkill(skill).CompareTo(b.skills.GetSkill(skill)));
        }
        else
        {
            people.Sort((a, b) => b.skills.GetSkill(skill).CompareTo(a.skills.GetSkill(skill)));
        }
        PopulateList();
    }

    private void FilterListByAge(bool inverse = false)
    {
        skillfilter = null;
        Filterlable.text = "Age " + Icons.GetAgeIconForTextMeshPro();
        if (inverse)
        {
            people.Sort((a, b) => b.Age.CompareTo(a.Age));
        }
        else
        {
            people.Sort((a, b) => a.Age.CompareTo(b.Age));
        }

        PopulateList();
    }


    public void InvertOrder()
    {
        Inverted = !Inverted;
        if (skillfilter == null)
        {
            FilterListByAge(Inverted);
        }
        else
        {
            FilterListBySkill((SkillsList)skillfilter, Inverted);
        }
    }

    public void NextFilter()
    {
        Inverted = false;
        if (skillfilter == null)
        {
            FilterListBySkill((SkillsList)1);
            return;
        }
        else if ((int)skillfilter == Enum.GetValues(typeof(SkillsList)).Length)
        {
            FilterListByAge();
            return;
        }
        else
        {
            FilterListBySkill((SkillsList)((int)skillfilter + 1));
        }
    }
    public void PreviousFilter()
    {
        Inverted = false;
        if (skillfilter == null)
        {
            FilterListBySkill((SkillsList)Enum.GetValues(typeof(SkillsList)).Length);
            return;
        }
        else if ((int)skillfilter == 1)
        {
            FilterListByAge();
            return;
        }
        else
        {
            FilterListBySkill((SkillsList)((int)skillfilter - 1));
        }
    }


    private void PopulateList()
    {
        foreach (Transform child in PersonScrollPanal)
        {
            Destroy(child.gameObject);
        }
        foreach (PersonInfo personInfo in people)
        {
            GameObject personViewItem = GameObject.Instantiate(PersonListItemTemplate, PersonScrollPanal);
            personViewItem.GetComponent<PersonListViewItem>().SetPerson(personInfo, skillfilter);
            if (OnSelectMethod != null)
            {
                personViewItem.GetComponent<Button>().onClick.AddListener(() => OnSelectMethod(personInfo));
                if (QuestSelected)
                {
                    personViewItem.GetComponent<PersonListViewItem>().SetQuestText(QuestSelected);
                }
            }
            else
            {
                personViewItem.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.OpenPersonView(personInfo));
            }
        }
    }
}
