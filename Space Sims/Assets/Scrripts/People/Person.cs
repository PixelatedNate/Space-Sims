using RDG;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour, IInteractables
{
    [SerializeField]
    GameObject TESTICON;

    //  new Vector3 (6,1)
    //  6,-3
    //  10,1
    //  10,-3

    private Vector3Int[] _tempPathFindingPoints = { new Vector3Int(6, -3, 0), new Vector3Int(6, 1, 0), new Vector3Int(10, 1, 0), new Vector3Int(10, -3) };


    [SerializeField]
    private SpriteRenderer HeadRender;
    [SerializeField]
    private SpriteRenderer BodyRender;
    [SerializeField]
    private SpriteRenderer HairRender;
    [SerializeField]
    private SpriteRenderer ClothesRender;


    [SerializeField]
    private SpriteRenderer SelectOutline;

    private PersonInfo _personInfo = null;
    public PersonInfo PersonInfo { get { return _personInfo; } }

    [SerializeField]
    GameObject TempSelected;

    public bool IsBeingHeld { get; private set; } = false;


    LinkedList<Vector3Int> MovePath { get; set; }

    AbstractRoom PathfindingRoom { get; set; }

    Dictionary<AbstractRoom, LinkedList<Vector3Int>> RoomMovePath { get; set; }
    private AbstractRoom RoomUnderMouseOnDrag { get; set; }


    bool IsMouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //testing
        if (PersonInfo == null)
        {
            RandomisePerson();
        }
    }


    public void RandomisePerson()
    {
        if (PersonInfo != null)
        {
            GlobalStats.Instance.PlayersPeople.Remove(PersonInfo);
        }
        PersonInfo person = new PersonInfo();
        person.Randomize();
        AssginPerson(person);
        ReRenderPerson();
        GlobalStats.Instance.PlayersPeople.Add(PersonInfo);
    }

    public void AssginPerson(PersonInfo person)
    {
        if (PersonInfo != null)
        {
            //       throw new Exception("Trying to assgin a person who allready has a personholder");
        }
        gameObject.name = person.Name;
        _personInfo = person;
        person.PersonMonoBehaviour = this;
        GlobalStats.Instance.AddorUpdatePersonDelta(this, PersonInfo.Upkeep);
        ReRenderPerson();
    }


    public bool AssginRoomToPerson(AbstractRoom room)
    {
        if (room == null) // tried to drop person where there was no room like in space.
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Not a room", Color.red);
            transform.position = Vector3.zero;
            transform.position = PersonInfo.Room.transform.position;
            return false;
        }

        if (room == PersonInfo.Room)
        {
            return false;
        }
        AbstractRoom oldRoom = PersonInfo.Room;
        if (room != null && room.AddWorker(this))
        {
            if (oldRoom != null)
            {
                oldRoom.RemoveWorker(this);
            }
            MovePath = null;
            PersonInfo.Room = room;
            return true;
        }
        else
        {
            Vector3Int personposition = room.PathFindingTileMap.WorldToCell(transform.position);
            RoomMovePath = PathFinding.CalculateRoomPath(personposition, room, PersonInfo.Room);
            MovePath = null;
            return false;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.Equals(TouchControls.GetInteractableUnderMouse()))
            {
                IsMouseOver = true;
                return;
            }
        }
        else if (IsMouseOver && Input.GetMouseButtonUp(0))
        {
            IsMouseOver = false;
        }
        else if (IsMouseOver)
        {
            return;
        }


        float walkSpeed = 1;

        bool IsWalkingBetweenRooms = false;

        if (RoomMovePath != null && RoomMovePath.Count > 1)
        {
            walkSpeed = 2;
            IsWalkingBetweenRooms = true;
            if (MovePath == null)
            {
                PathfindingRoom = RoomMovePath.ElementAt(0).Key;
                MovePath = RoomMovePath.ElementAt(0).Value;
            }
            else if (MovePath.Count == 0)
            {
                Vector3Int nextPosition = RoomMovePath.ElementAt(1).Value.First.Value;
                Vector3 worldSpacePositionNextPosition = RoomMovePath.ElementAt(1).Key.PathFindingTileMap.GetCellCenterWorld(nextPosition);
                Vector3 dir = (worldSpacePositionNextPosition - transform.position).normalized;
                transform.Translate(dir * walkSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, worldSpacePositionNextPosition) < 0.05)
                {
                    RoomMovePath.Remove(RoomMovePath.ElementAt(0).Key);
                    PathfindingRoom = RoomMovePath.ElementAt(0).Key;
                    MovePath = RoomMovePath.ElementAt(0).Value;
                }
            }
        }

        if (MovePath != null && MovePath.Count > 0)
        {
            Vector3 worldSpacePositionNextPosition = PathfindingRoom.PathFindingTileMap.GetCellCenterWorld(MovePath.First.Value);
            Vector3 dir = (worldSpacePositionNextPosition - transform.position).normalized;
            transform.Translate(dir * walkSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, worldSpacePositionNextPosition) < 0.05)
            {
                MovePath.RemoveFirst();
            }
        }
        else if (PersonInfo.Room != null && !IsWalkingBetweenRooms)
        {
            Vector3Int randomPoint;
            Vector3Int personposition = PersonInfo.Room.PathFindingTileMap.WorldToCell(transform.position);
            randomPoint = GetRandomPathFindingPoint();
            PathfindingRoom = PersonInfo.Room;
            MovePath = PathFinding.CalculateInternalRoomPath(PersonInfo.Room.PathFindingTileMap, personposition, randomPoint);
        }
    }


    private Vector3Int GetRandomPathFindingPoint()
    {
        //private Vector3Int[] _tempPathFindingPoints = { new Vector3Int(6, -3, 0), new Vector3Int(6, 1, 0), new Vector3Int(10, 1, 0), new Vector3Int(10, -3) };

        int x = 0;
        int y = 0;
        if (PersonInfo.Room.RoomType == RoomType.QuestRoom)
        {
            x = Random.Range(6, 18);
            y = Random.Range(-3, 2);
        }
        else
        {
            x = Random.Range(6, 11);
            y = Random.Range(-3, 2);
        }
        return new Vector3Int(x, y, 0);
    }


    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            if (IsBeingHeld)
            {
                throw new Exception("Trying to destroy a person whilstBeingheld");
            }
            GlobalStats.Instance.RemovePersonDelta(this);
            PersonInfo.PersonMonoBehaviour = null;
        }
    }

    public void ReRenderPerson()
    {
        BodyRender.sprite = PersonInfo.Body;
        BodyRender.material.color = PersonInfo.SkinColor;
        HeadRender.sprite = PersonInfo.Head;
        HeadRender.material.color = PersonInfo.SkinColor;
        ClothesRender.sprite = PersonInfo.Clothes;
        HairRender.sprite = PersonInfo.Hair;
        HairRender.material.color = PersonInfo.HairColor;
    }


    private void LateUpdate()
    {
        if (IsBeingHeld)
        {
            float yOffset = -0.5f;

            Vector3 mousePointOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePointOnWorld.z = 0;
            mousePointOnWorld.y = mousePointOnWorld.y + yOffset;

            transform.position = mousePointOnWorld;
            AbstractRoom roomUnderMouse = TouchControls.GetRoomUnderMouse();
            if (roomUnderMouse != RoomUnderMouseOnDrag)
            {
                if (RoomUnderMouseOnDrag != null)
                {
                    RoomUnderMouseOnDrag.ClearPersonHover();
                }
                if (roomUnderMouse != null)
                {
                    RoomUnderMouseOnDrag = roomUnderMouse;
                    RoomUnderMouseOnDrag.PersonHover(PersonInfo);
                }
                else
                {
                    RoomUnderMouseOnDrag = null;
                }
            }
        }
    }

    #region InteractableInterace
    public void OnSelect()
    {
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_CLICK);
        UIManager.Instance.OpenPersonView(_personInfo);
    }

    public void SetOutline(bool value)
    {

        if (value)
        {
            SelectOutline.sprite = PersonInfo.Body;
            SelectOutline.gameObject.SetActive(true);
            SelectOutline.sharedMaterial.SetTexture("_Head", PersonInfo.Head.texture);
            SelectOutline.sharedMaterial.SetTexture("_Cloths", PersonInfo.Clothes.texture);
            SelectOutline.sharedMaterial.SetTexture("_Hair", PersonInfo.Hair.texture);
        }
        else
        {
            if (SelectOutline != null)
            {
                SelectOutline.gameObject.SetActive(false);
            }
        }
    }

    public bool OnHold()
    {
        if (IsBeingHeld)
        {
            throw new Exception("Cannot start a hold on someone who is allready being held");
        }
        UIManager.Instance.OpenPersonView(PersonInfo);
        SoundManager.Instance.PlaySound(SoundManager.VoiceSounds.PickVoiceLines, PersonVoice.GetPitch(PersonInfo));
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_HEAVY_CLICK);
        RoomUnderMouseOnDrag = null;
        IsBeingHeld = true;
        return true;
    }

    public void OnHoldRelease()
    {
        UIManager.Instance.DeselectAll();
        SoundManager.Instance.PlaySound(SoundManager.VoiceSounds.PutDownVoiceLines, PersonVoice.GetPitch(PersonInfo));
        AbstractRoom room = RoomGridManager.Instance.GetRoomAtPosition(transform.position);
        AssginRoomToPerson(room);
        if (RoomUnderMouseOnDrag != null)
        {
            RoomUnderMouseOnDrag.ClearPersonHover();
        }
        RoomUnderMouseOnDrag = null;
        IsBeingHeld = false;
        SetOutline(false);
    }

    public void OnDeselect()
    {
        SetOutline(false);
    }
    #endregion
}
