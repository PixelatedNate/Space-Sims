using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{


    [SerializeField]
    AbstractQuest[] QuesttestsForMenu;

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


    private int NegativeFood = 0;

    [SerializeField]
    private int NegativeFoodPersonLevelVlue; // food is counted  into negative value when this value is reach a perosn will leave +ve value needed here


    private int _maxPeople;
    public int MaxPeople { get { return _maxPeople; } set { SetMaxPeople(value); } }

    public List<PersonInfo> PlayersPeople { get; set; } = new List<PersonInfo>();
    public List<AbstractRoom> PlyaerRooms { get; set; } = new List<AbstractRoom>();
    public QuestRoom QuestRoom { get; set; }



    private float RawFuelNeeded;
    public float FuelProductionMultiplyer { get; private set; } = 1; // this value is chagned to reflect low fuel and in turn reduce produciton.

    //  private List<Quest> Quests { get; } = new List<Quest>();


    private bool _lowFood = false;
    private Alert _lowFoodAlert;

    private bool _lowFuel = false;
    private Alert _lowFuelAlert;



    #region CustomGetterAndSetters


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
        QuestManager.SetAvalibleQuest(QuesttestsForMenu);
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
        RawFuelNeeded = 0;
        foreach (var keyPair in RoomDeltaResources)
        {
            RoomDeltaResourcesTotal += keyPair.Value;
            if (keyPair.Value.Fuel < 0)
            {
                RawFuelNeeded += MathF.Abs(keyPair.Value.Fuel);
            }
          
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
            NegativeFood = NegativeFood - PlayerResources.Food;

            if (NegativeFood >= NegativeFoodPersonLevelVlue)
            {
                RandomPersonLeave();
                NegativeFood = 0;

            }

            PlayerResources.Food = 0;
        }
        if (PlayerResources.Food > 0 && _lowFood)
        {
            _lowFood = false;
            NegativeFood = 0;
            AlertManager.Instance.RemoveAlert(_lowFoodAlert);
        }
        else if (PlayerResources.Food >= MaxStorage.Food)
        {
            PlayerResources.Food = MaxStorage.Food;
        }

        if (PlayerResources.Fuel < 0)
        {
            if (!_lowFuel)
            {
               _lowFuel = true;
               _lowFuelAlert = Alert.LowFuelAlert;
               AlertManager.Instance.SendAlert(_lowFuelAlert);
            }
              float  percent = RawFuelNeeded / 100f;
              float  MissingFuelPercent = TotalDelta.Fuel / percent;
              FuelProductionMultiplyer = (100f - Mathf.Abs(MissingFuelPercent))/100f;
                foreach(AbstractRoom room in PlyaerRooms)
                {
                room.UpdateRoomStats();
                }
              Debug.Log(MissingFuelPercent);
              PlayerResources.Fuel = 0;
        }
        else if (PlayerResources.Fuel > 0 && _lowFuel)
        {
            FuelProductionMultiplyer = 1;
               foreach(AbstractRoom room in PlyaerRooms)
               {
                room.UpdateRoomStats();
               }
            _lowFuel = false;
            AlertManager.Instance.RemoveAlert(_lowFuelAlert);
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


    public void RandomPersonLeave()
    {
        List<PersonInfo> peopleNotOnQuest = PlayersPeople.FindAll(p => p.IsQuesting == false);

        if(peopleNotOnQuest.Count <= 4)
        {
            return;
        }

        PersonInfo personToLeave = peopleNotOnQuest[0];
        Alert PersonLeaveAlert = new Alert(personToLeave.Name, "you coun't keep the person happy so they left", Alert.AlertPrority.High, Icons.GetMiscUIIcon(UIIcons.Person));
        AlertManager.Instance.SendAlert(PersonLeaveAlert);
        personToLeave.PersonMonoBehaviour.LeaveShipForGood();
    }

    #endregion
}

