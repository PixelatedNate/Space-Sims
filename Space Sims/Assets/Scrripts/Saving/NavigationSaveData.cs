using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NavigationSaveData {

    public bool InNavigation;

    public string timmerId;


    public string[] planetId;


    public string currentPlanetNameId;
    public string TargetPalnetNameId;
    public string PreviousPalnetNameId;


    public NavigationSaveData()
    {
        this.InNavigation = NavigationManager.InNavigation;


        this.planetId = new string[NavigationManager.PlanetList.Count];

        int index = 0;
        foreach(var keyValuePair in NavigationManager.PlanetList)
        {
            var plantData = keyValuePair.Value.Save();
            if (this.InNavigation)
            {
                if (keyValuePair.Value == NavigationManager.TargetPlanet)
                {
                    this.TargetPalnetNameId = plantData.planetId;
                }
                if (keyValuePair.Value == NavigationManager.PreviousPlanet)
                {
                    this.PreviousPalnetNameId = plantData.planetId;
                }
            }
            else if (keyValuePair.Value == NavigationManager.CurrentPlanet)
            {
                this.currentPlanetNameId = plantData.planetId;
            }
            planetId[index] = plantData.planetId;
            index++;
        }

        if(this.InNavigation)
        {
            TimerSaveData timmerData = NavigationManager._navTimer.Save();
            this.timmerId = timmerData.ID;
            MobilePushNotification.Instance.sendNotificationNow("Arrived at planet", "your ship has arrived at it's destonations", (float)NavigationManager._navTimer.RemainingDuration.TotalSeconds);

        }

      }

}
