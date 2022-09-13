using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonSelectUIView : MonoBehaviour
{
    PersonInfo SelectedPerson;

    [SerializeField]
    Image Head; 
    [SerializeField]
    Image Body;

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
        Body.sprite = SelectedPerson.Body;
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
        for(int i = 0; i < skillDots.childCount; i++)
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
