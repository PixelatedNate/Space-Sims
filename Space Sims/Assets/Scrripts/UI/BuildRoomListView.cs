using UnityEngine;

public class BuildRoomListView : MonoBehaviour
{

    private Vector3Int RoomPos { get; set; }
    [SerializeField]
    private BuildRoomListViewItem[] _buildRoomListItems;

    #region publicMethods

    public void EnableView(Vector3Int newRoomPos)
    {
        RoomPos = newRoomPos;
        foreach (var item in _buildRoomListItems)
        {
            item.RoomPosition = RoomPos;
            item.UpdateItem();
        }
    }

    #endregion

}
