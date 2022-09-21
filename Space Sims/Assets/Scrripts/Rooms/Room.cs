using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, IInteractables
{
    [Serializable]
    public class RoomStats
    {
        [SerializeField]
        GameResources _upkeep;
        public GameResources Upkeep { get { return _upkeep; } }
        
        [SerializeField]
        GameResources _outPut;
        public GameResources OutPut { get { return _outPut; } }
        
        [SerializeField]
        GameResources _buildCost;
        public GameResources BuildCost { get { return _buildCost; } }

        [SerializeField]
        private int _maxWorkers;
        public int MaxWorkers { get { return _maxWorkers; } }

        [SerializeField]
        public double _buildTime;
        public double BuildTime { get { return _buildTime; } }
}


    [SerializeField]
    private GameObject TempSelect;

    private int level { get; set; } = 0; // overide setmethod at somepoint
    
    [SerializeField]
    private RoomStats[] Roomlevels;

    public Vector3Int RoomPosition;
    
    private Sprite _roomImg;

    public Sprite RoomImg { get { return _roomImg; } }

    [SerializeField]
    private string _roomName; 
    public string RoomName { get { return _roomName; } }

    [SerializeField, TextArea(10,10)]
    private string _roomDiscription;
    public string RoomDiscription { get { return _roomDiscription; } }

    [SerializeField]
    private RoomType _roomType;

    public RoomType RoomType { get { return _roomType; } }

    [SerializeField]
    public List<PersonInfo> Workers;

    [SerializeField]
    private SkillsList _desiredSkill;

    public SkillsList DesiredSkill { get { return _desiredSkill; } }

    private ResourcesEnum? _outputType = null;
    public  ResourcesEnum? OutPutType { get { return _outputType; } }
    public int? OutputValue { get {
            if (OutPutType == null) return null;
            return RoomStat.OutPut.GetResorce((ResourcesEnum)OutPutType);
                } }
    
    private ResourcesEnum? _upkeepType = null;
    public  ResourcesEnum? UpkeepType { get { return _upkeepType; } }
    public int? UpkeepValue
    {
        get
        {
            if (UpkeepType == null) return null;
            return -RoomStat.Upkeep.GetResorce((ResourcesEnum)UpkeepType);
        }
    }
    
    public bool IsUnderConstruction { get; private set; }

    public TimeDelayManager.Timer ConstructionTimer { get; private set; }



    [SerializeField]
    private Camera camera;
    public RoomStats RoomStat { get { return Roomlevels[level]; } }

    void Start()
    {
        IntisaliseRoom();
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }



    public void StartConstruction(int level)
    {
        Debug.Log("starting");
        IsUnderConstruction = true;
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        ConstructionTimer = TimeDelayManager.Instance.AddTimer( new TimeDelayManager.Timer(DateTime.Now.AddMinutes(Roomlevels[level].BuildTime),ConstructionCompleat));
    }

    public void ConstructionCompleat()
    {
        Debug.Log("Complete");
        IsUnderConstruction = false;
        GlobalStats.Instance.AddorUpdateRoomDelta(this, RoomStat.OutPut - RoomStat.Upkeep);
    }

    public void IntisaliseRoom()
    {
        SetUpkeepAndOutPut();
    }

    public bool Upgrade()
    {
        if (level == Roomlevels.Length-1)
        {
            throw new Exception("trying to upgradeMaxLevel Room");
        }
        if (Roomlevels[level + 1].BuildCost < GlobalStats.Instance.PlayerResources)
        {
            level++;
            return true;
        }
        else
        {
            return false;
        }      
    }

    public bool addWorker(Person person)
    {
        if(Workers.Count == RoomStat.MaxWorkers)
        {
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
        if (Workers.Count == 0)
        {
            throw new Exception("trying to reomve a person that dosn't exsist");
        }
        else
        {
            Workers.Remove(person.PersonInfo);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCamera(RenderTexture renderTexture)
    {
        camera.gameObject.SetActive(true);
        camera.targetTexture = renderTexture;
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


#region InteractableInterface

    public void OnSelect()
    {
        TempSelect.SetActive(true);
        UIManager.Instance.DisplaySelected(this);
    }

    public void OnDeselect()
    {
        camera.gameObject.SetActive(false);
        TempSelect.SetActive(false);
    }

    public bool OnHold()
    {
        return false;
     //   throw new NotImplementedException();
    }

    public void OnHoldRelease()
    {
       // throw new NotImplementedException();
    }

    #endregion
}
