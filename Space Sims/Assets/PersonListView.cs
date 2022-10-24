using System;
using System.Collections;
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
    TextMeshProUGUI Filterlable;

    SkillsList? skillfilter = null;

    private Action<PersonInfo> OnSelectMethod = null;


    public void SetView()
    {
        OnSelectMethod = null;
        people = GlobalStats.Instance.PlayersPeople;
        FilterListByAge();
    }


    public void GetPerson(Action<PersonInfo> onSelectMethod)
    {
        OnSelectMethod = onSelectMethod;
        people = GlobalStats.Instance.PlayersPeople;
        FilterListByAge();
    }


    private void FilterListBySkill(SkillsList skill)
       {
         skillfilter = skill;
         Filterlable.text = skill.ToString();
         people.Sort((a,b) => b.skills.GetSkill(skill).CompareTo(a.skills.GetSkill(skill)));
         PopulateList(); 
       }

       private void FilterListByAge()
       {
         skillfilter = null;
         Filterlable.text = "Age";
         people.Sort((a,b) => a.Age.CompareTo(b.Age));
         PopulateList(); 
       }


    public void NextFilter()
    {
        if (skillfilter == null)
        {
            FilterListBySkill((SkillsList)1);
            return;
        }
        else if((int)skillfilter == Enum.GetValues(typeof(SkillsList)).Length)
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
        foreach(PersonInfo personInfo in people)
        {
           GameObject personViewItem = GameObject.Instantiate(PersonListItemTemplate ,PersonScrollPanal);
            personViewItem.GetComponent<PersonListViewItem>().SetPerson(personInfo);
            if (OnSelectMethod != null)
            {               
                personViewItem.GetComponent<Button>().onClick.AddListener(() => OnSelectMethod(personInfo));
            }
            else
            {
                personViewItem.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.DisplayPerson(personInfo));
            }
        }
    }
}
