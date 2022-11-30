using RDG;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public bool IsNavigation { get; private set; } = false;

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
    NavTimerProgressBarUIView navTimerUI;
    [SerializeField]
    ConformationUI _conformationUI;
    [SerializeField]
    AlertsUI alertsUI;

    private GameObject MainLight { get; set; }
    private GameObject MainCamera { get; set; }
    public void UpdateTopBar(GameResources currentResources, GameResources deltaResources, int numberofPoeple, int maxPeople, GameResources maxResources)
    {
        topBar.SetValues(currentResources, deltaResources, numberofPoeple, maxPeople, maxResources);
    }

    public void DisplayPerson(PersonInfo personInfo)
    {
        RoomGridManager.Instance.SetBuildMode(false);
        leftPanal.SelectPerson(personInfo);
        alertsUI.CloseAlerts();
    }

    public void DisplayPlanet(Planet planet)
    {
        RoomGridManager.Instance.SetBuildMode(false);
        leftPanal.SelectPlanet(planet);
        alertsUI.CloseAlerts();
    }

    public void DisplayRoomView(AbstractRoom room)
    {
        RoomGridManager.Instance.SetBuildMode(false);
        leftPanal.SelectRoom(room);
        alertsUI.CloseAlerts();
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
            alertsUI.CloseAlerts();
    }
    public void OpenQuestListView(Quest.Status status = Quest.Status.Available)
    {
            leftPanal.SelectQuestListView(status);       
            alertsUI.CloseAlerts();
    }

    public void OpenQuestViewOnQuest(Quest quest)
    {
        leftPanal.OpenOnQuest(quest);
        alertsUI.CloseAlerts();
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
        alertsUI.CloseAlerts();
    }
    public void OpenSelectPersonForQuestListView(Action<PersonInfo> OnSelectMethod, Quest quest)
    {
        leftPanal.activeLSideView = LeftPanal.ActiveLSideView.PersonList;
        leftPanal.SelectPersonForQuest(OnSelectMethod, quest);
        alertsUI.CloseAlerts();
    }

    public void OpenBuildRoomMenu(Vector3Int roomCellPos)
    {
        leftPanal.OpenBuildRoomView(roomCellPos);
        alertsUI.CloseAlerts();
    }

     public void ClearLeftPanal()
    {
        leftPanal.ClearAllView();
    }  
     public void ClearAlertsPanal()
    {
        alertsUI.CloseAlerts();
    }

    public void DeselectAll()
    {
        leftPanal.ClearAllView();
        alertsUI.CloseAlerts();
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

    public void ToggleNavigation()
    {
        if (IsNavigation)
        {
            LoadMainShipView();
        }
        else
        {
            LoadNavigation();
        }
    }

    public bool LoadNavigation()
    {
        if (!IsNavigation)
        {
            DeselectAll();
            MainLight = GameObject.FindGameObjectWithTag("MainLight");
            MainCamera = Camera.main.gameObject;
            MainCamera.SetActive(false);
            MainLight.SetActive(false);
            SceneManager.LoadScene("Navigation", LoadSceneMode.Additive);
            IsNavigation = true;
            return true;
        }
        return false;
    }


    public bool LoadMainShipView()
    {
        if (IsNavigation)
        {
            DeselectAll();
            SceneManager.UnloadSceneAsync("Navigation");
            MainCamera.SetActive(true);
            MainLight.SetActive(true);
            IsNavigation = false;
            return true;
        }
        return false;
    }


    public void TrackNavTimer(TimeDelayManager.Timer timer, Planet targetPlanet)
    {
        navTimerUI.gameObject.SetActive(true);
        navTimerUI.TrackTimer(timer, targetPlanet);
    }
    

}   
