using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomStats
{
    [SerializeField]
    GameResources _upkeep;
    public GameResources Upkeep { get { return _upkeep; } }
    
    [SerializeField]
    GameResources _outPut;
    public GameResources OutPut { get { return _outPut; } } 

    [SerializeField]
    GameResources _buildCost;
    public GameResources BuildCost { get { return _buildCost; } }

    [SerializeField]
    private int _maxWorkers;
    public int MaxWorkers { get { return _maxWorkers; } }

    [SerializeField]
    public double _buildTime;
    public double BuildTime { get { return _buildTime; } }
}
