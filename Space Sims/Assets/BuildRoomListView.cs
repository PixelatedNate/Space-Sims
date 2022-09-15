using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomListView : MonoBehaviour
{

    public Vector3Int roomPos { get; set; }
    [SerializeField]
    BuildRoomListViewItem[] buildRoomListItems;


    // Start is called before the first frame update
    void Start()
    {
    }


    public void EnableView(Vector3Int newRoomPos)
    {
        roomPos = newRoomPos;
        foreach(var item in buildRoomListItems)
        {
            item.roomPos = roomPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
