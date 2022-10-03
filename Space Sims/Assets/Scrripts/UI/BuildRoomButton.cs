using UnityEngine;

public class BuildRoomButton : MonoBehaviour
{

    public Vector3Int CellPos { get; set; }
    public RoomGridManager roomManager;

    public void OnClick()
    {
        UIManager.Instance.OpenBuildRoomMenu(CellPos);
    }

}
