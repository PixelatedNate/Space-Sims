using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RDG;

public abstract class AbstractRoom : MonoBehaviour, IInteractables
{

    [SerializeField]
    private Vector2Int _size = new Vector2Int(1,1);

    public Vector2Int Size { get { return _size; } }

    [SerializeField]
    protected GameObject _roomLight, _overlay;
    
    [SerializeField]
    private GameObject _tempSelect, _underConstructionBanner;

    [SerializeField]
    private RoomStats[] _roomlevels;

    [SerializeField]
    private string _roomName;
    public string RoomName { get { return _roomName; } }

    [SerializeField, TextArea(10, 10)]
    private string _roomDiscription;
    public string RoomDiscription { get { return _roomDiscription; } }
    public Vector3Int RoomPosition { get; set; }

    [SerializeField]
    private RoomType _roomType;
    public RoomType RoomType { get { return _roomType; } }
    private int Level { get; set; } = 0;
    public RoomStats RoomStat { get { return _roomlevels[Level]; } }
    public bool IsUnderConstruction { get; private set; }
    public TimeDelayManager.Timer ConstructionTimer { get; private set; }
    public List<PersonInfo> Workers { get; private set; } = new List<PersonInfo>();

    [SerializeField]
    private Camera _roomCameraPortal;

    [SerializeField]
    private ParticleSystem _buildRoomParticalEffect;

    [SerializeField]
    private Tilemap _floorTileMap;
    [SerializeField]
    private Tilemap _WallTileMap;
    [SerializeField]
    private Tilemap _Shipedges;
    public Tilemap PathFindingTileMap { get { return _floorTileMap; } }

    protected bool isRoomActive { get; set; } = false;


    private TimeDelayManager.Timer _buildTimer;
    

    [SerializeField]
    private TileBase ConectingTile;

    #region PublicMethods

    public void BuildRoom()
    {
        if (IsUnderConstruction || Level != 0)
        {
            throw new Exception("Trying to build a new room which allready exsistes");
        }
        _buildRoomParticalEffect.Play();
        SoundManager.Instance.PlaySound(SoundManager.Sound.RoomPlaced);
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        BuildOrUpgradeRoom(0);
    }

    public void UpgradeRoom()
    {
        if (IsUnderConstruction || Level == _roomlevels.Length - 1)
        {
            throw new Exception("Trying to uprage a room which is max level or allready under construction");
        }

        if (_roomlevels[Level + 1].BuildCost < GlobalStats.Instance.PlayerResources)
        {
            GlobalStats.Instance.PlayerResources -= _roomlevels[Level + 1].BuildCost;
            BuildOrUpgradeRoom(Level++);
            UIManager.Instance.DisplayRoomView(this);
            //SoundManager.Instance.PlaySound(SoundManager.Sound.);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Error);
        }
    }
    public abstract void IntisaliseRoom();

    public virtual bool AddWorker(Person person)
    {
        if (Workers.Count == RoomStat.MaxWorkers)
        {
            return false;
        }
        if (Workers.Contains(person.PersonInfo))
        {
            Debug.LogWarning("Failed to add a person to room: as they are allready in");
            return false;
        }
        else
        {
            Workers.Add(person.PersonInfo);
            if (!IsUnderConstruction)
            {
                setRoomActive(true);
            }
            if(IsUnderConstruction && ConstructionTimer.IsPause)
            {
                ConstructionTimer.RestartTimer();
            }
            UpdateRoomStats();
            return true;
        }
    }

    public void RemoveWorker(Person person)
    {

        if (Workers.Count == 0 || !Workers.Contains(person.PersonInfo))
        {
            Debug.LogWarning("Failed to reomve a person from room: as they are not present in");
        }
        else
        {
            Workers.Remove(person.PersonInfo);
            UpdateRoomStats();
            if (Workers.Count == 0)
            {
                setRoomActive(false);
                if(IsUnderConstruction)
                {
                    ConstructionTimer.PauseTimer();
                }
            }
        }

    }

    public void SetCamera(RenderTexture renderTexture)
    {
        _roomCameraPortal.GetComponent<Camera>().gameObject.SetActive(true);
        _roomCameraPortal.GetComponent<Camera>().targetTexture = renderTexture;
    }

    public void setRoomActive(bool active)
    {
        if (active == isRoomActive) { return; }
        _roomLight.SetActive(active);
        isRoomActive = active;
        UpdateRoomStats();
    }

    public void SkipRoom()
    {
        UIManager.Instance.Conformation(AbleToSkipRoom,"are you sure you want to spend x in order to instatly build this room");
    }

    public void EnableRoomConection(Vector3Int AdjacentPos)
    {

        Vector3Int delataPos = AdjacentPos - RoomPosition;
        
        if(Size != new Vector2Int(1,1))
        {
            for(int x = 0; x < Size.x; x++)
            {
                delataPos = AdjacentPos - RoomPosition - new Vector3Int(x, 0, 0);
                if(MathF.Abs(delataPos.magnitude) == 1)
                {
                    SetRoomTiles(delataPos, x, 0);
                    return;
                }
            }
            for(int y = 0; y < Size.y; y++)
            {
                delataPos = AdjacentPos - RoomPosition - new Vector3Int(0, y, 0);
                if(MathF.Abs(delataPos.magnitude) == 1)
                {
                    SetRoomTiles(delataPos, 0 ,y);
                    return;
                }
            }

        }
        if (Size == new Vector2Int(1, 1))
        {
            SetRoomTiles(delataPos);
        }
    }


    #endregion

    #region PrivateMethods


