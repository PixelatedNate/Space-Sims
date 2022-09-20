using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomView : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI Name, Type, Modifer, MaxPeopel, CurrentPeople, Output, Upkeep;

    [SerializeField]
    Image OutPutImge, UpkeepImage;

    Room SelectedRoom;

    [SerializeField]
    RenderTexture CameraRenderTexture;

    public void SetRoom(Room room)
    {
        SelectedRoom = room;
        UpdateText();
        UpdateCamera();

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
}
