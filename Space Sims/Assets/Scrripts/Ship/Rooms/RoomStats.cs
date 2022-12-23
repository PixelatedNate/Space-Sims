using System;
using UnityEngine;

[Serializable]
public class RoomStats
{

    [SerializeField]
    private GameResources _buildCost;
    public GameResources BuildCost { get { return _buildCost; } }

    [SerializeField]
    private int _maxWorkers;
    public int MaxWorkers { get { return _maxWorkers; } }

    [SerializeField]
    private double _buildTime;
    public double BuildTime { get { return _buildTime; } }

    [SerializeField]
    private GameResources _outPut;
    public GameResources OutPut { get { return _outPut; } }

    [SerializeField]
    private GameResources _Upkeep;
    public GameResources Upkeep { get { return _Upkeep; } }

    [SerializeField]
    private GameResources _storage;
    public GameResources Storage { get { return _storage; } }

    [SerializeField]
    private int _peopleChange = 0;
    public int PoepleChange { get { return _peopleChange; } }


    // add list array of gear that can be produced;



}
