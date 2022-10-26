using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonListViewItem : MonoBehaviour
{
    PersonInfo person;

    [SerializeField]
    TextMeshProUGUI PersonName, Age, Strenght, Dexterity, Inteligence, Wisdom, Charisma, QuestText;
    [SerializeField]
    Image head, Body, Clothes;

    public void SetPerson(PersonInfo person)
    {
        this.person = person;
        SetStats();
        SetPersonImg();
    }
    private void SetPersonImg()
    {
        head.sprite = person.Head;
        Body.sprite = person.Body;
        Clothes.sprite = person.Clothes;
    }

    public void SetQuestText(Quest quest)
    {
        Debug.Log(person);
        if (person.IsQuesting)
        {
            GetComponent<Button>().interactable = false;
            QuestText.color = Color.red;
            QuestText.text = "On Quest";
        }
        else if (quest.PeopleAssgined.Contains(person))
        {
            GetComponent<Button>().interactable = false;
            QuestText.color = Color.green;
            QuestText.text = "Allready Selcted";
        }
        else if (!quest.DosePersonMeetRequiment(person)) 
        {
            GetComponent<Button>().interactable = false;
            QuestText.color = Color.green;
            QuestText.text = "Missing Skill Requiment";
        }

    }

    private void SetStats()
    {
        PersonName.text = person.Name;
        Age.text = person.Age.ToString();
        Strenght.text = person.skills.GetSkill(SkillsList.Strength).ToString();
        Dexterity.text = person.skills.GetSkill(SkillsList.Dexterity).ToString();
        Inteligence.text = person.skills.GetSkill(SkillsList.Intelligence).ToString();
        Wisdom.text = person.skills.GetSkill(SkillsList.Wisdom).ToString();
        Charisma.text = person.skills.GetSkill(SkillsList.Charisma).ToString();

    }
}
