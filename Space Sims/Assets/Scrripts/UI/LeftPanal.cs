using System;
using UnityEngine;

public class LeftPanal : MonoBehaviour
{
    bool open;

    [SerializeField]
    PersonSelectUIView personView;
    [SerializeField]
    planetView planetView;
    [SerializeField]
    BuildRoomListView buildRoomView;
    [SerializeField]
    UIButton uiButton;
    [SerializeField]
    UniversalRoomView roomView;
    [SerializeField]
    QuestListView QuestListView;
    [SerializeField]
    PersonSkillListFilterView PersonListView;
    //PersonListView PersonListView;
    [SerializeField]
    GameObject MilstonListView;

    [SerializeField]
    GameObject clearAllButton;

    [SerializeField]
    QuestView questView;

    [SerializeField]
    ClothSelectionMenuUIView ClothUIMenu;

    GameObject ActiveView;


    public enum ActiveLSideView
    {
        BuildRoomView,
        QuestListView,
        PersonView,
        RoomView,
        PersonList,
        MileStones,
    }



    public ActiveLSideView? activeLSideView { get; set; } = null;


    public void OpenBuildRoomView(Vector3Int roomCellPos)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(buildRoomView.gameObject);
        buildRoomView.EnableView(roomCellPos);
    }

    public void SelectPerson(PersonInfo personInfo)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(personView.gameObject);
        personView.SetPerson(personInfo);
    }

    public void OpenMilston()
    {
        DisableActiveView();
        activeLSideView = ActiveLSideView.MileStones;
        uiButton.LeftTabSlideOut();
        SetActiveView(MilstonListView);
    }


    public void SelectPlanet(PlanetContainer planet)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(planetView.gameObject);
        planetView.SetPlanet(planet);
    }



    public void SelectRoom(AbstractRoom room)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(roomView.gameObject);
        roomView.SetRoom(room);
    }


    public void OpenOnQuest(AbstractQuest quest)
    {
        DisableActiveView(false);
        activeLSideView = ActiveLSideView.QuestListView;
        uiButton.LeftTabSlideOut();
        SetActiveView(QuestListView.gameObject);
        QuestListView.OpenOnQuest(quest);
    }

    public void SelectQuestListView(QuestStatus staus)
    {
        DisableActiveView(false);
        activeLSideView = ActiveLSideView.QuestListView;
        uiButton.LeftTabSlideOut();
        SetActiveView(QuestListView.gameObject);
        QuestListView.SetView(staus);
    }

    public void SelectPersonListView(Predicate<PersonInfo> filtter = null)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(PersonListView.gameObject);
        PersonListView.SetView(filtter);
        activeLSideView = ActiveLSideView.PersonList;
    }


    public void SelectPersonForQuest(Action<PersonInfo> onSelectMethod, WaittingQuest quest)
    {
        QuestListView.gameObject.SetActive(false);
        uiButton.LeftTabSlideOut();
        SetActiveView(PersonListView.gameObject);
        PersonListView.GetPersonForQuest(onSelectMethod, quest);
        activeLSideView = ActiveLSideView.PersonList;
    }

    private void DisableQuestView()
    {
        if (questView.gameObject.activeInHierarchy)
        {
            questView.gameObject.SetActive(false);
        }
    }

    private void DisableActiveView()
    {
        DisableActiveView(true);
    }

    private void DisableActiveView(bool disableQuestView)
    {
        //ClothUIMenu.gameObject.SetActive(false);
        if (disableQuestView)
        {
            DisableQuestView();
        }

        activeLSideView = null;
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
    }

    private void SetActiveView(GameObject activeView)
    {
        clearAllButton.SetActive(true);
        ActiveView = activeView;
        ActiveView.SetActive(true);
    }

    public void ClearAllView()
    {
        clearAllButton.SetActive(false);
        ClothUIMenu.gameObject.SetActive(false);
        activeLSideView = null;
        if (ActiveView != null)
        {
            uiButton.LeftTabSlideIn();
        }
    }


}
