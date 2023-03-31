using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonSkillListFilterView : AbstractSkillFilterList<PersonInfo>
{

    [SerializeField]
    GameObject PersonListItemTemplate;
    [SerializeField]
    GameObject RemoveFromQuestBtn;

    private Action<PersonInfo> OnSelectMethod = null;
    private WaittingQuest QuestSelected = null;


    public void GetPersonForQuest(Action<PersonInfo> onSelectMethod, WaittingQuest quest)
    {
        QuestSelected = quest;
        OnSelectMethod = onSelectMethod;
        Items = GlobalStats.Instance.PlayersPeople;
        FilterListBySkill(quest.WaittingQuestData.QuestRequiments.SkillRequiment);
        // ApplyDetailForQuest(quest);
    }

    private void ApplyDetailForQuest(WaittingQuest quest)
    {
        foreach (Transform child in ScrollPanal)
        {
            child.GetComponent<PersonListViewItem>().SetQuestText(quest);
        }
    }

    public override List<PersonInfo> GetAllItems()
    {
        return GlobalStats.Instance.PlayersPeople;
    }

    public override int FilterSkill(PersonInfo a, PersonInfo b, SkillsList skill)
    {
        return a.skills.GetSkill(skill).CompareTo(b.skills.GetSkill(skill));
    }

    public override int FilterByAge(PersonInfo a, PersonInfo b)
    {
        return a.Age.CompareTo(b.Age);
    }



    protected override void SpawnNewUIItemConponent(PersonInfo item, SkillsList? skill)
    {
         GameObject personViewItem = GameObject.Instantiate(PersonListItemTemplate, ScrollPanal);
         personViewItem.GetComponent<PersonListViewItem>().SetPerson(item, skill);
         if(QuestSelected != null)
         {
             personViewItem.GetComponent<PersonListViewItem>().SetQuestText(QuestSelected);
            this.QuestSelected = null;
         }
         
         if (OnSelectMethod != null)
         {
             personViewItem.GetComponent<Button>().onClick.AddListener(() => OnSelectMethod(item));
         }
         else
         {
            personViewItem.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.OpenPersonView(item)); ;
         }
    }

}
