using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{

    private const float TICK_TIMER = 1f;
    private const float MAJOR_TICK_TIMER = 5f;

    private float tickTimer;
    private float majorTickTimer;

    public static event EventHandler<EventArgs> OnTick;

    public static event EventHandler<EventArgs> OnMajorTick;
    // Update is called once per frame
    void Update()
    {
        majorTickTimer += Time.deltaTime;
        if(majorTickTimer >= MAJOR_TICK_TIMER)
        {
            Debug.Log("Major Tick");
            majorTickTimer -= MAJOR_TICK_TIMER;
            if (OnMajorTick != null)
                OnMajorTick(this, null);
        }

        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER)
        {
            Debug.Log("Raw Tick");
            tickTimer -= TICK_TIMER;
            if (OnTick != null)
                OnTick(this, null);
        }
    }
}
