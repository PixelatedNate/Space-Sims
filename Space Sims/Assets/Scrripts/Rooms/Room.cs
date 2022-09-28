using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour, IInteractables
{
    [SerializeField]
    private GameObject _tempSelect;
    [SerializeField]
    private RoomStats[] _roomlevels;

    [SerializeField]
    private string _roomName;
    public string RoomName { get { return _roomName; } }

    [SerializeField, TextArea(10, 10)]
    private string _roomDiscription;
    public string RoomDiscription { get { return _roomDiscription; } }
    public Vector3Int RoomPosition { get; set; }

    [SerializeField]
    private RoomType _roomType;
    public RoomType RoomType { get { return _roomType; } }
    private int Level { get; set; } = 0;
    public RoomStats RoomStat { get { return _roomlevels[Level]; } }

    [SerializeField]
    private SkillsList _desiredSkill;
    public SkillsList DesiredSkill { get { return _desiredSkill; } }
    public bool IsUnderConstruction { get; private set; }
    public TimeDelayManager.Timer ConstructionTimer { get; private set; }

    private ResourcesEnum? _outputType = null;
    public ResourcesEnum? OutPutType { get { return _outputType; } }
    public int? OutputValue { get { return GetOutPutValue(); } }

    private ResourcesEnum? _upkeepType = null;
    public ResourcesEnum? UpkeepType { get { return _upkeepType; } }
    public int? UpkeepValue { get { return GetUpkeepValue(); } }
    public List<PersonInfo> Workers { get; private set; } = new List<PersonInfo>();

    [SerializeField]
    private Camera _roomCameraPortal;


    [SerializeField]
    private Tilemap _pathFindingTileMap;
    public Tilemap PathFindingTileMap { get { return _pathFindingTileMap; } }

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


    void Start()
    {
        IntisaliseRoom();
       // GlobalStats.Instance.PlyaerRooms.Add(this);
     //   GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }



    #region PublicMethods


    public void BuildRoom()
    {
        if(IsUnderConstruction || Level != 0)
        {
            throw new Exception("Trying to build a new room which allready exsistes");
        }
     //   GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        BuildOrUpgradeRoom(0);
    }
    public void UpgradeRoom(int level)
    {
        if(IsUnderConstruction || Level == _roomlevels.Length-1)
        {
            throw new Exception("Trying to uprage a room which is max level or allready under construction");
        }
        BuildOrUpgradeRoom(++level);
    }
    public void IntisaliseRoom()
    {
        SetUpkeepAndOutPut();
    }

    public bool AddWorker(Person person)
    {
        if(Workers.Count == RoomStat.MaxWorkers)
        {
            return false;
        }
        if(Workers.Contains(person.PersonInfo))
        {
            Debug.LogWarning("Failed to add a person to room: as they are allready in");
            return false;
        }
        else
        {
            Workers.Add(person.PersonInfo);
            return true;
        }
    }

    public void RemoveWorker(Person person)
    {
        if (Workers.Count == 0 || !Workers.Contains(person.PersonInfo))
        {
            Debug.LogWarning("Failed to reomve a person from room: as they are not present in");
        }
        else
        {
            Workers.Remove(person.PersonInfo);
        }
    }

    public void SetCamera(RenderTexture renderTexture)
    {
       _roomCameraPortal.GetComponent<Camera>().gameObject.SetActive(true);
       _roomCameraPortal.GetComponent<Camera>().targetTexture = renderTexture;
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

    #endregion

    #region PrivateMethods

    private void BuildOrUpgradeRoom(int newLevel)
    {
        IsUnderConstruction = true;
     //   GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        ConstructionTimer = TimeDelayManager.Instance.AddTimer( new TimeDelayManager.Timer(DateTime.Now.AddMinutes(_roomlevels[newLevel].BuildTime),ConstructionCompleat));
    }
    private void ConstructionCompleat()
    {
        IsUnderConstruction = false;
     //   GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }

    #endregion

    #region InteractableInterface

    public void OnSelect()
    {
        _tempSelect.SetActive(true);
       // UIManager.Instance.DisplayRoomView(this);
    }

    public void OnDeselect()
    {
        _roomCameraPortal.GetComponent<Camera>().gameObject.SetActive(false);
        _tempSelect.SetActive(false);
    }

    public bool OnHold()
    {
        return false;
    }
    public void OnHoldRelease() { }

    #endregion
}
