using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewQuatersRoom : AbstractRoom
{
    public override void IntisaliseRoom()
    {
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;
    }

    void Start()
    {          
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;
    }

    protected override void UpdateRoomStats()
    {
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;
    }
}
