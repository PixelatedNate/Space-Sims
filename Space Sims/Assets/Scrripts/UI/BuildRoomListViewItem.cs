using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoomListViewItem : MonoBehaviour
{


    public Vector3Int RoomPosition { get; set; }

    [SerializeField]
    private Room _room;
    [SerializeField]
    private TextMeshProUGUI _roomName,_buildTimeText,_roomDiscription,_buildCost,_upkeepCost,_output;
    [SerializeField]
    private Image _upkeepImg, _outputImg, _buildCostImg;
    [SerializeField]
    private Image _backgroundImg;

    void Start()
    {
        UpdateItem();
    }


#region PublicMethods

    public void SetRoom(Room room)
    {
        this._room = room;
        UpdateItem();
    }

    public void OnClick()
    {
        Room newRoom = RoomGridManager.Instance.BuildNewRoom(RoomPosition, _room.RoomType);
        UIManager.Instance.DeselectAll();
        newRoom.BuildRoom();
    }

    public void UpdateItem()
    {
        _room.IntisaliseRoom();
        _roomName.text = _room.RoomName;
        _roomDiscription.text = _room.RoomDiscription;

        if (_room.UpkeepType != null)
        {
            _upkeepImg.gameObject.SetActive(true);
            _upkeepImg.sprite = Icons.GetIcon((ResourcesEnum)_room.UpkeepType);
            _upkeepCost.text = ((int)_room.UpkeepValue).ToString("+0;-#");
        }
        else
        {
            _upkeepImg.gameObject.SetActive(false);
            _upkeepCost.text = "";
        }
        if (_room.OutPutType != null)
        {
            _outputImg.gameObject.SetActive(true);
            _outputImg.sprite = Icons.GetIcon((ResourcesEnum)_room.OutPutType);
            _output.text = ((int)_room.OutputValue).ToString("+0;-#");
        }
        else
        {
            _outputImg.gameObject.SetActive(false);
            _output.text = "";
        }
    }
}

#endregion
