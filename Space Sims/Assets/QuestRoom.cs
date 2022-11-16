using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRoom : AbstractRoom
{

    [SerializeField]
    private TextMeshProUGUI _overlayCurrentPeople,  _deltaAddPerosn;
 
    void Start()
    {
        IntisaliseRoom();
        GlobalStats.Instance.PlyaerRooms.Add(this);
        GlobalStats.Instance.QuestRoom = this;
    }

    public override bool AddWorker(Person person)
    {
        return false;
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
