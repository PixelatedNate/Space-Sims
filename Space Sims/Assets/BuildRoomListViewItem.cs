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
    TextMeshProUGUI _roomName,_buildTimeText,_roomDiscription;
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
      //  backgroundImg.sprite = room.RoomImg;
        UpdateText();
    }

    private void UpdateText()
    {
        //_buildTimeText = room.RoomStat.ti
        _roomName.text = room.RoomName;
        _roomDiscription.text = room.RoomDiscription;
    }

}
