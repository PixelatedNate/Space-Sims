using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class Quest : ScriptableObject
{
    [Serializable]
    class Requiments
    {
        [SerializeField]
        PersonInfo[] PeopleRequiments;

        public bool Ismet()
        {
            return true;
        }

    }
    class Reward
    {
        [SerializeField]
        GameResources gameResourcesReward;
        [SerializeField]
        PersonInfo[] peopleReward;
    }

    [SerializeField]
    string Name;
    [TextArea(15, 20),SerializeField]
    string Description;

    [SerializeField]
    Requiments requiments;
    [SerializeField]
    int Duration;
    [SerializeField]
    Reward reward;

    [Serializable]

}
