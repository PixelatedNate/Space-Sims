using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomButton : MonoBehaviour
{

    public Vector3Int cellPos;
    public RoomGridManager roomManager;
    
    public void OnClick()
    {
        roomManager.AddRoom(cellPos,RoomType.Room1);
    }
    
}
