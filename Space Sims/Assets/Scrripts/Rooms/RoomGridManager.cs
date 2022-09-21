using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGridManager : MonoBehaviour
{

    public static RoomGridManager Instance;
    private void Awake()
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


    [SerializeField]
    GameObject BuildRoomTemplate;


    Grid roomGrid;
    Dictionary<Vector3Int,Room> RoomList = new Dictionary<Vector3Int, Room>();
    Dictionary<Vector3Int,GameObject> BuildCellList = new Dictionary<Vector3Int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        roomGrid = GetComponent<Grid>();
        AddRoom(Vector3Int.zero, RoomType.Fuel);
        AddRoom(new Vector3Int(1,0,0), RoomType.Fuel);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public Room GetRoomAtPosition(Vector3 pos)
    {
        Vector3Int cellPos = roomGrid.WorldToCell(pos);
        if(!RoomList.ContainsKey(cellPos)) 
            return null;

        return RoomList[cellPos];
    }



    public Room AddRoom(Vector3Int cellPos,RoomType roomType)
    {
        if (RoomList.ContainsKey(cellPos)) { return null; }
       
        if(BuildCellList.ContainsKey(cellPos))
        {
            GameObject.Destroy(BuildCellList[cellPos]);
            BuildCellList.Remove(cellPos);
        }
        
        GameObject room = PrefabSpawner.Instance.SpawnRoom(roomType);
        room.transform.parent = transform;
        Vector3 CellCenter = roomGrid.GetCellCenterWorld(cellPos);
        room.transform.position = CellCenter;
        Room roomScript = room.GetComponent<Room>();
        roomScript.RoomPosition = cellPos;
        RoomList.Add(cellPos,roomScript);


        Vector3Int[] adjacentCells = GetAdjacentGridCells(cellPos);
        foreach(Vector3Int adjacentCell in adjacentCells)
        {
            if(!RoomList.ContainsKey(adjacentCell) && !BuildCellList.ContainsKey(adjacentCell))
            {
                GameObject buildTemplate = GameObject.Instantiate(BuildRoomTemplate, transform);
                buildTemplate.transform.position = roomGrid.GetCellCenterWorld(adjacentCell);
                BuildRoomButton buildRoomButton = buildTemplate.GetComponent<BuildRoomButton>();
                buildRoomButton.CellPos = adjacentCell;
                buildRoomButton.roomManager = this;
                BuildCellList.Add(adjacentCell, buildTemplate);
            }
        }
        return roomScript;

    }







    public Vector3Int[] GetAdjacentGridCells(Vector3Int cellPos)
    {
        Vector3Int[] adjacentcells = new Vector3Int[4];
        adjacentcells[0] = cellPos + Vector3Int.left;
        adjacentcells[1] = cellPos + Vector3Int.right;
        adjacentcells[2] = cellPos + Vector3Int.up;
        adjacentcells[3] = cellPos + Vector3Int.down;

        return adjacentcells;
            


    }



}
