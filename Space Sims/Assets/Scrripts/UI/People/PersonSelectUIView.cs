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

    [SerializeField]
    GameObject QuestLinkBtn;

    public void SetPerson(PersonInfo person)
    {
        SelectedPerson = person;
        UpdateHeadAndBody();
        UpdateText();
        UpdateSkills();
        if (person.PersonMonoBehaviour != null)
        {
            person.PersonMonoBehaviour.SetOutline(true);
        }
        if (person.CurrentQuest != null)
        {
            QuestLinkBtn.SetActive(true);
        }
        else
        {
            QuestLinkBtn.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (SelectedPerson.PersonMonoBehaviour != null)
        {
            SelectedPerson.PersonMonoBehaviour.SetOutline(false);
        }
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
            Room.text = SelectedPerson.Room.RoomName;
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

    public void GoToPerson()
    {
        if (SelectedPerson.IsQuesting)
        {
            UIManager.Instance.OpenQuestViewOnQuest(SelectedPerson.CurrentQuest);
        }
        else
        {
            CameraManager.Instance.CameraFocus(SelectedPerson.PersonMonoBehaviour.gameObject);
        }
    }
    public void FollowPerson()
    {
        if (SelectedPerson.IsQuesting)
        {
            UIManager.Instance.OpenQuestViewOnQuest(SelectedPerson.CurrentQuest);
        }
        else
        {
            CameraManager.Instance.CameraFollow(SelectedPerson.PersonMonoBehaviour.gameObject);
        }
    }

    public void OpenPersonRoom()
    {
        UIManager.Instance.OpenRoomView(SelectedPerson.Room);
    }


    private void UpdateSkill(SkillsList skill, Transform skillDots)
    {
        // add my room code here to get skills from an enum;
        for (int i = 0; i < skillDots.childCount; i++)
        {
            if (i < SelectedPerson.skills.GetSkill(skill))
            {
                skillDots.GetChild(i).GetComponent<Image>().color = SkillColourMap.GetSkillColour(skill);
            }
            else
            {
                skillDots.GetChild(i).GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void ToggleCosmeticChageView()
    {
        UIManager.Instance.OpenPersonCosmetics(SelectedPerson);
    }

    public void TogglePersonBackStory()
    {
        UIManager.Instance.OpenPersonBackStory(SelectedPerson);
    }

    public void GoToPersonLinkedQuest()
    {
        if (SelectedPerson.CurrentQuest != null)
        {
            UIManager.Instance.OpenQuestView(SelectedPerson.CurrentQuest);
        }
    }



}
