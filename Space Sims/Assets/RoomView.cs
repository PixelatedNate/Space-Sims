using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name, _type, _modifer, _maxPeopel, _currentPeople, _output, _upkeep, _constructionTime;
    [SerializeField]
    private Image _outPutImge, _upkeepImage;
    [SerializeField]
    private Transform _progressbar;
    [SerializeField]
    private GameObject _activeSubview, _disabledSubView, _construtionSubView;
    [SerializeField]
    RenderTexture _cameraRenderTexture;
    private PassiveProductionRoom SelectedRoom { get; set; }


    #region PublicMethods   

    /// <summary>
    /// Set the room info to be displayed in the view
    /// </summary>
    /// <param name="room"></param>
    public void SetRoom(PassiveProductionRoom room)
    {
        SelectedRoom = room;
        UpdateCamera();
        UpdateView();
    }

    #endregion

    #region PrivateMethods

    private void UpdateView()
    {
        UpdateUniversalText();
        if (SelectedRoom.IsUnderConstruction)
        {
            TimeTickSystem.OnTick += OnTick;
            _activeSubview.SetActive(false);
            _disabledSubView.SetActive(false);
            _construtionSubView.SetActive(true);
            UpdateContructionValues();
            return;
        }
        else
        {
            TimeTickSystem.OnTick -= OnTick;
            _construtionSubView.SetActive(false);
            _activeSubview.SetActive(true);
            UpdateOutput();
            UpdateUpkeep();
        }
          
    }
    private void UpdateContructionValues()
    {
      _constructionTime.text = SelectedRoom.ConstructionTimer.RemainingDuration.ToString("h'h 'm'm 's's'");
      double ProgressBarPercent = (SelectedRoom.ConstructionTimer.RemainingDuration.TotalSeconds/(SelectedRoom.ConstructionTimer.TotalBuildDuration.TotalSeconds / 100));
      _progressbar.localScale = new Vector3(1-(float)ProgressBarPercent/100,1,1);
    }

    
    private void UpdateUniversalText()
    {
        _name.text = SelectedRoom.RoomName;
        _type.text = SelectedRoom.RoomType.ToString();
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
            _upkeep.text = SelectedRoom.UpkeepValue.ToString();
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
            _output.text = SelectedRoom.OutputValue.ToString();
        }
        else
        {
            _outPutImge.gameObject.SetActive(false);
            _output.text = "";
        }
    }
        

    private void UpdateCamera()
    {
        SelectedRoom.SetCamera(_cameraRenderTexture);
    }


    private void OnTick(object source, EventArgs e)
    {
        if(SelectedRoom.IsUnderConstruction)
        {
            UpdateContructionValues();
        }
        else
        {
            SetRoom(SelectedRoom);
        }
    }

    #endregion

}
