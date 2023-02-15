using System;
using UnityEngine;

[Serializable]
public class TimerSaveData
{

    [SerializeField]
    public string ID;

    [SerializeField]
    public bool IsPause;

    public int EndYear;
    public int EndMonth;
    public int EndDay;
    public int EndHour;
    public int EndMinuit;
    public int EndSecond;


    public int PuauseYear;
    public int PuauseMonth;
    public int PuauseDay;
    public int PuauseHour;
    public int PuauseMinuit;
    public int PuauseSecond;



    public int DurationDay;
    public int DurationHour;
    public int DurationMinuit;
    public int DurationSecond;


    public TimerSaveData(TimeDelayManager.Timer timer)
    {

        ID = Guid.NewGuid().ToString();


        this.EndYear = timer.EndTime.Year;
        this.EndMonth = timer.EndTime.Month;
        this.EndDay = timer.EndTime.Day;
        this.EndHour = timer.EndTime.Hour;
        this.EndMinuit = timer.EndTime.Minute;
        this.EndSecond = timer.EndTime.Second;

        this.DurationHour = timer.TotalDuration.Hours;
        this.DurationMinuit = timer.TotalDuration.Minutes;
        this.DurationSecond = timer.TotalDuration.Seconds;

        this.IsPause = timer.IsPause;
        if (IsPause)
        {
            this.PuauseYear = timer.pauseTimeStart.Year;
            this.PuauseMonth = timer.pauseTimeStart.Month;
            this.PuauseDay = timer.pauseTimeStart.Day;
            this.PuauseHour = timer.pauseTimeStart.Hour;
            this.PuauseMinuit = timer.pauseTimeStart.Minute;
            this.PuauseSecond = timer.pauseTimeStart.Second;
        }

    }


}
