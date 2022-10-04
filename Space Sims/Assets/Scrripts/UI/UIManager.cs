using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    [SerializeField]
    LeftPanal leftPanal;
    [SerializeField]
    TopBar topBar;



    public void UpdateTopBar(GameResources currentResources, GameResources deltaResources, int numberofPoeple, int maxPeople, GameResources maxResources)
    {
        topBar.SetValues(currentResources, deltaResources, numberofPoeple, maxPeople, maxResources);
    }

    public void DisplayPerson(PersonInfo personInfo)
    {
        RoomGridManager.Instance.SetBuildMode(false);
        leftPanal.SelectPerson(personInfo);
    }

    public void DisplayRoomView(AbstractRoom room)
    {
        RoomGridManager.Instance.SetBuildMode(false);
        leftPanal.SelectRoom(room);
    }

    public void OpenBuildRoomMenu(Vector3Int roomCellPos)
    {
        leftPanal.OpenBuildRoomView(roomCellPos);
    }

    public void DeselectAll()
    {
        leftPanal.ClearAllView();
    }


    public void ToggleRoomBuildMode()
    {
        RoomGridManager.Instance.TogleBuildMode();
    }

}
