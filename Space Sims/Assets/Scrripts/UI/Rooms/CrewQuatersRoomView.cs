using TMPro;
using UnityEngine;

public class CrewQuatersRoomView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _maxPeopel, _currentPeople, _globalMaxPeopleChange;
    [SerializeField]
    private GameObject _activeSubview, _disabledSubView;

    private CrewQuatersRoom SelectedRoom { get; set; }
    public void SetRoom(CrewQuatersRoom room)
    {
        SelectedRoom = room;
        _activeSubview.SetActive(true);
        UpdateView();
    }
    private void UpdateView()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        _globalMaxPeopleChange.text = SelectedRoom.RoomStat.PoepleChange.ToString();
        _maxPeopel.text = SelectedRoom.RoomStat.MaxWorkers.ToString();
        _currentPeople.text = SelectedRoom.Workers.Count.ToString();
    }

}
