using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/QuestLines", order = 3)]
public class QuestLineData : ScriptableObject
{
    [SerializeField]
    public QuestData[] quests;

}
