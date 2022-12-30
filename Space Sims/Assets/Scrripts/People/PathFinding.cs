using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;




// make it run on anothe thread to increase performeace in futer

public class PathFinding

{
    private class Node<T>
    {
        public bool Visted { get; set; }
        public T PreviousPosition { get; set; }
        public float H { get; set; }
        public float Score { get; set; }
    }

    public static LinkedList<Vector3Int> CalculateInternalRoomPath(Tilemap tileMap, Vector3Int start, Vector3Int target)
    {
        Dictionary<Vector3Int, Node<Vector3Int>> graph = new Dictionary<Vector3Int, Node<Vector3Int>>();

        Node<Vector3Int> rootNode = new Node<Vector3Int>() { Visted = false, H = 0, Score = 0 };

        graph.Add(start, rootNode);

        while (true)
        {
            Vector3Int currentPosition;
            try
            {
                currentPosition = GetNodeKeyWithLowestScore(graph);
            }
            catch
            {
                Debug.LogWarning("No path was found when parth finding Target was: " + target);
                return null;
            }          
            graph[currentPosition].Visted = true;
            foreach (Vector3Int adjacentTile in GetAdjacentTiles(currentPosition, tileMap))
            {
                if (!graph.ContainsKey(adjacentTile))
                {
                    float newScore = 1; // can be modifyed for account for move speed such as walking on mud or rood e.t.c (not really relevent in our game so all tiles are equal)
                    float hValue = Mathf.Abs(Vector3Int.Distance(adjacentTile, target));
                    graph.Add(adjacentTile, new Node<Vector3Int>() { Visted = false, PreviousPosition = currentPosition, H = hValue, Score = newScore }); 
                }

                if (currentPosition == target)
                {
                    return BuildPath(currentPosition, start, graph);
                }
            }
        }
    }




    public static Dictionary<AbstractRoom, LinkedList<Vector3Int>> CalculateRoomPath(Vector3Int startPositionInFirstRoom, AbstractRoom startRoom, AbstractRoom targetRoom)
    {
        Dictionary<AbstractRoom, Node<AbstractRoom>> graph = new Dictionary<AbstractRoom, Node<AbstractRoom>>();
        Node<AbstractRoom> rootNode = new Node<AbstractRoom>() { Visted = false, H = 0, Score = 0 };

        graph.Add(startRoom, rootNode);

        while (true)
        {
            AbstractRoom currentPosition;
            try
            {
                currentPosition = GetNodeKeyWithLowestScore(graph);
            }
            catch
            {
                Debug.LogWarning("No path was found when parth finding Target was: " + targetRoom);
                return null;
            }

            graph[currentPosition].Visted = true;
            foreach (AbstractRoom adjacentRoom in GetAdjacentRooms(currentPosition))
            {
                if (!graph.ContainsKey(adjacentRoom))
                {
                    float newScore = 1; // can be modifyed for account for move speed such as walking on mud or rood e.t.c (not really relevent in our game so all rooms are equal)
                    float hValue = Mathf.Abs(Vector3Int.Distance(adjacentRoom.RoomPosition, targetRoom.RoomPosition));
                    graph.Add(adjacentRoom, new Node<AbstractRoom>() { Visted = false, PreviousPosition = currentPosition, H = hValue, Score = newScore });
                }

                if (currentPosition == targetRoom)
                {

                    Dictionary<AbstractRoom, LinkedList<Vector3Int>> path = new Dictionary<AbstractRoom, LinkedList<Vector3Int>>();

                    LinkedList<AbstractRoom> roomPath = BuildPath(currentPosition, startRoom, graph);
                    AbstractRoom[] roomPathArray = roomPath.ToArray();
                    
                    Direction  SecondRoomDirection = GetRoomDirection(roomPathArray[0],roomPathArray[1]);
                    Vector3Int FirstRoomEndPosition = roomPathArray[1].GetConectorTile(SecondRoomDirection);

                    LinkedList<Vector3Int> firstRoomInternalPath = CalculateInternalRoomPath(roomPathArray[0].PathFindingTileMap, startPositionInFirstRoom,FirstRoomEndPosition);
                    path.Add(roomPathArray[0], firstRoomInternalPath);


                    for(int i = 1; i < roomPathArray.Length-1; i++)
                    {
                        Direction  previousRoomDirection = GetRoomDirection(roomPathArray[i],roomPathArray[i-1]);
                        Vector3Int StartPosition = roomPathArray[i].GetConectorTile(previousRoomDirection);
                        
                        Direction  nextRoomDirection = GetRoomDirection(roomPathArray[i],roomPathArray[i+1]);
                        Vector3Int endPosition = roomPathArray[i].GetConectorTile(nextRoomDirection);

                        LinkedList<Vector3Int> internalPath = CalculateInternalRoomPath(roomPathArray[i].PathFindingTileMap, StartPosition, endPosition);
                        path.Add(roomPathArray[i], internalPath);
                    }

                    int lastRoomIndex = roomPathArray.Length - 1;
                    Direction LastRoompreviousDirection = GetRoomDirection(roomPathArray[lastRoomIndex], roomPathArray[lastRoomIndex - 1]);
                    Vector3Int LastRoomStartPosition = roomPathArray[lastRoomIndex].GetConectorTile(SecondRoomDirection);
                    Vector3Int LastRoomCenterTile = roomPathArray[lastRoomIndex].GetCenterTile();
                    LinkedList<Vector3Int> LastRoomInternalPath = CalculateInternalRoomPath(roomPathArray[0].PathFindingTileMap,LastRoomStartPosition,LastRoomCenterTile);
                    path.Add(roomPathArray[lastRoomIndex], LastRoomInternalPath);

                    return path;
                }
            }
        }
    }
          
