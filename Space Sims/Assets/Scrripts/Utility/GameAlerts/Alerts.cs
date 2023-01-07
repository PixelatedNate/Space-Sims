using System;
using UnityEngine;
using UnityEngine.Events;

public class Alert
{

    public enum AlertPrority
    {
        low,
        Med,
        High,
        Permanet,
    }

    public string name;
    public string message;
    public DateTime time;
    public UnityAction OnClickEffect;
    public AlertPrority prority;
    public Sprite AlertIcon;

    public Alert(string name, string message, UnityAction onClickEffect, AlertPrority prority, Sprite alertIcon)
    {
        this.name = name;
        this.message = message;
        this.OnClickEffect = onClickEffect;
        this.prority = prority;
        time = DateTime.Now;
        AlertIcon = alertIcon;
    }

    public Alert(string name, string message, AlertPrority prority, Sprite alertIcon)
    {
        this.name = name;
        this.message = message;
        this.prority = prority;
        time = DateTime.Now;
        AlertIcon = alertIcon;
    }

    public static Alert LowFoodAlert { get { return new Alert("LOW FOOD", "you have low food fix this you should", AlertPrority.Permanet, Icons.GetResourceIcon(ResourcesEnum.Food)); } }
    public static Alert LowFuelAlert { get { return new Alert("LOW Fuel", "you have low fuel fix this you should", AlertPrority.Permanet, Icons.GetResourceIcon(ResourcesEnum.Fuel)); } }
}
