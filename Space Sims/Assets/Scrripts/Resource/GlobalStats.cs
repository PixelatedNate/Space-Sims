using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{


    [SerializeField]
    Quest[] QuesttestsForMenu;



    public static GlobalStats Instance;

    private Dictionary<Person, GameResources> PersonDeltaResources { get; set; } = new Dictionary<Person, GameResources>();
    private GameResources PersonDeltaResourcesTotal { get; set; } = new GameResources();

    private Dictionary<AbstractRoom, GameResources> RoomDeltaResources { get; set; } = new Dictionary<AbstractRoom, GameResources>();

    [SerializeField]
    private GameResources _bassMaxStorage;
    private GameResources MaxStorage { get; set; } = new GameResources();
    private GameResources RoomDeltaResourcesTotal { get; set; } = new GameResources();

    private GameResources TotalDelta { get; set; } = new GameResources();

    [SerializeField]
    private GameResources _startingResources;

    private GameResources _playerResources = new GameResources();
    public GameResources PlayerResources { get { return _playerResources; } set { SetPlayerResources(value); } }

    private int _maxPeople;
    public int MaxPeople { get { return _maxPeople; } set { SetMaxPeople(value); } }
    
    public List<PersonInfo> PlayersPeople { get; set; } = new List<PersonInfo>();  
    public List<AbstractRoom> PlyaerRooms { get; set; } = new List<AbstractRoom>();
    public QuestRoom QuestRoom { get; set; }


    private List<Quest> Quests { get; } = new List<Quest>();


    private bool _lowFood;
    private Alert _lowFoodAlert;
    


    #region CustomGetterAndSetters

    public List<Quest> GetQuestsByStaus(Quest.Status status)
    {
        return Quests.FindAll((a) => a.questStaus == status);   
    }




    private void SetMaxPeople(int value)
    {
        _maxPeople = value;
        UIManager.Instance.UpdateTopBar(_playerResources, TotalDelta, PlayersPeople.Count, MaxPeople, MaxStorage);
    }
    private void SetPlayerResources(GameResources value)
    {
        _playerResources = value;
        checkResourcesForAlertChanges();
        UIManager.Instance.UpdateTopBar(_playerResources, TotalDelta, PlayersPeople.Count, MaxPeople, MaxStorage);
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
        // for testing at this stage only   
        Quests.AddRange(QuesttestsForMenu);
        foreach(Quest q in Quests)
        {
            q.UnassginAllPeopople();
            q.questStaus = Quest.Status.Available;
        }



        MaxStorage = _bassMaxStorage;
        PlayerResources = _startingResources;
        TimeTickSystem.OnTick += OnTick;
    }




    #region PublicMethods

    public void AddorUpdatePersonDelta(Person person, GameResources personDeltaResources)
    {
        if (PersonDeltaResources.ContainsKey(person))
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
        if (RoomDeltaResources.ContainsKey(room))
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
        foreach (var keyPair in PersonDeltaResources)
        {
            PersonDeltaResourcesTotal += keyPair.Value;
        }
        TotalDelta = PersonDeltaResourcesTotal + RoomDeltaResourcesTotal;
    }
    private void RecalculateRoomDeltaTotal()
    {
        RoomDeltaResourcesTotal = new GameResources();
        foreach (var keyPair in RoomDeltaResources)
        {
            RoomDeltaResourcesTotal += keyPair.Value;
        }
        TotalDelta = PersonDeltaResourcesTotal + RoomDeltaResourcesTotal;
    }

    private void checkResourcesForAlertChanges()
    {
        if (PlayerResources.Food <= 0)
        {
            if (!_lowFood)
            {
                _lowFoodAlert = Alert.LowFoodAlert;
                AlertManager.Instance.SendAlert(_lowFoodAlert);
                _lowFood = true;
            }            
            PlayerResources.Food = 0;
        }
        if(PlayerResources.Food > 0 && _lowFood)
        {
            _lowFood = false; 
            AlertManager.Instance.RemoveAlert(_lowFoodAlert);
        }
        else if (PlayerResources.Food >= MaxStorage.Food)
        {
            PlayerResources.Food = MaxStorage.Food;
        }

        if (PlayerResources.Fuel < 0)
        {
        //    AlertManager.Instance.SendAlert(Alerts.LowFuel);
            PlayerResources.Fuel = 0;
        }
        else if (PlayerResources.Fuel >= MaxStorage.Fuel)
        {
            PlayerResources.Fuel = MaxStorage.Fuel;
        }
        if (PlayerResources.Minerals < 0)
        {
            PlayerResources.Minerals = 0;
        }
        else if (PlayerResources.Minerals >= MaxStorage.Minerals)
        {
            PlayerResources.Minerals = MaxStorage.Minerals;
        }

    }

    #endregion
}

