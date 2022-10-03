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

    GameObject ActiveView;




    public void OpenBuildRoomView(Vector3Int roomCellPos)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        uiButton.LeftTabSlideOut();
        //ClearAllView();
        ActiveView = buildRoomView.gameObject;
        ActiveView.SetActive(true);
        buildRoomView.EnableView(roomCellPos);
    }

    public void SelectPerson(PersonInfo personInfo)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        uiButton.LeftTabSlideOut();
        ActiveView = personView.gameObject;
        ActiveView.SetActive(true);
        personView.SetPerson(personInfo);
    }

    public void SelectRoom(AbstractRoom room)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        uiButton.LeftTabSlideOut();
        roomView.SetRoom(room);
        ActiveView = roomView.gameObject;
        ActiveView.SetActive(true);
    }

    public void ClearAllView()
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
            uiButton.LeftTabSlideIn();
        }
    }


}
