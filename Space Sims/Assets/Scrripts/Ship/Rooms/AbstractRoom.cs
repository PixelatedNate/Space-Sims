using RDG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractRoom : MonoBehaviour, IInteractables, ISaveable<RoomSaveData>
{

    [SerializeField]
    private Vector2Int _size = new Vector2Int(1, 1);

    public Vector2Int Size { get { return _size; } }

    [SerializeField]
    protected GameObject _roomDarkFilter, _overlay;

    [SerializeField]
    private GameObject _underConstructionBanner;

    [SerializeField]
    private RoomStats[] _roomlevels;

    [SerializeField]
    public float RoomCostModifyer;

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
    public int Level { get; private set; } = 0;
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

    //private TimeDelayManager.Timer _buildTimer;


    private Vector3Int offset = new Vector3Int(4, -5, 0);


    private Alert constructionPusedAlert;


    public bool IsUnlocked { get { return UnlocksManager.UnlockedRooms.Contains(RoomType); } }

    private void Awake()
    {
        constructionPusedAlert = new Alert("Construction Paused", "No worker is present in room", OpenRoomUIandFocusRoom, Alert.AlertPrority.Permanet, Icons.GetMiscUIIcon(UIIcons.RoomIcon));
    }

    public Vector3Int GetConectorTile(Direction dir)
    {
        // have to offest as tilemaps are not starting 0,0 bottom left (I know it sucks but it sucks even more to fix it)
        //      Vector3Int offset = new Vector3Int(4, -5, 0);
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        int halfYValue = Mathf.CeilToInt(gridSize.y / 2);
        int halfXValue = Mathf.CeilToInt(gridSize.x / 2);
        switch (dir)
        {
            case Direction.Up: return new Vector3Int(halfXValue, (int)gridSize.y, 0) + offset;
            case Direction.Down: return new Vector3Int(halfXValue, 1, 0) + offset;
            case Direction.Right: return new Vector3Int((int)gridSize.x, halfYValue, 0) + offset;
            case Direction.Left: return new Vector3Int(1, halfYValue, 0) + offset;
            default: return Vector3Int.zero;
        }
    }
    public Vector3Int GetCenterTile()
    {
        // have to offest as tilemaps are not starting 0,0 bottom left (I know it sucks but it sucks even more to fix it)
        Vector3Int offset = new Vector3Int(4, -5, 0);
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        int halfYValue = Mathf.CeilToInt(gridSize.y / 2);
        int halfXValue = Mathf.CeilToInt(gridSize.x / 2);
        return new Vector3Int(halfXValue, halfYValue, 0) + offset;

    }

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
        if (IsUnderConstruction)
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Allready UnderConstruction", Color.red);
            return;
        }
        if (Level == _roomlevels.Length - 1)
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Max level", Color.red);
            return;
        }

        if (_roomlevels[Level + 1].BuildCost < GlobalStats.Instance.PlayerResources)
        {
            GlobalStats.Instance.PlayerResources -= _roomlevels[Level + 1].BuildCost;
            BuildOrUpgradeRoom(Level++);
            UIManager.Instance.OpenRoomView(this);
        }
        else
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Insificent Resources", Color.red);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Error);
        }
    }
    public abstract void IntisaliseRoom();

    public virtual bool AddWorker(Person person)
    {
        if (Workers.Count == RoomStat.MaxWorkers)
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Room Full", Color.red);
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
            if (IsUnderConstruction && ConstructionTimer.IsPause)
            {
                ResumeConstructionTimer(ConstructionTimer);
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
                if (IsUnderConstruction)
                {
                    PauseConstructionTimer(ConstructionTimer);
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
        _roomDarkFilter.SetActive(!active);
        isRoomActive = active;
        UpdateRoomStats();
    }


    public int GetPersonLayerInRoom(Vector3Int personPos)
    {
        return (int)RoomGridManager.Instance.roomGridSize.y - (personPos.y - offset.y);

    }

    public void SkipRoom()
    {
        UIManager.Instance.Conformation(AbleToSkipRoom, "are you sure you want to spend x in order to instatly build this room");
    }

    public void EnableRoomConection(Vector3Int AdjacentPos)
    {

        Vector3Int delataPos = AdjacentPos - RoomPosition;

        if (Size != new Vector2Int(1, 1))
        {
            for (int x = 0; x < Size.x; x++)
            {
                delataPos = AdjacentPos - RoomPosition - new Vector3Int(x, 0, 0);
                if (MathF.Abs(delataPos.magnitude) == 1)
                {
                    SetRoomTiles(delataPos, x, 0);
                    return;
                }
            }
            for (int y = 0; y < Size.y; y++)
            {
                delataPos = AdjacentPos - RoomPosition - new Vector3Int(0, y, 0);
                if (MathF.Abs(delataPos.magnitude) == 1)
                {
                    SetRoomTiles(delataPos, 0, y);
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


    private void SetRoomTiles(Vector3 deltaPos, int xOfset = 0, int yOfset = 0)
    {
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        Vector3Int tilePosToChange = Vector3Int.zero;


        // for these checks first offset is for room positoin
        // second offset is for if the room is bigger than a 1x1 size;

        if (deltaPos.y == 1)
        {
            tilePosToChange = GetConectorTile(Direction.Up);
            // this  offset is for rooms which are more than 1x1 size
            tilePosToChange = new Vector3Int(tilePosToChange.x + (int)(gridSize.x * (xOfset)), tilePosToChange.y + (int)(gridSize.y * (yOfset)), 0);
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.left, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallUpLeft));
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.right, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallUpRight));
        }
        if (deltaPos.y == -1)
        {
            tilePosToChange = GetConectorTile(Direction.Down);
            // this offset is for rooms which are more than 1x1 size
            tilePosToChange = new Vector3Int(tilePosToChange.x + (int)(gridSize.x * (xOfset)), tilePosToChange.y + (int)(gridSize.y * (yOfset)), 0);
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.left, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallDownLeft));
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.right, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallDownRight));
        }
        if (deltaPos.x == 1)
        {
            tilePosToChange = GetConectorTile(Direction.Right);
            tilePosToChange = new Vector3Int(tilePosToChange.x + (int)(gridSize.x * (xOfset)), tilePosToChange.y + (int)(gridSize.y * (yOfset)), 0);
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.up, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallUpRight));
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.down, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallDownRight));
        }
        if (deltaPos.x == -1)
        {
            tilePosToChange = GetConectorTile(Direction.Left);
            tilePosToChange = new Vector3Int(tilePosToChange.x + (int)(gridSize.x * (xOfset)), tilePosToChange.y + (int)(gridSize.y * (yOfset)), 0);
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.up, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallUpLeft));
            _WallTileMap.SetTile(tilePosToChange + Vector3Int.down, TileMapHelper.GetTile(TileMapHelper.TileName.ConectingWallDownLeft));
        }


        _WallTileMap.SetTile(tilePosToChange, null);
        _floorTileMap.SetTile(tilePosToChange, ConectingTile);
    }

    public void UpdateEdgeTiles()
    {
        // have to offest as tilemaps are not starting 0,0 bottom left (I know it sucks but it sucks even more to fix it)
        Vector3Int offset = new Vector3Int(4, -5, 0);
        Vector3 gridSize = RoomGridManager.Instance.roomGridSize;
        int hight = (int)gridSize.y + 1;
        int width = (int)gridSize.x + 1;
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
                        _Shipedges.SetTile(new Vector3Int(width - 1, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeInnerLeft));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimdown));
                    }
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.up) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width - 1, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimInnerRightDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimUp));
                    }
                }
                if (diffrence.x == 1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.down) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(1, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimInnerRight));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimdown));
                    }

                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.up) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(1, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeInnerLeftDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimUp));
                    }
                }
                if (diffrence.y == 1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.left) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimInnerRightDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimLeft));
                    }

                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.right) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeInnerLeftDown));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, 0, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimRight));
                    }
                }
                if (diffrence.y == -1)
                {
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.left) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight - 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeInnerLeft));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(0, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimLeft));
                    }
                    if (RoomGridManager.Instance.GetRoomAtPosition(room + Vector3Int.right) != null)
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight - 1, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimInnerRight));
                    }
                    else
                    {
                        _Shipedges.SetTile(new Vector3Int(width, hight, 0) + offset, TileMapHelper.GetTile(TileMapHelper.TileName.EdgeRimRight));
                    }
                }
            }
        }
    }





    private void AbleToSkipRoom()
    {
        TimeDelayManager.Instance.RemoveTimer(ConstructionTimer);
        ConstructionCompleat();
    }


    private void PauseConstructionTimer(TimeDelayManager.Timer constructionTimer)
    {
        AlertManager.Instance.SendAlert(constructionPusedAlert);
        constructionTimer.PauseTimer();
    }
    private void ResumeConstructionTimer(TimeDelayManager.Timer constructionTimer)
    {
        AlertManager.Instance.RemoveAlert(constructionPusedAlert);
        constructionTimer.RestartTimer();
    }


    private void BuildOrUpgradeRoom(int newLevel)
    {
        IsUnderConstruction = true;
        _underConstructionBanner.SetActive(true);
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        ConstructionTimer = new TimeDelayManager.Timer(DateTime.Now.AddMinutes(_roomlevels[newLevel].BuildTime), ConstructionCompleat);
        ConstructionTimer = TimeDelayManager.Instance.AddTimer(ConstructionTimer);
        if (Workers.Count == 0)
        {
            PauseConstructionTimer(ConstructionTimer);
            //   ConstructionTimer.PauseTimer();
        }
        UpdateRoomStats();
    }
    private void ConstructionCompleat()
    {
        IsUnderConstruction = false;
        _underConstructionBanner.SetActive(false);
        ConstructionTimer = null;
        if (Workers.Count != 0)
        {
            setRoomActive(true);
        }
        AlertManager.Instance.SendAlert(new Alert("Room Built", "Room " + _roomName + " has been built", OpenRoomUIandFocusRoom, Alert.AlertPrority.low, Icons.GetSkillIcon(SkillsList.Strength)));
        GlobalStats.Instance.MaxStorage += RoomStat.Storage;
        UpdateRoomStats();


        // code related to crew room but here as could later be relevent to other rooms
        if (Level != 0)
        {
            int deltaMaxPeople = RoomStat.PoepleChange - _roomlevels[Level - 1].PoepleChange;
            GlobalStats.Instance.MaxPeople += deltaMaxPeople;
        }
    }

    public void SetWallColor(Color color)
    {
        _WallTileMap.gameObject.GetComponent<TilemapRenderer>().material.color = color;
    }


    public void OpenRoomUIandFocusRoom()
    {
        FocusRoom();
        UIManager.Instance.OpenRoomView(this);
    }

    public void FocusRoom()
    {
        CameraManager.Instance.CameraFocus(gameObject);
    }

    public abstract void UpdateRoomStats();
    public abstract void SetOverLay(bool value);

    public abstract void PersonHover(PersonInfo personInfo);
    public abstract void ClearPersonHover();

    protected abstract void UpdateOverlay();

    #endregion

    #region InteractableInterface

    public void OnSelect()
    {
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_CLICK);

        _WallTileMap.gameObject.GetComponent<TilemapRenderer>().material.SetFloat("_IsSelected", 1);
        if (RoomType == RoomType.QuestRoom)
        {
            UIManager.Instance.OpenAvalibalQuestListView();
        }
        else
        {
            UIManager.Instance.OpenRoomView(this);
        }
    }

    public void OnDeselect()
    {
        _roomCameraPortal.GetComponent<Camera>().gameObject.SetActive(false);
        _WallTileMap.gameObject.GetComponent<TilemapRenderer>().material.SetFloat("_IsSelected", 0);
    }

    public bool OnHold()
    {
        return false;
    }
    public void OnHoldRelease() { }

    #endregion

    #region Savable

    public RoomSaveData Save()
    {
        RoomSaveData data = new RoomSaveData(this);
        data.Save();
        return data;
    }

    public void Load(string path)
    {
        RoomSaveData data = SaveSystem.LoadData<RoomSaveData>(path);
        Load(data);

    }
    public void Load(RoomSaveData data)
    {
        this.Level = data.level;
        this.RoomPosition = new Vector3Int(data.roomPosition[0], data.roomPosition[1], data.roomPosition[2]);
        this.IsUnderConstruction = data.UnderCostruction;
        if (data.UnderCostruction)
        {
            _underConstructionBanner.SetActive(true);
            string Timmerpath = SaveSystem.TimerPath + "/" + data.ConstructionTimerId + SaveSystem.TimerPrefix;
            TimerSaveData timerData = SaveSystem.LoadData<TimerSaveData>(Timmerpath);
            ConstructionTimer = new TimeDelayManager.Timer(timerData, ConstructionCompleat);
            if (timerData.IsPause)
            {
                AlertManager.Instance.SendAlert(constructionPusedAlert);
            }
        }
    }



    #endregion


}
