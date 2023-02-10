using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestData : ScriptableObject
{
    [Serializable]
    public class Reward
    {
        public Reward()
        {
            GameResourcesReward = new GameResources();
        }

        public GameResources GameResourcesReward { get; set; }

        [SerializeField]
        public PersonTemplate[] people;

        [SerializeField]
        public bool roomBlueprintUnlock;

        [SerializeField]
        public RoomType roomBlueprint;

    }

    public string Title;
    
    public string Description;

    [SerializeField]
    public Reward reward = new Reward();

    public abstract AbstractQuest CreateQuest(QuestLine questline = null);    

}

