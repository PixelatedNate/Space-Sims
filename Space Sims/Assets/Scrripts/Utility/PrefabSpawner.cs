using System;
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
    private GameObject PersonPrefab;

    [SerializeField]
    private GameObject FuelRoomTemplate;
    [SerializeField]
    private GameObject FoodRoomTemplate;
    [SerializeField]
    private GameObject MineralsRoomTemplate;
    [SerializeField]
    private GameObject CrewQuaterRoomTemplate;
    [SerializeField]
    private GameObject QuestRoomTemplate;

    public GameObject SpawnPerson()
    {
        return GameObject.Instantiate(PersonPrefab);
    }
    public GameObject SpawnPerson(AbstractRoom room)
    {
        var person = GameObject.Instantiate(PersonPrefab);
        //person.GetComponent<Person>().RandomisePerson();
        if (person.GetComponent<Person>().AssginRoomToPerson(room))
        {
            person.transform.position = room.transform.position;
            person.transform.position = person.transform.position - Vector3.forward;
            return person;
        }
        else
        {
            GameObject.Destroy(person);
            Debug.LogWarning("could not spawn person in room");
            return null;
        }
    }

    public GameObject SpawnPerson(AbstractRoom room, PersonTemplate personTemplate)
    {

        PersonInfo personInfo = new PersonInfo(personTemplate);

        var person = GameObject.Instantiate(PersonPrefab);
        person.GetComponent<Person>().AssginPerson(personInfo);
        if (person.GetComponent<Person>().AssginRoomToPerson(room))
        {
            person.transform.position = room.transform.position;
            person.transform.position = person.transform.position - Vector3.forward;
            return person;
        }
        else
        {
            GameObject.Destroy(person);
            Debug.LogWarning("could not spawn person in room");
            return null;
        }


        /*
     //   PersonInfo newPersonInfo = personInfo.Clone();
        
        var person = GameObject.Instantiate(PersonTemplate);
        newPersonInfo.FirstTimeIntilize();
        Debug.Log(newPersonInfo);  
        person.GetComponent<Person>().AssginPerson(newPersonInfo);
        if (person.GetComponent<Person>().AssginRoomToPerson(room))
        {
            person.transform.position = room.transform.position;
            person.transform.position = person.transform.position - Vector3.forward;
            return person;
        }
        else
        {
            GameObject.Destroy(person);
            Debug.LogWarning("could not spawn person in room");
            return null;
        }
        */
        throw new Exception("Commneted out temperioly");
    }


    public GameObject SpawnRoom(RoomType roomType)
    {
        switch (roomType)
        {
            case (RoomType.Fuel): return GameObject.Instantiate(FuelRoomTemplate);
            case (RoomType.Food): return GameObject.Instantiate(FoodRoomTemplate);
            case (RoomType.Minerals): return GameObject.Instantiate(MineralsRoomTemplate);
            case (RoomType.CrewQuaters): return GameObject.Instantiate(CrewQuaterRoomTemplate);
            case (RoomType.QuestRoom): return GameObject.Instantiate(QuestRoomTemplate);
            default: throw new Exception(roomType.ToString() + ": roomTypeCanNotBeFound");
        }
    }


}
