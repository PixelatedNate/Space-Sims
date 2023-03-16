using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelayManager : MonoBehaviour
{
    public class Timer : ISaveable<TimerSaveData>
    {
        //    public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Action EndMethod { get; }
        public TimeSpan TotalDuration { get; private set; }
        public TimeSpan RemainingDuration { get { return CalculateRemainingDuration(); } }
        public bool IsPause { get; set; } = false;

        public double PercentaceTravled { get { return CalculatePercent(); } }

        public DateTime pauseTimeStart { get; private set; }

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

        public static Timer ReconstructPlanetTimer(string saveTimerName, Action endMethod)
        {
            string Timmerpath = SaveSystem.TimerPath + "/" + saveTimerName + SaveSystem.TimerPrefix;
            TimerSaveData timerData = SaveSystem.LoadData<TimerSaveData>(Timmerpath);
            Timer t = new Timer(timerData, endMethod, true);
            return t;
        }


        public static Timer ReconstructTimer(string saveTimerName, Action endMethod)
        {
            string Timmerpath = SaveSystem.TimerPath + "/" + saveTimerName + SaveSystem.TimerPrefix;
            TimerSaveData timerData = SaveSystem.LoadData<TimerSaveData>(Timmerpath);
            Timer t = new Timer(timerData, endMethod);
            return t;
        }

        public Timer(TimerSaveData saveData, Action endMethod, bool isPlanet = false)
        {
            this.EndTime = new DateTime(saveData.EndYear, saveData.EndMonth, saveData.EndDay, saveData.EndHour, saveData.EndMinuit, saveData.EndSecond);
            this.TotalDuration = new TimeSpan(saveData.DurationHour, saveData.DurationMinuit, saveData.DurationSecond);
            this.EndMethod = endMethod;
            IsPause = saveData.IsPause;
            if (IsPause)
            {
                this.pauseTimeStart = new DateTime(saveData.PuauseYear, saveData.PuauseMonth, saveData.PuauseDay, saveData.PuauseHour, saveData.PuauseMinuit, saveData.PuauseSecond);
            }

            // add puase stuff

            TimeDelayManager.Instance.AddTimer(this,isPlanet);
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

        public TimerSaveData Save()
        {
            TimerSaveData data = new TimerSaveData(this);
            data.Save();
            return data;
        }

        public TimerSaveData Save(string notificationTitle, string notificationText)
        {
            TimerSaveData data = new TimerSaveData(this);
            data.Save();
            if (!this.IsPause)
            {
                MobilePushNotification.Instance.sendNotificationNow(notificationTitle, notificationText, (float)RemainingDuration.TotalSeconds);
            }
            return data;
        }


        public void Load(string Path)
        {
            throw new NotImplementedException();
        }
        public void Load(TimerSaveData data)
        {
            throw new NotImplementedException();
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
    }

    public void StartTickerTimer()
    {
        OnTick(this, null);
        TimeTickSystem.OnTick += OnTick;
    }

    public Timer AddTimer(Timer timer, bool isPalnetReset = false)
    {
        //timer.StartTime = DateTime.Now;

#if UNITY_EDITOR
        if (!isPalnetReset)
        {
            timer.EndTime = DateTime.Now + TimeSpan.FromSeconds(5);
        }
#endif

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
                SpentTimer.Add(t);
            }
        }
        foreach (Timer t in SpentTimer)
        {
            t.EndMethod.Invoke();
            ActiveTimers.Remove(t);
        }
    }

}
