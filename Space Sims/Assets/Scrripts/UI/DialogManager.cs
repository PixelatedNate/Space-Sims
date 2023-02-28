using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject[] DialogChat;

    public Dialog activeDialog = null;

    void Awake()
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

    public void StartDiaglogIndex(int i)
    {
        if (activeDialog != null)
        {
            activeDialog.EndDialog();
        }

        ButtonManager.Instance.SetAllButtons(false);
        DialogChat[i].SetActive(true);
    }

    public void WaitForPersonInFuelRoom()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
        AbstractRoom.OnPersonAssgined += PeronInRoom;
    }

    public void WaitForPersonInFoodRoom()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
        AbstractRoom.OnPersonAssgined += PeronInRoom;
    }


    private void PeronInRoom(AbstractRoom abstractRoom, Person person)
    {
        if (abstractRoom.RoomType == RoomType.Food)
        {
            activeDialog.NextLine();
            AbstractRoom.OnPersonAssgined -= PeronInRoom;
        }
        else if (abstractRoom.RoomType == RoomType.Fuel)
        {
            activeDialog.EndDialog();
            AbstractRoom.OnPersonAssgined -= PeronInRoom;
        }
        else if (activeDialog.HasAnotherLine())
        {
            activeDialog.NextLine();
        }
    }

    private void OnRoomBuilt(AbstractRoom room)
    {
        if (room.RoomType == RoomType.Minerals)
        {
            StartDiaglogIndex(6);
            ButtonManager.Instance.SetAllButtons(true);
        }
        if (room.RoomType == RoomType.Fuel)
        {
            StartDiaglogIndex(2);
        }
        if (room.RoomType == RoomType.Food)
        {
            StartDiaglogIndex(4);
        }
        AbstractRoom.OnRoomBuilt -= OnRoomBuilt;
    }


    public void WaitForRoomBuilt()
    {
        AbstractRoom.OnRoomBuilt += OnRoomBuilt;
    }

    public void WaitForFilterToWisdom()
    {
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.PersonListFillterLeft, true);
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.PersonListFillterRight, true);
        PersonListView.OnFiltterChange += FilterByWisdom;
    }

    private void FilterByWisdom(SkillsList? skill)
    {
        DisableCrewWisdom();
        if (skill == SkillsList.Wisdom)
        {
            ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.PersonListFillterLeft, false);
            ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.PersonListFillterRight, false);
            activeDialog.NextLine();
            PersonListView.OnFiltterChange -= FilterByWisdom;
        }
    }


    private void OnBuildMenuOpen()
    {
        activeDialog.NextLine();
        UIManager.OnBuildMenuOpen -= OnBuildMenuOpen;
    }

    public void DisableTouchAndZoom(bool value)
    {
        TouchControls.EnableCameramovemntAndSelection(!value);
    }


    public void WaitUntillBuildRoomIsOpened()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.BuildRoom, true);
        UIManager.OnBuildMenuOpen += OnBuildMenuOpen;
    }

    public void DisableCrewWisdom()
    {
        var peopleLUList = GameObject.FindObjectsOfType<PersonListViewItem>();
        foreach(var people in peopleLUList)
        {
            if(people.person.skills.Wisdom > 5)
            {
                people.gameObject.name = "WisdomPerson";
            }
            people.GetComponent<Button>().interactable = false;

        }
    }

    public void EnableAllButtons()
    {
        ButtonManager.Instance.SetAllButtons(true);
    }



}
