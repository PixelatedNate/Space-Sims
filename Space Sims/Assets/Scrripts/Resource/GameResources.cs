using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameResources 
{
    public int Fuel;
    public int Food;
    public int Other1;
    public int Premimum;


    #region Comparators
    public static GameResources operator+ (GameResources a, GameResources b)
    {
        GameResources resources = new GameResources();
        resources.Fuel = a.Fuel + b.Fuel;
        resources.Food = a.Food + b.Food;
        resources.Other1 = a.Other1 + b.Food;
        resources.Premimum = a.Premimum + b.Premimum;

        return resources;
    }
    public static GameResources operator- (GameResources a, GameResources b)
    {
        GameResources resources = new GameResources();
        resources.Fuel = a.Fuel - b.Fuel;
        resources.Food = a.Food - b.Food;
        resources.Other1 = a.Other1 - b.Food;
        resources.Premimum = a.Premimum - b.Premimum;

        return resources;
    }
    public static bool operator> (GameResources a, GameResources b)
    {
        return( (a.Fuel > b.Fuel) &&
                (a.Food > b.Food) &&
                (a.Other1 > b.Food) &&
                (a.Premimum > b.Premimum) );
    }

    public static bool operator< (GameResources a, GameResources b)
    {
        return( (a.Fuel < b.Fuel) &&
                (a.Food < b.Food) &&
                (a.Other1 < b.Food) &&
                 (a.Premimum < b.Premimum) );    
    }

    public static bool operator== (GameResources a, GameResources b)
    {
        return( (a.Fuel == b.Fuel) &&
                (a.Food == b.Food) &&
                (a.Other1 == b.Food) &&
                 (a.Premimum == b.Premimum) );    
    }

    public static bool operator!= (GameResources a, GameResources b)
    {
        return ((a.Fuel != b.Fuel) &&
                (a.Food != b.Food) &&
                (a.Other1 != b.Food) &&
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




