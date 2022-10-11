using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelayManager : MonoBehaviour
{
    public class Timer
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Action EndMethod { get; }
        public TimeSpan TotalBuildDuration { get { return (EndTime - StartTime); } }
        public TimeSpan RemainingDuration { get { return (EndTime - DateTime.Now); } }


        public Timer(DateTime endTime, Action endMethod)
        {
            this.EndTime = endTime;
            this.EndMethod = endMethod;
        }
        public Timer(int minutes, Action endMethod)
        {
            this.EndTime = DateTime.Now.AddMinutes(minutes);
            this.EndMethod = endMethod;
        }
    }

    public static TimeDelayManager Instance { get; set; }
    private List<Timer> ActiveTimers { get; set; } = new List<Timer>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        TimeTickSystem.OnTick += OnTick;
    }

    public Timer AddTimer(Timer timer)
    {
        timer.StartTime = DateTime.Now;
        ActiveTimers.Add(timer);
        ActiveTimers.Sort((timer1, timer2) => timer1.EndTime.CompareTo(timer2.EndTime));
        return timer;
    }

    public bool RemoveTimer(Timer t)
    {
       return ActiveTimers.Remove(t);
    }

    private void OnTick(object source, EventArgs e)
    {
        List<Timer> SpentTimer = new List<Timer>();
        foreach (Timer t in ActiveTimers)
        {
            if (t.EndTime >= DateTime.Now) { break; }
            t.EndMethod.Invoke();
            SpentTimer.Add(t);
        }
        foreach (Timer t in SpentTimer)
        {
            ActiveTimers.Remove(t);
        }
    }

}
