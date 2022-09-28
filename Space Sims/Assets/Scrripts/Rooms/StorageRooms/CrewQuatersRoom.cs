using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewQuatersRoom : AbstractRoom
{
    public override void IntisaliseRoom()
    {

    }

    protected override void UpdateRoomStats()
    {
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;
    }
}
