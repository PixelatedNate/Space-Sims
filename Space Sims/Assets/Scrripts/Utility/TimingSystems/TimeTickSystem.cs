using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{

    private const float TICK_TIMER = 1f;

    private float tickTimer;

    public static event EventHandler<EventArgs> OnTick;

    // Update is called once per frame
    void Update()
    {
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
