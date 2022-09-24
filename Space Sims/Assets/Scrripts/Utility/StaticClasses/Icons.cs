using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Icons
{
    private const string FuelIconPath = "ArtWork/Icons/Fuel";
    private const string FoodIconPath = "ArtWork/Icons/Food";
    private const string MineralIconPath = "ArtWork/Icons/Mineral";
    //private const string PremiumIconPath = "ArtWork/Icons/Food";





    public static Sprite GetIcon(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return Resources.Load<Sprite>(FuelIconPath);
            case ResourcesEnum.Food: return Resources.Load<Sprite>(FoodIconPath);
            case ResourcesEnum.Minerals: return Resources.Load<Sprite>(MineralIconPath);              
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }



}
