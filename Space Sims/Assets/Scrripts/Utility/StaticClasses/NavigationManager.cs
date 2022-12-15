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
    public static Planet TargetPlanet { get; set; }
    public static Planet PreviousPlanet { get; set; }

    public static bool NavigateToTargetPlanet(Planet planet)
    {
        if(planet == CurrentPlanet)
        {
            Debug.LogWarning("trying to traval to a planet the player is allready at");
            return false;
        }
        if(InNavigation)
        {
            return false; // can't navigate to a diffrent palnet when allready in montion
        }
        BackgroundManager.Instance.setBackgroundToInTransit();
        DateTime ariveralTime = DateTime.Now.Add(CalcualteTravleTime(planet));
        PreviousPlanet = CurrentPlanet;
        CurrentPlanet = null;
        TargetPlanet = planet;
        InNavigation = true;
        _navTimer = new TimeDelayManager.Timer(ariveralTime, ArriveAtPlanet);
        TimeDelayManager.Instance.AddTimer(_navTimer);
        UIManager.Instance.TrackNavTimer(_navTimer,TargetPlanet);
        // start timer
        return true;

    }


    public static Vector2 GetPositionRelativeToJourny()
    {
        if(!InNavigation)
        {
            Debug.LogWarning("Trying to get Posittion Relative To Journy when no journy is in progress");
            return Vector2.zero;
        }
        else
        {
            float percentage = 1 - (float)_navTimer.PercentaceTravled/100;
            Vector2 CurrentPositoin = Vector2.Lerp(PreviousPlanet.transform.position, TargetPlanet.transform.position,percentage);
            return CurrentPositoin;
        }
    }

    private static void ArriveAtPlanet()
    {
        InNavigation = false;
        CurrentPlanet = TargetPlanet;
        TargetPlanet = null;
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
       
        return TimeSpan.FromSeconds(20);
        
        return TimeSpan.FromMinutes(Math.Round(rawTime.TotalSeconds)); // round to nearest second
    }



}
