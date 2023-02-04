using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileMapHelper
{


    public enum TileName
    {
        EdgeRimUp,
        EdgeRimdown,
        EdgeRimLeft,
        EdgeRimRight,
        EdgeRimInnerRight,
        EdgeRimInnerRightDown,
        EdgeInnerLeft,
        EdgeInnerLeftDown,

        ConectingWallDownLeft,
        ConectingWallDownRight,
        ConectingWallUpLeft,
        ConectingWallUpRight
    }

    private const string DownMiddle = "ArtWork/Tiles/Ship Outtards - Down Middle";
    private const string UpMiddle = "ArtWork/Tiles/Ship Outtards - Top Middle";
    private const string LeftMiddle = "ArtWork/Tiles/Ship Outtards - Left Middle";
    private const string RightMiddle = "ArtWork/Tiles/Ship Outtards - Right Middle";

    private const string innerRight = "ArtWork/Tiles/Ship Outtards - Inner Right";
    private const string innerRightDown = "ArtWork/Tiles/Ship Outtards - Inner Right Down";
    private const string innerLeft = "ArtWork/Tiles/Ship Outtards - Inner Left";
    private const string innerLeftDown = "ArtWork/Tiles/Ship Outtards - Inner Left Down";



    private const string ConectingWallDownLeft = "ArtWork/Tilesets/Ship Parts A_5";
    private const string ConectingWallDownRight = "ArtWork/Tilesets/Ship Parts A_1";
    private const string ConectingWallUpLeft = "ArtWork/Tilesets/Ship Parts A_0";
    private const string ConectingWallUpRight = "ArtWork/Tilesets/Ship Parts A_8";



    public static TileBase GetTile(TileName tileEnum)
    {
        switch (tileEnum)
        {
            case TileName.EdgeRimdown: return Resources.Load<TileBase>(DownMiddle);
            case TileName.EdgeRimUp: return Resources.Load<TileBase>(UpMiddle);
            case TileName.EdgeRimLeft: return Resources.Load<TileBase>(LeftMiddle);
            case TileName.EdgeRimRight: return Resources.Load<TileBase>(RightMiddle);
            case TileName.EdgeRimInnerRight: return Resources.Load<TileBase>(innerRight);
            case TileName.EdgeInnerLeft: return Resources.Load<TileBase>(innerLeft);
            case TileName.EdgeRimInnerRightDown: return Resources.Load<TileBase>(innerRightDown);
            case TileName.EdgeInnerLeftDown: return Resources.Load<TileBase>(innerLeftDown);

            case TileName.ConectingWallDownLeft: return Resources.Load<TileBase>(ConectingWallDownLeft);
            case TileName.ConectingWallDownRight: return Resources.Load<TileBase>(ConectingWallDownRight);
            case TileName.ConectingWallUpLeft: return Resources.Load<TileBase>(ConectingWallUpLeft);
            case TileName.ConectingWallUpRight: return Resources.Load<TileBase>(ConectingWallUpRight);


            default: throw new Exception("Enum TileRPosition returned No corisponding value");
        }
    }

}
