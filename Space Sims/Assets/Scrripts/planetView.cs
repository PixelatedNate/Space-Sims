using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class planetView : MonoBehaviour
{
    Planet SelectedPlanet;

    [SerializeField]
    Image PlanetImg;

    [SerializeField]
    TextMeshProUGUI Name, TravalTime, NumberOfQuest;

    public void SetPlanet(Planet planet)
    {
        SelectedPlanet = planet;
        UpdateText();
        UpdateImage();
    }

    public void TravelToPlanet()
    {
        NavigationManager.NavigateToTargetPlanet(SelectedPlanet);
    }


    private void UpdateImage()
    {
        PlanetImg.sprite = SelectedPlanet.PlanetSprite;
    }

    private void UpdateText()
    {
        Name.text = SelectedPlanet.name;
        NumberOfQuest.text = SelectedPlanet.Quests.Length.ToString();
        if (NavigationManager.InNavigation)
        {
            TravalTime.text = "Unable To calcualte whilst in transit";
        }
        else
        {
           TravalTime.text = NavigationManager.CalcualteTravleTime(SelectedPlanet).ToString("h'h 'm'm 's's'");
        }
    }

}
