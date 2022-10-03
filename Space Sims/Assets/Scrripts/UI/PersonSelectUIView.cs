using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonSelectUIView : MonoBehaviour
{
    PersonInfo SelectedPerson;

    [SerializeField]
    Image Head, Body, Hair, Cloths;

    [SerializeField]
    TextMeshProUGUI Name, Age, Room;

    [SerializeField]
    Transform Strength, Dexterity, Intelligence, Wisdom, Charisma;

    public void SetPerson(PersonInfo person)
    {
        SelectedPerson = person;
        UpdateHeadAndBody();
        UpdateText();
        UpdateSkills();
    }




    private void UpdateHeadAndBody()
    {
        Head.sprite = SelectedPerson.Head;
        Head.color = SelectedPerson.SkinColor;
        Body.sprite = SelectedPerson.Body;
        Body.color = SelectedPerson.SkinColor;
        Cloths.sprite = SelectedPerson.Clothes;
        Hair.sprite = SelectedPerson.Hair;
        Hair.color = SelectedPerson.HairColor;
    }

    private void UpdateText()
    {
        Name.text = SelectedPerson.Name;
        Age.text = SelectedPerson.Age.ToString();
        if (SelectedPerson.Room != null)
        {
            Room.text = SelectedPerson.Room.gameObject.name;
        }
    }

    private void UpdateSkills()
    {
        UpdateSkill(SkillsList.Strength, Strength);
        UpdateSkill(SkillsList.Intelligence, Intelligence);
        UpdateSkill(SkillsList.Dexterity, Dexterity);
        UpdateSkill(SkillsList.Charisma, Charisma);
        UpdateSkill(SkillsList.Wisdom, Wisdom);

    }


    private void UpdateSkill(SkillsList skill, Transform skillDots)
    {
        // add my room code here to get skills from an enum;
        for (int i = 0; i < skillDots.childCount; i++)
        {
            if (i < SelectedPerson.skills.GetSkill(skill))
            {
                skillDots.GetChild(i).GetComponent<Image>().color = Color.red;
            }
            else
            {
                skillDots.GetChild(i).GetComponent<Image>().color = Color.gray;
            }
        }

    }




}
