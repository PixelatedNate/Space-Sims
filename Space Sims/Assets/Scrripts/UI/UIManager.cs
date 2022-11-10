using RDG;
using System;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField]
    ConformationUI _conformationUI;
    [SerializeField]
    AlertsUI alertsUI;

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

    public void OpenAvalibalQuestListView()
    {
        OpenQuestListView(Quest.Status.Available);
    }
    public void OpenInProgressQuestView()
    {
        OpenQuestListView(Quest.Status.InProgress);
    }

    public void OpenCompletedQuestViewView()
    {
        OpenQuestListView(Quest.Status.Completed);
    }

    public void OpenQuestListViewBtn()
    {
        if (leftPanal.activeLSideView == LeftPanal.ActiveLSideView.QuestListView)
        {
            leftPanal.ClearAllView();
        }
        else
        {
            OpenQuestListView();  
        }
    }
    public void OpenQuestListView(Quest.Status status = Quest.Status.Available)
    {
            leftPanal.SelectQuestListView(status);       
    }

    public void OpenQuestViewOnQuest(Quest quest)
    {
        leftPanal.OpenOnQuest(quest);
    }



    public void OpenPersonListView()
    {
        if (leftPanal.activeLSideView == LeftPanal.ActiveLSideView.PersonList)
        {
            leftPanal.ClearAllView();
        }
        else
        {
            leftPanal.SelectPersonListView();
        }
    }
    public void OpenSelectPersonForQuestListView(Action<PersonInfo> OnSelectMethod, Quest quest)
    {
        leftPanal.activeLSideView = LeftPanal.ActiveLSideView.PersonList;
        leftPanal.SelectPersonForQuest(OnSelectMethod, quest);
    }

    public void OpenBuildRoomMenu(Vector3Int roomCellPos)
    {
        leftPanal.OpenBuildRoomView(roomCellPos);
    }

   

    public void DeselectAll()
    {
        leftPanal.ClearAllView();
        alertsUI.closeAlerts();

    }

    /// <summary>
    /// A popUp Conformation UI with yes or no Option
    /// Will close after selection
    /// </summary>
    /// <param name="onAccept">method on accept</param>
    /// <param name="onDecline">method on decline</param>
    public void Conformation(UnityAction onAccept, UnityAction onDecline, string text)
    {
        _conformationUI.SetListeners(onAccept,onDecline);
        _conformationUI.setText(text);
    }
    /// <summary>
    ///  A popUp Conformation UI with yes or no Option
    /// Will close after selection
    /// </summary>
    /// <param name="onAccept">Method on accept</param>
    public void Conformation(UnityAction onAccept, string text)
    {
        _conformationUI.gameObject.SetActive(true);
        _conformationUI.SetListeners(onAccept,null);
        _conformationUI.setText(text);
    }



    public void ToggleRoomBuildMode()
    {
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_CLICK);
       SoundManager.Instance.PlaySound(SoundManager.Sound.UIclick);
       bool buildRoom = RoomGridManager.Instance.TogleBuildMode();
       DeselectAll(); // will clear either slecet item or build RoomMenu
    }

}
