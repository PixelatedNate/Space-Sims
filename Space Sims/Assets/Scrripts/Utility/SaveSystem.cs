using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    public static string RootPath { get { return Application.persistentDataPath + "/Save";  }}
    public static string RoomPath { get { return Application.persistentDataPath + "/Save/Rooms";  }}


    public static void Save(this RoomSaveData roomData)
    {
        if (!Directory.Exists(RoomPath))
        {
            System.IO.Directory.CreateDirectory(RoomPath);
        }
        var path = RoomPath + "/" + System.IO.Path.GetRandomFileName(); 
        SaveData<RoomSaveData>(roomData,path);
    }


    public static void SaveAll()
    {
        if (Directory.Exists(RootPath))
        {
            Directory.Delete(RootPath, true);
        }
        foreach(AbstractRoom room in GlobalStats.Instance.PlyaerRooms)
        {
            room.Save();
        }
    }

    public static RoomSaveData[] GetAllSavedRoomData()
    {
        if(!Directory.Exists(RoomPath))
        {
            return new RoomSaveData[0];
        }
        List<RoomSaveData> roomsaveData = new List<RoomSaveData>();
        foreach (string roomPath in Directory.GetFiles(RoomPath))
        {
            RoomSaveData roomData = LoadData<RoomSaveData>(roomPath);
            Debug.Log(roomData.roomType);
            Debug.Log(roomData.level);
            Debug.Log(roomData.roomPosition.ToString());
            roomsaveData.Add(roomData);
        }
        return roomsaveData.ToArray();
     }
    

    public static void SaveData<T>(T data, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        Debug.LogWarning(path);
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
