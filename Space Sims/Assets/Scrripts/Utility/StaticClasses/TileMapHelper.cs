using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileMapHelper
{



    public enum TilePosition
    {
        Up,
        down,
        left,
        right,
        innerRight,
        innerRightDown,
        innerLeft,
        innerLeftDown
    }

    private const string DownMiddle = "ArtWork/Tiles/Ship Outtards - Down Middle";
    private const string UpMiddle = "ArtWork/Tiles/Ship Outtards - Top Middle";
    private const string LeftMiddle = "ArtWork/Tiles/Ship Outtards - Left Middle";
    private const string RightMiddle = "ArtWork/Tiles/Ship Outtards - Right Middle";

    private const string innerRight = "ArtWork/Tiles/Ship Outtards - Inner Right";
    private const string innerRightDown = "ArtWork/Tiles/Ship Outtards - Inner Right Down";
    private const string innerLeft = "ArtWork/Tiles/Ship Outtards - Inner Left";
    private const string innerLeftDown = "ArtWork/Tiles/Ship Outtards - Inner Left Down";

    public static TileBase GetTile(TilePosition tileEnum)
    {
        switch (tileEnum)
        {
            case TilePosition.down: return Resources.Load<TileBase>(DownMiddle);
            case TilePosition.Up: return   Resources.Load<TileBase>(UpMiddle);
            case TilePosition.left: return Resources.Load<TileBase>(LeftMiddle);
            case TilePosition.right: return Resources.Load<TileBase>(RightMiddle);
            case TilePosition.innerRight: return Resources.Load<TileBase>(innerRight);
            case TilePosition.innerLeft: return Resources.Load<TileBase>(innerLeft);
            case TilePosition.innerRightDown: return Resources.Load<TileBase>(innerRightDown);
            case TilePosition.innerLeftDown: return Resources.Load<TileBase>(innerLeftDown);
         default: throw new Exception("Enum TileRPosition returned No corisponding value");
        }
    }
    
}
