using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static string RootPath { get { return Application.persistentDataPath + "/Save"; } }
    public static string SaveStatsPath { get { return Application.persistentDataPath + "/Save/GlobalData.gd"; } }
    public static string NavigationSavePath { get { return Application.persistentDataPath + "/Save/Navigation.gd"; } }
   
    public static string RoomPath { get { return Application.persistentDataPath + "/Save/Rooms"; } }
    public static string PeoplePath { get { return Application.persistentDataPath + "/Save/People"; } }
    public static string PlanetPath { get { return Application.persistentDataPath + "/Save/Planet"; } }
    public static string WaittingQuestPath { get { return Application.persistentDataPath + "/Save/Quests/WaittingQuests"; } }
    public static string TransportQuestPath { get { return Application.persistentDataPath + "/Save/Quests/TransportQuests"; } }
    public static string TimerPath { get { return Application.persistentDataPath + "/Save/Timers"; } }
    public static string TimerPrefix = ".Timer";


    public static Dictionary<PersonInfo, string> SavedPeople = new Dictionary<PersonInfo, string>();
    public static Dictionary<string, PersonInfo> LoadedPeople = new Dictionary<string, PersonInfo>();

    public static void Save(this RoomSaveData roomData)
    {
        if (!Directory.Exists(RoomPath))
        {
            System.IO.Directory.CreateDirectory(RoomPath);
        }
        var path = RoomPath + "/" + System.IO.Path.GetRandomFileName();
        SaveData<RoomSaveData>(roomData, path);
    }

    public static void Save(this NavigationSaveData data)
    {
        SaveData<NavigationSaveData>(data, NavigationSavePath);
    }



    public static void Save(this PersonSaveData personData, PersonInfo personInfo)
    {
        if (!Directory.Exists(PeoplePath))
        {
            System.IO.Directory.CreateDirectory(PeoplePath);
        }
        var path = PeoplePath + "/" + personData.personId + ".person";
        SaveData<PersonSaveData>(personData, path);
        SavedPeople.Add(personInfo, personData.personId);

    }
    public static void Save(this WaittingQuestSaveData waittingQuestData)
    {
        if (!Directory.Exists(WaittingQuestPath))
        {
            System.IO.Directory.CreateDirectory(WaittingQuestPath);
        }
        var path = WaittingQuestPath + "/" + System.IO.Path.GetRandomFileName();
        SaveData<WaittingQuestSaveData>(waittingQuestData, path);

    }


    public static void Save(this TimerSaveData timerData)
    {
        if (!Directory.Exists(TimerPath))
        {
            System.IO.Directory.CreateDirectory(TimerPath);
        }
        var path = TimerPath + "/" + timerData.ID.ToString() + TimerPrefix;
        SaveData<TimerSaveData>(timerData, path);
    }
    public static void Save(this GlobalStatsSaving data)
    {
        SaveData<GlobalStatsSaving>(data, SaveStatsPath);
    }

    public static void Save(this PlanetConttainerSaveData data)
    {
        if (!Directory.Exists(PlanetPath))
        {
            System.IO.Directory.CreateDirectory(TimerPath);
        }
        var path = PlanetPath + "/" + data.planetDataName.ToString() + ".planet";
        SaveData<PlanetConttainerSaveData>(data, path);
    }



    public static void SaveAll()
    {
        if (Directory.Exists(RootPath))
        {
            Directory.Delete(RootPath, true);
        }
        foreach (AbstractRoom room in GlobalStats.Instance.PlyaerRooms)
        {
            room.Save();
        }
        QuestManager.SaveQuests();
        GlobalStatsSaving saveData = new GlobalStatsSaving(GlobalStats.Instance.PlayerResources);
        saveData.Save();
        NavigationSaveData navData = new NavigationSaveData();
        navData.Save();
    }

    public static RoomSaveData[] GetAllSavedRoomData()
    {
        if (!Directory.Exists(RoomPath))
        {
            return new RoomSaveData[0];
        }
        List<RoomSaveData> roomsaveData = new List<RoomSaveData>();
        foreach (string roomPath in Directory.GetFiles(RoomPath))
        {
            RoomSaveData roomData = LoadData<RoomSaveData>(roomPath);
            roomsaveData.Add(roomData);
        }
        return roomsaveData.ToArray();
    }

    public static WaittingQuestSaveData[] GetAllWaittingSavedQuest()
    {
        if (!Directory.Exists(WaittingQuestPath))
        {
            return null;
        }
        List<WaittingQuestSaveData> questsaveData = new List<WaittingQuestSaveData>();
        foreach (string waittingQuestPath in Directory.GetFiles(WaittingQuestPath))
        {
            WaittingQuestSaveData data = LoadData<WaittingQuestSaveData>(waittingQuestPath);
            questsaveData.Add(data);
        }
        return questsaveData.ToArray();
    }


    public static PersonSaveData GetPersonData(string personId)
    {
        var personpath = PeoplePath + "/" + personId + ".person";
        PersonSaveData personData = SaveSystem.LoadData<PersonSaveData>(personpath);
        return personData;
    }


    public static void SaveData<T>(T data, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static T LoadData<T>(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string fullPath = path;
        FileStream stream = new FileStream(fullPath, FileMode.Open);
        T data = (T)formatter.Deserialize(stream);
        stream.Close();

        return data;
    }

}
