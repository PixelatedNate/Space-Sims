using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    [SerializeField]
    LeftPanal leftPanal;
    [SerializeField]
    TopBar topBar;



    public void UpdateTopBar(GameResources currentResources, GameResources deltaResources,int numberofPoeple ,int maxPeople)
    {
        topBar.SetValues(currentResources, deltaResources, numberofPoeple ,maxPeople);
    }

    public void DisplayPerson(PersonInfo personInfo)
    {
            leftPanal.SelectPerson(personInfo);
    }

    public void DisplayRoomView(AbstractRoom room)
    {
        leftPanal.SelectRoom(room);
    }

    public void OpenBuildRoomMenu(Vector3Int roomCellPos)
    {
        leftPanal.OpenBuildRoomView(roomCellPos);
    }

    public void DeselectAll()
    {
        leftPanal.ClearAllView();
    }

}
