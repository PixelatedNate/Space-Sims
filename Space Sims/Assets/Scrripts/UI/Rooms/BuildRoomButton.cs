using UnityEngine;
using UnityEngine.UI;

public class BuildRoomButton : MonoBehaviour
{

    public Vector3Int CellPos { get; set; }
    public RoomGridManager roomManager;

    [SerializeField]
    public GameObject outline;

    public void OnClick()
    {
        UIManager.Instance.OpenBuildRoomMenu(CellPos, this);
        outline.SetActive(true);
    }

}
