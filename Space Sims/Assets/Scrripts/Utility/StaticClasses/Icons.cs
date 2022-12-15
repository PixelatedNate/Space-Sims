using System;
using UnityEngine;

public static class Icons
{
    private const string FuelIconPath = "ArtWork/Icons/Fuel";
    private const string FoodIconPath = "ArtWork/Icons/Food";
    private const string MineralIconPath = "ArtWork/Icons/Mineral";
    //private const string PremiumIconPath = "ArtWork/Icons/Food";

    private const string StrengthIconPath = "ArtWork/Icons/Strength";
    private const string CharismaIconPath = "ArtWork/Icons/Charisma";
    private const string DexterityIconPath = "ArtWork/Icons/Dexterity";
    private const string IntelligenceIconPath = "ArtWork/Icons/Intelligence";
    private const string WisdomIconPath = "ArtWork/Icons/Wisdom";



    public static Sprite GetResourceIcon(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return Resources.Load<Sprite>(FuelIconPath);
            case ResourcesEnum.Food: return Resources.Load<Sprite>(FoodIconPath);
            case ResourcesEnum.Minerals: return Resources.Load<Sprite>(MineralIconPath);
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }
    
     public static int GetResourceIDForTextMeshPro(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return 4;
            case ResourcesEnum.Food: return 3;
            case ResourcesEnum.Minerals: return 6;
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }

    public static int GetPersonIconIDForTextMeshPro()
    {
        return 7;
    }






    public static Sprite GetSkillIcon(SkillsList skillEnum)
    {
        switch (skillEnum)
        {
            case SkillsList.Strength: return Resources.Load<Sprite>(StrengthIconPath);
            case SkillsList.Charisma: return Resources.Load<Sprite>(CharismaIconPath);
            case SkillsList.Dexterity: return Resources.Load<Sprite>(DexterityIconPath);
            case SkillsList.Intelligence: return Resources.Load<Sprite>(IntelligenceIconPath);
            case SkillsList.Wisdom: return Resources.Load<Sprite>(WisdomIconPath);
            default: throw new Exception("Enum SkillList returned No corisponding value");
        }
    }



}
