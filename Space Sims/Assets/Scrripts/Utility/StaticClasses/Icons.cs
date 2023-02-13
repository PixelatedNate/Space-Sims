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

    private const string QuestCompletIconPath = "ArtWork/Icons/QuestComplet";
    private const string PersonIconPath = "ArtWork/Icons/Person";
    private const string RoomIconPath = "ArtWork/Icons/Room";

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

    public static string GetResourceIconForTextMeshPro(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return "<sprite=4>";
            case ResourcesEnum.Food: return "<sprite=3>";
            case ResourcesEnum.Minerals: return "<sprite=6>";
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }
    public static string GetSkillIconForTextMeshPro(SkillsList skillsList)
    {
        switch (skillsList)
        {
            case SkillsList.Strength: return "<sprite=8>";
            case SkillsList.Charisma: return "<sprite=1>";
            case SkillsList.Dexterity: return "<sprite=2>";
            case SkillsList.Intelligence: return "<sprite=5>";
            case SkillsList.Wisdom: return "<sprite=9>";
            default: throw new Exception("Enum SkillList returned No corisponding value");
        }
    }


    public static string GetPersonIconForTextMeshPro()
    {
        return "<sprite=7>";
    }

    public static string GetBluePrintIconForTextMeshPro()
    {
        return "<sprite=12>";
    }


    public static string GetAgeIconForTextMeshPro()
    {
        return "<sprite=0>";
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


    public static Sprite GetMiscUIIcon(UIIcons uIIcons)
    {
        switch (uIIcons)
        {
            case UIIcons.QuestComplete: return Resources.Load<Sprite>(QuestCompletIconPath);
            case UIIcons.Person: return Resources.Load<Sprite>(PersonIconPath);
            case UIIcons.RoomIcon: return Resources.Load<Sprite>(RoomIconPath);
            default: throw new Exception("Enum UIIcons returned No corisponding value");
        }
    }




}
