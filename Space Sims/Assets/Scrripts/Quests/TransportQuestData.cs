using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/TransportQuest", order = 1)]
public class TransportQuestData : QuestData
{
    [SerializeField]
    public PersonTemplate[] TransaportPeople = new PersonTemplate[0];
    public readonly QuestEncounter[] PossibleEncounters;
    public PlanetData TargetPlanetData;

    public override AbstractQuest CreateQuest(QuestLine questline = null)
    {
        var quest = new TransportQuest(this, questline);
        return quest;
    }
}
