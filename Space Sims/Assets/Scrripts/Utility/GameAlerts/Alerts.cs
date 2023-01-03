using System;
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

    public Alert(string name, string message, UnityAction onClickEffect, AlertPrority prority)
    {
        this.name = name;
        this.message = message;
        this.OnClickEffect = onClickEffect;
        this.prority = prority;
        time = DateTime.Now;
    }

    public Alert(string name, string message, AlertPrority prority)
    {
        this.name = name;
        this.message = message;
        this.prority = prority;
        time = DateTime.Now;
    }

    public static Alert LowFoodAlert { get { return new Alert("LOW FOOD", "you have low food fix this you should", AlertPrority.Permanet); } }

}
