using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomEventTriggers
{

    public enum EventName
    {
        None,
        PersonAssginedToRoom,
        OnQuestCompleted

    }
    public class Event  
    {
        EventName id = EventName.None;
        public delegate void eventDelaget(object souceObj);
        public eventDelaget onEventDelaget;

       public Event(EventName name)
        {
            this.id = name;
        }
    }

    public static Event GetEvent(EventName name)
    {
        if(name == EventName.OnQuestCompleted)
        {
            return OnQuestCompleted;
        }
        else
        {
            return OnPersonAssginedToRoom;
        }
    }

    public static Event OnPersonAssginedToRoom = new Event(EventName.PersonAssginedToRoom);
    public static Event OnQuestCompleted = new Event(EventName.OnQuestCompleted);

    }



