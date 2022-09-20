using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoomListViewItem : MonoBehaviour
{


    public Vector3Int roomPos { get; set; }

    [SerializeField]
    Room room;
    [SerializeField]
    TextMeshProUGUI _roomName,_buildTimeText,_roomDiscription,_buildCost,_upkeepCost,_outPut;
    [SerializeField]
    Image _upkeepImg, _outputImg, _buildCostImg;
    [SerializeField]
    Image backgroundImg;

    void Start()
    {
        UpdateItem();
    }

    public void SetRoom(Room room)
    {
        this.room = room;
        UpdateItem();
    }

    public void onClick()
    {
        RoomGridManager.Instance.AddRoom(roomPos, room.RoomType);
        UIManager.Instance.DeselectAll();
    }

    public void UpdateItem()
    {
        room.IntisaliseRoom();
        _roomName.text = room.RoomName;
        _roomDiscription.text = room.RoomDiscription;

        if (room.UpkeepType != null)
        {
            _upkeepImg.gameObject.SetActive(true);
            _upkeepImg.sprite = Icons.GetIcon(room.UpkeepType);
            _upkeepCost.text = ((int)room.UpkeepValue).ToString("+0;-#");
        }
        else
        {
            _upkeepImg.gameObject.SetActive(false);
            _upkeepCost.text = "";
        }
        if (room.OutPutType != null)
        {
            _outputImg.gameObject.SetActive(true);
            _outputImg.sprite = Icons.GetIcon(room.OutPutType);
            _outPut.text = ((int)room.OutputValue).ToString("+0;-#");
        }
        else
        {
            _outputImg.gameObject.SetActive(false);
            _outPut.text = "";
        }
    }
}
