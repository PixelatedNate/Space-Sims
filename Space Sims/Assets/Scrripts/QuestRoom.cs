using TMPro;
using UnityEngine;

public class QuestRoom : AbstractRoom
{

    [SerializeField]
    private TextMeshProUGUI _overlayCurrentPeople, _deltaAddPerosn;

    void Start()
    {
        IntisaliseRoom();
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.QuestRoom = this;
    }

    public override bool AddWorker(Person person)
    {
        if (Workers.Contains(person.PersonInfo))
        {
            Debug.LogWarning("Failed to add a person to room: as they are allready in");
            return false;
        }
        else if (person.PersonInfo.Room == null)
        {
            Workers.Add(person.PersonInfo);
            UpdateRoomStats();
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ClearPersonHover()
    {
        _deltaAddPerosn.text = "";
        _overlay.SetActive(false);
    }

    public override void IntisaliseRoom()
    {
        return;
    }

    public override void PersonHover(PersonInfo personInfo)
    {
        UpdateOverlay();
        _overlay.SetActive(true);
        _deltaAddPerosn.text = "N/A";
        _deltaAddPerosn.color = Color.red;
    }

    public override void SetOverLay(bool value)
    {
        _overlay.SetActive(true);
    }

    protected override void UpdateOverlay()
    {
        _overlayCurrentPeople.text = Workers.Count.ToString();
    }

    protected override void UpdateRoomStats()
    {
        GameResources delta = new GameResources();
        GlobalStats.Instance.AddorUpdateRoomDelta(this, delta);
    }

}
