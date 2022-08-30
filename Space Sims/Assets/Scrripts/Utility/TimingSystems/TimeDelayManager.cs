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

    static DateTime currentTime;

    public class Timer
    {
        public DateTime EndTime;
        public Action EndMethod { get; }
        TimeSpan RemainingDuration { get { return (EndTime - currentTime); } }


        public Timer(DateTime endTime, Action endMethod)
        {
            this.EndTime = endTime;
            this.EndMethod = endMethod;
        }
        public Timer(int timeInSeconds, Action endMethod)
        {
            this.EndTime = DateTime.Now.AddSeconds(timeInSeconds);
            this.EndMethod = endMethod;
            Debug.Log(this.EndTime);
        }



    }


   private List<Timer> ActiveTimers = new List<Timer>();


    private void Start()
    {
      //  ForTesting
        /*
        TimeTickSystem.OnTick += OnTick;
        TimeDelayManager.Instance.AddTimer(new Timer(10,testMethodForEndOfTimer));
        */
    }


    public void AddTimer(Timer timer)
    {
        ActiveTimers.Add(timer);
        ActiveTimers.Sort((timer1,timer2)  => timer1.EndTime.CompareTo(timer2.EndTime));
    }



    // can be revmoved
    private void testMethodForEndOfTimer()
    {
        Debug.Log("MethodSpnet");
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
