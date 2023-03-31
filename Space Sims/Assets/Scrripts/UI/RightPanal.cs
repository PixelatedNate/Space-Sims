using UnityEngine;

public class RightPanal : MonoBehaviour
{

    GameObject ActiveView;
    [SerializeField]
    UIButton UiButton;

    [SerializeField]
    QuestView questView;

    [SerializeField]
    ClothSelectionMenuUIView PersonCosmetics;

    [SerializeField]
    PersonBackStoryView backStoryView;

    [SerializeField]
    GearSkillFilterList GearSkillFilterList;

    public enum ActiveRSideView
    {
        Quest,
        Cosmetic,
        BackStory,
        GearSkillFinterList
    }

    public ActiveRSideView? activeRSideView { get; private set; } = null;


    public void OpenQuest(AbstractQuest quest)
    {
        if (activeRSideView == ActiveRSideView.Quest)
        {
            if (questView.questSelected == quest)
            {
                ClearAllView();
            }
        }

        activeRSideView = ActiveRSideView.Quest;
        UiButton.RightTabSlideOut();
        SetActiveView(questView.gameObject);
        questView.SelectQuest(quest);
    }

    public void OpenPersonCosmetic(PersonInfo personInfo)
    {
        activeRSideView = ActiveRSideView.Cosmetic;
        UiButton.RightTabSlideOut();
        SetActiveView(PersonCosmetics.gameObject);
        PersonCosmetics.PopulateList(personInfo);
    }

    public void OpenPersonBackStory(PersonInfo personInfo)
    {
        activeRSideView = ActiveRSideView.BackStory;
        UiButton.RightTabSlideOut();
        SetActiveView(backStoryView.gameObject);
        backStoryView.SetBackStory(personInfo);
    }
    public void OpenGearList(PersonInfo personInfo)
    {
        activeRSideView = ActiveRSideView.GearSkillFinterList;
        UiButton.RightTabSlideOut();
        SetActiveView(GearSkillFilterList.gameObject);
        GearSkillFilterList.SetView(personInfo);
    }




    private void SetActiveView(GameObject activeView)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        ActiveView = activeView;
        ActiveView.SetActive(true);
    }



    public void ClearAllView()
    {
        activeRSideView = null;
        if (ActiveView != null)
        {
            UiButton.RightTabSlideIn();
        }
    }

}
