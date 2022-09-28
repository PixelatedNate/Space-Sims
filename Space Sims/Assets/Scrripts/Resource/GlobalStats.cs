using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{

    public static GlobalStats Instance;

    private Dictionary<Person, GameResources> PersonDeltaResources { get; set; } = new Dictionary<Person, GameResources>();
    private GameResources PersonDeltaResourcesTotal { get; set; } = new GameResources();

    private Dictionary<AbstractRoom, GameResources> RoomDeltaResources { get; set; } = new Dictionary<AbstractRoom, GameResources>();
    private GameResources RoomDeltaResourcesTotal { get; set; } = new GameResources();
 
    private GameResources TotalDelta { get; set; }
    
    private GameResources _playerResources = new GameResources();
    public GameResources PlayerResources { get { return _playerResources; } set { SetPlayerResources(value); } }

    private int _maxPeople;
    public int MaxPeople { get { return _maxPeople; } set {SetMaxPeople(value); } }
    public List<PersonInfo> PlayersPeople = new List<PersonInfo>();
    public List<AbstractRoom> PlyaerRooms = new List<AbstractRoom>();
    
    #region CustomGetterAndSetters

    private void SetMaxPeople(int value)
    {
        _maxPeople = value;
        UIManager.Instance.UpdateTopBar(_playerResources, TotalDelta, PlayersPeople.Count ,MaxPeople);
    }
    private void SetPlayerResources(GameResources value)
    {
       _playerResources = value;
       checkResourcesForAlertChanges();
       UIManager.Instance.UpdateTopBar(_playerResources, TotalDelta, PlayersPeople.Count, MaxPeople);
    }

    #endregion

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




    #region PublicMethods
    
    public void AddorUpdatePersonDelta(Person person, GameResources personDeltaResources)
    {
        if(PersonDeltaResources.ContainsKey(person))
        {
            PersonDeltaResources[person] = personDeltaResources;
        }
        else
        {
            PersonDeltaResources.Add(person, personDeltaResources);
        }
            RecalculatePersonDeltaTotal();
    }
    public void RemovePersonDelta(Person person)
    {
        if (PersonDeltaResources.ContainsKey(person))
        {
            PersonDeltaResources.Remove(person);
            RecalculatePersonDeltaTotal();
        }
    }
    public void AddorUpdateRoomDelta(AbstractRoom room, GameResources delta)
    {
        if(RoomDeltaResources.ContainsKey(room))
        {
            RoomDeltaResources[room] = delta;
        }
        else
        {
            RoomDeltaResources.Add(room, delta);
        }
            RecalculateRoomDeltaTotal();
    }
    public void RemoveRoomDelta(AbstractRoom room)
    {
        if (RoomDeltaResources.ContainsKey(room))
        {
            RoomDeltaResources.Remove(room);
            RecalculateRoomDeltaTotal();
        }
    }
    
    #endregion

    #region PrivateMethods

    private void OnTick(object source, EventArgs e)
    {
        PlayerResources += TotalDelta;
    }

    private void RecalculatePersonDeltaTotal()
    {
        PersonDeltaResourcesTotal = new GameResources();
        foreach(var keyPair in PersonDeltaResources)
        {
            PersonDeltaResourcesTotal += keyPair.Value;
        }
        TotalDelta = PersonDeltaResourcesTotal + RoomDeltaResourcesTotal;
    }
    private void RecalculateRoomDeltaTotal()
    {
        RoomDeltaResourcesTotal = new GameResources();
        foreach(var keyPair in RoomDeltaResources)
        {
            RoomDeltaResourcesTotal += keyPair.Value;
        }
        TotalDelta = PersonDeltaResourcesTotal + RoomDeltaResourcesTotal;        
    }

    private void checkResourcesForAlertChanges()
    {
            if(PlayerResources.Food < 0)
            {
                AlertManager.Instance.SendAlert(Alerts.LowFood);
            }
            if(PlayerResources.Fuel < 0)
            {
                AlertManager.Instance.SendAlert(Alerts.LowFuel);
            }
    }

    #endregion
}

