using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CrewQuatersRoom : AbstractRoom
{

    [SerializeField]
    private TextMeshProUGUI _overlayMaxPeople, _overlayCurrentPeople, _overlayDeltaPeople, _deltaAddPerosn;
    [SerializeField]
    private Image _overlayBackGroundImg;
    public override void IntisaliseRoom()
    {
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;
    }

    void Start()
    {
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.MaxPeople += RoomStat.PoepleChange;

        UpdateOverlay();
    }

    protected override void UpdateRoomStats()
    {

    }

    public override void SetOverLay(bool value)
    {
        _overlay.SetActive(value);
        UpdateOverlay();
    }

    protected override void UpdateOverlay()
    {
        _overlayCurrentPeople.text = Workers.Count.ToString();
        _overlayMaxPeople.text = RoomStat.MaxWorkers.ToString();
        _overlayDeltaPeople.text = RoomStat.PoepleChange.ToString();
    }

    public override void PersonHover(PersonInfo personInfo)
    {
        if(Workers.Contains(personInfo)) { UpdateOverlay(); return; }
        _deltaAddPerosn.color = Color.green;
        _deltaAddPerosn.text = "+1";

        Color imgBackgroundColour = Color.green;
        imgBackgroundColour.a = 0.2f;
        _overlayBackGroundImg.color = imgBackgroundColour;

        if(Workers.Count == RoomStat.MaxWorkers)
        {
        imgBackgroundColour = Color.red;
        imgBackgroundColour.a = 0.2f;
        _overlayBackGroundImg.color = imgBackgroundColour;
            _deltaAddPerosn.text = "N/A";
            _deltaAddPerosn.color = Color.red;
        }
        SetOverLay(true);
    }

    public override void ClearPersonHover()
    {
        SetOverLay(false);
    }
}
