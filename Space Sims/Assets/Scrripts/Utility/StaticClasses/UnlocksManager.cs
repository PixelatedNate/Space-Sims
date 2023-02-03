using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnlocksManager
{
    public static List<RoomType> UnlockedRooms { get; private set; } = new List<RoomType>();
    //cosmetics

    public static void UnlockRoom(RoomType roomType)
    {
        if(UnlockedRooms.Contains(roomType))
        {
            Debug.LogWarning("Trying to unlock a room that is allready unlcoked");
            return;
        }
        UnlockedRooms.Add(roomType);
        return;
    }
}
