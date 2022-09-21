using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomView : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI Name, Type, Modifer, MaxPeopel, CurrentPeople, Output, Upkeep, ConstructionTime;

    [SerializeField]
    Image OutPutImge, UpkeepImage;

    [SerializeField]
    Transform Progressbar;

    Room SelectedRoom;




    [SerializeField]
    private GameObject ActiveSubview, DisabledSubView, ConstrutionSubView;

    [SerializeField]
    RenderTexture CameraRenderTexture;

    public void SetRoom(Room room)
    {
        SelectedRoom = room;
        UpdateCamera();


        if(SelectedRoom.IsUnderConstruction)
        {
            TimeTickSystem.OnTick += OnTick;
            ActiveSubview.SetActive(false);
            DisabledSubView.SetActive(false);
            ConstrutionSubView.SetActive(true);
            UpdateContructionValues();
            return;
        }

        TimeTickSystem.OnTick -= OnTick;
        ConstrutionSubView.SetActive(false);
        ActiveSubview.SetActive(true);

        UpdateText();
        if (SelectedRoom.UpkeepType != null)
        {
            UpkeepImage.gameObject.SetActive(true);
            UpkeepImage.sprite = Icons.GetIcon(SelectedRoom.UpkeepType);
            Upkeep.text = SelectedRoom.UpkeepValue.ToString();
        }
        else
        {
            UpkeepImage.gameObject.SetActive(false);
            Upkeep.text = "";
        }
        if (SelectedRoom.OutPutType != null)
        {
            OutPutImge.gameObject.SetActive(true);
            OutPutImge.sprite = Icons.GetIcon(SelectedRoom.OutPutType);
            Output.text = SelectedRoom.OutputValue.ToString();
        }
        else
        {
            OutPutImge.gameObject.SetActive(false);
            Output.text = "";
        }
        
    }

    private void UpdateText()
    {
        Name.text = SelectedRoom.RoomName;
        Type.text = SelectedRoom.RoomType.ToString();
        Modifer.text = SelectedRoom.DesiredSkill.ToString();
        MaxPeopel.text = SelectedRoom.RoomStat.MaxWorkers.ToString();
        CurrentPeople.text = SelectedRoom.Workers.Count.ToString();
    }
    
    private void UpdateCamera()
    {
        SelectedRoom.SetCamera(CameraRenderTexture);
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


    private void UpdateContructionValues()
    {
      ConstructionTime.text = SelectedRoom.ConstructionTimer.RemainingDuration.ToString("h'h 'm'm 's's'");
      double ProgressBarPercent = (SelectedRoom.ConstructionTimer.RemainingDuration.TotalSeconds/(SelectedRoom.ConstructionTimer.TotalBuildDuration.TotalSeconds / 100));
      Progressbar.localScale = new Vector3(1-(float)ProgressBarPercent/100,1,1);
    }



}
