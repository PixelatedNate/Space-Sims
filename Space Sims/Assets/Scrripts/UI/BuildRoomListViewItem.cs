using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoomListViewItem : MonoBehaviour
{


    public Vector3Int RoomPosition { get; set; }

    [SerializeField]
    private AbstractRoom _room;
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

    public void SetRoom(AbstractRoom room)
    {
        this._room = room;
        UpdateItem();
    }

    public void OnClick()
    {
        AbstractRoom newRoom = RoomGridManager.Instance.BuildNewRoom(RoomPosition, _room.RoomType);
        UIManager.Instance.DeselectAll();
        newRoom.BuildRoom();
    }

    public void UpdateItem()
    {
        if (_room is PassiveProductionRoom)
        {
           PassiveProductionRoom  passiveProductionRoom = (PassiveProductionRoom)_room;

            passiveProductionRoom.IntisaliseRoom();
            _roomName.text = passiveProductionRoom.RoomName;
            _roomDiscription.text = passiveProductionRoom.RoomDiscription;

            if (passiveProductionRoom.UpkeepType != null)
            {
                _upkeepImg.gameObject.SetActive(true);
                _upkeepImg.sprite = Icons.GetIcon((ResourcesEnum)passiveProductionRoom.UpkeepType);
                _upkeepCost.text = ((int)passiveProductionRoom.UpkeepValue).ToString("+0;-#");
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
                _output.text = ((int)passiveProductionRoom.OutputValue).ToString("+0;-#");
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
