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
    GameObject ActiveView;


    public enum ActiveLSideView
    {
        BuildRoomView,
        QuestListView,
        PersonView,
        RoomView,
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

    private void DisableActiveView()
    {
        activeLSideView = null;
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
    }

    private void SetActiveView(GameObject activeView)
    {
        ActiveView =activeView;
        ActiveView.SetActive(true);
    }

    public void ClearAllView()
    {
        activeLSideView = null;
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
            uiButton.LeftTabSlideIn();
        }
    }


}
