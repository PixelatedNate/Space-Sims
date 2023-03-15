using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static string RootPath { get { return Application.persistentDataPath + "/Save"; } }
    public static string PostToutoiralRootPath { get { return Application.persistentDataPath + "/PostToutorialSave"; } }
    public static string SaveStatsPath { get { return Application.persistentDataPath + "/Save/GlobalData.gd"; } }
    public static string NavigationSavePath { get { return Application.persistentDataPath + "/Save/Navigation.gd"; } }

    public static string RoomPath { get { return Application.persistentDataPath + "/Save/Rooms"; } }
    public static string PeoplePath { get { return Application.persistentDataPath + "/Save/People"; } }
    public static string PlanetPath { get { return Application.persistentDataPath + "/Save/Planet"; } }
    public static string PlanetPrefix = ".Planet";
    public static string WaittingQuestPath { get { return Application.persistentDataPath + "/Save/Quests/WaittingQuests"; } }
    public static string TransportQuestPath { get { return Application.persistentDataPath + "/Save/Quests/TransportQuests"; } }
    public static string BuildRoomQuestPath { get { return Application.persistentDataPath + "/Save/Quests/BuildRoomQuest"; } }
    public static string QuestLinePath { get { return Application.persistentDataPath + "/Save/Quests/QuestLines"; } }
    public static string QuestLinePrefix = ".QL";

    public static string TimerPath { get { return Application.persistentDataPath + "/Save/Timers"; } }
    public static string TimerPrefix = ".Timer";


    public static Dictionary<PersonInfo, string> SavedPeople = new Dictionary<PersonInfo, string>();
    public static Dictionary<string, PersonInfo> LoadedPeople { get; set; } = new Dictionary<string, PersonInfo>();

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

    public static void Save(this TransportQuestSaveData transportQuestSaveData)
    {
        if (!Directory.Exists(TransportQuestPath))
        {
            System.IO.Directory.CreateDirectory(TransportQuestPath);
        }
        var path = TransportQuestPath + "/" + System.IO.Path.GetRandomFileName();
        SaveData<TransportQuestSaveData>(transportQuestSaveData, path);
    }

    public static void Save(this BuildRoomSaveData QuestSaveData)
    {
        if (!Directory.Exists(BuildRoomQuestPath))
        {
            System.IO.Directory.CreateDirectory(BuildRoomQuestPath);
        }
        var path = BuildRoomQuestPath + "/" + System.IO.Path.GetRandomFileName();
        SaveData<BuildRoomSaveData>(QuestSaveData, path);

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
            System.IO.Directory.CreateDirectory(PlanetPath);
        }
        var path = PlanetPath + "/" + data.planetId.ToString() + PlanetPrefix;
        SaveData<PlanetConttainerSaveData>(data, path);
    }

    public static void Save(this QuestLineSaveData data)
    {
        if (!Directory.Exists(QuestLinePath))
        {
            System.IO.Directory.CreateDirectory(QuestLinePath);
        }
        var path = QuestLinePath + "/" + data.Id.ToString() + QuestLinePrefix;
        SaveData<QuestLineSaveData>(data, path);
    }


    public static void ClearSave()
    {
        if (Directory.Exists(RootPath))
        {
            Directory.Delete(RootPath, true);
        }
    }

    public static void LoadPostToutorial()
    {
        ClearSave();
        CopyDirectory(PostToutoiralRootPath, RootPath, true);
    }



    private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }


    public static void saveAfterToutorial()
    {
        SaveAll();
        if (Directory.Exists(PostToutoiralRootPath))
        {
            Directory.Delete(PostToutoiralRootPath, true);
        }
        CopyDirectory(RootPath, PostToutoiralRootPath, true);
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
        SavedPeople.Clear();
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

    public static T[] GetAllSavedQuest<T>(string path)
    {
        if (!Directory.Exists(path))
        {
            return new T[0];
        }
        List<T> questsaveData = new List<T>();
        foreach (string fullPath in Directory.GetFiles(path))
        {
            T data = LoadData<T>(fullPath);
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
