using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




// make it run on anothe thread to increase performeace in futer

public class PathFinding

{
    private class Node
    {
        public bool Visted { get; set; }
        public Vector3Int PreviousPoint { get; set; }
        public float H { get; set; }
        public float Score { get; set; }

    }

    public static LinkedList<Vector3Int> CalculatePath(Tilemap tileMap, Vector3Int start, Vector3Int target)
    {


        Dictionary<Vector3Int, Node> graph = new Dictionary<Vector3Int, Node>();

        Node rootNode = new Node() { Visted = false, H = 0, Score = 0 };

        graph.Add(start, rootNode);

        while (true)
        {
            Vector3Int? currentPositionNullable = GetNodeKeyWithLowestScore(graph);
            if (GetNodeKeyWithLowestScore(graph) == null)
            {
                Debug.LogWarning("No path was found when parth finding Target was: " + target);
                return null;
            }
            Vector3Int currentPosition = (Vector3Int)currentPositionNullable;

            graph[currentPosition].Visted = true;
            foreach (Vector3Int adjacentTile in GetAdjacentTiles(currentPosition, tileMap))
            {
                if (!graph.ContainsKey(adjacentTile))
                {
                    float newScore = 1; // can be modifyed for account for move speed such as walking on mud or rood e.t.c (not really relevent in our game so all tiles are equal)
                    float hValue = Mathf.Abs(Vector3Int.Distance(adjacentTile, target));
                    graph.Add(adjacentTile, new Node() { Visted = false, PreviousPoint = currentPosition, H = hValue, Score = newScore }); 
                }

                if (currentPosition == target)
                {
                    return BuildPath(currentPosition, start, graph);
                }
            }
        }
    }

    private static LinkedList<Vector3Int> BuildPath(Vector3Int currentPos, Vector3Int startPosition, Dictionary<Vector3Int, Node> graph)
    {
        LinkedList<Vector3Int> path = new LinkedList<Vector3Int>();
        while (currentPos != startPosition)
        {
            path.AddFirst(currentPos);
            currentPos = graph[currentPos].PreviousPoint;
        }
        return path;
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



    private static Vector3Int? GetNodeKeyWithLowestScore(Dictionary<Vector3Int, Node> graph)
    {
        Vector3Int? lowestNode = null;
        float MinH = float.MaxValue;
        foreach (var node in graph)
        {
            if (node.Value.H < MinH)
            {
                if (!node.Value.Visted)
                {
                    MinH = node.Value.H;
                    lowestNode = node.Key;
                }
            }
        }
        return lowestNode;
    }
}