    private static Direction GetRoomDirection(AbstractRoom firstRoom, AbstractRoom secondRoom)
    {

        Vector3Int deltaPos = secondRoom.RoomPosition - firstRoom.RoomPosition;
        if (deltaPos.y == 1)
        {
            return Direction.Up;
        }
        if (deltaPos.y == -1)
        {
            return Direction.Down;
        }
        if (deltaPos.x == 1)
        {
            return Direction.Right;
        }
        if (deltaPos.x == -1)
        {
            return Direction.Left;
        }
        throw new System.Exception("Can't find direction for rooms make sure the are next to each other");
    }

    public static Vector3Int GetStartingTileForRoomJump()
    {
        return Vector3Int.up;
    }

    private static LinkedList<T> BuildPath<T>(T currentPos, T startPosition, Dictionary<T, Node<T>> graph)
    {
        LinkedList<T> path = new LinkedList<T>();
        while (!currentPos.Equals(startPosition))
        {
            path.AddFirst(currentPos);
            currentPos = graph[currentPos].PreviousPosition;
        }
        return path;
    }

    private static AbstractRoom[] GetAdjacentRooms(AbstractRoom room)
    {
        List<AbstractRoom> adjacentRooms = new List<AbstractRoom>();
        AbstractRoom adjcentRoomLeft = RoomGridManager.Instance.GetRoomAtPosition(room.RoomPosition + Vector3Int.left);
        if(adjcentRoomLeft != null)
        {
            adjacentRooms.Add(adjcentRoomLeft);
        }
        AbstractRoom adjcentRoomRight = RoomGridManager.Instance.GetRoomAtPosition(room.RoomPosition + Vector3Int.right);
        if(adjcentRoomRight != null)
        {
            adjacentRooms.Add(adjcentRoomRight);
        }
        AbstractRoom adjcentRoomUp = RoomGridManager.Instance.GetRoomAtPosition(room.RoomPosition + Vector3Int.up);
        if(adjcentRoomUp != null)
        {
            adjacentRooms.Add(adjcentRoomUp);
        }
        AbstractRoom adjcentRoomDown = RoomGridManager.Instance.GetRoomAtPosition(room.RoomPosition + Vector3Int.down);
        if(adjcentRoomDown != null)
        {
            adjacentRooms.Add(adjcentRoomDown);
        }

        return adjacentRooms.ToArray();

    }

    

    private static Vector3Int[] GetAdjacentTiles(Vector3Int pos, Tilemap tilemap)
    {
        List<Vector3Int> adjacentTiles = new List<Vector3Int>();

        if (tilemap.GetTile(pos + Vector3Int.left) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.left);
        }
        if (tilemap.GetTile(pos + Vector3Int.right) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.right);
        }
        if (tilemap.GetTile(pos + Vector3Int.up) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.up);
        }
        if (tilemap.GetTile(pos + Vector3Int.down) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.down);
        }

        //Diagonals
        if (tilemap.GetTile(pos + Vector3Int.left + Vector3Int.up) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.left + Vector3Int.up);
        }
        if (tilemap.GetTile(pos + Vector3Int.left + Vector3Int.down) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.down);
        }
        if (tilemap.GetTile(pos + Vector3Int.right + Vector3Int.up) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.right + Vector3Int.up);
        }
        if (tilemap.GetTile(pos + Vector3Int.right + Vector3Int.down) != null)
        {
            adjacentTiles.Add(pos + Vector3Int.right + Vector3Int.down);
        }

        return adjacentTiles.ToArray();

    }



    private static T GetNodeKeyWithLowestScore<T>(Dictionary<T,Node<T>> graph) 
    {
        bool found = false;
        T lowestNode = graph.Keys.First();
        float MinH = float.MaxValue;
        foreach (var node in graph)
        {
            if (node.Value.H < MinH)
            {
                if (!node.Value.Visted)
                {
                    MinH = node.Value.H;
                    lowestNode = node.Key;
                    found = true;
                }
            }
        }
        if(!found)
        {
            throw new System.Exception();   
        }
        return lowestNode;
    }
}
