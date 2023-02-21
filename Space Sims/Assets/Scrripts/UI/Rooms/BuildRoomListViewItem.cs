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
    GameObject LockedView, UnlockedView;

    [SerializeField]
    Image _buttonImg;

    void Start()
    {
        TimeTickSystem.OnTick += OnTick;
        UpdateItem();
    }


    private void OnTick(object source, EventArgs e)
    {
        Color c = _buttonImg.color;
        if (GlobalStats.Instance.PlayerResources >= getCost())
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
        if (!_room.IsUnlocked)
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Locked Room", Color.red);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Error);
            return;
        }
        if (GlobalStats.Instance.PlayerResources >= getCost())
        {
            GlobalStats.Instance.PlayerResources -= getCost();
            AbstractRoom newRoom = RoomGridManager.Instance.BuildNewRoom(RoomPosition, _room.RoomType);
            UIManager.Instance.DeselectAll();
            newRoom.BuildRoom();
            newRoom.IntisaliseRoom();
            RoomGridManager.Instance.SetBuildMode(false);
        }
        else
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Incerficent Resorces", Color.red);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Error);
            return;
        }
    }

    private GameResources getCost()
    {
        float cost = _room.RoomStat.BuildCost.Minerals + (_room.RoomStat.BuildCost.Minerals * _room.RoomCostModifyer * GlobalStats.Instance.GetAllPlyerRoomsOfType(_room.RoomType).Length);
        int intcost = Mathf.FloorToInt(cost);
        return new GameResources(ResourcesEnum.Minerals, intcost);
    }

    public void SetRoomLocked()
    {

    }

    public void UpdateItem()
    {
        if (!_room.IsUnlocked)
        {
            UnlockedView.SetActive(false);
            LockedView.SetActive(true);
            SetRoomLocked();
            return;
        }
        UnlockedView.SetActive(true);
        LockedView.SetActive(false);
        _roomName.text = _room.RoomName;
        _roomDiscription.text = _room.RoomDiscription;
        _buildCost.text = getCost().Minerals.ToString();

        TimeSpan buildTime = new TimeSpan(0, (int)_room.RoomStat.BuildTime, 0);
        _buildTimeText.text = buildTime.ToString();
        if (_room is PassiveProductionRoom)
        {
            PassiveProductionRoom passiveProductionRoom = (PassiveProductionRoom)_room;

            passiveProductionRoom.IntisaliseRoom();

            if (passiveProductionRoom.UpkeepType != null)
            {
                _upkeepImg.gameObject.SetActive(true);
                _upkeepImg.sprite = Icons.GetResourceIcon((ResourcesEnum)passiveProductionRoom.UpkeepType);
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
                _outputImg.sprite = Icons.GetResourceIcon((ResourcesEnum)passiveProductionRoom.OutPutType);
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
