using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGridManager : MonoBehaviour
{


    [SerializeField]
    GameObject BuildRoomTemplate;


    Grid roomGrid;
    Dictionary<Vector3Int,Room> RoomList = new Dictionary<Vector3Int, Room>();
    Dictionary<Vector3Int,GameObject> BuildCellList = new Dictionary<Vector3Int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        roomGrid = GetComponent<Grid>();
        AddRoom(Vector3Int.zero, RoomType.Room1);
        AddRoom(new Vector3Int(1,0,0), RoomType.Room1);
        AddRoom(Vector3Int.zero, RoomType.Room1);
        //PrefabSpawner.Instance.
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddRoom(Vector3Int cellPos,RoomType roomType)
    {
        if (RoomList.ContainsKey(cellPos)) { return; }
       
        if(BuildCellList.ContainsKey(cellPos))
        {
            GameObject.Destroy(BuildCellList[cellPos]);
            BuildCellList.Remove(cellPos);
        }
        
        GameObject room = PrefabSpawner.Instance.SpawnRoom(roomType);
        room.transform.parent = transform;
        Vector3 CellCenter = roomGrid.CellToLocal(cellPos);
        room.transform.position = CellCenter;
        RoomList.Add(cellPos,room.GetComponent<Room>());

        Vector3Int[] adjacentCells = GetAdjacentGridCells(cellPos);
        foreach(Vector3Int adjacentCell in adjacentCells)
        {
            if(!RoomList.ContainsKey(adjacentCell) && !BuildCellList.ContainsKey(adjacentCell))
            {
                GameObject buildTemplate = GameObject.Instantiate(BuildRoomTemplate, transform);
                buildTemplate.transform.position = roomGrid.CellToLocal(adjacentCell);
                BuildRoomButton buildRoomButton = buildTemplate.GetComponent<BuildRoomButton>();
                buildRoomButton.cellPos = adjacentCell;
                buildRoomButton.roomManager = this;
                BuildCellList.Add(adjacentCell, buildTemplate);
            }
        }

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
