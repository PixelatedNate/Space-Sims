using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/Encounters", order = 1)]
public class QuestEncounter : ScriptableObject
{
    [SerializeField]
    public string Name;
    [TextArea(5, 10), SerializeField]
    public string Discription;

    [SerializeField]
    public int frequancy;

    [SerializeField]
    PersonInfo.Skills PeopleRequiments;
    [SerializeField]
    GameResources gameResources;

}
