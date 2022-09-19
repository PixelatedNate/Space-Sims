using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
   
    #region Singlton
    public static GlobalStats Instance;
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
    #endregion

    //section for all the code relating to DeltaGameResourcesChyanges
    #region DeltaChanges
    Dictionary<Person, GameResources> PersonDeltaResources = new Dictionary<Person, GameResources>();
    GameResources PersonDeltaResourcesTotal = new GameResources();
    Dictionary<Room, GameResources> RoomDeltaResources = new Dictionary<Room, GameResources>();
    GameResources RoomDeltaResourcesTotal = new GameResources();
    GameResources TotalDelta;
    #endregion

    [SerializeField]
    private GameResources _playerResources;
    public GameResources PlayerResources { get { return _playerResources; } 
        set {
            _playerResources = value;
            checkResourcesForAlertChanges();
            UIManager.Instance.UpdateTopBar(_playerResources, TotalDelta, 0);
            }
        }


    public List<PersonInfo> PlayersPeople = new List<PersonInfo>();
    public List<Room> PlyaerRooms = new List<Room>();



    #region DeltaGameResources Methods
    public void AddorUpdatePersonDelta(Person person, GameResources delta)
    {
        if(PersonDeltaResources.ContainsKey(person))
        {
            PersonDeltaResources[person] = delta;
        }
        else
        {
            PersonDeltaResources.Add(person, delta);
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
    private void RecalculatePersonDeltaTotal()
    {
        PersonDeltaResourcesTotal = new GameResources();
        foreach(var keyPair in PersonDeltaResources)
        {
            PersonDeltaResourcesTotal += keyPair.Value;
        }
        TotalDelta = PersonDeltaResourcesTotal + RoomDeltaResourcesTotal;
        
    }



    #endregion



    private void Start()
    {
        TimeTickSystem.OnTick += OnTick;
    }

    private void OnTick(object source, EventArgs e)
    {

        PlayerResources += TotalDelta;
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
}
