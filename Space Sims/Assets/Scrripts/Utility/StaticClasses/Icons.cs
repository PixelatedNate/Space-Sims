using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Icons
{
    private const string FuelIconPath = "ArtWork/Icons/Fuel";
    private const string FoodIconPath = "ArtWork/Icons/Food";
    private const string MineralsIconPath = "ArtWork/Icons/Minerals";
    //private const string PremiumIconPath = "ArtWork/Icons/Food";





    public static Sprite GetIcon(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return Resources.Load<Sprite>(FuelIconPath);
            case ResourcesEnum.Food: return Resources.Load<Sprite>(FoodIconPath);
            case ResourcesEnum.Minerals: return Resources.Load<Sprite>(MineralsIconPath);              
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }



}
