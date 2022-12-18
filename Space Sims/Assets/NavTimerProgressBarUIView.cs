using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavTimerProgressBarUIView : MonoBehaviour
{
    private TimeDelayManager.Timer NavTimer { get; set; }

    private Planet _planet;

    [SerializeField]
    private Transform _progressbar;
    [SerializeField]
    private TextMeshProUGUI _timeLeft;

    

    public void onClick()
    {
        UIManager.Instance.LoadNavigation();
        UIManager.Instance.OpenPlanetView(_planet);
    }

    public void TrackTimer(TimeDelayManager.Timer timer, Planet targetPlanet)
    {
        _planet = targetPlanet;
        _progressbar.localScale = new Vector3(0, 1, 1); // ensuer bar is reset
        NavTimer = timer;
        TimeTickSystem.OnTick += OnTick;
        // subscribe to on ticket event
    }

    public void UpdateTimer()
    {
        double ProgressBarPercent = (NavTimer.RemainingDuration.TotalSeconds / (NavTimer.TotalDuration.TotalSeconds / 100));
        _progressbar.localScale = new Vector3(1 - (float)ProgressBarPercent/100, 1, 1);
        _timeLeft.text = NavTimer.RemainingDuration.ToString("h'h 'm'm 's's'");
        if(NavTimer.RemainingDuration.TotalSeconds <= 0)
        {
            TimeTickSystem.OnTick -= OnTick;
            gameObject.SetActive(false);
            _progressbar.localScale = new Vector3(0, 1, 1); // ensure bar is reset
        }
    }


    private void OnTick(object source, EventArgs e)
    {
        if (NavTimer != null)
        {
            UpdateTimer();
        }
    }


}

