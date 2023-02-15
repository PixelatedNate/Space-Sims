using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NavigationSaveData {

    public bool InNavigation;

    public string timmerId;


    public string planetId;


    public string currentPlanetName;
    public string TargetPalnetName;
    public string PreviousPalnetName;


    public NavigationSaveData()
    {
        this.InNavigation = NavigationManager.InNavigation;

        this.currentPlanetName = NavigationManager.CurrentPlanet.planetData.name;
        
        if(this.InNavigation)
        {
            TimerSaveData timmerData = NavigationManager._navTimer.Save();
            this.timmerId = timmerData.ID;
            this.TargetPalnetName = NavigationManager.TargetPlanet.planetData.name;
            this.PreviousPalnetName = NavigationManager.PreviousPlanet.planetData.name;
        }
        else
        {
            this.currentPlanetName = NavigationManager.CurrentPlanet.planetData.name;
        }
    }

}
