using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveProductionRoom : AbstractRoom
{

    [SerializeField]
    private SkillsList _desiredSkill;
    public SkillsList DesiredSkill { get { return _desiredSkill; } }

    private ResourcesEnum? _outputType = null;
    public ResourcesEnum? OutPutType { get { return _outputType; } }
    public int? OutputValue { get { return GetOutPutValue(); } }

    private ResourcesEnum? _upkeepType = null;
    public ResourcesEnum? UpkeepType { get { return _upkeepType; } }
    public int? UpkeepValue { get { return GetUpkeepValue(); } }

    #region CustomGettersAndSetters

    private int? GetOutPutValue()
    {
        if (OutPutType == null)
        {
            return null;
        }
        return RoomStat.OutPut.GetResorce((ResourcesEnum)OutPutType);
    }

    private int? GetUpkeepValue()
    {
        if (UpkeepType == null)
        {
            return null;
        }
        return RoomStat.OutPut.GetResorce((ResourcesEnum)UpkeepType);
    }
    #endregion

    public override void IntisaliseRoom()
    {
          SetUpkeepAndOutPut();
    }

    void Start()
    {          
        IntisaliseRoom();
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }

    private void SetUpkeepAndOutPut()
    {
        bool upkeepFound = false;
        bool outPutFound = false;
        foreach (ResourcesEnum re in Enum.GetValues(typeof(ResourcesEnum)))
        {
            if (RoomStat.Upkeep.GetResorce(re) != 0)
            {
                if (upkeepFound == true) { throw new Exception("Room " + gameObject.name + " has two diffrent Resources used for Upkeep"); }
                upkeepFound = true;
                _upkeepType = re;
            }
            if (RoomStat.OutPut.GetResorce(re) != 0)
            {
                if (outPutFound == true) { Debug.LogWarning("Room " +  gameObject.name + " has two diffrent Resources used for OutPut"); }
                outPutFound = true;
                _outputType = re;
            }
        }
    }

    protected override void UpdateRoomStats()
    {
        GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }
}
