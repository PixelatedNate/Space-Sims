using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanal : MonoBehaviour
{
    bool open;

    [SerializeField]
    PersonSelectUIView personView;
    [SerializeField]
    BuildRoomListView buildRoomView;
    [SerializeField]
    UIButton uiButton;

    GameObject ActiveView;

    


    public void OpenBuildRoomView(Vector3Int roomCellPos)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        if (!uiButton.leftSliding)
        {
            uiButton.LeftTabSlideOut();
        }
        //ClearAllView();
        ActiveView = buildRoomView.gameObject;
        ActiveView.SetActive(true);
        buildRoomView.EnableView(roomCellPos);
    }

    public void SelectPerson(PersonInfo personInfo)
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
        }
        uiButton.LeftTabSlideOut();
        //ClearAllView();
        ActiveView = personView.gameObject;
        ActiveView.SetActive(true);
        personView.SetPerson(personInfo);
    }


    public void ClearAllView()
    {
        if (ActiveView != null)
        {
            ActiveView.SetActive(false);
            uiButton.LeftTabSlideIn();
        }
    }

  
}
