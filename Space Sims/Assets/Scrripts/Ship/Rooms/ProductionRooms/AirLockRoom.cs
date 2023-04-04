using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLockRoom : AbstractRoom
{
    public override void ClearPersonHover()
    {
      //  throw new System.NotImplementedException();
    }

    public override void IntisaliseRoom()
    {
       // throw new System.NotImplementedException();
    }

    public override void PersonHover(PersonInfo personInfo)
    {
     //   throw new System.NotImplementedException();
    }

    public override void SetOverLay(bool value)
    {
       // throw new System.NotImplementedException();
    }

    public override void UpdateRoomStats()
    {
      //  throw new System.NotImplementedException();
    }

    protected override void UpdateOverlay()
    {
     //   throw new System.NotImplementedException();
    }

    public override bool AddWorker(Person person)
    {
        person.LeaveShipForGood();
        return true;
    }

}
