using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestData : ScriptableObject
{
    public class Reward
    {
        [SerializeField]
        GameResources _gameResourcesReward;
        public GameResources GameResourcesReward { get { return _gameResourcesReward; } }
        [SerializeField]
        public int _numberofpeopleReward;
        public int NumberOfPeopleReward { get { return _numberofpeopleReward; } }

        public RoomType roomBlueprint;

    }

    public string Title;
    
    public string Description;

    public Reward reward = new Reward();

}

