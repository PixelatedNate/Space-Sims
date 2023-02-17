using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/BuildRoom", order = 2)]
public class BuildRoomData : QuestData
{
    [SerializeField]
    public RoomType roomToBuild;

    public override AbstractQuest CreateQuest(QuestLine questline = null)
    {
        return new BuildRoomQuest(this, questline);
    }
}
