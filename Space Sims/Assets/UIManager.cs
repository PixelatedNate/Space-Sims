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
    

    public void DisplaySelected<T>(T obj)
    {
        if(obj.GetType() == typeof(PersonInfo))
        {
            leftPanal.SelectPerson((PersonInfo)(object)obj);
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
