using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RDG;

public abstract class AbstractRoom : MonoBehaviour, IInteractables
{

    [SerializeField]
    protected GameObject _roomLight, _overlay;

    [SerializeField]
    private GameObject _tempSelect;

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
    private Tilemap _pathFindingTileMap;
    public Tilemap PathFindingTileMap { get { return _pathFindingTileMap; } }

    protected bool isRoomActive { get; set; } = false;

    #region PublicMethods

    public void BuildRoom()
    {
        if (IsUnderConstruction || Level != 0)
        {
            throw new Exception("Trying to build a new room which allready exsistes");
        }
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        BuildOrUpgradeRoom(0);
    }

    public void UpgradeRoom(int level)
    {
        if (IsUnderConstruction || Level == _roomlevels.Length - 1)
        {
            throw new Exception("Trying to uprage a room which is max level or allready under construction");
        }
        BuildOrUpgradeRoom(++level);
    }

    public abstract void IntisaliseRoom();

    public bool AddWorker(Person person)
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
            setRoomActive(true);
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

    #endregion

    #region PrivateMethods

    private void BuildOrUpgradeRoom(int newLevel)
    {
        IsUnderConstruction = true;
        GlobalStats.Instance.AddorUpdateRoomDelta(this, new GameResources());
        ConstructionTimer = TimeDelayManager.Instance.AddTimer(new TimeDelayManager.Timer(DateTime.Now.AddMinutes(_roomlevels[newLevel].BuildTime), ConstructionCompleat));
        UpdateRoomStats();
    }
    private void ConstructionCompleat()
    {
        IsUnderConstruction = false;
        UpdateRoomStats();
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
        UIManager.Instance.DisplayRoomView(this);
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
