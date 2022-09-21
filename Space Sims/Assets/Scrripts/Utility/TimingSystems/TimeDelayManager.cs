using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelayManager : MonoBehaviour
{
    
    
    public static TimeDelayManager Instance;
    private void Awake()
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
        public Timer(int timeInSeconds, Action endMethod)
        {
            this.EndTime = DateTime.Now.AddSeconds(timeInSeconds);
            this.EndMethod = endMethod;
        }



    }


   private List<Timer> ActiveTimers = new List<Timer>();


    private void Start()
    {
      TimeTickSystem.OnTick += OnTick;
      //  ForTesting
        /*
        TimeDelayManager.Instance.AddTimer(new Timer(10,testMethodForEndOfTimer));
        */
    }


    public Timer AddTimer(Timer timer)
    {
        timer.StartTime = DateTime.Now;
        ActiveTimers.Add(timer);
        ActiveTimers.Sort((timer1,timer2)  => timer1.EndTime.CompareTo(timer2.EndTime));
        return timer;
    }



    // can be revmoved
    private void testMethodForEndOfTimer()
    {
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
