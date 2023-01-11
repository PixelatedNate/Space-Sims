using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractQuest : ScriptableObject
{
    [Serializable]
    public class Reward
    {
        [SerializeField]
        GameResources _gameResourcesReward;
        public GameResources GameResourcesReward { get { return _gameResourcesReward; } }
        [SerializeField]
        int _numberofpeopleReward;
        public int NumberOfPeopleReward { get { return _numberofpeopleReward; } }
    }



    [SerializeField]
    public string Title;
    
    [TextArea(15, 20), SerializeField]
    public string Description;

    [SerializeField]
    Reward _reward;
    public Reward reward { get { return _reward; } }
    public QuestStatus questStaus { get; set; } = QuestStatus.Available;

    public abstract bool StartQuest();
    public abstract void CompleatQuest();

}
