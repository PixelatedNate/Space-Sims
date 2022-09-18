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



    public void UpdateTopBar(GameResources currentResources, GameResources deltaResources, int people)
    {
        topBar.SetValues(currentResources, deltaResources, people);
    }

    public void DisplaySelected<T>(T obj)
    {
        if(obj.GetType() == typeof(PersonInfo))
        {
            leftPanal.SelectPerson((PersonInfo)(object)obj);
        }
        if(obj.GetType() == typeof(Room))
        {
            leftPanal.SelectRoom((Room)(object)obj);
        }
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
