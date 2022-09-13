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
    private GameObject RoomTemplate;
 
    public GameObject SpawnPerson()
    {
       return GameObject.Instantiate(PersonTemplate);
    }

    public GameObject SpawnRoom(RoomType roomType)
    {
        return GameObject.Instantiate(RoomTemplate);
    }


}
