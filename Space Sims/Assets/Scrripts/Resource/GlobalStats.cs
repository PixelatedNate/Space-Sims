using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{

    
    public static GlobalStats Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    
    [SerializeField]
    private GameResources _playerResources;
    public GameResources PlayerResources { get { return _playerResources; } 
        set {
            _playerResources = value;
            checkResourcesForAlertChanges();
            }
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


    private void Start()
    {
        //For tessting.
      //  PlayerResources = PlayerResources + new GameResources { Food = 10, Fuel = 100 };
      //  PlayerResources = PlayerResources - new GameResources { Food = 10, Fuel = 100 };
      //  PlayerResources = PlayerResources - new GameResources { Food = 10, Fuel = 100, Premimum = 3};
    }


}
