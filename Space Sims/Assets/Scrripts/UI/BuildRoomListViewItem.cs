using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoomListViewItem : MonoBehaviour
{


    public Vector3Int RoomPosition { get; set; }

    [SerializeField]
    private AbstractRoom _room;
    [SerializeField]
    private TextMeshProUGUI _roomName, _buildTimeText, _roomDiscription, _buildCost, _upkeepCost, _output;
    [SerializeField]
    private Image _upkeepImg, _outputImg, _buildCostImg;
    [SerializeField]
    private Image _backgroundImg;

    [SerializeField]
    Image _buttonImg;

    void Start()
    {
        UpdateItem();
        TimeTickSystem.OnTick += OnTick;
    }


    private void OnTick(object source, EventArgs e)
    {
        Color c = _buttonImg.color;
        if (GlobalStats.Instance.PlayerResources >= _room.RoomStat.BuildCost)
        {
            c.a = 0;
        }
        else
        {
            c.a = 0.8f;
        }
        _buttonImg.color = c;
    }



    #region PublicMethods

    public void SetRoom(AbstractRoom room)
    {
        this._room = room;
        UpdateItem();

    }

    public void OnClick()
    {
        if (GlobalStats.Instance.PlayerResources >= _room.RoomStat.BuildCost)
        {
            GlobalStats.Instance.PlayerResources -= _room.RoomStat.BuildCost;
            AbstractRoom newRoom = RoomGridManager.Instance.BuildNewRoom(RoomPosition, _room.RoomType);
            UIManager.Instance.DeselectAll();
            newRoom.BuildRoom();
            newRoom.IntisaliseRoom();
        }
    }

    public void UpdateItem()
    {
        _roomName.text = _room.RoomName;
        _roomDiscription.text = _room.RoomDiscription;
        _buildCost.text = _room.RoomStat.BuildCost.Minerals.ToString();

        TimeSpan buildTime = new TimeSpan(0, (int)_room.RoomStat.BuildTime, 0);
        _buildTimeText.text = buildTime.ToString();
        if (_room is PassiveProductionRoom)
        {
            PassiveProductionRoom passiveProductionRoom = (PassiveProductionRoom)_room;

            passiveProductionRoom.IntisaliseRoom();

            if (passiveProductionRoom.UpkeepType != null)
            {
                _upkeepImg.gameObject.SetActive(true);
                _upkeepImg.sprite = Icons.GetIcon((ResourcesEnum)passiveProductionRoom.UpkeepType);
                _upkeepCost.text = ((int)passiveProductionRoom.BaseUpkeepValue).ToString("+0;-#");
            }
            else
            {
                _upkeepImg.gameObject.SetActive(false);
                _upkeepCost.text = "";
            }
            if (passiveProductionRoom.OutPutType != null)
            {
                _outputImg.gameObject.SetActive(true);
                _outputImg.sprite = Icons.GetIcon((ResourcesEnum)passiveProductionRoom.OutPutType);
                _output.text = ((int)passiveProductionRoom.BaseOutputValue).ToString("+0;-#");
            }
            else
            {
                _outputImg.gameObject.SetActive(false);
                _output.text = "";
            }
        }
    }
}

#endregion
