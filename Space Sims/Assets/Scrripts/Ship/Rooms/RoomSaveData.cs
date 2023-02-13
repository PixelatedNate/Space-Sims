using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomSaveData  {

    public int[] roomPosition = new int[3];
    public int level;
    public int roomType;

    public RoomSaveData(AbstractRoom room)
    {
        this.roomPosition[0] = room.RoomPosition.x;
        this.roomPosition[1] = room.RoomPosition.y;
        this.roomPosition[2] = room.RoomPosition.z;
        this.level = room.Level;
        this.roomType = (int)room.RoomType;
    }



   
}
