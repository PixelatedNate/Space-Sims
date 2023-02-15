using System.Collections.Generic;

public class TransportQuest : AbstractQuest
{
    private List<Person> peopleOnShip = new List<Person>();
    public override QuestData QuestData => transportQuestData;
    public TransportQuestData transportQuestData { get; }

    public TransportQuest(TransportQuestData questdata, QuestLine questLine = null)
    {
        this.transportQuestData = questdata;
        this.questLine = questLine;
    }

    public override void CompleatQuest()
    {
        base.CompleatQuest();

        SoundManager.Instance.PlaySound(SoundManager.Sound.QuestCompleted);
        //add stuff like reweards for quest compleation
        GlobalStats.Instance.PlayerResources += QuestData.reward.GameResourcesReward;

        questStaus = QuestStatus.Completed;
        foreach (Person p in peopleOnShip)
        {
            p.LeaveShipForGood();
        }
        for (int i = 0; i < QuestData.reward.people.Length; i++)
        {
            PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom, QuestData.reward.people[i]);
        }

        AlertManager.Instance.SendAlert(new Alert("Quest Complet", QuestData.Title, OpenAlertQuest, Alert.AlertPrority.low, Icons.GetMiscUIIcon(UIIcons.QuestComplete)));
    }

    public override bool StartQuest()
    {
        questStaus = QuestStatus.InProgress;
        foreach (PersonTemplate p in transportQuestData.TransaportPeople)
        {
            var person = PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom, p).GetComponent<Person>();
            peopleOnShip.Add(person);
            person.PersonInfo.SetAsCargoForTransprotQuest(this);
        }

        return true;
    }

}
