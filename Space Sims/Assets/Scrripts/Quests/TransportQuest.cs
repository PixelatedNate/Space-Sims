using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/TransportQuest", order = 2)]
public class TransportQuest : AbstractQuest
{

    [SerializeField]
    public PersonInfo[] TransaportPeople;

    [SerializeField]
    public string TargetPlanetName;

    public override void CompleatQuest()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.QuestCompleted);
        //add stuff like reweards for quest compleation
        GlobalStats.Instance.PlayerResources += reward.GameResourcesReward;

        questStaus = QuestStatus.Completed;
        foreach (PersonInfo p in TransaportPeople)
        {
          //  p.PersonMonoBehaviour.LeaveShipForGood();
        }
        for (int i = 0; i < reward.NumberOfPeopleReward; i++)
        {
            PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom);
        }

        AlertManager.Instance.SendAlert(new Alert("Quest Complet", Title, OpenAlertQuest, Alert.AlertPrority.low,Icons.GetMiscUIIcon(UIIcons.QuestComplete)));

    }

    public override bool StartQuest()
    {
        questStaus = QuestStatus.InProgress;
        foreach (PersonInfo p in TransaportPeople)
        {
            PrefabSpawner.Instance.SpawnPerson(GlobalStats.Instance.QuestRoom);
        }
        return true;
    }

    public override void ResetQuest()
    {
        questStaus = QuestStatus.Available;
    }
}
