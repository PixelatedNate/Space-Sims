using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public static PrefabSpawner Instance;
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

    [SerializeField]
    private GameObject PersonTemplate;
    
    [SerializeField]
    private GameObject FuelRoomTemplate;
    [SerializeField]
    private GameObject FoodRoomTemplate;
    [SerializeField]
    private GameObject MineralsRoomTemplate;
    [SerializeField]
    private GameObject CrewQuaterRoomTemplate;

    public GameObject SpawnPerson()
    {
       return GameObject.Instantiate(PersonTemplate);
    }

    public GameObject SpawnRoom(RoomType roomType)
    {
        switch(roomType)
        {
            case (RoomType.Fuel): return GameObject.Instantiate(FuelRoomTemplate);
            case (RoomType.Food): return GameObject.Instantiate(FoodRoomTemplate);
            case (RoomType.Minerals): return GameObject.Instantiate(MineralsRoomTemplate);
            case (RoomType.CrewQuaters): return GameObject.Instantiate(CrewQuaterRoomTemplate);
            default: throw new Exception(roomType.ToString() + ": roomTypeCanNotBeFound");
        }
    }


}
