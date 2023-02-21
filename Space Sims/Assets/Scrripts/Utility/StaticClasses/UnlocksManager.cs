using System.Collections.Generic;
using UnityEngine;

public static class UnlocksManager
{
    public static List<RoomType> UnlockedRooms { get; private set; } = new List<RoomType>();
    //cosmetics

    public static List<string> UnlockedCoths { get; private set; } = new List<string>();

    public static void UnlockRoom(RoomType roomType)
    {
        if (UnlockedRooms.Contains(roomType))
        {
            Debug.LogWarning("Trying to unlock a room that is allready unlcoked");
            return;
        }
        UnlockedRooms.Add(roomType);
        return;
    }

    public static void UnlockCloth(Sprite sprite)
    {
        if (UnlockedCoths.Contains(sprite.name))
        {
            Debug.LogWarning("Trying to unlock a cloth that is allready unlcoked");
            return;
        }
        UnlockedCoths.Add(sprite.name);
        return;
    }

}
