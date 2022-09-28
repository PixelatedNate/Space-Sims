using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UniversalRoomView : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _name, _type, _constructionTime;
    [SerializeField]
    private Transform _progressbar;
    [SerializeField]
    private GameObject _construtionSubView;
    [SerializeField]
    RenderTexture _cameraRenderTexture;
    

    [SerializeField]
    PassiveProductionRoomView PassiveProductionRoomViewScript;

    private GameObject SubRoomView { get; set; }

    private AbstractRoom SelectedRoom { get; set; }


    #region PublicMethods   

    /// <summary>
    /// Set the room info to be displayed in the view
    /// </summary>
    /// <param name="room"></param>
    public void SetRoom(AbstractRoom room)
    {
        if (SubRoomView != null)
        {
            SubRoomView.SetActive(false);
        }
        SelectedRoom = room;
        UpdateCamera();
        UpdateView();
    }


    private void EnableCorrectRoomView()
    {
    if (SelectedRoom is PassiveProductionRoom)
     {
        PassiveProductionRoomViewScript.gameObject.SetActive(true);
        SubRoomView = PassiveProductionRoomViewScript.gameObject;
        PassiveProductionRoomViewScript.SetRoom((PassiveProductionRoom)SelectedRoom);
     }
    }

    #endregion

    #region PrivateMethods

    private void UpdateView()
    {
        UpdateUniversalText();
        if (SelectedRoom.IsUnderConstruction)
        {
            TimeTickSystem.OnTick += OnTick;
            _construtionSubView.SetActive(true);
            UpdateContructionValues();
            return;
        }
        else
        {
            TimeTickSystem.OnTick -= OnTick;
            _construtionSubView.SetActive(false);
            EnableCorrectRoomView();
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
