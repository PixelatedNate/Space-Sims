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
        public TimeSpan TotalDuration { get; private set; }
        public TimeSpan RemainingDuration { get { return CalculateRemainingDuration(); } }
        public bool IsPause { get; set; } = false;

        public double PercentaceTravled { get { return CalculatePercent(); } }

        private DateTime pauseTimeStart;

        private double CalculatePercent()
        {
            return RemainingDuration.TotalSeconds / (TotalDuration.TotalSeconds / 100);
        }

        private TimeSpan CalculateRemainingDuration()
        {
            if (!IsPause)
            {
                return EndTime - DateTime.Now;
            }
            else
            {
                return EndTime + (DateTime.Now - pauseTimeStart) - DateTime.Now;
            }
        }


        public void PauseTimer()
        {
            pauseTimeStart = DateTime.Now;
            IsPause = true;
        }

        public void RestartTimer()
        {
            EndTime = EndTime + (DateTime.Now - pauseTimeStart);
            IsPause = false;
        }


        public Timer(DateTime endTime, Action endMethod)
        {
            this.EndTime = endTime;
            this.TotalDuration = EndTime - DateTime.Now;
            this.EndMethod = endMethod;
        }

        public Timer(TimeSpan duration, Action endMethod)
        {
            this.EndTime = DateTime.Now.Add(duration);
            this.TotalDuration = duration;
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
            else if (!t.IsPause)
            {
                t.EndMethod.Invoke();
                SpentTimer.Add(t);
            }
        }
        foreach (Timer t in SpentTimer)
        {
            ActiveTimers.Remove(t);
        }
    }

}
