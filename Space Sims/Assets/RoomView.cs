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
        DisplayInputAndOutPuts();
        UpdateCamera();
        
    }


    private void UpdateText()
    {
        Name.text = SelectedRoom.RoomName;
        Type.text = SelectedRoom.RoomType.ToString();
        Modifer.text = SelectedRoom.DesiredSkill.ToString();
        MaxPeopel.text = SelectedRoom.RoomStat.MaxWorkers.ToString();
        CurrentPeople.text = SelectedRoom.Workers.Count.ToString();
    }

    private void DisplayInputAndOutPuts()
    {

        bool upkeepFound = false;
        bool outPutFound = false;
        foreach (ResourcesEnum re in Enum.GetValues(typeof(ResourcesEnum)))
        {
            if (SelectedRoom.RoomStat.Upkeep.GetResorce(re) != 0)
            {
                if (upkeepFound == true) { throw new Exception("Room " + SelectedRoom.gameObject.name + " has two diffrent Resources used for Upkeep"); }
                upkeepFound = true;
                UpkeepImage.sprite = Icons.GetIcon(re);
                Upkeep.text = SelectedRoom.RoomStat.Upkeep.GetResorce(re).ToString();
            }
            if (SelectedRoom.RoomStat.OutPut.GetResorce(re) != 0)
            {
                if (outPutFound == true) { Debug.LogWarning("Room " + SelectedRoom.gameObject.name + " has two diffrent Resources used for OutPut"); }
                outPutFound = true;
                OutPutImge.sprite = Icons.GetIcon(re);
                Upkeep.text = SelectedRoom.RoomStat.OutPut.GetResorce(re).ToString();
            }
        }
    }      
    
    private void UpdateCamera()
    {
        SelectedRoom.SetCamera(CameraRenderTexture);
    }
}
