using System;
using System.Collections.Generic;
using UnityEngine;

public static class NavigationManager
{

    public static List<PlanetContainer> vistedPlalnets = new List<PlanetContainer>();

    public static bool ReachedFirstPlanet { private set; get; } = false;
 
 //   public static event Action<PlanetContainer> OnArriveAtPlanet;


    public static Dictionary<PlanetData, PlanetContainer> PlanetList { get; set; } = new Dictionary<PlanetData, PlanetContainer>();
    public static bool InNavigation { get; private set; } = false;

    public static TimeDelayManager.Timer _navTimer;

    public static float UniversSpeedModifyer = 0.02f;

    public static PlanetContainer CurrentPlanet { get; set; }
    public static PlanetContainer TargetPlanet { get; set; }
    public static PlanetContainer PreviousPlanet { get; set; }

    public static bool NavigateToTargetPlanet(PlanetContainer planet)
    {
        if (planet == CurrentPlanet)
        {
            Debug.LogWarning("trying to traval to a planet the player is allready at");
            return false;
        }
        if (InNavigation)
        {
            return false; // can't navigate to a diffrent palnet when allready in montion
        }
        TouchControls.DeceletAll();
        BackgroundManager.Instance.setBackgroundToInTransit();
        DateTime ariveralTime = DateTime.Now.Add(CalcualteTravleTime(planet));
        PreviousPlanet = CurrentPlanet;
        CurrentPlanet?.LeavePlanet();
        CurrentPlanet = null;
        TargetPlanet = planet;
        InNavigation = true;
        _navTimer = new TimeDelayManager.Timer(ariveralTime, ArriveAtPlanet);
        TimeDelayManager.Instance.AddTimer(_navTimer);
        UIManager.Instance.TrackNavTimer(_navTimer, TargetPlanet);
        UIManager.Instance.ToggleNavigation();
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.Navigation, false);
        // start timer
        return true;

    }

    private static void ArriveAtPlanet()
    {
        InNavigation = false;
        CurrentPlanet = TargetPlanet;
        CurrentPlanet.ArriveAtPlanet();
        vistedPlalnets.Add(CurrentPlanet);
        //  QuestManager.SetAvalibleQuest(CurrentPlanetQuests);
        TargetPlanet = null;
        BackgroundManager.Instance.setBackground(CurrentPlanet.planetData.Background);
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.Navigation, true);
        foreach (AbstractQuest quest in QuestManager.GetQuestsByStaus(QuestStatus.InProgress))
        {
            if (quest.GetType() == typeof(TransportQuest))
            {
                TransportQuest tq = (TransportQuest)quest;
                if (tq.transportQuestData.TargetPlanetData == CurrentPlanet.planetData)
                {
                    tq.CompleatQuest();
                }
            }
        }

        if (!ReachedFirstPlanet)
        {
            DialogManager.Instance.StartDiaglogIndex(7);
            ReachedFirstPlanet = true;
        }

        }

    public static void Load(NavigationSaveData saveData)
    {
        InNavigation = saveData.InNavigation;
        ReachedFirstPlanet = saveData.ReachedFirstPlanet;

        foreach (string s in saveData.planetId)
        {
            PlanetConttainerSaveData planetContainerSaveData = SaveSystem.LoadData<PlanetConttainerSaveData>(SaveSystem.PlanetPath + "/" + s + SaveSystem.PlanetPrefix);
            PlanetContainer plantContaire = new PlanetContainer(planetContainerSaveData);
            PlanetList.Add(plantContaire.planetData, plantContaire);

            if (s == saveData.currentPlanetNameId)
            {
                CurrentPlanet = plantContaire;
                plantContaire.quests = QuestManager.GetQuestsByStaus(QuestStatus.Available).ToArray(); // only the AvailableQuest matter as they are the only one's that can get remvoed when leaving a plaent.u
            }
            if (s == saveData.TargetPalnetNameId)
            {
                TargetPlanet = plantContaire;
            }
            if (s == saveData.PreviousPalnetNameId)
            {
                PreviousPlanet = plantContaire;
            }

        }

        if (InNavigation)
        {
            _navTimer = TimeDelayManager.Timer.ReconstructTimer(saveData.timmerId, ArriveAtPlanet);
            BackgroundManager.Instance.setBackgroundToInTransit();
            UIManager.Instance.TrackNavTimer(_navTimer, TargetPlanet);
            ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.Navigation, false);
        }
        else if (CurrentPlanet != null)
        {
            BackgroundManager.Instance.setBackground(CurrentPlanet.planetData.Background);
        }
    }


    public static TimeSpan CalcualteTravleTime(PlanetContainer b)
    {
        return (CalcualteTravleTime(CurrentPlanet, b));
    }

    public static TimeSpan CalcualteTravleTime(PlanetContainer a, PlanetContainer b)
    {
        if(a == null)
        {
            return TimeSpan.FromMinutes(5);
        }
        return CalcualteTravleTime(a.PlanetPosition, b.PlanetPosition);
    }
    public static TimeSpan CalcualteTravleTime(Vector3 a, PlanetContainer b)
    {
        return CalcualteTravleTime(a, b.PlanetPosition);
    }
    public static TimeSpan CalcualteTravleTime(Vector3 a, Vector3 b)
    {
        TimeSpan rawTime = TimeSpan.FromMinutes(UniversSpeedModifyer * Vector3.Distance(a, b));

        return TimeSpan.FromMinutes(Math.Round(rawTime.TotalSeconds)); // round to nearest second
    }



}
