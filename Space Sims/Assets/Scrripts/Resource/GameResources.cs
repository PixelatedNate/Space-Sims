using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameResources 
{
    public int Fuel;
    public int Food;
    public int Minerals;
    public int Premimum;





    public GameResources()
    {
        Food = 0;
        Fuel = 0;
        Minerals = 0;
        Premimum = 0;
    }

   public GameResources(ResourcesEnum resourcesEnum,int value)
    {      
       SetResorce(resourcesEnum, value);
    }





    public void SetResorce(ResourcesEnum resourcesEnum, int value)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: Fuel = value; return;
            case ResourcesEnum.Food: Food = value; return;
            case ResourcesEnum.Minerals: Minerals = value; return;
            case ResourcesEnum.Premimum: Premimum = value; return;
            default: throw new Exception("Enum Resources Found No corisponding value");
        }
    }


    public int GetResorce(ResourcesEnum resourcesEnum)
    {
        switch (resourcesEnum)
        {
            case ResourcesEnum.Fuel: return Fuel;
            case ResourcesEnum.Food: return Food;
            case ResourcesEnum.Minerals: return Minerals;
            case ResourcesEnum.Premimum: return Premimum;
            default: throw new Exception("Enum Resources returned No corisponding value");
        }
    }


    #region Comparators
    public static GameResources operator+ (GameResources a, GameResources b)
    {
        GameResources resources = new GameResources();
        resources.Fuel = a.Fuel + b.Fuel;
        resources.Food = a.Food + b.Food;
        resources.Minerals = a.Minerals + b.Minerals;
        resources.Premimum = a.Premimum + b.Premimum;

        return resources;
    }
    public static GameResources operator- (GameResources a, GameResources b)
    {
        GameResources resources = new GameResources();
        resources.Fuel = a.Fuel - b.Fuel;
        resources.Food = a.Food - b.Food;
        resources.Minerals = a.Minerals - b.Minerals;
        resources.Premimum = a.Premimum - b.Premimum;

        return resources;
    }
    public static bool operator> (GameResources a, GameResources b)
    {
        return( (a.Fuel > b.Fuel) &&
                (a.Food > b.Food) &&
                (a.Minerals > b.Minerals) &&
                (a.Premimum > b.Premimum) );
    }

    public static bool operator< (GameResources a, GameResources b)
    {
        return( (a.Fuel < b.Fuel) &&
                (a.Food < b.Food) &&
                (a.Minerals < b.Minerals) &&
                 (a.Premimum < b.Premimum) );    
    }

    public static bool operator== (GameResources a, GameResources b)
    {
        return( (a.Fuel == b.Fuel) &&
                (a.Food == b.Food) &&
                (a.Minerals == b.Minerals) &&
                 (a.Premimum == b.Premimum) );    
    }

    public static bool operator!= (GameResources a, GameResources b)
    {
        return ((a.Fuel != b.Fuel) &&
                (a.Food != b.Food) &&
                (a.Minerals != b.Minerals) &&
                 (a.Premimum != b.Premimum));
    }


    public static bool operator<= (GameResources a, GameResources b)
    {
        return(a<b || a==b);    
    }
    public static bool operator>= (GameResources a, GameResources b)
    {
        return(a>b || a==b);    
    }

    #endregion


}