    private void SetRoomTiles(Vector3 deltaPos , int xOfset = 0, int yOfset = 0)
    {
        // have to offest as tilemaps are not starting 0,0 bottom left (I know it sucks but it sucks even more to fix it)
        Vector3Int offset = new Vector3Int(4, -5, 0);
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        int halfYValue = Mathf.CeilToInt(gridSize.y / 2);
        int halfXValue = Mathf.CeilToInt(gridSize.x / 2);

        Vector3Int tilePosToChange = Vector3Int.zero;

        if (deltaPos.y == 1)
        {
            tilePosToChange = new Vector3Int(halfXValue, (int)gridSize.y, 0);
        }
        if (deltaPos.y == -1)
        {
            tilePosToChange = new Vector3Int(halfXValue, 1, 0);
        }
        if (deltaPos.x == 1)
        {
            tilePosToChange = new Vector3Int((int)gridSize.x, halfYValue, 0);
        }
        if (deltaPos.x == -1)
        {
            tilePosToChange = new Vector3Int(1, halfYValue, 0);
        }

        Vector3Int tilePosToChangeWithAddedXYoffset = new Vector3Int(tilePosToChange.x + (int)(gridSize.x * (xOfset)), tilePosToChange.y + (int)(gridSize.y * (yOfset)), 0); 
    
        _WallTileMap.SetTile(tilePosToChangeWithAddedXYoffset + offset, ConectingTile);
    }

    public void UpdateEdgeTiles()
    {
        // have to offest as tilemaps are not starting 0,0 bottom left (I know it sucks but it sucks even more to fix it)
        Vector3Int offset = new Vector3Int(4, -5, 0);
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        int hight = (int)gridSize.y+1;
        int width = (int)gridSize.x+1;
        Vector3Int[] AdjacentRooms = RoomGridManager.Instance.GetAdjacentGridCells(RoomPosition);
        foreach (Vector3Int room in AdjacentRooms)
        {
            if (RoomGridManager.Instance.GetRoomAtPosition(room) != null)
            {
                Vector3Int diffrence = RoomPosition - room;
                if (diffrence.x == -1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.down) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width-1,0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerLeft));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.down));
                    }
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.up) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width-1, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerRightDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.Up));
                    }
                }
                if (diffrence.x == 1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.down) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(1, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerRight));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.down));
                    }

                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.up) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(1, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerLeftDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.Up));
                    }
                }
                if (diffrence.y == 1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.left) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerRightDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.left));
                    }

                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.right) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerLeftDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.right));
                    }
                }
                if (diffrence.y == -1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.left) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight-1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerLeft));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.left));
                    }
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.right) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight - 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.innerRight));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TilePosition.right));
                    }
                }
            }
        }
    }
                    
    
    


    private void AbleToSkipRoom()
    {
        TimeDelayManager.Instance.RemoveTimer(_buildTimer);
        ConstructionCompleat();
    }

    private void BuildOrUpgradeRoom(int newLevel)
    {
        IsUnderConstruction = true;
        _underConstructionBanner.SetActive(true);
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        _buildTimer = new TimeDelayManager.Timer(DateTime.Now.AddMinutes(_roomlevels[newLevel].BuildTime), ConstructionCompleat);
        ConstructionTimer = TimeDelayManager.Instance.AddTimer(_buildTimer);
        if(Workers.Count == 0)
        {
            ConstructionTimer.PauseTimer();
        }
        UpdateRoomStats();
    }
    private void ConstructionCompleat()
    {
        IsUnderConstruction = false;
        _underConstructionBanner.SetActive(false);
        _buildTimer = null;
        if(Workers.Count != 0)
        {
            setRoomActive(true);
        }
        AlertManager.Instance.SendAlert(new Alert("Room Built","Room " + _roomName + " has been built", OpenRoomUIandFocusRoom, Alert.AlertPrority.low));
        UpdateRoomStats();


        // code related to crew room but here as could later be relevent to other rooms
        if(Level != 0)
        {
           int deltaMaxPeople = RoomStat.PoepleChange - _roomlevels[Level - 1].PoepleChange;
            GlobalStats.Instance.MaxPeople += deltaMaxPeople;
        }
    }

    public void setWallColor(Color color)
    {
        //TileBase 
         BoundsInt bounds = _WallTileMap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            _WallTileMap.SetTileFlags(pos, TileFlags.None);
            _WallTileMap.SetColor(pos,color);
        }
    }


    public void OpenRoomUIandFocusRoom()
    {
        FocusRoom();
        UIManager.Instance.DisplayRoomView(this);
    }

    public void FocusRoom()
    {
        CameraManager.Instance.CameraFocus(gameObject);
    }

    protected abstract void UpdateRoomStats();
    public abstract void SetOverLay(bool value);

    public abstract void PersonHover(PersonInfo personInfo);
    public abstract void ClearPersonHover();

    protected abstract void UpdateOverlay();

    #endregion

    #region InteractableInterface

    public void OnSelect()
    {
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_CLICK);
        _tempSelect.SetActive(true);
        if (RoomType == RoomType.QuestRoom)
        {
            UIManager.Instance.OpenAvalibalQuestListView();
        }
        else
        {
            UIManager.Instance.DisplayRoomView(this);
        }
    }

    public void OnDeselect()
    {
        _roomCameraPortal.GetComponent<Camera>().gameObject.SetActive(false);
        _tempSelect.SetActive(false);
    }

    public bool OnHold()
    {
        return false;
    }
    public void OnHoldRelease() { }

    #endregion

}
