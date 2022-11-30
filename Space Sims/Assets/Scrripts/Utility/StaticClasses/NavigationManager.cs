using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NavigationManager
{
    public static bool InNavigation { get; private set; } = false;

    private static TimeDelayManager.Timer _navTimer;

    public static float UniversSpeedModifyer = 0.25f; 

    public static Planet CurrentPlanet { get; set; }

    private static Planet TargetPlanet { get; set; }

    public static bool NavigateToTargetPlanet(Planet planet)
    {
        if(InNavigation)
        {
            return false; // can't navigate to a diffrent palnet when allready in montion
        }
        BackgroundManager.Instance.setBackgroundToInTransit();
        DateTime ariveralTime = DateTime.Now.Add(CalcualteTravleTime(planet));
        CurrentPlanet = null;
        TargetPlanet = planet;
        InNavigation = true;
        _navTimer = new TimeDelayManager.Timer(ariveralTime, ArriveAtPlanet);
        TimeDelayManager.Instance.AddTimer(_navTimer);
        UIManager.Instance.TrackNavTimer(_navTimer,TargetPlanet);
        // start timer
        return true;

    }

    private static void ArriveAtPlanet()
    {
        InNavigation = false;
        CurrentPlanet = TargetPlanet;
        TargetPlanet = null;
        //GlobalStats.Instance.se
        QuestManager.SetAvalibleQuest(CurrentPlanet.Quests);
        BackgroundManager.Instance.setBackground(CurrentPlanet.Background);
    }




    public static TimeSpan CalcualteTravleTime(Planet b)
    {
        return (CalcualteTravleTime(CurrentPlanet, b));
    }

    public static TimeSpan CalcualteTravleTime(Planet a, Planet b)
    {
        return CalcualteTravleTime(a.transform.position, b.transform.position);
    }
    public static TimeSpan CalcualteTravleTime(Vector3 a, Planet b)
    {
        return CalcualteTravleTime(a, b.transform.position);
    }
    public static TimeSpan CalcualteTravleTime(Vector3 a, Vector3 b)
    {
        TimeSpan rawTime = TimeSpan.FromMinutes(UniversSpeedModifyer * Vector3.Distance(a, b));
       
       // return TimeSpan.FromSeconds(10);
        
        return TimeSpan.FromMinutes(Math.Round(rawTime.TotalSeconds)); // round to nearest second
    }



}
