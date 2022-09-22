using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds A list of all rooms and handles building new rooms (SingltonClass use Instance to acesse this class)
/// </summary>
public class RoomGridManager : MonoBehaviour
{

    public static RoomGridManager Instance;

    [SerializeField]
    private GameObject _buildRoomTemplate;
    private Grid roomGrid { get; set; }
    Dictionary<Vector3Int, Room> RoomList { get; set; } = new Dictionary<Vector3Int, Room>();
    Dictionary<Vector3Int, GameObject> BuildCellList { get; set; } = new Dictionary<Vector3Int, GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        roomGrid = GetComponent<Grid>();
        BuildNewRoom(Vector3Int.zero, RoomType.Fuel);
        BuildNewRoom(new Vector3Int(1,0,0), RoomType.Fuel);
    }

#region PublicMethods

    /// <summary>
    ///  Returns the room that is at the position provieded or null.
    /// </summary>
    /// <param name="position">the postion in world space</param>
    /// <returns></returns>
    public Room GetRoomAtPosition(Vector3 position)
    {
        Vector3Int cellPosition = roomGrid.WorldToCell(position);
        if (!RoomList.ContainsKey(cellPosition)) 
        {
            return null;
        }
        else
        {
            return RoomList[cellPosition];
        }
    }

    /// <summary>
    /// Build a new room at the given cell position
    /// </summary>
    /// <param name="cellPosition">cell position</param>
    /// <param name="roomType">type of room to build</param>
    /// <returns></returns>
    public Room BuildNewRoom(Vector3Int cellPosition,RoomType roomType)
    {
        if (RoomList.ContainsKey(cellPosition)) 
        { 
            return null; 
        }
        
        if(BuildCellList.ContainsKey(cellPosition))
        {
            Destroy(BuildCellList[cellPosition]);
            BuildCellList.Remove(cellPosition);
        }
        
        //Spawn the room Prefab and set up the room.
        GameObject newRoom = PrefabSpawner.Instance.SpawnRoom(roomType);
        newRoom.transform.parent = transform;
        Vector3 cellCenter = roomGrid.GetCellCenterWorld(cellPosition);
        newRoom.transform.position = cellCenter;
        Room newRoomScript = newRoom.GetComponent<Room>();
        newRoomScript.RoomPosition = cellPosition;
        RoomList.Add(cellPosition,newRoomScript);

        PopulateAdjacentBuildRoomCells(cellPosition);
        return newRoomScript;
    }

#endregion

#region PrivateMethods

    private void PopulateAdjacentBuildRoomCells(Vector3Int cellPosition)
    {
        Vector3Int[] adjacentCells = GetAdjacentGridCells(cellPosition);
        foreach(Vector3Int cell in adjacentCells)
        {
            if(!RoomList.ContainsKey(cell) && !BuildCellList.ContainsKey(cell))
            {
                GameObject buildTemplate = GameObject.Instantiate(_buildRoomTemplate, transform);
                buildTemplate.transform.position = roomGrid.GetCellCenterWorld(cell);
                BuildRoomButton buildRoomButton = buildTemplate.GetComponent<BuildRoomButton>();
                buildRoomButton.CellPos = cell;
                buildRoomButton.roomManager = this;
                BuildCellList.Add(cell, buildTemplate);
            }
        }
    }
    public Vector3Int[] GetAdjacentGridCells(Vector3Int cellPosition)
    {
        Vector3Int[] adjacentcells = new Vector3Int[4];
        adjacentcells[0] = cellPosition + Vector3Int.left;
        adjacentcells[1] = cellPosition + Vector3Int.right;
        adjacentcells[2] = cellPosition + Vector3Int.up;
        adjacentcells[3] = cellPosition + Vector3Int.down;

        return adjacentcells;           
    }

#endregion


}
