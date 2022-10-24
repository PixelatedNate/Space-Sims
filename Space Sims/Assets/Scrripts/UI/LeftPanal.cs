using System;
using UnityEngine;

public class LeftPanal : MonoBehaviour
{
    bool open;

    [SerializeField]
    PersonSelectUIView personView;
    [SerializeField]
    BuildRoomListView buildRoomView;
    [SerializeField]
    UIButton uiButton;
    [SerializeField]
    UniversalRoomView roomView;
    [SerializeField]
    QuestListView QuestListView;
    [SerializeField]
    PersonListView PersonListView;

    [SerializeField]
    QuestView questView;

    GameObject ActiveView;


    public enum ActiveLSideView
    {
        BuildRoomView,
        QuestListView,
        PersonView,
        RoomView,
        PersonList,
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

    public void SelectRoom(AbstractRoom room)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(roomView.gameObject);
        roomView.SetRoom(room);
    }

    public void SelectQuestListView(Quest.Status staus)
    {
        DisableActiveView();
        activeLSideView = ActiveLSideView.QuestListView;
        uiButton.LeftTabSlideOut();
        SetActiveView(QuestListView.gameObject);
        QuestListView.SetView(staus);
    }

    public void SelectPersonListView(SkillsList? skill)
    {
        DisableActiveView();
        uiButton.LeftTabSlideOut();
        SetActiveView(PersonListView.gameObject);
        PersonListView.SetView();
        activeLSideView = ActiveLSideView.PersonList;
    }

    public void SelectPersonForQuest(Action<PersonInfo> onSelectMethod)
    {
        QuestListView.gameObject.SetActive(false);
        uiButton.LeftTabSlideOut();
        SetActiveView(PersonListView.gameObject);
        PersonListView.GetPerson(onSelectMethod);
        activeLSideView = ActiveLSideView.PersonList;
    }

    public void SelectPersonListView()
    {
        SelectPersonListView(null);
    }

    private void DisableActiveView()
    {   
        if(questView.gameObject.activeInHierarchy)
        {
            questView.gameObject.SetActive(false);
        }
        activeLSideView = null;

        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
    }

    private void SetActiveView(GameObject activeView)
    {
        ActiveView = activeView;
        ActiveView.SetActive(true);
    }

    public void ClearAllView()
    {
        questView.gameObject.SetActive(false);
        activeLSideView = null;
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
            uiButton.LeftTabSlideIn();
        }
    }


}
