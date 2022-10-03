using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveProductionRoomView : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _modifer, _maxPeopel, _currentPeople, _output, _upkeep;
    [SerializeField]
    private Image _outPutImge, _upkeepImage;
    [SerializeField]
    private GameObject _activeSubview, _disabledSubView;

    private PassiveProductionRoom SelectedRoom { get; set; }


    #region PublicMethods   

    public void SetRoom(PassiveProductionRoom room)
    {
        SelectedRoom = room;
        _activeSubview.SetActive(true);
        UpdateView();
    }

    #endregion

    #region PrivateMethods

    private void UpdateView()
    {
        UpdateText();
        UpdateOutput();
        UpdateUpkeep();
    }

    private void UpdateText()
    {
        _modifer.text = SelectedRoom.DesiredSkill.ToString();
        _maxPeopel.text = SelectedRoom.RoomStat.MaxWorkers.ToString();
        _currentPeople.text = SelectedRoom.Workers.Count.ToString();
    }

    private void UpdateUpkeep()
    {
        if (SelectedRoom.UpkeepType != null)
        {
            _upkeepImage.gameObject.SetActive(true);
            _upkeepImage.sprite = Icons.GetIcon((ResourcesEnum)SelectedRoom.UpkeepType);
            _upkeep.text = SelectedRoom.BaseUpkeepValue.ToString();
        }
        else
        {
            _upkeepImage.gameObject.SetActive(false);
            _upkeep.text = "";
        }
    }

    private void UpdateOutput()
    {
        if (SelectedRoom.OutPutType != null)
        {
            _outPutImge.gameObject.SetActive(true);
            _outPutImge.sprite = Icons.GetIcon((ResourcesEnum)SelectedRoom.OutPutType);
            _output.text = SelectedRoom.BaseOutputValue.ToString();
        }
        else
        {
            _outPutImge.gameObject.SetActive(false);
            _output.text = "";
        }
    }
    #endregion

}
