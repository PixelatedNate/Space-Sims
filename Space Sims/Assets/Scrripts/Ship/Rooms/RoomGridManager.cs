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

    private List<GameObject> ExteralShipParts = new List<GameObject>();

    public Vector3 roomGridSize { get { return roomGrid.cellSize; } }

    Dictionary<Vector3Int, AbstractRoom> RoomList { get; set; } = new Dictionary<Vector3Int, AbstractRoom>();

    Dictionary<Vector3Int, GameObject> BuildCellList { get; set; } = new Dictionary<Vector3Int, GameObject>();

    private bool ShowBuildRoom { get; set; } = false;


    [SerializeField]
    GameObject EnginePrefabe;
    [SerializeField]
    GameObject CockPit;
    [SerializeField]
    GameObject UpWing;
    [SerializeField]
    GameObject DownWing;


    [SerializeField]
    PersonTemplate[] StartingPeople;

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
        AbstractRoom fistRoom = BuildNewRoom(Vector3Int.zero, RoomType.CrewQuaters);
        AbstractRoom secondRoom = BuildNewRoom(new Vector3Int(1, 0, 0), RoomType.Fuel);
        AbstractRoom ThirdRoom = BuildNewRoom(new Vector3Int(0, 1, 0), RoomType.QuestRoom);
        AbstractRoom forthRoom = BuildNewRoom(new Vector3Int(1, -1, 0), RoomType.Food);
        AbstractRoom fithRoom = BuildNewRoom(new Vector3Int(0, -1, 0), RoomType.Minerals);


        foreach (PersonTemplate personTemplate in StartingPeople)
        {
            PrefabSpawner.Instance.SpawnPerson(fistRoom, personTemplate);
        }



        //PrefabSpawner.Instance.SpawnPerson(fistRoom);
        //PrefabSpawner.Instance.SpawnPerson(fistRoom);
        //PrefabSpawner.Instance.SpawnPerson(fistRoom);
        //PrefabSpawner.Instance.SpawnPerson(fistRoom);
    }

    #region PublicMethods

    /// <summary>
    ///  Returns the room that is at the position provieded or null.
    /// </summary>
    /// <param name="position">the postion in world space</param>
    /// <returns></returns>
    public AbstractRoom GetRoomAtPosition(Vector3 position)
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

    public AbstractRoom GetRoomAtPosition(Vector3Int position)
    {
        if (!RoomList.ContainsKey(position))
        {
            return null;
        }
        else
        {
            return RoomList[position];
        }
    }



    /// <summary>
    ///  will enbale or disable the build mode and return the new state
    /// </summary>
    /// <returns> bool the new stat of BuildMode </returns>
    public bool TogleBuildMode()
    {
        SetBuildMode(!ShowBuildRoom);
        return ShowBuildRoom;
    }

    public void SetBuildMode(bool mode)
    {
        if (mode == ShowBuildRoom) { return; }
        foreach (var buildroom in BuildCellList)
        {
            buildroom.Value.SetActive(mode);
        }
        ShowBuildRoom = mode;
    }


    /// <summary>
    /// Build a new room at the given cell position
    /// </summary>
    /// <param name="cellPosition">cell position</param>
    /// <param name="roomType">type of room to build</param>
    /// <returns></returns>
    public AbstractRoom BuildNewRoom(Vector3Int cellPosition, RoomType roomType)
    {

        // DOSN'T NOT SUPORT BUILD NEW ROOMS NON STANDERED SIZE DUE TO POSILBE OVERIDING EXSISTING ROOMS unless checked/sure manualy, i.e at frist spawn

        if (RoomList.ContainsKey(cellPosition))
        {
            return null;
        }

        //Spawn the room Prefab and set up the room.
        GameObject newRoom = PrefabSpawner.Instance.SpawnRoom(roomType);
        newRoom.transform.parent = transform;
        AbstractRoom newRoomScript = newRoom.GetComponent<AbstractRoom>();
        Vector3 cellCenter = roomGrid.GetCellCenterWorld(cellPosition);
        Vector3 newPos = cellCenter;
        newRoom.transform.position = newPos;
        newRoomScript.RoomPosition = cellPosition;

        AddRoomToPos(cellPosition, newRoomScript);
        for (int x = 1; x < newRoomScript.Size.x; x++)
        {
            AddRoomToPos(cellPosition + new Vector3Int(x, 0, 0), newRoomScript);
        }
        for (int y = 1; y < newRoomScript.Size.y; y++)
        {
            AddRoomToPos(cellPosition + new Vector3Int(0, y, 0), newRoomScript);
        }
        UpdateEdgeTiles();
        return newRoomScript;
    }

    #endregion

    #region PrivateMethods


    private void AddRoomToPos(Vector3Int positon, AbstractRoom room)
    {
        if (BuildCellList.ContainsKey(positon))
        {
            Destroy(BuildCellList[positon]);
            BuildCellList.Remove(positon);
        }
        RoomList.Add(positon, room);
        PopulateAdjacentBuildRoomCells(positon);
        UpdateTileMapsForAdjacntRooms(positon);
    }

    private void UpdateTileMapsForAdjacntRooms(Vector3Int cellPosition)
    {
        Vector3Int[] adjacentCells = GetAdjacentGridCells(cellPosition);
        foreach (Vector3Int cell in adjacentCells)
        {
            if (RoomList.ContainsKey(cell))

            {
                if (RoomList[cell] != RoomList[cellPosition])
                {
                    RoomList[cell].EnableRoomConection(cellPosition); // update adjecnt room
                    RoomList[cellPosition].EnableRoomConection(cell);  //  update target room
                }
            }
        }
    }

    private void PopulateAdjacentBuildRoomCells(Vector3Int cellPosition)
    {
        Vector3Int[] adjacentCells = GetAdjacentGridCells(cellPosition);
        foreach (Vector3Int cell in adjacentCells)
        {
            if (!RoomList.ContainsKey(cell) && !BuildCellList.ContainsKey(cell))
            {
                GameObject buildTemplate = GameObject.Instantiate(_buildRoomTemplate, transform);
                buildTemplate.transform.position = roomGrid.GetCellCenterWorld(cell);
                BuildRoomButton buildRoomButton = buildTemplate.GetComponent<BuildRoomButton>();
                buildRoomButton.CellPos = cell;
                buildRoomButton.roomManager = this;
                BuildCellList.Add(cell, buildTemplate);
                buildTemplate.SetActive(ShowBuildRoom);
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

    private void UpdateEdgeTiles()
    {
        RemoveAllExternalParts();
        foreach (var room in RoomList)
        {
            room.Value.UpdateEdgeTiles();
            if (GetRoomAtPosition(room.Key + Vector3Int.left) == null)
            {
                Vector3 SpawnPoint = roomGrid.GetCellCenterWorld(room.Key + Vector3Int.left);
                var engineGO = GameObject.Instantiate(EnginePrefabe, SpawnPoint, Quaternion.identity);
                ExteralShipParts.Add(engineGO);
            }
            if (GetRoomAtPosition(room.Key + Vector3Int.right) == null
                && GetRoomAtPosition(room.Key + (Vector3Int.right * 2)) == null)
            {
                Vector3 SpawnPoint = roomGrid.GetCellCenterWorld(room.Key + Vector3Int.right);
                var cockPitGO = GameObject.Instantiate(CockPit, SpawnPoint, Quaternion.identity);
                ExteralShipParts.Add(cockPitGO);
            }
            if (GetRoomAtPosition(room.Key + Vector3Int.up) == null
                && GetRoomAtPosition(room.Key + Vector3Int.up + Vector3Int.left) == null
                && GetRoomAtPosition(room.Key + Vector3Int.up + Vector3Int.right) == null)
            {
                Vector3 SpawnPoint = roomGrid.GetCellCenterWorld(room.Key + Vector3Int.up);
                var upWing = GameObject.Instantiate(UpWing, SpawnPoint, Quaternion.identity);
                ExteralShipParts.Add(upWing);
            }
            if (GetRoomAtPosition(room.Key + Vector3Int.down) == null
                   && GetRoomAtPosition(room.Key + Vector3Int.down + Vector3Int.right) == null
                   && GetRoomAtPosition(room.Key + Vector3Int.down + Vector3Int.left) == null)
            {
                Vector3 SpawnPoint = roomGrid.GetCellCenterWorld(room.Key + Vector3Int.down);
                var downWing = GameObject.Instantiate(DownWing, SpawnPoint, Quaternion.identity);
                ExteralShipParts.Add(downWing);
            }

        }
    }

    private void RemoveAllExternalParts()
    {
        foreach (GameObject part in ExteralShipParts)
        {
            Destroy(part);
        }
        ExteralShipParts.Clear();
    }


    #endregion


}
