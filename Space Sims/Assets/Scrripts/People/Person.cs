using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using RDG;

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

    private PersonInfo _personInfo = null;
    public PersonInfo PersonInfo { get { return _personInfo; } }

    [SerializeField]
    GameObject TempSelected;

    [SerializeField]
    bool IsBeingHeld = false;


    LinkedList<Vector3Int> MovePath;
    
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
            if (PersonInfo.Room != null)
            {
                transform.position = room.transform.position;
            }
            else
            {
                transform.position = Vector3.zero;
            }
            return false;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.Equals(TouchControls.GetInteractableUnderMouse()))
            {
                Debug.Log("true");
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
        if (MovePath != null && MovePath.Count > 0)
        {
            Vector3 worldSpacePosition = PersonInfo.Room.PathFindingTileMap.GetCellCenterWorld(MovePath.First.Value);
            Vector3 dir = (worldSpacePosition - transform.position).normalized;
            transform.Translate(dir * 1 * Time.deltaTime);
            if (Vector3.Distance(transform.position, worldSpacePosition) < 0.05)
            {
                MovePath.RemoveFirst();
            }
        }
        else if (PersonInfo.Room != null)
        {
            Vector3Int randomPoint;
            Vector3Int personposition = PersonInfo.Room.PathFindingTileMap.WorldToCell(transform.position);
            int index = Random.Range(0, _tempPathFindingPoints.Length);
            randomPoint = _tempPathFindingPoints[index];
            MovePath = PathFinding.CalculatePath(PersonInfo.Room.PathFindingTileMap, personposition, randomPoint);
        }

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

    private void ReRenderPerson()
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
            float yOffset = -1.5f;

            Vector3 mousePointOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePointOnWorld.z = 0;
            mousePointOnWorld.y = mousePointOnWorld.y + yOffset;

            transform.position = mousePointOnWorld;
            AbstractRoom roomUnderMouse = TouchControls.GetRoomUnderMouse();
            if(roomUnderMouse != RoomUnderMouseOnDrag)
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
        TempSelected.SetActive(true);
        UIManager.Instance.DisplayPerson(_personInfo);
    }

    public bool OnHold()
    {
        if (IsBeingHeld)
        {
            throw new Exception("Cannot start a hold on someone who is allready being held");
        }
        Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_HEAVY_CLICK);
        RoomUnderMouseOnDrag = null;
        IsBeingHeld = true;


        return true;
    }

    public void OnHoldRelease()
    {
        AbstractRoom room = RoomGridManager.Instance.GetRoomAtPosition(transform.position);
        AssginRoomToPerson(room);
        if(RoomUnderMouseOnDrag != null)
        {
            RoomUnderMouseOnDrag.ClearPersonHover();
        }
        RoomUnderMouseOnDrag = null;
        IsBeingHeld = false;
    }

    public void OnDeselect()
    {
        TempSelected.SetActive(false);
    }
    #endregion
}
