using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonListViewItem : MonoBehaviour
{
    public PersonInfo person { get; private set; }

    [SerializeField]
    TextMeshProUGUI PersonName, Age, Strenght, Dexterity, Inteligence, Wisdom, Charisma, QuestText;
    [SerializeField]
    Image head, Body, Clothes;

    public void SetPerson(PersonInfo person, SkillsList? filter)
    {
        this.person = person;
        SetStats(filter);
        SetPersonImg();
    }
    private void SetPersonImg()
    {
        head.sprite = person.Head;
        head.color = person.SkinColor;
        Body.sprite = person.Body;
        Body.color = person.SkinColor;
        Clothes.sprite = person.Clothes;
    }

    public void SetQuestText(WaittingQuest quest)
    {
        if (person.IsQuesting)
        {
            GetComponent<Button>().interactable = false;
            QuestText.color = Color.red;
            QuestText.text = "On Quest";
        }
        else if (person.IsCargoForTransportQust)
        {
            GetComponent<Button>().interactable = false;
            QuestText.color = Color.red;
            QuestText.text = "Cargo for Quest";
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
            QuestText.color = Color.red;
            QuestText.text = "Missing Skill Requiment";
        }
        else
        {
            QuestText.text = "";
        }

    }

    private void SetStats(SkillsList? filter)
    {
        PersonName.text = person.Name;
        Age.text = person.Age.ToString();
        if (filter == null)
        {
            Age.fontSize += 2;
        }
        Strenght.text = person.skills.GetSkill(SkillsList.Strength).ToString();
        if (filter == SkillsList.Strength)
        {
            Strenght.fontSize += 2;
        }
        Dexterity.text = person.skills.GetSkill(SkillsList.Dexterity).ToString();
        if (filter == SkillsList.Dexterity)
        {
            Dexterity.fontSize += 2;
        }

        Inteligence.text = person.skills.GetSkill(SkillsList.Intelligence).ToString();
        if (filter == SkillsList.Intelligence)
        {
            Inteligence.fontSize += 2;
        }

        Wisdom.text = person.skills.GetSkill(SkillsList.Wisdom).ToString();
        if (filter == SkillsList.Wisdom)
        {
            Wisdom.fontSize += 2;
        }

        Charisma.text = person.skills.GetSkill(SkillsList.Charisma).ToString();
        if (filter == SkillsList.Charisma)
        {
            Charisma.fontSize += 2;
        }

    }
}
