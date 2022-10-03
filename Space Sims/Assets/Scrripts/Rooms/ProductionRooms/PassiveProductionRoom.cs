using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveProductionRoom : AbstractRoom
{

    [SerializeField]
    private TextMeshProUGUI _overlayMaxPeople, _overlayCurrentPeople, _upkeep, _output, _deltaAddPerosn, _deltaOutPutPersonAdd;

    [SerializeField]
    private Image _upkeepimg, _outputimg, overlayBackGroundImg;

    [SerializeField]
    private SkillsList _desiredSkill;
    public SkillsList DesiredSkill { get { return _desiredSkill; } }

    private ResourcesEnum? _outputType = null;
    public ResourcesEnum? OutPutType { get { return _outputType; } }
    public int? BaseOutputValue { get { return GetBaseOutPutValue(); } }
    public int? OutputValue { get { return GetOutPutValue(); } }
    private ResourcesEnum? _upkeepType = null;
    public ResourcesEnum? UpkeepType { get { return _upkeepType; } }
    public int? BaseUpkeepValue { get { return GetBaseUpkeepValue(); } }

    #region CustomGettersAndSetters

    private int? GetBaseOutPutValue()
    {
        if (OutPutType == null)
        {
            return null;
        }
        return RoomStat.OutPut.GetResorce((ResourcesEnum)OutPutType);
    }
    private int? GetOutPutValue()
    {
        if (OutPutType == null)
        {
            return null;
        } 
        return BaseOutputValue + CalculateAllPeoplesEffectOnProdution().GetResorce((ResourcesEnum)OutPutType);
    }

    private int? GetBaseUpkeepValue()
    {
        if (UpkeepType == null)
        {
            return null;
        }
        return RoomStat.Upkeep.GetResorce((ResourcesEnum)UpkeepType);
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
        UpdateRoomStats();
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
                if (outPutFound == true) { Debug.LogWarning("Room " + gameObject.name + " has two diffrent Resources used for OutPut"); }
                outPutFound = true;
                _outputType = re;
            }
        }
    }

    protected override void UpdateRoomStats()
    {
        GameResources delta;
        if(!isRoomActive || IsUnderConstruction)
        {
           delta = new GameResources();
        }
        else
        {
            GameResources output = RoomStat.OutPut;
            foreach(PersonInfo worker in Workers)
            {
                output = output + CalculatePersonEffectOnProduction(worker);
            }
            delta = output - RoomStat.Upkeep;
        }
        GlobalStats.Instance.AddorUpdateRoomDelta(this, delta);
    }


    private GameResources CalculateAllPeoplesEffectOnProdution()
    {
        GameResources peoplesImpact = new GameResources();
        foreach(PersonInfo worker in Workers)
        {
           peoplesImpact = peoplesImpact + CalculatePersonEffectOnProduction(worker);
        }
        return peoplesImpact;

    }

    private GameResources CalculatePersonEffectOnProduction(PersonInfo person)
    {
     return RoomStat.OutPut * (person.skills.GetSkill(DesiredSkill)/10);
    }

    public override void SetOverLay(bool value)
    {
        _overlay.SetActive(true);
    }

    protected override void UpdateOverlay()
    {

        _overlayCurrentPeople.text = Workers.Count.ToString();
        _overlayMaxPeople.text = RoomStat.MaxWorkers.ToString();

        if (OutPutType == null)
        {
            _output.text = "";
            _outputimg.gameObject.SetActive(false);
        }
        else
        { 
            _outputimg.gameObject.SetActive(true);
            _outputimg.sprite = Icons.GetIcon((ResourcesEnum)OutPutType);
            _output.text = "+" + OutputValue.ToString();
        }
        if (UpkeepType == null)
        {
            _upkeep.text = "";
            _upkeepimg.gameObject.SetActive(false);
        }
        else
        { 
            _upkeepimg.gameObject.SetActive(true);
            _upkeepimg.sprite = Icons.GetIcon((ResourcesEnum)UpkeepType);
            _upkeep.text = BaseUpkeepValue.ToString();
        }
    }

    public override void PersonHover(PersonInfo personInfo)
    {
        if(Workers.Contains(personInfo)) { UpdateOverlay(); return; }
        _deltaAddPerosn.color = Color.green;
        _deltaAddPerosn.text = "+1";
        Color imgBackgroundColour = Color.green;
        imgBackgroundColour.a = 0.2f;
        overlayBackGroundImg.color = imgBackgroundColour;
        if(Workers.Count == RoomStat.MaxWorkers)
        {
            imgBackgroundColour = Color.red;
            imgBackgroundColour.a = 0.2f;
            overlayBackGroundImg.color = imgBackgroundColour;
            _deltaAddPerosn.text = "N/A";
            _deltaAddPerosn.color = Color.red;
        }
        else
        {
            _deltaOutPutPersonAdd.text = "+" + CalculatePersonEffectOnProduction(personInfo).GetResorce((ResourcesEnum)OutPutType);
        }
        _overlay.SetActive(true);
        UpdateOverlay();
    }

    public override void ClearPersonHover()
    {
        _deltaAddPerosn.text = "";
        _deltaOutPutPersonAdd.text = "";
        _overlay.SetActive(false);
    }
}
